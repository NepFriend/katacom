using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class PlayerAnimation : MonoBehaviour
//{

//    int ani = 2;

//    public PlayerInput PlInput;

//    public SkeletonAnimation sk;
//    // Skeleton skeleton;

//    PlayerAttack playerAttack;

//    PlayerMove playerMove;

//    //움직였을경우 꺼지는 트리거
//    public bool Move;

//    //점프상태일경우
//    public bool Jump;

//    //어택 상태일경우
//    public bool Attack;
//    int AttackCount;
//    //사격 방향
//    // 0 기본사격, 1 대각선, 2 위, 3 가로조준 


//    //대쉬 상태일경우
//    bool Dash;

//    //마우스 포지션
//    Vector3 mousePos;
//    //그걸 위한 카메라
//    Camera cam;

//    //움직임에 레벨 부여
//    /*
//     0 대기자세
//     1 저속이동
//     2 이동
//     3 달리기
//     4 구르기
//     5 점프
//         */
//    public int moveLevel;

//    //다목적 멈추기
//    public bool stop;


//    //점프폴은 이미지의 Y축을 -2 해야 적절하다

//    // Start is called before the first frame update
//    public void Start()
//    {

//        PlInput = transform.GetComponent<PlayerInput>();
//        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
//        playerMove = GetComponent<PlayerMove>();
//        playerAttack = GetComponent<PlayerAttack>();
//        sk = GetComponent<SkeletonAnimation>();

//        //sk.AnimationName = "Idle";
//        //sk.Start();

//        Move = true;
//        Jump = true;
//        Attack = true;
//        Dash = true;

//        //자세 초기화
//       // sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);

//       // sk.AnimationState.SetAnimation(1, "Main_Weapon/idle_M", true);


//        stop = false;

//        AttackCount = 0;


//    }


//    // Update is called once per frame
//    public void Update()
//    {

//        if (!stop)
//        {

//            //플레이어 방향 설정
//            if (playerAttack.playerAttackDir == 0 && !playerMove.sit)
//            {//정자세일때

//                if (PlInput.moveDir && playerMove.moveLevel != 4 && AttackCount == 0)
//                {
//                    sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
//                }
//                else if (!PlInput.moveDir && playerMove.moveLevel != 4 && AttackCount == 0)
//                {
//                    sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
//                }

//            }






//            //플레이어 이동
//            if (PlInput.move != 0f && playerMove.moveLevel < 4 && Move)
//            { //이동 중

                

//                if (moveLevel >= 2)
//                {
                  
//                    //기본 걷기 이동
//                    if (!playerMove.sit)
//                    {
//                        if (playerAttack.playerAttackDir == 0)
//                        {//없음
//                            sk.AnimationState.SetAnimation(1, "move_f", true);
//                        }
//                        else if (playerAttack.playerAttackDir == 1)
//                        {//대각선

//                            if (PlInput.move* sk.skeleton.ScaleX > 0)
//                            { // 보는 방향과 이동방향이 같을경우 전진
//                                sk.AnimationState.SetAnimation(2, "move_f_diagonal", true);
//                            }
//                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
//                            { // 보는 방향과 이동방향이 다를경우 후진
//                                sk.AnimationState.SetAnimation(2, "move_b_diagonal", true);
//                            }
//                        }
//                        else if (playerAttack.playerAttackDir == 2)
//                        {//위

//                            if (PlInput.move * sk.skeleton.ScaleX > 0)
//                            { // 보는 방향과 이동방향이 같을경우 전진
//                                sk.AnimationState.SetAnimation(2, "move_f_top", true);
//                            }
//                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
//                            { // 보는 방향과 이동방향이 다를경우 후진
//                                sk.AnimationState.SetAnimation(2, "move_b_top", true);
//                            }
//                        }
//                        else if (playerAttack.playerAttackDir == 3)
//                        {//가로조준

//                            if (PlInput.move * sk.skeleton.ScaleX > 0)
//                            { // 보는 방향과 이동방향이 같을경우 전진
//                                sk.AnimationState.SetAnimation(1, "move_f_aiming", true);
//                            }
//                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
//                            { // 보는 방향과 이동방향이 다를경우 후진
//                                sk.AnimationState.SetAnimation(1, "move_b_aiming", true);
//                            }
//                        }



//                    }
//                    else if (playerMove.sit)
//                    {
//                          if (PlInput.move * sk.skeleton.ScaleX > 0)
//                            { // 보는 방향과 이동방향이 같을경우 전진
//                                sk.AnimationState.SetAnimation(1, "move_f_sit", true);
//                            }
//                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
//                            { // 보는 방향과 이동방향이 다를경우 후진
//                                sk.AnimationState.SetAnimation(1, "move_b_sit", true);
//                            }
//                    }

