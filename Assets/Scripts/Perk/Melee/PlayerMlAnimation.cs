using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMlAnimation : PlayerAnimation
{

    int attackCountStrong;

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

        AttackCount = 0;
        attackCountStrong = 0;


    }


    // Update is called once per frame
    public void Update()
    {

        if (!stop)
        {

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






            //�÷��̾� �̵�
            if (PlInput.move != 0f && playerMove.moveLevel < 4 && Move)
            { //�̵� ��



                if (moveLevel >= 2)
                {

                    //�⺻ �ȱ� �̵�
                    if (!playerMove.sit)
                    {
                        if (playerAttack.playerAttackDir == 0)
                        {//����
                            if (Dash)
                            {
                                sk.AnimationState.SetAnimation(1, "run", true);
                            }
                            else
                            {
                                sk.AnimationState.SetAnimation(1, "move_f", true);
                            }

                        }




                    }
                    else if (playerMove.sit)
                    {
                        if (PlInput.move * sk.skeleton.ScaleX > 0)
                        { // ���� ����� �̵������� ������� ����
                            sk.AnimationState.SetAnimation(1, "move_f_sit", true);
                        }
                        else if (PlInput.move * sk.skeleton.ScaleX < 0)
                        { // ���� ����� �̵������� �ٸ���� ����
                            sk.AnimationState.SetAnimation(1, "move_b_sit", true);
                        }
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

                    
                    sk.AnimationState.AddEmptyAnimation(2, 0, 0);
                    sk.AnimationState.SetAnimation(1, "idle", true);

                }
                else if (playerMove.sit)
                {
                    sk.AnimationState.SetAnimation(1, "idle_sit", true);
                }



                Move = true;
            }
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
                sk.AnimationState.SetAnimation(1, "idle", true);
            }
            else if (playerMove.sit)
            {
                sk.AnimationState.SetAnimation(1, "idle_sit", true);
            }
        }
    }

    public override void runChangeMotion(int moveLevel)
    {
        if (moveLevel >= 3)
        {
            sk.AnimationState.SetAnimation(1, "run", true);
        }
        else
        {
            if (moveLevel >= 2)
            {
                sk.AnimationState.SetAnimation(1, "move_f", true);
            }
            else if (moveLevel >= 1)
            {
                sk.AnimationState.SetAnimation(1, "move_f_sit", true);
            }
        }
    }

    public override void jumpPl()
    {
        Jump = false;
        sk.AnimationState.SetAnimation(1, "jump", false);
        sk.AnimationState.AddAnimation(1, "jumpfall", true, 0f);
    }

    public override void Landing()
    {
        Jump = true;

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
            Move = false;
            moveLevel = 0;
        }
        else
        {
            Move = true;
            moveLevel = 2;
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

        if (AttackNum != AttackCount && !Attack)
        {
            AttackCount = AttackNum;

            Move = true;
        }





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

        sk.AnimationState.SetAnimation(1, "roll", false);
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



  
    public override void Shoot(int dir)
    {// 0 �⺻�����, 1 ����������, 2 �޸�������, 3 �⺻������, 4 �����Ⱝ����, 5 �޸��Ⱝ����, 6 ��������, 7 ��� ����
     //AttackCount = dir;

        if (dir == 0)
        {

            if (AttackCount == 0)
            {
                sk.AnimationState.SetAnimation(3, "attack_weak", false);
                AttackCount++;
            }
            else if (AttackCount == 1)
            {
                sk.AnimationState.SetAnimation(3, "attack_weak2", false);
                AttackCount++;
            }
            else
            {
                sk.AnimationState.SetAnimation(3, "attack_weak3", false);
                AttackCount = 0;
            }
            sk.AnimationState.AddEmptyAnimation(3, 0, 1f);
        }
        else if (dir == 1)
        {
            sk.AnimationState.SetAnimation(3, "attack_roll_weak", false);
            AttackCount++;

            sk.AnimationState.AddEmptyAnimation(3, 0, 0.6f);
        }
        else if (dir == 2)
        {
            sk.AnimationState.SetAnimation(3, "attack_run_weak", false);
            AttackCount++;

            sk.AnimationState.AddEmptyAnimation(3, 0, 0.9f);
        }
        else if (dir == 3)
        {

            if (AttackCount == 0)
            {
                sk.AnimationState.SetAnimation(3, "attack_strong", false);
                AttackCount++;
            }
            else
            {
                sk.AnimationState.SetAnimation(3, "attack_strong2", false);
                AttackCount = 0;
            }
            sk.AnimationState.AddEmptyAnimation(3, 0, 1.3f);
        }
        else if (dir == 4)
        {
            sk.AnimationState.SetAnimation(3, "attack_roll_strong", false);

            sk.AnimationState.AddEmptyAnimation(3, 0, 1.3f);
        }
        else if (dir == 5)
        {
            sk.AnimationState.SetAnimation(3, "attack_run_strong", false);

            sk.AnimationState.AddEmptyAnimation(3, 0, 1.4f);
        }
        else if (dir == 6)
        {
          
        }
        else if (dir == 7)
        {
            sk.AnimationState.SetAnimation(3, "attack_mod", false);

            sk.AnimationState.AddEmptyAnimation(3, 0, 1.1f);
        }


    }

    public override void ShootReady(int dir)
    {// 0 �������� �޺� �ʱ�ȭ


        AttackCount = 0;
        attackCountStrong = 0;
       


    }

    public override void StopAction(int kind)
    {


        //����,  �麴��, �Ӽ�����
        if (kind == 0)
        {
          //  sk.AnimationState.SetEmptyAnimation(2, 0.1f);
            sk.AnimationState.SetAnimation(7, "ctrl", false);
            sk.AnimationState.AddEmptyAnimation(7, 0.1f, 1.7f);
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

    public override void Reload(float time)
    {
        
    }




    //���⼭���ʹ� Ŭ������ �� ���� ��� ã�� �� �ӽ÷� �ٴ� �ִϸ��̼� ����
    //�� ��ũ���� ���� �ִϸ��̼ǰ� �ƴѰ��� �־� ��������, ��ü�� �����ϰ� �Ǳ� ����

}
