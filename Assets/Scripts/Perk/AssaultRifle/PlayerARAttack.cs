using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerARAttack : PlayerAttack
{

    bool modeChange;

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
        shootDelay = 0.13f;
        modeChange = true;
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
            //shootDelay = 0.13f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAttackDir = 2;
            //shootDelay = 0.13f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (Input.GetMouseButtonDown(1) && playerAttackDir != 2 && playerAttackDir != 1)
        {
            playerAttackDir = 3;
            //shootDelay = 0.13f;
            PlInputAnimation.ShootReady(playerAttackDir);
        }

        if (playerMove.jump && playerInput.lookDown && playerAttackDir == 0 && playerAttackDir != 4)
        {
            playerAttackDir = 4;
            //shootDelay = 0.13f;
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
        if (modeChange)
        {

            if (Input.GetMouseButton(0) && shoot)
            {
                shoot = false;
                StartCoroutine(PlayerWidthShoot());
            }
        }
        else
        {

            if (Input.GetMouseButtonDown(0) && shoot)
            {
                shoot = false;
                StartCoroutine(PlayerWidthShoot());
            }
        }


    }

    public override void stopAction()
    {
        if (!stopActionOnOff)
        {
            //????, ??????, ????????
            if (Input.GetKeyDown(KeyCode.R) && !reloading)
            {
                stopActionKind = 0;
                stopActionTimeLimit = 0.5f;
                reloading = true;
                StartCoroutine(reload());

            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                stopActionKind = 1;
                stopActionTimeLimit = 0.8f;
                StartCoroutine(stopActionTime());
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                //????, 3????
                if (modeChange)
                {
                    modeChange = false;
                    PlInputAnimation.StopAction(2);
                    shootDelay = 0.5f;
                }
                else
                {
                    modeChange = true;
                    PlInputAnimation.StopAction(2);
                    shootDelay = 0.13f;
                }

            }
        }

    }

    public override void granade()
    {
        //???????? ???????? ?????? ????
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

    IEnumerator PlayerWidthShoot() // ???????? ??
    {
        // PlInputAnimation.DamagedAnime();


        shoot = false;
        PlInputAnimation.Shoot(playerAttackDir);

        //?????????? ?????? ???? ?????????? ???????? ????


        yield return new WaitForSeconds(shootDelay);


        shoot = true;


    }

    IEnumerator stopActionTime() // ?????????? ???????? ???? ???? ????
    {

        stopActionOnOff = true;
        PlInputAnimation.StopAction(stopActionKind);
        yield return new WaitForSeconds(stopActionTimeLimit);
        PlInputAnimation.StopActionDone();

        stopActionOnOff = false;
    }

    IEnumerator reload() // ???????? ??????
    { // ar ????

        reloading = true;
        PlInputAnimation.StopAction(0);
        yield return new WaitForSeconds(1.8f);
        PlInputAnimation.StopActionDone();

        reloading = false;

    }
}