//                }




//                Move = false;

//            }
//            else if (PlInput.move == 0f && playerMove.moveLevel < 4 && !Move)
//            {
//                sk.AnimationState.AddEmptyAnimation(2, 0, 0.3f);
//                if (!playerMove.sit )
//                {
//                    AttackCount = playerAttack.playerAttackDir;

//                    if (AttackCount == 1)
//                    {
//                        sk.AnimationState.SetAnimation(2, "idle_diagonal", true);
//                    }
//                    else if (AttackCount == 2)
//                    {
//                        sk.AnimationState.SetAnimation(2, "idle_top", true);
//                    }
//                    else if (AttackCount == 3)
//                    {
//                        sk.AnimationState.SetAnimation(2, "idle_aiming", true);
//                    }
//                    else
//                    {
//                        sk.AnimationState.AddEmptyAnimation(2, 0, 0);
//                        sk.AnimationState.SetAnimation(0, "idle", true);
//                    }

//                }
//                else if (playerMove.sit )
//                {
//                    sk.AnimationState.SetAnimation(1, "idle_sit", true);
//                }



//                Move = true;
//            }
//        }



//    }
//    public void sitChangeMotion()
//    {
//        if (PlInput.move == 0f)
//        {
//            if (!playerMove.sit)
//            {
//                sk.AnimationState.SetAnimation(1, "idle", true);
//            }
//            else if (playerMove.sit)
//            {
//                sk.AnimationState.SetAnimation(1, "idle_sit", true);
//            }
//        }
//        else
//        {
//            if (!playerMove.sit)
//            {
//                sk.AnimationState.SetAnimation(1, "idle", true);
//            }
//            else if (playerMove.sit)
//            {
//                sk.AnimationState.SetAnimation(1, "idle_sit", true);
//            }
//        }
//    }

//    public void runChangeMotion(int moveLevel)
//    {
//        if (moveLevel >= 3)
//        {
//            sk.AnimationState.SetAnimation(1, "run", true);
//        }
//        else
//        {
//            if (moveLevel >= 2)
//            {
//                sk.AnimationState.SetAnimation(1, "move_f", true);
//            }
//            else if(moveLevel >= 1)
//            {
//                sk.AnimationState.SetAnimation(1, "move_f_sit", true);
//            }
//        }
//    }

//    public void jumpPl()
//    {
//        Jump = false;
//        sk.AnimationState.SetAnimation(1, "jump", false);
//        sk.AnimationState.AddAnimation(1, "jumpfall", true, 0f);
//    }

//    public void Landing()
//    {
//        Jump = true;

//        //sk.AnimationState.SetAnimation(0, "Land", false);

//        //if (PlInput.move != 0)
//        //{
//        //    sk.AnimationState.AddAnimation(0, "Run", true, 0f);
//        //}
//        //else
//        //{
//        //    sk.AnimationState.AddAnimation(0, "Main_Weapon/idle_M", true, 0f);
//        //}
//        if (PlInput.move == 0f)
//        {
//            Move = false;
//            moveLevel = 0;
//        }
//        else
//        {
//            Move = true;
//            moveLevel = 2;
//        }

//    }

//    public void DamageAfterCare()
//    {
       
//        if (PlInput.move != 0)
//        {
//            sk.AnimationState.AddAnimation(0, "Run", true, 0f);
//        }
//        else
//        {
//            sk.AnimationState.AddAnimation(0, "Main_Weapon/idle_M", true, 0f);
//        }
//        Attack = false;
//        Move = false;
//        stop = false;
//    }


//    public void MovingAttack(int AttackNum)
//    {

//        if (AttackNum != AttackCount && !Attack)
//        {
//            AttackCount = AttackNum;

//            Move = true;
//        }





//    }

//    public void MovingAttackStop()
//    {
//        StopAllCoroutines();

//        AttackCount = 1;
//        if (!Attack)
//        {


//            Move = true;
//        }


//    }

//    public void DashAnime()
//    {
//        //버그 픽스 스페이스 홀드할경우 방향전환 불가능한 문제 대체
//        if (PlInput.moveDir)
//        {
//            sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
//        }
//        else if (!PlInput.moveDir)
//        {
//            sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
//        }

//        sk.AnimationState.SetAnimation(1, "step", false);
//    }

//    public void DashAnimeDone()
//    {


