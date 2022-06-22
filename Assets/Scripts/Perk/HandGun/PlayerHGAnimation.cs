using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHGAnimation : PlayerAnimation
{
    public bool dual;
    bool leftRight;

    float delayTime;

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

        //�ڼ� �ʱ�ȭ
        // sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);

        // sk.AnimationState.SetAnimation(1, "Main_Weapon/idle_M", true);


        stop = false;

        moveLevel = 2;
        AttackCount = 0;

        dual = false;
        leftRight = true;

        shoot = false;

        delayTime = 0.2f;

        //���� ���� ��Ų
        sk.Skeleton.SetSkin("1gun");
        sk.Skeleton.SetSlotsToSetupPose();

    }


    // Update is called once per frame
    public void Update()    
    {



        if (!stop)
        {
            moveMotion();


        }



    }

    public override void moveMotion()
    {
        //�÷��̾� ���� ����
        if (playerAttack.playerAttackDir == 0)
        {//���ڼ��϶�

            if (PlInput.moveDir && playerMove.moveLevel != 4 && AttackCount == 0)
            {
                sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);

                if (!turn)
                {

                    if (!dual)
                    {
                        if (Dash && !playerMove.sit)
                        {
                            sk.AnimationState.SetAnimation(12, "1hand/turn_run", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                        else if (!Dash && playerMove.sit)
                        {
                            sk.AnimationState.SetAnimation(12, "1hand/turn_sit", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                        else if (!Dash && !playerMove.sit)
                        {

                            sk.AnimationState.SetAnimation(12, "1hand/turn", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                    }
                    else
                    {
                        if (Dash && !playerMove.sit)
                        {
                            sk.AnimationState.SetAnimation(12, "2hand/turn_run", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                        else if (!Dash && playerMove.sit)
                        {
                            sk.AnimationState.SetAnimation(12, "2hand/turn_sit", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                        else if (!Dash && !playerMove.sit)
                        {

                            sk.AnimationState.SetAnimation(12, "2hand/turn", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                    }
                   


                    turn = true;
                }
            }
            else if (!PlInput.moveDir && playerMove.moveLevel != 4 && AttackCount == 0)
            {
                sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);

                if (turn)
                {
                    if (!dual)
                    {
                        if (Dash && !playerMove.sit)
                        {
                            sk.AnimationState.SetAnimation(12, "1hand/turn_run", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                        else if (!Dash && playerMove.sit)
                        {
                            sk.AnimationState.SetAnimation(12, "1hand/turn_sit", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                        else if (!Dash && !playerMove.sit)
                        {

                            sk.AnimationState.SetAnimation(12, "1hand/turn", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                    }
                    else
                    {
                        if (Dash && !playerMove.sit)
                        {
                            sk.AnimationState.SetAnimation(12, "2hand/turn_run", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                        else if (!Dash && playerMove.sit)
                        {
                            sk.AnimationState.SetAnimation(12, "2hand/turn_sit", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                        else if (!Dash && !playerMove.sit)
                        {

                            sk.AnimationState.SetAnimation(12, "2hand/turn", false);
                            sk.AnimationState.AddEmptyAnimation(12, 0.1f, 0.1f);
                        }
                    }


                    turn = false;
                }
            }


        }






        //�÷��̾� �̵�
        if (PlInput.move != 0f && playerMove.moveLevel < 4 && Move)
        { //�̵� ��


            if (moveLevel >= 2)
            {

                if (dual)
                {//���
                 //�⺻ �ȱ� �̵�
                    if (!playerMove.sit)
                    {
                        if (playerAttack.playerAttackDir == 0)
                        {//����



                            if (Dash)
                            {
                                sk.AnimationState.SetAnimation(1, "2hand/run", true);
                            }
                            else
                            {
                                sk.AnimationState.SetAnimation(1, "2hand/move_f", true);
                            }


                        }
                        else if (playerAttack.playerAttackDir == 1)
                        {//�밢��

                            if (PlInput.move * sk.skeleton.ScaleX > 0)
                            { // ���� ����� �̵������� ������� ����
                                sk.AnimationState.SetAnimation(2, "2hand/move_f_diagonal", true);
                            }
                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
                            { // ���� ����� �̵������� �ٸ���� ����
                                sk.AnimationState.SetAnimation(2, "2hand/move_b_diagonal", true);
                            }
                        }
                        else if (playerAttack.playerAttackDir == 2)
                        {//��

                            if (PlInput.move * sk.skeleton.ScaleX > 0)
                            { // ���� ����� �̵������� ������� ����
                                sk.AnimationState.SetAnimation(2, "2hand/move_f_top", true);
                            }
                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
                            { // ���� ����� �̵������� �ٸ���� ����
                                sk.AnimationState.SetAnimation(2, "2hand/move_b_top", true);
                            }
                        }
                        else if (playerAttack.playerAttackDir == 3)
                        {//��������

                            if (PlInput.move * sk.skeleton.ScaleX > 0)
                            { // ���� ����� �̵������� ������� ����
                                sk.AnimationState.SetAnimation(2, "2hand/move_f_aiming", true);
                            }
                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
                            { // ���� ����� �̵������� �ٸ���� ����
                                sk.AnimationState.SetAnimation(2, "2hand/move_b_aiming", true);
                            }
                        }
                        else
                        {
                            Debug.Log(playerAttack.playerAttackDir);
                        }



                    }
                    else if (playerMove.sit)
                    {
                        sk.AnimationState.SetAnimation(1, "2hand/move_f_sit", true);
                    }
                }
                else
                {
                    //�⺻ �ȱ� �̵�
                    if (!playerMove.sit)
                    {
                        if (playerAttack.playerAttackDir == 0)
                        {//����



                            if (Dash)
                            {
                                sk.AnimationState.SetAnimation(1, "1hand/run", true);
                            }
                            else
                            {
                                sk.AnimationState.SetAnimation(1, "1hand/move_f", true);
                            }


                        }
                        else if (playerAttack.playerAttackDir == 1)
                        {//�밢��

                            if (PlInput.move * sk.skeleton.ScaleX > 0)
                            { // ���� ����� �̵������� ������� ����
                                sk.AnimationState.SetAnimation(2, "1hand/move_f_diagonal", true);
                            }
                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
                            { // ���� ����� �̵������� �ٸ���� ����
                                sk.AnimationState.SetAnimation(2, "1hand/move_b_diagonal", true);
                            }
                        }
                        else if (playerAttack.playerAttackDir == 2)
                        {//��

                            if (PlInput.move * sk.skeleton.ScaleX > 0)
                            { // ���� ����� �̵������� ������� ����
                                sk.AnimationState.SetAnimation(2, "1hand/move_f_top", true);
                            }
                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
                            { // ���� ����� �̵������� �ٸ���� ����
                                sk.AnimationState.SetAnimation(2, "1hand/move_b_top", true);
                            }
                        }
                        else if (playerAttack.playerAttackDir == 3)
                        {//��������

                            if (PlInput.move * sk.skeleton.ScaleX > 0)
                            { // ���� ����� �̵������� ������� ����
                                sk.AnimationState.SetAnimation(2, "1hand/move_f_aiming", true);
                            }
                            else if (PlInput.move * sk.skeleton.ScaleX < 0)
                            { // ���� ����� �̵������� �ٸ���� ����
                                sk.AnimationState.SetAnimation(2, "1hand/move_b_aiming", true);
                            }
                        }
                        else
                        {
                            Debug.Log(playerAttack.playerAttackDir);
                        }



                    }
                    else if (playerMove.sit)
                    {
                        sk.AnimationState.SetAnimation(1, "1hand/move_f_sit", true);
                    }
                }


            }




            Move = false;

        }
        else if (PlInput.move == 0f && playerMove.moveLevel < 4 && !Move)
        {
            if (dual)
            { // �μ�
                sk.AnimationState.AddEmptyAnimation(2, 0, 0);
                if (!playerMove.sit)
                {
                    AttackCount = playerAttack.playerAttackDir;

                    if (AttackCount == 0)
                    {
                        sk.AnimationState.AddEmptyAnimation(2, 0, 0);
                        sk.AnimationState.SetAnimation(1, "2hand/idle", true);
                    }
                    if (AttackCount == 1)
                    {
                        sk.AnimationState.SetAnimation(2, "2hand/idle_diagonal", true);
                    }
                    else if (AttackCount == 2)
                    {
                        sk.AnimationState.SetAnimation(2, "2hand/idle_top", true);
                    }
                    else if (AttackCount == 3)
                    {
                        sk.AnimationState.SetAnimation(2, "2hand/idle_aiming", true);
                    }
                    else if (AttackCount == 4)
                    {
                        sk.AnimationState.SetAnimation(2, "2hand/jumpfallbot", true);
                    }



                }
                else if (playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "2hand/idle_sit", true);
                }


            }
            else
            {
                sk.AnimationState.AddEmptyAnimation(2, 0, delayTime);
                if (!playerMove.sit)
                {
                    AttackCount = playerAttack.playerAttackDir;

                    if (AttackCount == 0)
                    {
                        sk.AnimationState.AddEmptyAnimation(2, 0, 0);
                        sk.AnimationState.SetAnimation(1, "1hand/idle", true);
                    }
                    if (AttackCount == 1)
                    {
                        sk.AnimationState.SetAnimation(2, "1hand/idle_diagonal", true);
                    }
                    else if (AttackCount == 2)
                    {
                        sk.AnimationState.SetAnimation(2, "1hand/idle_top", true);
                    }
                    else if (AttackCount == 3)
                    {
                        sk.AnimationState.SetAnimation(2, "1hand/idle_aiming", true);
                    }
                    else if (AttackCount == 4)
                    {
                        sk.AnimationState.SetAnimation(2, "1hand/jumpfallbot", true);
                    }



                }
                else if (playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "1hand/idle_sit", true);
                }

            }




            Move = true;
        }
    }

    public override void sitChangeMotion()
    {
        if (dual)
        {//���
            if (PlInput.move == 0f)
            {
                if (!playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "2hand/idle", true);
                }
                else if (playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "2hand/idle_sit", true);
                }
            }
            else
            {
                if (!playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "2hand/move_f", true);
                }
                else if (playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "2hand/move_f_sit", true);
                }
            }
        }
        else
        {
            if (PlInput.move == 0f)
            {
                if (!playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "1hand/idle", true);
                }
                else if (playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "1hand/idle_sit", true);
                }
            }
            else
            {
                if (!playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "1hand/move_f", true);
                }
                else if (playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "1hand/move_f_sit", true);
                }
            }
        }

        
    }

    public override void runChangeMotion(int moveLevel)
    {
        if (moveLevel >= 3)
        {
            Dash = true;
            sk.AnimationState.SetEmptyAnimation(1, delayTime);

            if (!dual)
            {

                TrackEntry entry = sk.AnimationState.SetAnimation(1, "1hand/run", true);
                entry.MixDuration = delayTime;
            }
            else
            {

                TrackEntry entry = sk.AnimationState.SetAnimation(1, "2hand/run", true);
                entry.MixDuration = delayTime;
            }

            //sk.AnimationState.SetEmptyAnimation(1, 1f);
            //sk.AnimationState.SetAnimation(1, "run", true);
        }
        else
        {

            if (moveLevel >= 3)
            {
                sk.AnimationState.SetAnimation(1, "2hand/run", true);
            }
            else if (moveLevel >= 2)
            {
                sk.AnimationState.SetAnimation(1, "2hand/move_f", true);
                Dash = false;
            }
            else if (moveLevel >= 1)
            {
                sk.AnimationState.SetAnimation(1, "2hand/move_f_sit", true);
                Dash = false;
            }
        }
    }

    public override void jumpPl()
    {
        Jump = false;
        if (!Dash)
        {
            sk.AnimationState.SetAnimation(1, "2hand/jump", false);
        }
        else
        {
            sk.AnimationState.SetAnimation(1, "2hand/jumpforward", false);
        }
        sk.AnimationState.AddAnimation(1, "2hand/jumpfall", true, 0f);
    }

    public override void Landing()
    {
        Jump = true;

        Debug.Log("adads");
        sk.AnimationState.SetAnimation(1, "2hand/jumpland", false);

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
            Debug.Log("����");
            Move = true;
            moveLevel = 2;

            if (!dual)
            {
                sk.AnimationState.AddAnimation(1, "1hand/idle", true, 0f);

            }
            else
            {
                sk.AnimationState.AddAnimation(1, "2hand/idle", true, 0f);
            }
          
        }
        else
        {
            Debug.Log("�̵�");
            Move = false;
            moveLevel = 2;
            if (Dash)
            {
             

                if (!dual)
                {
                    sk.AnimationState.AddAnimation(1, "1hand/run", true, 0f);

                }
                else
                {
                    sk.AnimationState.AddAnimation(1, "2hand/run", true, 0f);
                }

            }
            else
            {
             


                if (!dual)
                {
                    sk.AnimationState.AddAnimation(1, "1hand/move_f", true, 0f);

                }
                else
                {
                    sk.AnimationState.AddAnimation(1, "2hand/move_f", true, 0f);
                }
            }

        }


    }

    public override void DamageAfterCare()
    {

        if (PlInput.move != 0)
        {
            sk.AnimationState.AddAnimation(0, "2hand/Run", true, 0f);
        }
        else
        {
            sk.AnimationState.AddAnimation(0, "2hand/Main_Weapon/idle_M", true, 0f);
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
        //���� �Ƚ� �����̽� Ȧ���Ұ�� ������ȯ �Ұ����� ���� ��ü
        if (PlInput.moveDir)
        {
            sk.skeleton.ScaleX = Mathf.Abs(sk.skeleton.ScaleX);
        }
        else if (!PlInput.moveDir)
        {
            sk.skeleton.ScaleX = -Mathf.Abs(sk.skeleton.ScaleX);
        }

        sk.AnimationState.SetEmptyAnimation(1, delayTime);
        TrackEntry entry = sk.AnimationState.AddAnimation(1, "2hand/roll", false, 0);
        entry.MixDuration = delayTime;

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

    //    //�̵���� ���� �� ���콺�� �÷��̾� X���� �հ� �ݴ�� �̵����� ���
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
            sk.AnimationState.SetAnimation(1, "2hand/Idle2", true);
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
        sk.AnimationState.SetAnimation(1, "2hand/DamageForward", false);
    }

    public void JumpDamagedAnime()
    {
        sk.timeScale = 1;
        GetComponent<MeshRenderer>().enabled = true;
        sk.AnimationState.SetAnimation(1, "2hand/DamageJump", false);
    }

    public void RunAnimationClear()
    {
        //switch (AttackCount)
        //{ //�̵��� ����ڼ��� �����ϰ� �ٲٱ� ���� �ڵ�

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
    {// 0 �⺻���, 1 �밢��, 2 ��, 3 ����

        if (!dual)
        {
            if (dir == 0)
            {
                if (!playerMove.sit)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "1hand/up_shoot", false, 0);
                    entry.MixDuration = 0.01f;
                    //entry.AnimationStart = 0.5f;
                }
                else
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "1hand/shoot_sit", false, 0);
                    entry.MixDuration = 0.01f;
                }


            }
            else if (dir == 1)
            {
                sk.AnimationState.SetEmptyAnimation(3, delayTime);
                TrackEntry entry = sk.AnimationState.AddAnimation(3, "1hand/up_shoot_diagonal", false, 0);
                entry.MixDuration = 0.01f;
            }
            else if (dir == 2)
            {
                sk.AnimationState.SetEmptyAnimation(3, delayTime);
                TrackEntry entry = sk.AnimationState.AddAnimation(3, "1hand/up_shoot_top", false, 0);
                entry.MixDuration = 0.01f;
            }
            else if (dir == 3)
            {
                sk.AnimationState.SetEmptyAnimation(3, delayTime);
                TrackEntry entry = sk.AnimationState.AddAnimation(3, "1hand/up_shoot_aiming", false, 0);
                entry.MixDuration = 0.01f;
            }
            else if (dir == 4)
            {
                sk.AnimationState.SetEmptyAnimation(3, delayTime);
                TrackEntry entry = sk.AnimationState.AddAnimation(3, "1hand/shoot_bot", false, 0);
                entry.MixDuration = 0.01f;
            }

            sk.AnimationState.AddEmptyAnimation(3, 0.1f, 0.6f);
        }
        else
        {


            AttackCount = dir;

            if (leftRight)
            {

                if (dir == 0)
                {
                    if (!playerMove.sit)
                    {
                        sk.AnimationState.SetEmptyAnimation(3, delayTime);
                        TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/up_shoot_left", false, 0);
                        entry.MixDuration = 0.01f;
                        //entry.AnimationStart = 0.5f;
                    }
                    else
                    {
                        sk.AnimationState.SetEmptyAnimation(3, delayTime);
                        TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/shoot_left_sit", false, 0);
                        entry.MixDuration = 0.01f;
                    }


                }
                else if (dir == 1)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/up_shoot_left_diagonal", false, 0);
                    entry.MixDuration = 0.01f;
                }
                else if (dir == 2)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/up_shoot_left_top", false, 0);
                    entry.MixDuration = 0.01f;
                }
                else if (dir == 3)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/up_shoot_left_aiming", false, 0);
                    entry.MixDuration = 0.01f;
                }
                else if (dir == 4)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/shoot_left_bot", false, 0);
                    entry.MixDuration = 0.01f;
                }

                sk.AnimationState.AddEmptyAnimation(3, delayTime, 0.6f);
            }
            else
            {

                if (dir == 0)
                {
                    if (!playerMove.sit)
                    {
                        sk.AnimationState.SetEmptyAnimation(3, delayTime);
                        TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/up_shoot_right", false, 0);
                        entry.MixDuration = 0.01f;
                        //entry.AnimationStart = 0.5f;
                    }
                    else
                    {
                        sk.AnimationState.SetEmptyAnimation(3, delayTime);
                        TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/shoot_right_sit", false, 0);
                        entry.MixDuration = 0.01f;
                    }


                }
                else if (dir == 1)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/up_shoot_right_diagonal", false, 0);
                    entry.MixDuration = 0.01f;
                }
                else if (dir == 2)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/up_shoot_right_top", false, 0);
                    entry.MixDuration = 0.01f;
                }
                else if (dir == 3)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/up_shoot_right_aiming", false, 0);
                    entry.MixDuration = 0.01f;
                }
                else if (dir == 4)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(3, "2hand/shoot_right_bot", false, 0);
                    entry.MixDuration = 0.01f;
                }
            }


            if (leftRight)
            {
                leftRight = false;

            }
            else
            {
                leftRight = true;
            }

            //Debug.Log(leftRight);
            sk.AnimationState.AddEmptyAnimation(3, delayTime, 0.6f);

        }

    }

    public override void ShootReady(int dir)
    {// 0 �⺻���, 1 �밢��, 2 ��, 3 ����

        if (!dual)
        {
            AttackCount = dir;
            if (PlInput.move == 0f)
            {

                if (dir == 0)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    if (Jump)
                    {
                        sk.AnimationState.SetAnimation(1, "1hand/idle", true);
                    }
                    else
                    {
                        sk.AnimationState.SetAnimation(1, "1hand/jumpfall", true);
                    }
                }
                else if (dir == 1)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "1hand/idle_diagonal", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 2)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "1hand/idle_top", true, 0);
                    entry.MixDuration = 0.1f;

                }
                else if (dir == 3)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "1hand/idle_aiming", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 4)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "1hand/jumpfallbot", true, 0);
                    entry.MixDuration = 0.1f;
                }

            }
            else
            {
                if (dir == 0)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    if (Jump)
                    {
                        sk.AnimationState.SetAnimation(1, "1hand/move_f", true);
                    }
                    else
                    {
                        sk.AnimationState.SetAnimation(1, "1hand/jumpfall", true);
                    }
                }
                else if (dir == 1)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "1hand/move_f_diagonal", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 2)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "1hand/move_f_top", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 3)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "1hand/move_f_aiming", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 4)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "1hand/jumpfallbot", true, 0);
                    entry.MixDuration = 0.1f;
                }
            }



        }
        else
        {
            AttackCount = dir;
            if (PlInput.move == 0f)
            {

                if (dir == 0)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    if (Jump)
                    {
                        sk.AnimationState.SetAnimation(1, "2hand/idle", true);
                    }
                    else
                    {
                        sk.AnimationState.SetAnimation(1, "2hand/jumpfall", true);
                    }
                }
                else if (dir == 1)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "2hand/idle_diagonal", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 2)
                {
                    sk.AnimationState.SetEmptyAnimation(3, delayTime);
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "2hand/idle_top", true, 0);
                    entry.MixDuration = 0.1f;

                }
                else if (dir == 3)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "2hand/idle_aiming", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 4)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "2hand/jumpfallbot", true, 0);
                    entry.MixDuration = 0.1f;
                }

            }
            else
            {
                if (dir == 0)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    if (Jump)
                    {
                        sk.AnimationState.SetAnimation(1, "2hand/move_f", true);
                    }
                    else
                    {
                        sk.AnimationState.SetAnimation(1, "2hand/jumpfall", true);
                    }
                }
                else if (dir == 1)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "2hand/move_f_diagonal", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 2)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "2hand/move_f_top", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 3)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "2hand/move_f_aiming", true, 0);
                    entry.MixDuration = 0.1f;
                }
                else if (dir == 4)
                {
                    sk.AnimationState.SetEmptyAnimation(2, delayTime);
                    TrackEntry entry = sk.AnimationState.AddAnimation(2, "2hand/jumpfallbot", true, 0);
                    entry.MixDuration = 0.1f;
                }
            }



        }



    }

    public override void StopAction(int kind)
    {


        //����,  �麴��, �Ӽ�����
        if (kind == 0)
        {
            sk.AnimationState.SetEmptyAnimation(2, delayTime);
            sk.AnimationState.SetAnimation(7, "2hand/up_reload", false);
        }
        else if (kind == 1)
        {
            sk.AnimationState.SetAnimation(7, "2hand/combat", false);
        }
        else if (kind == 2)
        {
            //sk.AnimationState.SetAnimation(7, "shoot_ready", false);
        }


    }

    public override void MoveAction(int kind)
    {


        //�Ӽ�����
        if (kind == 0 && !shoot)
        {
            if (!dual)  
            {
                
                sk.Skeleton.SetSkin("2gun");
                sk.Skeleton.SetSlotsToSetupPose();
                dual = true;
            }
            else
            {
                sk.Skeleton.SetSkin("1gun");
                sk.Skeleton.SetSlotsToSetupPose();
                dual = false;
            }

        }

        sk.AnimationState.SetEmptyAnimations(0);    
        delayTime = 0;
        moveMotion();
        sitChangeMotion();
        //delayTime = 0.2f;
    }

    public override void Reload(float time)
    {

    }

    public override void StopActionDone()
    {
        sk.AnimationState.SetEmptyAnimation(7, delayTime);


    }

    public override void granadeStart()
    {
        //sk.AnimationState.SetEmptyAnimations(0.1f);
        sk.AnimationState.SetEmptyAnimation(10, delayTime);
        sk.AnimationState.SetAnimation(10, "throw_ready", false);
        sk.AnimationState.AddAnimation(10, "throw_idle", true, 0);


    }

    public override void granadeEnd()
    {
        sk.AnimationState.SetAnimation(10, "throw", false);
        sk.AnimationState.AddEmptyAnimation(10, delayTime, 0.7f);


    }


    //���⼭���ʹ� Ŭ������ �� ���� ��� ã�� �� �ӽ÷� �ٴ� �ִϸ��̼� ����
    //�� ��ũ���� ���� �ִϸ��̼ǰ� �ƴѰ��� �־� ��������, ��ü�� �����ϰ� �Ǳ� ����

}
