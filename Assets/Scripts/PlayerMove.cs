using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //자식 가져오기
    GameObject Player;
    //부모의 키입력인식
    PlayerAnimation PlInputAnimation;

    //플레이어 키 누른거랑 자신의 리지드바디
    PlayerInput playerInput;
    Rigidbody2D playerRigidBody;

    //이동속도
    public float moveSpeed = 10f;

    //자신의 트랜스폼
    Transform tr;

    //메인카메라
    public Camera mainCamera;

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

    bool stop;

    bool DirFix;

    // Start is called before the first frame update
    public void Start()
    {
        Player = transform.gameObject;
        PlInputAnimation = GetComponent<PlayerAnimation>();

        playerInput = GetComponent<PlayerInput>();
        playerRigidBody = GetComponent<Rigidbody2D>();

        tr = GetComponent<Transform>();
        
        moveLevel = 0;

        stop = false;


        DirFix = true;
    }

  

    public void FixedUpdate()
    {

        // 정지 도중이 아닐경우
        if (!stop)
        {
           
            PlayerHorizontalMove();

        }

       
      

    }

    private void PlayerHorizontalMove()
    {

      

        if (playerInput.move != 0)
        {
            moveLevel = 2;
        }
        else if (playerInput.move == 0 && moveLevel < 3)
        {
            moveLevel = 0;
        }

        //구르기
        if (Input.GetKey(KeyCode.LeftShift) && moveLevel < 4)
        {
            moveLevel = 4;
            StartCoroutine(Roll());
        }

        //점프
        if (Input.GetKey(KeyCode.Space) && moveLevel < 4)
        {
            moveLevel = 5;
            StartCoroutine(JumpPlayer());

        }

       
        if (moveLevel == 1)
        { //기어가기
            Vector2 moveDistance = (playerInput.move * tr.right) * (moveSpeed / 3f) * Time.deltaTime;
            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
        }
        else if (moveLevel == 2 || moveLevel == 5)
        { //걷기 / 점프
            Vector2 moveDistance = (playerInput.move * tr.right) * moveSpeed * Time.deltaTime;
            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
        }
        else if (moveLevel == 3)
        { //달리기

            if (playerInput.moveDir)
            {
                Vector2 moveDistance = (1 * tr.right) * (moveSpeed + 30f) * Time.deltaTime;
                playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
            }
            else
            {
                Vector2 moveDistance = (-1 * tr.right) * (moveSpeed + 30f) * Time.deltaTime;
                playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
            }
           
        }
        else if (moveLevel == 4)
        { //구르기
            Vector2 moveDistance = (1 * tr.right) * (moveSpeed + 100f) * Time.deltaTime;
            playerRigidBody.MovePosition(playerRigidBody.position + moveDistance);
        }



    }

    public void DamageMotionStart()
    { 
       
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        //만약 바닥에 붙었을 경우
        if (collision.transform.tag == "Floor")
        {
            //PlInputAnimation.Landing();


        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.transform.tag == "ArmyBullet" || collision.transform.tag == "TerrorBullet")
        { //적 공격에 닿았을 경우


        }
    }





    IEnumerator PlayerDamaged() // 플레이어 대미지 입었을 시 행동목록
    {
       // PlInputAnimation.DamagedAnime();
      

        yield return new WaitForSeconds(0.5f);

    }


    IEnumerator JumpPlayer() // 플레이어 점프 했을 시 움직임 일람
    {
       // PlInputAnimation.jumpPl();

        yield return new WaitForSeconds(0.24f);

    }


    IEnumerator Roll() // 플레이어 구르기 했을 시 움직임 일람
    {
        PlInputAnimation.DashAnime();

        yield return new WaitForSeconds(0.8f);

        if (playerInput.move != 0)
        {
            moveLevel = 2;
        }
        else if (playerInput.move == 0 && moveLevel < 3)
        {
            moveLevel = 0;
        }
    }


    IEnumerator Bomb() // 플레이어 폭탄썼을 시 움직임 일람   이것만큼은 커플링 상관안하고 코딩
    {
       // PlInputAnimation.DashAnime();

        yield return new WaitForSeconds(0.2f);
       // PlInputAnimation.DashAnimeDone();
    }
}
