using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PerkAssaultRifle : Perk
{
    private bool modeChange;

    public PerkAssaultRifle()
    {
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

        if (playerMove.jump && playerInput.lookDown && playerAttackDir == AttackDirection.Default && playerAttackDir != AttackDirection.Down)
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
        if (!shoot) return;

        bool burst = !modeChange && Input.GetMouseButtonDown(0);
        bool auto = modeChange && Input.GetMouseButton(0);

        if (auto || burst)
        {
            SmartCoroutine.Create(CoPlayerWidthShoot());
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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //연사, 3점사
            if (modeChange)
            {
                modeChange = false;
                playerAnimation.StopAction(2);
                shootDelay = 0.5f;
            }
            else
            {
                modeChange = true;
                playerAnimation.StopAction(2);
                shootDelay = 0.13f;
            }

        }
    }

    public override void MoveAction()
    {
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