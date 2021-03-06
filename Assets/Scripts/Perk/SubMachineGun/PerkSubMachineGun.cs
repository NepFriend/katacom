using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSubMachineGun : Perk
{
    public PerkSubMachineGun()
    {
        playerAttackDir = 0;
        stopActionOnOff = false;
        shoot = true;
        shootDirTime = 0;
        granadeOn = false;
        stopActionKind = 0;
        reloading = false;
        shootDelay = 0.1f;
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

        if (Input.GetMouseButtonDown(1) && playerAttackDir != AttackDirection.Up && playerAttackDir != AttackDirection.Diagonal)
        {
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
        if (Input.GetMouseButton(0) && shoot)
        {
            shoot = false;
            SmartCoroutine.Create(CoPlayerWidthShoot());
        }
    }

    public override void MoveAction()
    {

    }

    public override void StopAction()
    {
        if (stopActionOnOff) return;

        //??????, ?????????, ????????????
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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            stopActionKind = 2;
            stopActionTimeLimit = 1f;
            SmartCoroutine.Create(CoStopControllActionTime());
        }
    }

    public override void ThrowGrenade()
    {
        //???????????? ???????????? ????????? ??????
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


    protected IEnumerator CoStopControllActionTime() // smg?????? ????????? ??? ????????? ?????? ?????????
    {
        stopActionOnOff = true;

        playerAnimation.StopAction(stopActionKind);
        yield return new WaitForSeconds(1f);
        playerAnimation.StopAction(3);
        playerAnimation.StopActionDone();

        stopActionOnOff = false;
    }

    protected override IEnumerator CoReload()
    {
        reloading = true;

        playerAnimation.StopAction(0);
        yield return new WaitForSeconds(1.7f);
        playerAnimation.StopActionDone();

        reloading = false;
    }
}
