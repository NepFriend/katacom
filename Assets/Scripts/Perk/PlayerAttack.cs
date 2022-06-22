using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInput playerInput;

    public PlayerAnimation PlInputAnimation;

    public PlayerMove playerMove;

    //사격 방향
    // 0 기본사격, 1 대각선, 2 위, 3 가로조준, 4 아래 보기
    public int playerAttackDir;

    public bool shoot;

    public bool stopActionOnOff;

    public bool reloading;

    public bool granadeOn;

    //1장전, 2백병전, 3속성변경
    public int stopActionKind;

    public float stopActionTimeLimit;

    public float shootDelay;

    public float shootDirTime;

    //// Start is called before the first frame update
    //void Start()
    //{
     
    //}

    //// Update is called once per frame
    //void Update()
    //{
      


    //}

    public virtual void AttackDir()
    {
      

    }

    public virtual void attack()
    {
      
    }

    public virtual void stopAction()
    {
     
      
    }

    public virtual void moveAction()
    {


    }

    public virtual void granade()
    {
      
    }

}
