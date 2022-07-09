using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSniperRifle : Perk
{
    public PerkSniperRifle()
    {
        playerAttackDir = 0;
        stopActionOnOff = false;
        shoot = true;
        shootDirTime = 0;
        granadeOn = false;
        stopActionKind = 0;
        reloading = false;
        shootDelay = 1.5f;
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
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //저격총은 수직으로 못쏜다
            //playerAttackDir = 2;
            //playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (Input.GetMouseButtonDown(1) && playerAttackDir != AttackDirection.Up && playerAttackDir != AttackDirection.Diagonal)
        {
            playerAttackDir = AttackDirection.Horizontal;
            playerAnimation.ShootReady((int)playerAttackDir);
        }

        if (playerMove.jump && playerInput.lookDown && playerAttackDir == AttackDirection.Default && playerAttackDir != AttackDirection.Down)
        {
            playerAttackDir = AttackDirection.Down;
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
        if (Input.GetMouseButton(0) && shoot && playerAttackDir > AttackDirection.Default && playerAttackDir < AttackDirection.Down && !playerMove.sit || Input.GetMouseButton(0) && shoot && playerAttackDir == 0 && playerMove.sit)
        {
            SmartCoroutine.Create(CoPlayerWidthShoot());
        }
    }

    public override void MoveAction()
    {

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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            stopActionKind = 2;
            stopActionTimeLimit = 0.3f;
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
}
