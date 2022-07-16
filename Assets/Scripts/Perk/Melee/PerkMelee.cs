using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMelee : Perk
{
    bool shootAgain;

    //��� ü������ ����Ѱ� Ȯ��
    bool modeChange;
    bool modeFire;

    int rollAttacklevel;

    public PerkMelee()
    {
        playerAttackDir = 0;
        stopActionOnOff = false;
        shoot = true;
        shootAgain = true;
        shootDirTime = 0;
        granadeOn = false;
        stopActionKind = 0;
        reloading = false;
        shootDelay = 1f;
        modeChange = true;
        modeFire = true;

        rollAttacklevel = 0;
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

    // TODO: Need refactoring...
    public override void DoAttack()
    {
        if (!modeChange)
        {
            if (shoot && (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)))
            {
                shootDelay = 1f;
                SmartCoroutine.Create(CoPlayerModeAttack());
                modeFire = false;
                modeChange = true;
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!playerMove.run && playerMove.moveLevel != 4)
            {
                if (shoot)
                {
                    shootDelay = 1f;
                    SmartCoroutine.Create(CoPlayerWeakAttack());
                }
                else
                {
                    shootAgain = false;
                }
            }
            else if (playerMove.run)
            {
                if (shoot)
                {
                    SmartCoroutine.Create(CoPlayerWeakRunAttack());
                }
            }
            else if (playerMove.moveLevel == 4)
            {
                if (shoot)
                {
                    SmartCoroutine.Create(CoPlayerWeakRollAttack());
                }
            }

            Debug.Log(modeChange + "  " + modeFire);
        }

        if (Input.GetMouseButtonDown(1))
        {

            if (!playerMove.run && playerMove.moveLevel != 4)
            {
                if (!modeChange && modeFire)
                {
                    modeChange = true;
                    modeFire = false;

                    SmartCoroutine.Create(CoPlayerModeAttack());


                }
                else
                {
                    if (shoot)
                    {
                        shootDelay = 1f;
                        SmartCoroutine.Create(CoPlayerStrongAttack());
                    }
                    else
                    {
                        shootAgain = false;
                    }
                }


            }
            else if (playerMove.run)
            {
                if (shoot)
                {
                    SmartCoroutine.Create(CoPlayerStrongRunAttack());
                }
            }
            else if (playerMove.moveLevel == 4)
            {
                if (shoot)
                {
                    SmartCoroutine.Create(CoPlayerStrongRollAttack());
                }
            }


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
            stopActionTimeLimit = 1.7f;
            reloading = true;
            SmartCoroutine.Create(CoReload());

        }

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    stopActionKind = 1;
        //    stopActionTimeLimit = 0.8f;
        //    SmartCoroutine.Create(stopActionTime());
        //}

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //밀리 필살기 강력한 한방
            if (modeChange && modeFire)
            {
                modeChange = false;
                playerAnimation.StopAction(0);
                shootDelay = 0.5f;

                Debug.Log(modeChange + "  " + modeFire);
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

    public override void CalculateRecoil()
    {
        recoil = 0F;
    }


    private IEnumerator CoPlayerWeakAttack() // 플레이어 약공격
    {
        shoot = false;

        // playerAnimation.DamagedAnime();
        playerAnimation.Shoot(0);

        yield return new WaitForSeconds(shootDelay);

        if (shootAgain)
        {
            shoot = true;
            playerAnimation.ShootReady(0);
        }
        else
        {
            SmartCoroutine.Create(CoPlayerWeakAttack());
            shootAgain = true;
        }
    }

    private IEnumerator CoPlayerStrongAttack() // 플레이어 강공격
    {
        shoot = false;

        // playerAnimation.DamagedAnime();
        playerAnimation.Shoot(3);

        yield return new WaitForSeconds(shootDelay);

        if (shootAgain)
        {
            shoot = true;
            playerAnimation.ShootReady(0);
        }
        else
        {
            SmartCoroutine.Create(CoPlayerStrongAttack());
            shootAgain = true;
        }
    }

    private IEnumerator CoPlayerWeakRollAttack() // 플레이어 구르기약공격
    {
        shoot = false;
        playerAnimation.Shoot(1);

        yield return new WaitForSeconds(0.6f);

        shoot = true;
    }

    private IEnumerator CoPlayerStrongRollAttack() // 플레이어 구르기강공격
    {
        shoot = false;
        playerAnimation.Shoot(4);

        yield return new WaitForSeconds(1.3f);

        shoot = true;
    }

    private IEnumerator CoPlayerWeakRunAttack() // 플레이어 달리기 약공격
    {
        shoot = false;
        playerAnimation.Shoot(2);

        yield return new WaitForSeconds(0.9f);

        shoot = true;
    }

    private IEnumerator CoPlayerStrongRunAttack() // 플레이어 달리기 강공격
    {
        shoot = false;
        playerAnimation.Shoot(5);

        yield return new WaitForSeconds(1.4f);

        shoot = true;
    }

    private IEnumerator CoPlayerJumpAttack() // 플레이어 점프공격
    {
        shoot = false;
        playerAnimation.Shoot(6);

        yield return new WaitForSeconds(shootDelay);
    }

    private IEnumerator CoPlayerModeAttack() // 플레이어 달리기 약공격
    {
        shoot = false;

        modeFire = false;
        playerAnimation.Shoot(7);

        yield return new WaitForSeconds(1.1f);

        shoot = true;
    }
}
