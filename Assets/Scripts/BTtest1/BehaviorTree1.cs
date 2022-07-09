

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReturnState
{
    //���� ǥ�⸦ ���� enum
    SUCCESS,
    FAILURE,
    RUNNING
}

public abstract class Action : MonoBehaviour
{
    public Collider2D[] TargetPlayer;
    public int TargetNum = 0;

    public EnemyZombieAnimation EnemyZombie;

    public bool AnimaionOnOff = true;
    public float AnimationTime = 0;
    public float LeftRight = 0;

    public bool playerAttack = false;
    public bool spawned = true;


    // ������ �Ǵ� �׼�
    public static Action Create<T>() where T : Action, new() { return new T(); }

    public static Action Failure() { return new ForceFailure(); }
    public static Action Success() { return new ForceSuccess(); }

    public abstract ReturnState Update(GameObject obj, float dt);
}


public class BehaviorTree1 : Action
{
    //�⺻���� Ʋ ����
    private Sequence sequence;

    public static BehaviorTree1 Create(Sequence seq)
    {
        return new BehaviorTree1() { sequence = seq };
    }

    public override ReturnState Update(GameObject obj, float dt)
    {
        return sequence.Update(obj, dt);
    }
}


public class Selector : Action
{
    //���� �ؾ� ����Ǵ�?
    //�ڽĵ� �߿� �ϳ��� ������ ���Ѿ� ���� �� �ִ� ����

    private List<Action> children = new List<Action>();
    private int latest = 0;

    public static Selector Create(params Action[] nodes)
    {
        Selector s = new Selector();
        s.children.AddRange(nodes);
        return s;
    }

    public override ReturnState Update(GameObject obj, float dt)
    {
        while (latest < children.Count)
        {
            var ret = children[latest].Update(obj, dt);

            if (ret == ReturnState.FAILURE)
            {
                latest++;
                continue;
            }

            if (ret == ReturnState.SUCCESS) latest = 0;
            return ret;
        }

        latest = 0;
        return ReturnState.FAILURE;
    }
}

public class SelectorRandom : Action
{
    //���� ������ ������
    //���� �ڽĵ� �߿� �ϳ� ���ؼ� �׼ǽ���

    private List<Action> children = new List<Action>();
    private int latest = -1;

    public static SelectorRandom Create(params Action[] nodes)
    {
        SelectorRandom s = new SelectorRandom();
        s.children.AddRange(nodes);
        return s;
    }

    public override ReturnState Update(GameObject obj, float dt)
    {
        if (latest < 0) latest = Random.Range(0, children.Count);

        var ret = children[latest].Update(obj, dt);

        if (ret == ReturnState.SUCCESS)
        {
            latest = -1;
        }

        return ret;
    }


}


public class Sequence : Action
{
    //�ϳ��� �����ϸ� �ٽ� ÷���� �����Ǵ� ������
    //���� �� ��������� ���������� ����ȴ�

    private List<Action> children = new List<Action>();
    private int latest = 0;

    public static Sequence Create(params Action[] nodes)
    {
        Sequence s = new Sequence();
        s.children.AddRange(nodes);
        return s;
    }

    public override ReturnState Update(GameObject obj, float dt)
    {
        while (latest < children.Count)
        {
            var ret = children[latest].Update(obj, dt);

            if (ret == ReturnState.SUCCESS)
            {
                latest++;
                continue;
            }

            if (ret == ReturnState.FAILURE) latest = 0;
            return ret;
        }

        latest = 0;
        return ReturnState.SUCCESS;
    }
}

public class Parallel : Action
{
    // ���ķ� ����ȴ�
    // ���߿� ���� ���� �ʴ� �� �Ѳ����� ���� ����ȴ�

    private List<Action> children = new List<Action>();
    private int[] indices;
    private int successCount = 0;

