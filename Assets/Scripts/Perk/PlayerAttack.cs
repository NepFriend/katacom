using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInput playerInput;

    public PlayerAnimation PlInputAnimation;

    public PlayerMove playerMove;

    //��� ����
    // 0 �⺻���, 1 �밢��, 2 ��, 3 ��������, 4 �Ʒ� ����
    public int playerAttackDir;

    public bool shoot;

    public bool stopActionOnOff;

    public bool reloading;

    public bool granadeOn;

    //1����, 2�麴��, 3�Ӽ�����
    public int stopActionKind;

    public float stopActionTimeLimit;

    public float shootDelay;

    public float shootDirTime;

    //// Start is called before the first frame update
    //void Start()
    //{
     
    //}

    //// Update is called once per frame
    //void Update()
    //{
      


    //}

    public virtual void AttackDir()
    {
      

    }

    public virtual void attack()
    {
      
    }

    public virtual void stopAction()
    {
     
      
    }

    public virtual void moveAction()
    {


    }

    public virtual void granade()
    {
      
    }

}
