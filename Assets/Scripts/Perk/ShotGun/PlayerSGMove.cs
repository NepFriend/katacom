using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSGMove : PlayerMove
{
    // Start is called before the first frame update
    void Start()
    {
        // Player = transform.gameObject;
        PlInputAnimation = GetComponent<PlayerAnimation>();

        playerInput = GetComponent<PlayerInput>();
        playerRigidBody = GetComponent<Rigidbody2D>();

        tr = GetComponent<Transform>();

        moveLevel = 0;
        shift_clock = 0;

        stop = false;
        sit = false;
        run = false;
        jump = false;

        DirFix = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ������ �ƴҰ��
        if (!stop)
        {

            PlayerHorizontalMove();


        }



    }

    public override void PlayerHorizontalMove()
    {
        //Debug.Log(sit);



        if (playerInput.move != 0 && moveLevel < 4)
        {
            if (shift_clock > 0.5f)
            {
                moveLevel = 3;
                if (!run)
                {
                    PlInputAnimation.runChangeMotion(moveLevel);
                    run = true;
                }
            }
            else
            {


                if (sit)
                {
                    moveLevel = 1;
                }
                else
                {
                    moveLevel = 2;
                }

                if (run)
                {
                    PlInputAnimation.runChangeMotion(moveLevel);
                    run = false;
                }

            }


            PlInputAnimation.moveLevel = 2;

        }
        else if (playerInput.move == 0 && moveLevel < 3)
        {
            moveLevel = 0;
        }

        //����
        if (Input.GetKey(KeyCode.Space) && moveLevel < 4 && !jump)
        {
            moveLevel = 5;
            StartCoroutine(JumpPlayer());
            jump = true;
        }


        //�ɱ�
        if (playerInput.lookDown && moveLevel < 4 && !jump && !run)
        {

            if (!sit)
            {
                sit = true;
                PlInputAnimation.sitChangeMotion();

            }
        }
        if (!playerInput.lookDown && moveLevel < 4 && !jump && !run)
        {
            if (sit)
            {
                sit = false;
                PlInputAnimation.sitChangeMotion();

            }
        }

        //if (Input.GetKeyDown(KeyCode.C) && moveLevel < 4 && !jump && !run)
        //{
        //    Debug.Log("ok");
        //    if (!sit)
        //    {
        //        sit = true;
        //        PlInputAnimation.sitChangeMotion();

        //    }
        //    else if (sit)
        //    {
        //        sit = false;
        //        PlInputAnimation.sitChangeMotion();

        //    }
        //}

        //������
        if (Input.GetKey(KeyCode.LeftShift))
        {

            shift_clock += Time.deltaTime;
        }
        else
        {
            if (shift_clock > 0)
            {
                if (shift_clock < 0.5f && moveLevel <= 2)
                {
                    StartCoroutine(Roll());
                }

                shift_clock = 0;
            }
        }



        if (moveLevel == 1)
        { //����
            Vector2 moveDistance = (playerInput.move * tr.right) * (moveSpeed / 3) * Time.deltaTime;
            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
        }
        else if (moveLevel == 2 || moveLevel == 5)
        { //�ȱ� / ����
            Vector2 moveDistance = (playerInput.move * tr.right) * moveSpeed * Time.deltaTime;
            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);


        }
        else if (moveLevel == 3)
        { //�޸���

            Vector2 moveDistance = (playerInput.move * tr.right) * (moveSpeed + 10f) * Time.deltaTime;
            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);

        }
        else if (moveLevel == 4)
        { //������

            Vector2 moveDistance = (DirFix * tr.right) * moveSpeed * Time.deltaTime;
            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
        }
        else if (moveLevel == 5)
        { //���� ����
            Vector2 moveDistance = (playerInput.move * tr.right) * moveSpeed * Time.deltaTime;

            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
        }
        else if (moveLevel == 6)
        { //���� �Ʒ���
            Vector2 moveDistance = (playerInput.move * tr.right) * moveSpeed * Time.deltaTime;

            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
        }


    }

    public override void Landing()
    {

        PlInputAnimation.Landing();
        if (playerInput.move != 0)
        {
            if (shift_clock != 0)
            {
                moveLevel = 3;
            }
            else
            {
                moveLevel = 2;
            }


        }
        else if (playerInput.move == 0)
        {
            moveLevel = 0;
        }
        jump = false;
    }







    private void OnCollisionEnter2D(Collision2D collision)
    {


        //���� �ٴڿ� �پ��� ���
        //if (collision.transform.tag == "Floor")
        //{
        //    //PlInputAnimation.Landing();


        //}

        //���� �� ����� ������
        //if (playerInput.move != 0)
        //{
        //    moveLevel = 2;
        //}
        //else if (playerInput.move == 0)
        //{
        //    moveLevel = 0;
        //}

        Landing();
    }



    IEnumerator PlayerDamaged() // �÷��̾� ����� �Ծ��� �� �ൿ���
    {
        // PlInputAnimation.DamagedAnime();


        yield return new WaitForSeconds(0.5f);

    }


    IEnumerator JumpPlayer() // �÷��̾� ���� ���� �� ������ �϶�
    {
        //�ִϸ��̼�
        PlInputAnimation.jumpPl();

        //�������� ���� ���ϱ�
        moveLevel = 5;
        PlInputAnimation.moveLevel = 5;
        //���� ����
        //if (playerInput.moveDir)
        //{
        //    DirFix = 1f;
        //}
        //else
        //{
        //    DirFix = -1f;
        //}
        playerRigidBody.gravityScale = -40;
        yield return new WaitForSeconds(4f);
        moveLevel = 6;
        ////���� �� ����� ������
        //if (playerInput.move != 0)
        //{
        //    moveLevel = 2;
        //}
        //else if (playerInput.move == 0)
        //{
        //    moveLevel = 0;
        //}
        //PlInputAnimation.Landing();
        playerRigidBody.gravityScale = 40;
    }


    IEnumerator Roll() // �÷��̾� ������ ���� �� ������ �϶�
    {
        //�ִϸ��̼�
        PlInputAnimation.DashAnime();

        //�������� ���� ���ϱ�
        moveLevel = 4;
        PlInputAnimation.moveLevel = 4;
        //���� ����
        if (playerInput.moveDir)
        {
            DirFix = 1f;
        }
        else
        {
            DirFix = -1f;
        }

        yield return new WaitForSeconds(0.5f);

        moveLevel = 0; // �������� 1
        yield return new WaitForSeconds(0.3f);
        //���� �� ����� ������
        if (playerInput.move != 0)
        {
            moveLevel = 2;
        }
        else if (playerInput.move == 0)
        {
            moveLevel = 0;
        }
        PlInputAnimation.DashAnimeDone();
    }
}