    public static Parallel Create(params Action[] nodes)
    {
        Parallel s = new Parallel();
        s.children.AddRange(nodes);
        s.indices = new int[nodes.Length];
        for (int i = 0; i < s.indices.Length; ++i) s.indices[i] = i;
        return s;
    }

    public override ReturnState Update(GameObject obj, float dt)
    {
        for (int i = 0; i < indices.Length - successCount; ++i)
        {
            var ret = children[indices[i]].Update(obj, dt);
            if (ret == ReturnState.FAILURE) return ReturnState.FAILURE;

            if (ret == ReturnState.SUCCESS)
            {
                int temp = indices[i];
                indices[i] = indices[indices.Length - successCount - 1];
                indices[indices.Length - successCount - 1] = temp;
                successCount++;
            }
        }

        if (successCount == indices.Length)
        {
            successCount = 0;
            return ReturnState.SUCCESS;
        }
        return ReturnState.RUNNING;
    }
}

public class Inverter : Action
{
    //�ڽ��� �����ϸ� �θ� ����, �Ǵ� �� ����
    //���ݴ��� ����� �ҷ�����Ų��

    private Action child;

    public static Inverter Create(Action child)
    {
        Inverter i = new Inverter();
        i.child = child;
        return i;
    }

    public override ReturnState Update(GameObject obj, float dt)
    {
        var ret = child.Update(obj, dt);
        if (ret == ReturnState.SUCCESS) return ReturnState.FAILURE;
        if (ret == ReturnState.FAILURE) return ReturnState.SUCCESS;
        return ReturnState.RUNNING;
    }
}

public class If : Action
{
    //Ư�� ������ �˻��Ͽ� �װ��� ���� ��� �����Ѵ�

    private Action child;
    private Action condition;

    public static If Create(Action condition, Action child)
    {
        If i = new If();
        i.child = child;
        i.condition = condition;
        return i;
    }

    public override ReturnState Update(GameObject obj, float dt)
    {
        bool ret = condition.Update(obj, dt) != ReturnState.FAILURE;
        if (ret) return child.Update(obj, dt);
        return ReturnState.FAILURE;
    }
}

//���� �������� �����ϱ� ���� Ŀ�ǵ��̴�
public class ForceFailure : Action
{
    public override ReturnState Update(GameObject obj, float dt)
    {
        return ReturnState.FAILURE;
    }
}
public class ForceSuccess : Action
{
    public override ReturnState Update(GameObject obj, float dt)
    {
        return ReturnState.SUCCESS;
    }
}


//���⼭���� �����ϴ� �׼ǵ��̴�
public class Idle : Action
{

    public override ReturnState Update(GameObject obj, float dt)
    {

        if (AnimaionOnOff)
        {
            //AnimeIdle();
            AnimaionOnOff = false;
        }

        //obj.transform.position += Vector3.forward * dt;
        //if (obj.transform.position.z < 2F)
        //{
        //    return ReturnState.RUNNING;
        //}

        AnimationTime += dt;
        //Debug.Log(AnimationTime);

        if (AnimationTime < 1.5f)
        {
            return ReturnState.RUNNING;
        }

        AnimationTime = 0;
        AnimaionOnOff = true;
        return ReturnState.SUCCESS;
    }
}

public class EnemyMove : Action
{

