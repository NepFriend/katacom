using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkShotgun : Perk
{
    private bool shootAgain;
    private bool reloadAgain;

    public PerkShotgun()
    {
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

    public override void OnEquiped()
    {
        playerMove = Target.GetComponent<PlayerMove>();
        playerInput = Target.GetComponent<PlayerInput>();
        playerAnimation = Target.GetComponent<PlayerAnimation>();
    }

    public override void OnUnequiped()
    {

    }


    public override void SetAttackDirection()
    {
        if (Input.GetKeyDown(KeyCode.Q) && playerAttackDir != AttackDirection.Up)
        {
            playerAttackDir = AttackDirection.Diagonal;
            shootDelay = 0.95f;
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAttackDir = AttackDirection.Up;
            shootDelay = 0.95f;
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (Input.GetMouseButtonDown(1) && playerAttackDir != AttackDirection.Up && playerAttackDir != AttackDirection.Diagonal)
        {
            playerAttackDir = AttackDirection.Horizontal;
            shootDelay = 0.95f;
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (playerMove.jump && playerInput.lookDown && playerAttackDir == 0 && playerAttackDir != AttackDirection.Down)
        {
            playerAttackDir = AttackDirection.Down;
            shootDelay = 0.3f;
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (playerAttackDir > 0)
        {
            shootDirTime++;
        }

        if (Input.GetKeyUp(KeyCode.Q) && playerAttackDir == AttackDirection.Diagonal || Input.GetKeyUp(KeyCode.W) && playerAttackDir == AttackDirection.Up || Input.GetMouseButtonUp(1) && playerAttackDir == AttackDirection.Horizontal || playerAttackDir == AttackDirection.Down && !playerMove.jump || playerAttackDir == AttackDirection.Down && !playerInput.lookDown)
        {
            playerAttackDir = 0;
            shootDirTime = 0;
            playerAnimation.ShootReady((int)playerAttackDir);
        }
    }

    public override void DoAttack()
    {
        if (Input.GetMouseButton(0))
        {

            if (reloadAgain)
            {
                if (shoot)
                {

                    //Debug.Log("shoot");
                    SmartCoroutine.Create(CoPlayerWidthShoot());
                }
            }
            else
            {
                shootAgain = false;
            }


            //if (!reloading)
            //{
            //    Debug.Log("shoot");
            //    SmartCoroutine.Create(PlayerWidthShoot());
            //}
        }
    }

    public override void MoveAction()
    {

    }

    public override void StopAction()
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
                    SmartCoroutine.Create(CoReload());
                }
                else
                {

                }

            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                stopActionKind = 1;
                stopActionTimeLimit = 0.8f;
                SmartCoroutine.Create(CoStopActionTime());
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                stopActionKind = 2;
                stopActionTimeLimit = 0.3f;
                SmartCoroutine.Create(CoStopActionTime());
            }
        }
    }

    public override void ThrowGrenade()
    {
        //수류탄만 특수하게 여기다 표시
        if (Input.GetKeyDown(KeyCode.G) && playerAttackDir == 0 && !stopActionOnOff && !granadeOn)
        {
            granadeOn = true;
            playerAnimation.granadeStart();
        }

        if (Input.GetKeyUp(KeyCode.G) && granadeOn)
        {
            granadeOn = false;
            playerAnimation.granadeEnd();
        }
    }


    protected override IEnumerator CoPlayerWidthShoot()
    {
        shoot = false;

        //샷건만 특이하게 샷 불값 밖에서 계산 후 코루틴
        // playerAnimation.DamagedAnime();
        playerAnimation.Shoot((int)playerAttackDir);

        //공중에서의 샷건은 쏘고 특수모션을 준비해야 한다
        yield return new WaitForSeconds(shootDelay);

        playerAnimation.ShootDone();

        if (reloadAgain)
        {
            shoot = true;

        }
        else
        {
            shoot = true;
            SmartCoroutine.Create(CoReload());
            reloading = false;
        }
    }

    protected override IEnumerator CoReload()
    {
        // 샷건 기준

        //리로딩 불값이 샷건만 밖으로
        reloading = true;

        playerAnimation.StopAction(0);
        yield return new WaitForSeconds(0.5f);
        playerAnimation.StopActionDone();



        if (!shootAgain)
        {
            shoot = true;
            SmartCoroutine.Create(CoPlayerWidthShoot());
        }
        else
        {
            reloading = false;
            SmartCoroutine.Create(CoReload());
        }
    }
}