//        if (PlInput.move == 0f)
//        {
//            Move = false;
//            moveLevel = 0;
//        }
//        else
//        {
//            Move = true;
//            moveLevel = 2;
//        }
//    }

//    // 버그픽스를 위한 모습보이기
//    public void AttackDelay()
//    {

//    }

//    public void QComboLastAttack(bool dis)
//    {
//        //sk.timeScale = 1;

//        ////플레이어 방향 설정
//        //if (dis)
//        //{
//        //    sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
//        //}
//        //else
//        //{
//        //    sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
//        //}

//        //sk.AnimationState.SetAnimation(0, "Idle_Shoot", true);

//        GetComponent<MeshRenderer>().enabled = false;
//    }





//    public void StandCombo(bool dis)
//    {
//        // mousePos = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

//        //이동사격 전투 중 마우스가 플레이어 X축을 뚫고 반대로 이동했을 경우
//        //if (Input.mousePosition.x > transform.position.x)
//        //{
//        //    Debug.Log("dsfsdfdsfsfdsfs");
//        //}

//        if (dis)
//        {
//            sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
//        }
//        else
//        {
//            sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
//        }


//        sk.AnimationState.SetAnimation(1, "Idle_Shoot", true);
//    }

//    public void StandComboEnd(float speed)
//    {
//        if (speed < 0.1f)
//        {
//            sk.AnimationState.SetAnimation(1, "Idle2", true);
//        }
//        else
//        {
//            // sk.AnimationState.SetAnimation(0, "run", true);
//        }


//    }

//    public void DamagedAnime()
//    {
//        sk.timeScale = 1;
//        stop = true;
//        GetComponent<MeshRenderer>().enabled = true;
//        sk.AnimationState.SetAnimation(1, "DamageForward", false);
//    }

//    public void JumpDamagedAnime()
//    {
//        sk.timeScale = 1;
//        GetComponent<MeshRenderer>().enabled = true;
//        sk.AnimationState.SetAnimation(1, "DamageJump", false);
//    }

//    public void RunAnimationClear()
//    {
//        //switch (AttackCount)
//        //{ //이동중 사격자세를 용이하게 바꾸기 위한 코드

//        //    case 0:
//        //        sk.AnimationState.SetAnimation(1, "Run_Shoot", true);
//        //        break;
//        //    case 1:
//        //        sk.AnimationState.SetAnimation(1, "Run", true);
//        //        break;
//        //    case 2:
//        //        sk.AnimationState.SetAnimation(1, "Run_BackShoot", true);
//        //        break;


//        //    default:
//        //        break;
//        //}

//    }

//    public void Shoot(int dir)
//    {// 0 기본사격, 1 대각선, 2 위, 3 조준
//        AttackCount = dir;
//        if (dir == 0)
//        {
//            if (!playerMove.sit)
//            {
//                sk.AnimationState.SetAnimation(3, "shoot", false);
//            }
//            else
//            {
//                sk.AnimationState.SetAnimation(3, "shoot_sit", false);
//            }

          
//        }
//        else if (dir == 1)
//        {
//            sk.AnimationState.SetAnimation(3, "up_shoot_diagonal", false);
//        }
//        else if (dir == 2)
//        {
//            sk.AnimationState.SetAnimation(3, "up_shoot_top", false);
//        }
//        else if (dir == 3)
//        {
//            sk.AnimationState.SetAnimation(3, "up_shoot_aiming", false);
//        }

//        sk.AnimationState.AddEmptyAnimation(3,0,0.9f);


//    }

//    public void ShootReady(int dir)
//    {// 0 기본사격, 1 대각선, 2 위, 3 조준
//        AttackCount = dir;
//        if (dir == 0)
//        {
//            sk.AnimationState.AddEmptyAnimation(2, 0, 0);
//            sk.AnimationState.SetAnimation(0, "idle", true);
//        }
//        else if (dir == 1)
//        {
//            sk.AnimationState.SetAnimation(2, "idle_diagonal", true);
//        }
//        else if (dir == 2)
//        {
//            sk.AnimationState.SetAnimation(2, "idle_top", true);
//        }
//        else if (dir == 3)
//        {
//            sk.AnimationState.SetAnimation(2, "idle_aiming", true);
//        }



//    }



//    //여기서부터는 클래스로 더 나은 방법 찾기 전 임시로 다는 애니메이션 모음
//    //각 퍼크마다 들어가는 애니메이션과 아닌것이 있어 구분짓고, 교체가 용이하게 되기 위함

//}

