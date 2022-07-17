using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Perk
{
    public static Perk Create(PerkID id)
    {
        switch (id)
        {
            case PerkID.AssaultRifle:
                return new PerkAssaultRifle();
        }

        return null;
    }

    public enum PerkID
    {
        AssaultRifle,
        SubmachineGun
    }

    public enum AttackDirection
    {
        Default,
        Diagonal,
        Up,
        Horizontal,
        Down
    }

    public GameObject Target { get; set; }


    protected PlayerInput playerInput;
    protected PlayerAnimation playerAnimation;
    protected PlayerMove playerMove;

    protected Transform shotPosition;
    public void SetShotPosition(Transform shotPosition)
    {
        this.shotPosition = shotPosition;
    }

    //사격 방향
    // 0 기본사격, 1 대각선, 2 위, 3 가로조준, 4 아래 보기
    protected AttackDirection playerAttackDir;
    public AttackDirection PlayerAttackDir => playerAttackDir;

    protected bool shoot;

    protected bool stopActionOnOff;

    protected bool reloading;

    protected bool granadeOn;

    //1장전, 2백병전, 3속성변경
    protected int stopActionKind;
    protected float stopActionTimeLimit;

    protected float shootDelay;
    protected float shootDirTime;

    protected float recoil = 0F;


    public abstract void OnEquiped();
    public abstract void OnUnequiped();

    public void Update(float dt)
    {
        SetAttackDirection();
        DoAttack();

        StopAction();
        MoveAction();

        ThrowGrenade();

        CalculateRecoil();
    }


    public abstract void SetAttackDirection();

    public abstract void DoAttack();

    public abstract void StopAction();

    public abstract void MoveAction();

    public abstract void ThrowGrenade();

    public abstract void CalculateRecoil();


    protected virtual IEnumerator CoPlayerWidthShoot()
    {
        shoot = false;

        // playerAnimation.DamagedAnime();
        playerAnimation.Shoot((int)playerAttackDir);

        //공중에서의 샷건은 쏘고 특수모션을 준비해야 한다
        yield return new WaitForSeconds(shootDelay);

        shoot = true;
    }

    protected virtual IEnumerator CoStopActionTime()
    {
        stopActionOnOff = true;

        playerAnimation.StopAction(stopActionKind);
        yield return new WaitForSeconds(stopActionTimeLimit);
        playerAnimation.StopActionDone();

        stopActionOnOff = false;
    }

    protected virtual IEnumerator CoMoveActionTime()
    {
        yield break;
    }

    protected virtual IEnumerator CoReload()
    {
        reloading = true;

        playerAnimation.StopAction(0);
        yield return new WaitForSeconds(1.8f);
        playerAnimation.StopActionDone();

        reloading = false;
    }
}
