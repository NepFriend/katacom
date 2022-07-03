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
         스폰3개중 랜덤
        플레이여 양 옆 시야 밖에서 생성
        플레이어 시야 밖 위에서 생성 후 낙하(플레이어 근처면 공격 있을 수 있다)
        땅에서 솟아나며 생성(플레이어 근처면 공격 있을 수 있다)
         
        플레이어 추적 후 범위 안에 들어갔을 경우 공격

         일정 타수 이상 맞으면 행동 강화
         
         */


        bt1 = BehaviorTree1.Create( // 전체 기틀인 비헤이비어 
           Sequence.Create( // 시퀀스
              
                  // Action.Create<EnemyMove>(),

                 Selector.Create(
                        If.Create( // 조건부 랜덤실행
                         Action.Create<Spawn>(), // 여기서 조건이 맞나 안맞나를 물어본다 
                         SelectorRandom.Create( // 이 시퀀스 랜덤은 자식 갯수에 따라 그중 랜덤 하나를 선출해준다
                           Action.Create<GroundSpawn>(),
                           Action.Create<SkySpawn>(),
                           Action.Create<UnderGroundSpawn>()
                         )
               ),
                          If.Create( // 조건부 랜덤실행
                             Action.Create<PlayerDistanceDifference>(), // 여기서 조건이 맞나 안맞나를 물어본다 
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
