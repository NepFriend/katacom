using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkPistol : Perk
{
    private bool dual;

    public PerkPistol()
    {
        playerAttackDir = 0;
        stopActionOnOff = false;
        shoot = true;
        shootDirTime = 0;
        granadeOn = false;
        stopActionKind = 0;
        reloading = false;
        shootDelay = 0.3f;

        dual = false;
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
            //shootDelay = 0.13f;
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAttackDir = AttackDirection.Up;
            //shootDelay = 0.13f;
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (Input.GetMouseButtonDown(1) && playerAttackDir != AttackDirection.Up && playerAttackDir != AttackDirection.Diagonal && !dual)
        {

            Debug.Log("ready");

            playerAttackDir = AttackDirection.Horizontal;
            //shootDelay = 0.13f;
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (playerMove.jump && playerInput.lookDown && playerAttackDir == 0 && playerAttackDir != AttackDirection.Down)
        {
            playerAttackDir = AttackDirection.Down;
            //shootDelay = 0.13f;
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
        if (Input.GetMouseButtonDown(0) && shoot)
        {
            SmartCoroutine.Create(CoPlayerWidthShoot());
        }
    }

    public override void MoveAction()
    {
        if (stopActionOnOff) return;

        //속성변경
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            stopActionKind = 0;
            stopActionTimeLimit = 0.3f;

            dual = !dual;

            SmartCoroutine.Create(CoMoveActionTime());
        }
    }

    public override void StopAction()
    {
        if (stopActionOnOff) return;

        //장전, 백병전, 속성변경
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            stopActionKind = 0;
            stopActionTimeLimit = 0.5f;
            reloading = true;
            SmartCoroutine.Create(CoReload());

        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            stopActionKind = 1;
            stopActionTimeLimit = 0.8f;
            SmartCoroutine.Create(CoStopActionTime());
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

    public override void CalculateRecoil()
    {
        recoil = 0F;
    }


    protected override IEnumerator CoPlayerWidthShoot()
    {
        shoot = false;

        // playerAnimation.DamagedAnime();
        playerAnimation.Shoot((int)playerAttackDir);

        //쌍권총만 특수하게 슛 불값 반응
        playerAnimation.shoot = true;

        //공중에서의 샷건은 쏘고 특수모션을 준비해야 한다
        yield return new WaitForSeconds(shootDelay);

        playerAnimation.shoot = false;

        shoot = true;
    }

    protected override IEnumerator CoMoveActionTime() // 플레이어가 정지해야 하는 특수 액션
    {
        stopActionOnOff = true;

        playerAnimation.MoveAction(stopActionKind);
        yield return new WaitForSeconds(stopActionTimeLimit);
        playerAnimation.StopActionDone();

        stopActionOnOff = false;
    }
}
