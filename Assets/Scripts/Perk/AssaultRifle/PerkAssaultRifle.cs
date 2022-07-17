using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PerkAssaultRifle : Perk
{
    private bool modeChange;

    private SmartCoroutine shotCoroutine;

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
        bool burst = !modeChange && Input.GetMouseButtonDown(0);
        bool auto = modeChange && Input.GetMouseButton(0);

        Vector3 dir = Quaternion.AngleAxis(recoil, Vector3.forward) * shotPosition.forward;
        dir.x *= playerInput.moveDir ? 1 : -1;
        Debug.DrawRay(shotPosition.position, dir * 5F, Color.green);
        dir.y *= -1;
        Debug.DrawRay(shotPosition.position, dir * 5F, Color.green);

        if (!auto && !burst) return;

        var data = WeaponDataLoader.Instance.Get(WeaponData.ID.AssaultRifle);
        bool isAiming = playerAttackDir == AttackDirection.Horizontal;
        bool isBurst = !modeChange;
        bool isMoving = Mathf.Abs(playerInput.move) > Mathf.Epsilon;

        var recoilData = data.GetRecoilData(isBurst, isAiming, isMoving);

        recoil = Mathf.Clamp(recoil + recoilData.recoilIncPerSecond * Time.deltaTime, -recoilData.lowerAngle, recoilData.upperAngle);

        shootDelay = 1F / data.shotCountPerSecond;

        if (shotCoroutine == null) shotCoroutine = SmartCoroutine.Create(CoShot);
        if (shotCoroutine.IsRunning) return;

        shotCoroutine.Start();
        SmartCoroutine.Create(CoPlayerWidthShoot());

        IEnumerator CoShot()
        {
            float angle = Random.Range(-recoil, recoil);

            var bullet = PoolManager.Instance.Get("DummyBullet") as Bullet;
            bullet.transform.position = shotPosition.position;
            bullet.transform.rotation = Quaternion.Euler(0F, 0F, -angle);
            bullet.transform.localScale = new Vector3(playerInput.moveDir ? 1 : -1, 1F, 1F);
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * shotPosition.forward;
            dir.x *= playerInput.moveDir ? 1 : -1;
            bullet.Launch(dir);

            recoil += recoilData.recoilIncPerSecond * Time.deltaTime;

            yield return new WaitForSeconds(shootDelay);
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
            // 3점사
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

    public override void CalculateRecoil()
    {
        if (!shoot) return;
        if (recoil == 0F) return;

        var data = WeaponDataLoader.Instance.Get(WeaponData.ID.AssaultRifle);
        bool isAiming = playerAttackDir == AttackDirection.Horizontal;
        bool isBurst = !modeChange;
        bool isMoving = Mathf.Abs(playerInput.move) < Mathf.Epsilon;

        var recoilData = data.GetRecoilData(isBurst, isAiming, isMoving);

        recoil = Mathf.Max(0F, recoil - recoilData.recoilDecPerSecond * Time.deltaTime);
    }
}