    public override ReturnState Update(GameObject obj, float dt)
    {
        Debug.Log("������ ����");

        if (AnimaionOnOff)
        {
            //EnemyZombie = GetComponent<EnemyZombieAnimation>();
            //EnemyZombie.AnimeMove();
            AnimaionOnOff = false;
        }

        TargetPlayer = Physics2D.OverlapCircleAll(obj.transform.position, 1000f);


        if (TargetPlayer.Length > 0)
        {

            for (int i = 0; i < TargetPlayer.Length; i++)
            {
                if (TargetPlayer[i].tag == "Player")
                {
                    TargetNum = i;
                    Debug.Log("�÷��̾� �ٽ� ã�Ҵ�");
                }
            }


        }
        else
        {

        }


        if (obj.transform.position.x - TargetPlayer[TargetNum].transform.position.x > 10
           || obj.transform.position.x - TargetPlayer[TargetNum].transform.position.x < 10)
        {
            Vector3 dir = new Vector2(TargetPlayer[TargetNum].transform.position.x - obj.transform.position.x, 0);
            obj.transform.position += dir * dt;
            return ReturnState.RUNNING;
        }

        //AnimationTime += dt;
        //Debug.Log(AnimationTime);

        Debug.Log(TargetPlayer[TargetNum]);



        AnimationTime = 0;
        AnimaionOnOff = true;
        return ReturnState.SUCCESS;
    }
}

public class Tracking : Action
{
    public override ReturnState Update(GameObject obj, float dt)
    {

        //  AnimeMove();
        return ReturnState.SUCCESS;
    }
}

public class EnemyPlayerSearch : Action
{
    //����
    public override ReturnState Update(GameObject obj, float dt)
    {
        //Collider[] cols = Physics.OverlapSphere(transform.position, 10f, 1 << 8);
        Collider[] cols = Physics.OverlapSphere(obj.transform.position, 100f);


        if (cols.Length > 0)
        {

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Player")
                {
                    Debug.Log("�÷��̾� ã�Ҵ�");
                    return ReturnState.SUCCESS;
                }
            }


        }
        else
        {
            return ReturnState.FAILURE;
        }


        return ReturnState.FAILURE;
    }
}


public class DidPlayerAttacked : Action
{
    //����
    public override ReturnState Update(GameObject obj, float dt)
    {

        if (playerAttack)
        {
            return ReturnState.SUCCESS;


        }
        else
        {
            return ReturnState.FAILURE;
        }


        return ReturnState.FAILURE;
    }
}

public class EnemyAttack : Action
{


    public override ReturnState Update(GameObject obj, float dt)
    {
        if (AnimaionOnOff)
        {
            //AnimeMove();
            AnimaionOnOff = false;
        }


        AnimationTime += dt;

        if (AnimationTime < 0.7f)
        {
            obj.transform.position += Vector3.right * dt;
            return ReturnState.RUNNING;
        }

        AnimationTime = 0;
        AnimaionOnOff = true;
        return ReturnState.SUCCESS;
    }
}

public class Spawn : Action
{
    //����
    public override ReturnState Update(GameObject obj, float dt)
    {
        if (spawned)
        {
            //Collider[] cols2 = Physics.OverlapSphere(transform.position, 10f, 1 << 8);

            TargetPlayer = Physics2D.OverlapCircleAll(obj.transform.position, 1000f);


            if (TargetPlayer.Length > 0)
            {

                for (int i = 0; i < TargetPlayer.Length; i++)
                {
                    if (TargetPlayer[i].tag == "Player")
                    {
                        TargetNum = i;
                        Debug.Log("���� �� �÷��̾� ã�Ҵ�");
                        spawned = false;
                        return ReturnState.SUCCESS;
                    }
                }


            }
        }




        return ReturnState.FAILURE;
    }
}

public class GroundSpawn : Action
{
    //����
    public override ReturnState Update(GameObject obj, float dt)
    {
        Debug.Log("������");


        return ReturnState.FAILURE;
    }
}

public class UnderGroundSpawn : Action
{
    //����
    public override ReturnState Update(GameObject obj, float dt)
    {

        Debug.Log("���Ͻ���");

        return ReturnState.FAILURE;
    }
}

public class SkySpawn : Action
{
    //����
    public override ReturnState Update(GameObject obj, float dt)
    {
        Debug.Log("�ϴý���");


        return ReturnState.FAILURE;
    }
}

public class PlayerDistanceDifference : Action
{
    //����
    public override ReturnState Update(GameObject obj, float dt)
    {



        return ReturnState.FAILURE;
    }
}