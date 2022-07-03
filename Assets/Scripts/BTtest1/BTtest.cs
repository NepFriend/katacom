using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTtest : MonoBehaviour
{
    BehaviorTree1 bt1;

    // Start is called before the first frame update
    void Start()
    {
        BehaviorTreePlay();
    }

  

    void BehaviorTreePlay()
    {
        /*
         ����3���� ����
        �÷��̿� �� �� �þ� �ۿ��� ����
        �÷��̾� �þ� �� ������ ���� �� ����(�÷��̾� ��ó�� ���� ���� �� �ִ�)
        ������ �ھƳ��� ����(�÷��̾� ��ó�� ���� ���� �� �ִ�)
         
        �÷��̾� ���� �� ���� �ȿ� ���� ��� ����

         ���� Ÿ�� �̻� ������ �ൿ ��ȭ
         
         */


        bt1 = BehaviorTree1.Create( // ��ü ��Ʋ�� �����̺�� 
           Sequence.Create( // ������
              
                  // Action.Create<EnemyMove>(),

                 Selector.Create(
                        If.Create( // ���Ǻ� ��������
                         Action.Create<Spawn>(), // ���⼭ ������ �³� �ȸ³��� ����� 
                         SelectorRandom.Create( // �� ������ ������ �ڽ� ������ ���� ���� ���� �ϳ��� �������ش�
                           Action.Create<GroundSpawn>(),
                           Action.Create<SkySpawn>(),
                           Action.Create<UnderGroundSpawn>()
                         )
               ),
                          If.Create( // ���Ǻ� ��������
                             Action.Create<PlayerDistanceDifference>(), // ���⼭ ������ �³� �ȸ³��� ����� 
                             Action.Create<EnemyAttack>()
                            ),
                          Action.Create<EnemyMove>()


               )

           )
       );
    }

    // Update is called once per frame
    void Update()
    {
        bt1.Update(transform.gameObject, Time.deltaTime);   

        // Debug.Log(bt1.Update(transform.gameObject, Time.deltaTime));
    }
}
