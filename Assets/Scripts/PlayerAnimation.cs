using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    int ani = 2;

    public PlayerInput PlInput;

    public SkeletonAnimation sk;
   // Skeleton skeleton;

    //움직였을경우 꺼지는 트리거
    public bool Move;

    //점프상태일경우
    public bool Jump;

    //어택 상태일경우
    public bool Attack;
    int AttackCount;
    //카운트 0 전방이동사격자세, 1 대기, 2 후방이동사격자세


    //대쉬 상태일경우
    bool Dash;

    //마우스 포지션
    Vector3 mousePos;
    //그걸 위한 카메라
    Camera cam;


    //다목적 멈추기
    public bool stop;


    //점프폴은 이미지의 Y축을 -2 해야 적절하다

    // Start is called before the first frame update
    public void Start()
    {

        PlInput = transform.GetComponent<PlayerInput>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        sk = GetComponent<SkeletonAnimation>();

        //sk.AnimationName = "Idle";
        //sk.Start();

        Move = true;
        Jump = true;
        Attack = true;
        Dash = true;

        //자세 초기화
        sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);

        sk.AnimationState.SetAnimation(0, "JumpFall", true);


        stop = false;

        AttackCount = 1;


    }


    // Update is called once per frame
    public void Update()
    {
        if (!stop)
        {



            //플레이어 방향 설정
            if (PlInput.moveDir)
            {
                sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
            }
            else
            {
                sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
            }

            //공격 트리거
            if (Input.GetMouseButton(0) && PlInput.move == 0f && Jump && !Attack && Dash )
            {//정지 사격시 애니메이션 숨기기

                transform.GetComponent<MeshRenderer>().enabled = false;

            }
            else
            {


                transform.GetComponent<MeshRenderer>().enabled = true;
            }


            //플레이어 이동/이동하며 총쏘기
            if (PlInput.move != 0f && Move && Jump && Dash)
            { //이동 중

                if (Attack)
                {
                    sk.AnimationState.SetAnimation(0, "Main_Weapon/run_M", true);
                }
                else
                {
                    //if (stop || !quantum)
                    //{
                    //    AttackCount = 1;
                    //}

                    switch (AttackCount)
                    { //이동중 사격자세를 용이하게 바꾸기 위한 코드

                        case 0:
                            sk.AnimationState.SetAnimation(0, "Run_Shoot", true);
                            break;
                        case 1:
                            sk.AnimationState.SetAnimation(0, "Main_Weapon/run_M", true);
                            break;
                        case 2:
                            sk.AnimationState.SetAnimation(0, "Run_BackShoot", true);
                            break;


                        default:
                            break;
                    }

                 

                }



                Move = false;

            }
            //else if (PlInput.move != 0f && Move && Jump && !Attack)
            //{//이동중 공격
            //    sk.AnimationState.SetAnimation(0, "Run_Shoot", true);
            //}
            else if (PlInput.move == 0f && !Move && Jump && Dash)
            {
                if (Attack)
                {
                    sk.AnimationState.SetAnimation(0, "Main_Weapon/idle_M", true);
                }
                else
                {
                    sk.AnimationState.SetAnimation(0, "Idle2", true);
                }



                Move = true;
            }
        }


       
    }

    public void jumpPl()
    {
        Jump = false;
        sk.AnimationState.SetAnimation(0, "Jump", true);
        sk.AnimationState.AddAnimation(0, "JumpFall", true, 0f);
    }

    public void Landing()
    {
        Jump = true;

        sk.AnimationState.SetAnimation(0, "Land", false);

        if (PlInput.move != 0)
        {
            sk.AnimationState.AddAnimation(0, "Run", true, 0f);
        }
        else 
        {
            sk.AnimationState.AddAnimation(0, "Main_Weapon/idle_M", true, 0f);
        }

      
    }

    public void DamageAfterCare()
    {
       
        if (PlInput.move != 0)
        {
            sk.AnimationState.AddAnimation(0, "Run", true, 0f);
        }
        else
        {
            sk.AnimationState.AddAnimation(0, "Main_Weapon/idle_M", true, 0f);
        }
        Attack = false;
        Move = false;
        stop = false;
    }


    public void MovingAttack(int AttackNum)
    {

        if (AttackNum != AttackCount && !Attack)
        {
            AttackCount = AttackNum;

            Move = true;
        }


       


    }

    public void MovingAttackStop()
    {
        StopAllCoroutines();

        AttackCount = 1;
        if (!Attack)    
        {
            

            Move = true;
        }


    }

    public void DashAnime()
    {
        //버그 픽스 스페이스 홀드할경우 방향전환 불가능한 문제 대체
        if (PlInput.moveDir)
        {
            sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
        }
        else if (!PlInput.moveDir)
        {
            sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
        }

        sk.AnimationState.SetAnimation(0, "Main_Weapon/roll_M", false);
    }

    public void DashAnimeDone()
    {
        Dash = true;

        if (PlInput.move == 0f)
        {
            Move = false;
        }
        else
        {
            Move = true;
        }
    }

    // 버그픽스를 위한 모습보이기
    public void AttackDelay()
    {
        
    }

    public void QComboLastAttack(bool dis)
    {
        //sk.timeScale = 1;

        ////플레이어 방향 설정
        //if (dis)
        //{
        //    sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
        //}
        //else
        //{
        //    sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
        //}

        //sk.AnimationState.SetAnimation(0, "Idle_Shoot", true);

        GetComponent<MeshRenderer>().enabled = false;
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


        sk.AnimationState.SetAnimation(0, "Idle_Shoot", true);
    }

    public void StandComboEnd(float speed)
    {
        if (speed < 0.1f)
        {
            sk.AnimationState.SetAnimation(0, "Idle2", true);
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
        sk.AnimationState.SetAnimation(0, "DamageForward", false);
    }

    public void JumpDamagedAnime()
    {
        sk.timeScale = 1;
        GetComponent<MeshRenderer>().enabled = true;
        sk.AnimationState.SetAnimation(0, "DamageJump", false);
    }

    public void RunAnimationClear()
    {
        switch (AttackCount)
        { //이동중 사격자세를 용이하게 바꾸기 위한 코드

            case 0:
                sk.AnimationState.SetAnimation(0, "Run_Shoot", true);
                break;
            case 1:
                sk.AnimationState.SetAnimation(0, "Run", true);
                break;
            case 2:
                sk.AnimationState.SetAnimation(0, "Run_BackShoot", true);
                break;


            default:
                break;
        }

    }

}
