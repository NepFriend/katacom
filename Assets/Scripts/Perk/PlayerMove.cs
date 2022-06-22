using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //자식 가져오기
    //GameObject Player;
    //부모의 키입력인식
    public PlayerAnimation PlInputAnimation;

    //플레이어 키 누른거랑 자신의 리지드바디
    public PlayerInput playerInput;
    public Rigidbody2D playerRigidBody;

    //이동속도
    public float moveSpeed = 10f;

    //자신의 트랜스폼
    public Transform tr;

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

    public bool stop;

    public bool sit;

    public bool run;

    public bool jump;

    public float DirFix;

    public float shift_clock;

    // Start is called before the first frame update
    //public void Start()
    //{
     
    //}



    //public void Update()
    //{

    

    //}

    public virtual void PlayerHorizontalMove()
    {
       

    }

    public void DamageMotionStart()
    {

    }


    public virtual void Landing()
    {

    }







    //private void OnTriggerEnter2D(Collider2D collision)
    //{

       
    //}

    





}
