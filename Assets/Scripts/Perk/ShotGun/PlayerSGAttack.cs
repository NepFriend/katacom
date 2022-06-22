using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSGAttack : PlayerAttack
{

    bool shootAgain;
    bool reloadAgain;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();

        playerInput = GetComponent<PlayerInput>();

        PlInputAnimation = GetComponent<PlayerAnimation>();

        playerAttackDir = 0;
        stopActionOnOff = false;
        shoot = true;
        shootDirTime = 0;
        granadeOn = false;
        stopActionKind = 0;
        reloading = false;

        //퍼크마다 다 다름
        shootDelay = 0.95f;

        shootAgain = true;
        reloadAgain = true;
    }

    // Update is called once per frame
    void Update()
    {

        AttackDir();
        stopAction();
        attack();
        granade();
    }
    public override void AttackDir()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerAttackDir != 2)
        {
            playerAttackDir = 1;
            shootDelay = 0.95f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAttackDir = 2;
            shootDelay = 0.95f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (Input.GetMouseButtonDown(1) && playerAttackDir != 2 && playerAttackDir != 1)
        {
            playerAttackDir = 3;
            shootDelay = 0.95f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (playerMove.jump && playerInput.lookDown && playerAttackDir == 0 && playerAttackDir != 4)
        {
            playerAttackDir = 4;
            shootDelay = 0.3f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (playerAttackDir > 0)
        {
            shootDirTime++;
        }

        if (Input.GetKeyUp(KeyCode.Q) && playerAttackDir == 1 || Input.GetKeyUp(KeyCode.W) && playerAttackDir == 2 || Input.GetMouseButtonUp(1) && playerAttackDir == 3 || playerAttackDir == 4 && !playerMove.jump || playerAttackDir == 4 && !playerInput.lookDown)
        {
            playerAttackDir = 0;
            shootDirTime = 0;
            PlInputAnimation.ShootReady(playerAttackDir);
        }


    }

    public override void attack()
    {
        if (Input.GetMouseButton(0))
        {

            if (reloadAgain)
            {
                if (shoot)
                {

                    //Debug.Log("shoot");
                    StartCoroutine(PlayerWidthShoot());
                }
            }
            else
            {
                shootAgain = false;
            }

           
            //if (!reloading)
            //{
            //    Debug.Log("shoot");
            //    StartCoroutine(PlayerWidthShoot());
            //}
        }
    }

    public override void stopAction()
    {
        if (!stopActionOnOff)
        {
            //장전, 백병전, 속성변경
            if (Input.GetKeyDown(KeyCode.R))
            {
               
               
                if (!reloading)
                {
                   
                    stopActionKind = 0;
                    stopActionTimeLimit = 0.5f;
                    StartCoroutine(reload());
                }
                else
                {

                }

            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                stopActionKind = 1;
                stopActionTimeLimit = 0.8f;
                StartCoroutine(stopActionTime());
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                stopActionKind = 2;
                stopActionTimeLimit = 0.3f;
                StartCoroutine(stopActionTime());
            }
        }

    }

    public override void granade()
    {
        //수류탄만 특수하게 여기다 표시
        if (Input.GetKeyDown(KeyCode.G) && playerAttackDir == 0 && !stopActionOnOff && !granadeOn)
        {
            granadeOn = true;
            PlInputAnimation.granadeStart();
        }

        if (Input.GetKeyUp(KeyCode.G) && granadeOn)
        {
            granadeOn = false;
            PlInputAnimation.granadeEnd();
        }
    }

    IEnumerator PlayerWidthShoot() // 플레이어 슛
    {
        // PlInputAnimation.DamagedAnime();

        shoot = false;
        //샷건만 특이하게 샷 불값 밖에서 계산 후 코루틴
        PlInputAnimation.Shoot(playerAttackDir);

        //공중에서의 샷건은 쏘고 특수모션을 준비해야 한다


        yield return new WaitForSeconds(shootDelay);

        PlInputAnimation.ShootDone();
     
        if (reloadAgain)
        {
            shoot = true;
          
        }
        else
        {
            shoot = true;
            StartCoroutine(reload());
            reloading = false;
        }


    }

    IEnumerator stopActionTime() // 플레이어가 정지해야 하는 특수 액션
    {

        stopActionOnOff = true;
        PlInputAnimation.StopAction(stopActionKind);
        yield return new WaitForSeconds(stopActionTimeLimit);
        PlInputAnimation.StopActionDone();

        stopActionOnOff = false;
    }

    IEnumerator reload() // 플레이어 재장전
    { // 샷건 기준

        //리로딩 불값이 샷건만 밖으로
        reloading = true;
        PlInputAnimation.StopAction(0);
        yield return new WaitForSeconds(0.5f);
        PlInputAnimation.StopActionDone();

       

        if (!shootAgain)
        {
            shoot = true;
            StartCoroutine(PlayerWidthShoot());
        }
        else
        {
            reloading = false;
            StartCoroutine(reload());
        }
    }
}