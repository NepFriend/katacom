using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    //int ani = 2;

    public PlayerInput PlInput;

    public SkeletonAnimation sk;
    // Skeleton skeleton;

    public PlayerAttack playerAttack;

    public PlayerMove playerMove;

    //움직였을경우 꺼지는 트리거
    public bool Move;

    //점프상태일경우
    public bool Jump;

    //어택 상태일경우
    public bool Attack;
    public int AttackCount;

    //쏘는 도중의 시간 
    public float AttackTime;

    //사격 방향
    // 0 기본사격, 1 대각선, 2 위, 3 가로조준 


    //대쉬 상태일경우
    public bool Dash;


    //움직임에 레벨 부여
    /*
     0 대기자세
     1 저속이동
     2 이동
     3 달리기
     4 구르기
     5 점프
         */
    public int moveLevel;

    //다목적 멈추기
    public bool stop;

    //방향전환시에 나오는 턴 애니메이션을 위한
    public bool turn;

    //쌍권총을 위한 슛 확인 불값
    public bool shoot;

    //점프폴은 이미지의 Y축을 -2 해야 적절하다

    // Start is called before the first frame update
    //public void Start()
    //{

    //    PlInput = transform.GetComponent<PlayerInput>();
    //    playerMove = GetComponent<PlayerMove>();
    //    playerAttack = GetComponent<PlayerAttack>();
    //    sk = GetComponent<SkeletonAnimation>();

    //    //sk.AnimationName = "Idle";
    //    //sk.Start();

    //    Move = true;
    //    Jump = true;
    //    Attack = true;
    //    Dash = true;

    //    //자세 초기화
    //   // sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);

    //   // sk.AnimationState.SetAnimation(1, "Main_Weapon/idle_M", true);


    //    stop = false;

    //    AttackCount = 0;


    //}


    // Update is called once per frame
    //public void Update()
    //{

    //    if (!stop)
    //    {

    //        //플레이어 방향 설정
    //        if (playerAttack.playerAttackDir == 0 && !playerMove.sit)
    //        {//정자세일때

    //            if (PlInput.moveDir && playerMove.moveLevel != 4 && AttackCount == 0)
    //            {
    //                sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
    //            }
    //            else if (!PlInput.moveDir && playerMove.moveLevel != 4 && AttackCount == 0)
    //            {
    //                sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
    //            }

    //        }






    //        //플레이어 이동
    //        if (PlInput.move != 0f && playerMove.moveLevel < 4 && Move)
    //        { //이동 중



    //            if (moveLevel >= 2)
    //            {

    //                //기본 걷기 이동
    //                if (!playerMove.sit)
    //                {
    //                    if (playerAttack.playerAttackDir == 0)
    //                    {//없음
    //                        sk.AnimationState.SetAnimation(1, "move_f", true);
    //                    }
    //                    else if (playerAttack.playerAttackDir == 1)
    //                    {//대각선

    //                        if (PlInput.move* sk.skeleton.ScaleX > 0)
    //                        { // 보는 방향과 이동방향이 같을경우 전진
    //                            sk.AnimationState.SetAnimation(2, "move_f_diagonal", true);
    //                        }
    //                        else if (PlInput.move * sk.skeleton.ScaleX < 0)
    //                        { // 보는 방향과 이동방향이 다를경우 후진
    //                            sk.AnimationState.SetAnimation(2, "move_b_diagonal", true);
    //                        }
    //                    }
    //                    else if (playerAttack.playerAttackDir == 2)
    //                    {//위

    //                        if (PlInput.move * sk.skeleton.ScaleX > 0)
    //                        { // 보는 방향과 이동방향이 같을경우 전진
    //                            sk.AnimationState.SetAnimation(2, "move_f_top", true);
    //                        }
    //                        else if (PlInput.move * sk.skeleton.ScaleX < 0)
    //                        { // 보는 방향과 이동방향이 다를경우 후진
    //                            sk.AnimationState.SetAnimation(2, "move_b_top", true);
    //                        }
    //                    }
    //                    else if (playerAttack.playerAttackDir == 3)
    //                    {//가로조준

    //                        if (PlInput.move * sk.skeleton.ScaleX > 0)
    //                        { // 보는 방향과 이동방향이 같을경우 전진
    //                            sk.AnimationState.SetAnimation(1, "move_f_aiming", true);
    //                        }
    //                        else if (PlInput.move * sk.skeleton.ScaleX < 0)
    //                        { // 보는 방향과 이동방향이 다를경우 후진
    //                            sk.AnimationState.SetAnimation(1, "move_b_aiming", true);
    //                        }
    //                    }



    //                }
    //                else if (playerMove.sit)
    //                {
    //                      if (PlInput.move * sk.skeleton.ScaleX > 0)
    //                        { // 보는 방향과 이동방향이 같을경우 전진
    //                            sk.AnimationState.SetAnimation(1, "move_f_sit", true);
    //                        }
    //                        else if (PlInput.move * sk.skeleton.ScaleX < 0)
    //                        { // 보는 방향과 이동방향이 다를경우 후진
    //                            sk.AnimationState.SetAnimation(1, "move_b_sit", true);
    //                        }
    //                }

    //            }




    //            Move = false;

    //        }
    //        else if (PlInput.move == 0f && playerMove.moveLevel < 4 && !Move)
    //        {
    //            sk.AnimationState.AddEmptyAnimation(2, 0, 0.3f);
    //            if (!playerMove.sit )
    //            {
    //                AttackCount = playerAttack.playerAttackDir;

    //                if (AttackCount == 1)
    //                {
    //                    sk.AnimationState.SetAnimation(2, "idle_diagonal", true);
    //                }
    //                else if (AttackCount == 2)
    //                {
    //                    sk.AnimationState.SetAnimation(2, "idle_top", true);
    //                }
    //                else if (AttackCount == 3)
    //                {
    //                    sk.AnimationState.SetAnimation(2, "idle_aiming", true);
    //                }
    //                else
    //                {
    //                    sk.AnimationState.AddEmptyAnimation(2, 0, 0);
    //                    sk.AnimationState.SetAnimation(0, "idle", true);
    //                }

    //            }
    //            else if (playerMove.sit )
    //            {
    //                sk.AnimationState.SetAnimation(1, "idle_sit", true);
    //            }



    //            Move = true;
    //        }
    //    }



    //}

    public virtual void moveMotion()
    { 
    }

    public virtual void sitChangeMotion()
    {
       
    }

    public virtual void runChangeMotion(int moveLevel)
    {
     
    }

    public virtual void jumpPl()
    {
     
    }

    public virtual void Landing()
    {
       
      

    }

    public virtual void DamageAfterCare()
    {
      
    }


    public virtual void MovingAttack(int AttackNum)
    {

     

    }

    public virtual void MovingAttackStop()
    {
       
    }

    public virtual void DashAnime()
    {
     
    }

    public virtual void DashAnimeDone()
    {

    }

    




    public void StandCombo(bool dis)
    {
        // mousePos = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        //이동사격 전투 중 마우스가 플레이어 X축을 뚫고 반대로 이동했을 경우
        //if (Input.mousePosition.x > transform.position.x)
        //{
        //    Debug.Log("dsfsdfdsfsfdsfs");
        //}

        if (dis)
        {
            sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
        }
        else
        {
            sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
        }


        sk.AnimationState.SetAnimation(1, "Idle_Shoot", true);
    }

    public void StandComboEnd(float speed)
    {
        if (speed < 0.1f)
        {
            sk.AnimationState.SetAnimation(1, "Idle2", true);
        }
        else
        {
            // sk.AnimationState.SetAnimation(0, "run", true);
        }


    }

    public void DamagedAnime()
    {
        sk.timeScale = 1;
        stop = true;
        GetComponent<MeshRenderer>().enabled = true;
        sk.AnimationState.SetAnimation(1, "DamageForward", false);
    }

    public void JumpDamagedAnime()
    {
        sk.timeScale = 1;
        GetComponent<MeshRenderer>().enabled = true;
        sk.AnimationState.SetAnimation(1, "DamageJump", false);
    }

    public void RunAnimationClear()
    {
        //switch (AttackCount)
        //{ //이동중 사격자세를 용이하게 바꾸기 위한 코드

        //    case 0:
        //        sk.AnimationState.SetAnimation(1, "Run_Shoot", true);
        //        break;
        //    case 1:
        //        sk.AnimationState.SetAnimation(1, "Run", true);
        //        break;
        //    case 2:
        //        sk.AnimationState.SetAnimation(1, "Run_BackShoot", true);
        //        break;


        //    default:
        //        break;
        //}

    }

    public virtual void Shoot(int dir)
    {// 0 기본사격, 1 대각선, 2 위, 3 조준
      

    }

    public virtual void ShootReady(int dir)
    {// 0 기본사격, 1 대각선, 2 위, 3 조준
     
    }

    public virtual void ShootDone()
    {//공격 끝났을 경우
        Attack = false;
    }


    public virtual void StopAction(int kind)
    {
        // 


    }
    public virtual void MoveAction(int kind)
    {
        // 


    }

    public virtual void Reload(float time)
    {

    }

    public virtual void StopActionDone()
    {
    }
    public virtual void granadeStart()
    {
      
    }

    public virtual void granadeEnd()
    {
      

    }


    public virtual void getDamage(int type, bool leftRight)
    {//데미지 타입과 좌우구별 (좌가 0)
        /*
         소반응
         극소피격
         소피격
         중피격
         대피격
         공중피격
         쇼크
         스턴
         */


    }

    //여기서부터는 클래스로 더 나은 방법 찾기 전 임시로 다는 애니메이션 모음
    //각 퍼크마다 들어가는 애니메이션과 아닌것이 있어 구분짓고, 교체가 용이하게 되기 위함

}

