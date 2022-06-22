using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRFAnimation : PlayerAnimation
{

    public void Start()
    {

        PlInput = transform.GetComponent<PlayerInput>();
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
        sk = GetComponent<SkeletonAnimation>();
        //sk.AnimationName = "Idle";
        //sk.Start();

        Move = true;
        Jump = true;
        Attack = true;
        Dash = false;

        //자세 초기화
        // sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);

        // sk.AnimationState.SetAnimation(1, "Main_Weapon/idle_M", true);


        stop = false;

        moveLevel = 2;
        AttackCount = 0;

        turn = false;

    }


    // Update is called once per frame
    public void Update()
    {
        

        //임시 극소피격 소피격 중피격 대피격
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //sk.AnimationState.SetEmptyAnimations(0.1f);
            sk.AnimationState.SetAnimation(13, "dmg_f_veryweak", false);
            sk.AnimationState.AddEmptyAnimation(13, 0.1f, 0.4f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //sk.AnimationState.SetEmptyAnimations(0.1f);
            sk.AnimationState.SetAnimation(13, "dmg_f_weak", false);
            sk.AnimationState.AddEmptyAnimation(13, 0.1f, 0.55f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //sk.AnimationState.SetEmptyAnimations(0.1f);
            sk.AnimationState.SetAnimation(13, "dmg_f_mid", false);
            sk.AnimationState.AddEmptyAnimation(13, 0.1f, 0.9f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //sk.AnimationState.SetEmptyAnimations(0.1f);
            sk.AnimationState.SetAnimation(13, "dmg_b_veryweak", false);
            sk.AnimationState.AddEmptyAnimation(13, 0.1f, 0.4f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //sk.AnimationState.SetEmptyAnimations(0.1f);
            sk.AnimationState.SetAnimation(13, "dmg_b_weak", false);
            sk.AnimationState.AddEmptyAnimation(13, 0.1f, 0.55f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //sk.AnimationState.SetEmptyAnimations(0.1f);
            sk.AnimationState.SetAnimation(13, "dmg_f_mid", false);
            sk.AnimationState.AddEmptyAnimation(13, 0.1f, 0.9f);
        }



        if (!stop)
        {
            moveMotion();


        }



    }

    public override void moveMotion()
    {
        //플레이어 방향 설정
        if (playerAttack.playerAttackDir == 0)
        {//정자세일때

            if (PlInput.moveDir && playerMove.moveLevel != 4 && AttackCount == 0)
            {
                sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);

                if (!turn)
                {
                    if (Dash && !playerMove.sit)
                    {
                        sk.AnimationState.SetAnimation(12, "turn_run", false);
                        sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                    }
                    else if (!Dash && playerMove.sit)
                    {
                        sk.AnimationState.SetAnimation(12, "turn_sit", false);
                        sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                    }
                    else if (!Dash && !playerMove.sit)
                    {

                        sk.AnimationState.SetAnimation(12, "turn", false);
                        sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                    }


                    turn = true;
                }
            }
            else if (!PlInput.moveDir && playerMove.moveLevel != 4 && AttackCount == 0)
            {
                sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);

                if (turn)
                {
                    if (Dash && !playerMove.sit)
                    {
                        sk.AnimationState.SetAnimation(12, "turn_run", false);
                        sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                    }
                    else if (!Dash && playerMove.sit)
                    {
                        sk.AnimationState.SetAnimation(12, "turn_sit", false);
                        sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                    }
                    else if (!Dash && !playerMove.sit)
                    {

                        sk.AnimationState.SetAnimation(12, "turn", false);
                        sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                    }

                    turn = false;
                }
            }


        }






        //플레이어 이동
        if (PlInput.move != 0f && playerMove.moveLevel < 4 && Move)
        { //이동 중


            if (moveLevel >= 2)
            {

                //기본 걷기 이동
                if (!playerMove.sit)
                {
                    if (playerAttack.playerAttackDir == 0)
                    {//없음


                        if (Dash)
                        {
                            sk.AnimationState.SetAnimation(1, "run", true);
                        }
                        else
                        {
                            sk.AnimationState.SetAnimation(1, "move_f", true);
                        }


                    }
                    else if (playerAttack.playerAttackDir == 1)
                    {//대각선

                        if (PlInput.move * sk.skeleton.ScaleX > 0)
                        { // 보는 방향과 이동방향이 같을경우 전진
                            sk.AnimationState.SetAnimation(2, "move_f_diagonal", true);
                        }
                        else if (PlInput.move * sk.skeleton.ScaleX < 0)
                        { // 보는 방향과 이동방향이 다를경우 후진
                            sk.AnimationState.SetAnimation(2, "move_b_diagonal", true);
                        }
                    }
                    else if (playerAttack.playerAttackDir == 2)
                    {//위

                        if (PlInput.move * sk.skeleton.ScaleX > 0)
                        { // 보는 방향과 이동방향이 같을경우 전진
                            sk.AnimationState.SetAnimation(2, "move_f_top", true);
                        }
                        else if (PlInput.move * sk.skeleton.ScaleX < 0)
                        { // 보는 방향과 이동방향이 다를경우 후진
                            sk.AnimationState.SetAnimation(2, "move_b_top", true);
                        }
                    }
                    else if (playerAttack.playerAttackDir == 3)
                    {//가로조준

                        if (PlInput.move * sk.skeleton.ScaleX > 0)
                        { // 보는 방향과 이동방향이 같을경우 전진
                            sk.AnimationState.SetAnimation(2, "move_f_aiming", true);
                        }
                        else if (PlInput.move * sk.skeleton.ScaleX < 0)
                        { // 보는 방향과 이동방향이 다를경우 후진
                            sk.AnimationState.SetAnimation(2, "move_b_aiming", true);
                        }
                    }
                    else
                    {
                        Debug.Log(playerAttack.playerAttackDir);
                    }



                }
                else if (playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "move_f_sit", true);
                }

            }




            Move = false;

        }
        else if (PlInput.move == 0f && playerMove.moveLevel < 4 && !Move)
        {
            sk.AnimationState.AddEmptyAnimation(2, 0, 0.3f);
            if (!playerMove.sit)
            {
                AttackCount = playerAttack.playerAttackDir;

                if (AttackCount == 0)
                {
                    sk.AnimationState.AddEmptyAnimation(2, 0, 0);
                    sk.AnimationState.SetAnimation(1, "idle", true);
                }
                if (AttackCount == 1)
                {
                    sk.AnimationState.SetAnimation(2, "idle_diagonal", true);
                }
                else if (AttackCount == 2)
                {
                    sk.AnimationState.SetAnimation(2, "idle_top", true);
                }
                else if (AttackCount == 3)
                {
                    sk.AnimationState.SetAnimation(2, "idle_aiming", true);
                }
                else if (AttackCount == 4)
                {
                    sk.AnimationState.SetAnimation(2, "jumpfallbot", true);
                }

                //if (AttackCount == 1)
                //{
                //    sk.AnimationState.SetAnimation(2, "idle_diagonal", true);
                //}
                //else if (AttackCount == 2)
                //{
                //    sk.AnimationState.SetAnimation(2, "idle_top", true);
                //}
                //else if (AttackCount == 3)
                //{
                //    sk.AnimationState.SetAnimation(2, "idle_aiming", true);
                //}
                //else
                //{
                //    sk.AnimationState.AddEmptyAnimation(2, 0, 0);
                //    sk.AnimationState.SetAnimation(0, "idle", true);
                //}


            }
            else if (playerMove.sit)
            {
                sk.AnimationState.SetAnimation(1, "idle_sit", true);
            }



            Move = true;
        }
    }

    public override void sitChangeMotion()
    {
        if (PlInput.move == 0f)
        {
            if (!playerMove.sit)
            {
                sk.AnimationState.SetAnimation(1, "idle", true);
            }
            else if (playerMove.sit)
            {
                sk.AnimationState.SetAnimation(1, "idle_sit", true);
            }
        }
        else
        {
            if (!playerMove.sit)
            {
                sk.AnimationState.SetAnimation(1, "move_f", true);
            }
            else if (playerMove.sit)
            {
                sk.AnimationState.SetAnimation(1, "move_f_sit", true);
            }
        }
    }

    public override void runChangeMotion(int moveLevel)
    {
        if (moveLevel >= 3)
        {
            Dash = true;
            sk.AnimationState.SetEmptyAnimation(1, 0.2f);
            TrackEntry entry = sk.AnimationState.SetAnimation(1, "run", true);
            entry.MixDuration = 0.1f;
            //sk.AnimationState.SetEmptyAnimation(1, 1f);
            //sk.AnimationState.SetAnimation(1, "run", true);
        }
        else
        {

            if (moveLevel >= 3)
            {
                sk.AnimationState.SetAnimation(1, "run", true);
            }
            else if (moveLevel >= 2)
            {
                sk.AnimationState.SetAnimation(1, "move_f", true);
                Dash = false;
            }
            else if (moveLevel >= 1)
            {
                sk.AnimationState.SetAnimation(1, "move_f_sit", true);
                Dash = false;
            }
        }
    }

    public override void jumpPl()
    {
        Jump = false;
        if (!Dash)
        {
            sk.AnimationState.SetAnimation(1, "jump", false);
        }
        else
        {
            sk.AnimationState.SetAnimation(1, "jumpforward", false);
        }
        sk.AnimationState.AddAnimation(1, "jumpfall", true, 0f);
    }

    public override void Landing()
    {
        Jump = true;

        Debug.Log("adads");
        sk.AnimationState.SetAnimation(1, "jumpland", false);

        //if (PlInput.move != 0)
        //{
        //    sk.AnimationState.AddAnimation(0, "Run", true, 0f);
        //}
        //else
        //{
        //    sk.AnimationState.AddAnimation(0, "Main_Weapon/idle_M", true, 0f);
        //}
        if (PlInput.move == 0f)
        {
            Debug.Log("정지");
            Move = true;
            moveLevel = 2;
            sk.AnimationState.AddAnimation(1, "idle", true, 0f);
        }
        else
        {
            Debug.Log("이동");
            Move = false;
            moveLevel = 2;
            if (Dash)
            {
                sk.AnimationState.AddAnimation(1, "run", true, 0f);

            }
            else
            {
                sk.AnimationState.AddAnimation(1, "move_f", true, 0f);
            }

        }


    }

    public override void DamageAfterCare()
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


    public override void MovingAttack(int AttackNum)
    {

        //if (AttackNum != AttackCount && !Attack)
        //{
        //    AttackCount = AttackNum;

        //    Move = true;
        //}





    }

    public override void MovingAttackStop()
    {
        StopAllCoroutines();

        AttackCount = 1;
        if (!Attack)
        {


            Move = true;
        }


    }

    public override void DashAnime()
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

        sk.AnimationState.SetEmptyAnimation(1, 0f);
        TrackEntry entry = sk.AnimationState.AddAnimation(1, "roll", false, 0);
        entry.MixDuration = 0.1f;

        //sk.AnimationState.SetAnimation(1, "step", false);
    }

    public override void DashAnimeDone()
    {


        if (PlInput.move == 0f)
        {
            Move = false;
            moveLevel = 0;
        }
        else
        {
            Move = true;
            moveLevel = 2;
        }
    }






    //public void StandCombo(bool dis)
    //{
    //    // mousePos = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

    //    //이동사격 전투 중 마우스가 플레이어 X축을 뚫고 반대로 이동했을 경우
    //    //if (Input.mousePosition.x > transform.position.x)
    //    //{
    //    //    Debug.Log("dsfsdfdsfsfdsfs");
    //    //}

    //    if (dis)
    //    {
    //        sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
    //    }
    //    else
    //    {
    //        sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
    //    }


    //    sk.AnimationState.SetAnimation(1, "Idle_Shoot", true);
    //}

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

    public override void Shoot(int dir)
    {// 0 기본사격, 1 대각선, 2 위, 3 조준
        AttackCount = dir;
        if (dir == 0)
        {
            if (!playerMove.sit)
            {
                //sk.AnimationState.SetEmptyAnimation(3, 0f);
                //TrackEntry entry = sk.AnimationState.AddAnimation(3, "up_shoot", false, 0);
                //entry.MixDuration = 0.1f;
                ////entry.AnimationStart = 0.5f;
            }
            else
            {
                sk.AnimationState.SetEmptyAnimation(3, 0f);
                TrackEntry entry = sk.AnimationState.AddAnimation(3, "shoot_sit", false, 0);
                entry.MixDuration = 0.1f;
            }


        }
        else if (dir == 1)
        {
            sk.AnimationState.SetEmptyAnimation(3, 0f);
            TrackEntry entry = sk.AnimationState.AddAnimation(3, "shoot_diagonal", false, 0);
            entry.MixDuration = 0.1f;
        }
        else if (dir == 2)
        {
            //sk.AnimationState.SetEmptyAnimation(3, 0f);
            //TrackEntry entry = sk.AnimationState.AddAnimation(3, "shoot_top", false, 0);
            //entry.MixDuration = 0.1f;
        }
        else if (dir == 3)
        {
            sk.AnimationState.SetEmptyAnimation(3, 0f);
            TrackEntry entry = sk.AnimationState.AddAnimation(3, "shoot_aiming", false, 0);
            entry.MixDuration = 0.1f;
        }
        else if (dir == 4)
        {
            sk.AnimationState.SetEmptyAnimation(3, 0f);
            TrackEntry entry = sk.AnimationState.AddAnimation(3, "shoot_bot", false, 0);
            entry.MixDuration = 0.1f;
        }

        sk.AnimationState.AddEmptyAnimation(3, 0.3f, 1.5f);

    }

    public override void ShootReady(int dir)
    {// 0 기본사격, 1 대각선, 2 위, 3 조준
        AttackCount = dir;
        if (PlInput.move == 0f)
        {

            if (dir == 0)
            {
                //sk.AnimationState.SetEmptyAnimation(3, 0.1f);
                //sk.AnimationState.AddEmptyAnimation(2, 0, 0);
                sk.AnimationState.SetEmptyAnimation(2, 0.1f);
                if (Jump)
                {
                    sk.AnimationState.SetAnimation(1, "idle", true);
                }
                else
                {
                    sk.AnimationState.SetAnimation(1, "jumpfall", true);
                }
            }
            else if (dir == 1)
            {
                sk.AnimationState.SetEmptyAnimation(2, 0f);
                TrackEntry entry = sk.AnimationState.AddAnimation(2, "idle_diagonal", true, 0);
                entry.MixDuration = 0.1f;

                //sk.AnimationState.SetEmptyAnimation(2, 0f);
                //trackEntry = sk.AnimationState.SetAnimation(2, "dmg_falldown", true);
                //trackEntry.MixDuration = 0.1f;
                //trackEntry.AnimationStart = 1.3f;
            }
            else if (dir == 2)
            {
                //sk.AnimationState.SetEmptyAnimation(3, 0.1f);
                //sk.AnimationState.SetEmptyAnimation(2, 0f);
                //TrackEntry entry = sk.AnimationState.AddAnimation(2, "idle_top", true, 0);
                //entry.MixDuration = 0.1f;

            }
            else if (dir == 3)
            {
                sk.AnimationState.SetEmptyAnimation(2, 0f);
                TrackEntry entry = sk.AnimationState.AddAnimation(2, "idle_aiming", true, 0);
                entry.MixDuration = 0.1f;
            }
            else if (dir == 4)
            {
                sk.AnimationState.SetEmptyAnimation(2, 0f);
                TrackEntry entry = sk.AnimationState.AddAnimation(2, "jumpfallbot", true, 0);
                entry.MixDuration = 0.1f;
            }

        }
        else
        {
            if (dir == 0)
            {
                sk.AnimationState.SetEmptyAnimation(2, 0.1f);
                if (Jump)
                {
                    sk.AnimationState.SetAnimation(1, "move_f", true);
                }
                else
                {
                    sk.AnimationState.SetAnimation(1, "jumpfall", true);
                }
            }
            else if (dir == 1)
            {
                sk.AnimationState.SetEmptyAnimation(2, 0.1f);
                TrackEntry entry = sk.AnimationState.AddAnimation(2, "move_f_diagonal", true, 0);
                entry.MixDuration = 0.1f;
            }
            else if (dir == 2)
            {
                //sk.AnimationState.SetEmptyAnimation(2, 0.1f);
                //TrackEntry entry = sk.AnimationState.AddAnimation(2, "move_f_top", true, 0);
                //entry.MixDuration = 0.1f;
            }
            else if (dir == 3)
            {
                sk.AnimationState.SetEmptyAnimation(2, 0.1f);
                TrackEntry entry = sk.AnimationState.AddAnimation(2, "move_f_aiming", true, 0);
                entry.MixDuration = 0.1f;
            }
            else if (dir == 4)
            {
                sk.AnimationState.SetEmptyAnimation(2, 0.1f);
                TrackEntry entry = sk.AnimationState.AddAnimation(2, "jumpfallbot", true, 0);
                entry.MixDuration = 0.1f;
            }
        }



    }

    public override void StopAction(int kind)
    {


        //장전,  백병전, 속성변경
        if (kind == 0)
        {
            sk.AnimationState.SetEmptyAnimation(2, 0.1f);
            sk.AnimationState.SetAnimation(7, "up_reload", false);
        }
        else if (kind == 1)
        {
            sk.AnimationState.SetAnimation(7, "combat", false);
        }
        else if (kind == 2)
        {
            //sk.AnimationState.SetAnimation(7, "shoot_ready", false);
        }


    }

    public override void MoveAction(int kind)
    {


        //장전,  백병전, 속성변경
        if (kind == 0)
        {
            sk.AnimationState.SetEmptyAnimation(2, 0.1f);
            sk.AnimationState.SetAnimation(7, "up_reload", false);
        }
        else if (kind == 1)
        {
            sk.AnimationState.SetAnimation(7, "combat", false);
        }
        else if (kind == 2)
        {
            //sk.AnimationState.SetAnimation(7, "shoot_ready", false);
        }


    }

    public override void Reload(float time)
    {

    }

    public override void StopActionDone()
    {
        sk.AnimationState.SetEmptyAnimation(7, 0);


    }

    public override void granadeStart()
    {
        //sk.AnimationState.SetEmptyAnimations(0.1f);
        sk.AnimationState.SetEmptyAnimation(10, 0.1f);
        sk.AnimationState.SetAnimation(10, "throw_ready", false);
        sk.AnimationState.AddAnimation(10, "throw_idle", true, 0);


    }

    public override void granadeEnd()
    {
        sk.AnimationState.SetAnimation(10, "throw", false);
        sk.AnimationState.AddEmptyAnimation(10, 0.1f, 0.7f);


    }


    //여기서부터는 클래스로 더 나은 방법 찾기 전 임시로 다는 애니메이션 모음
    //각 퍼크마다 들어가는 애니메이션과 아닌것이 있어 구분짓고, 교체가 용이하게 되기 위함

}
