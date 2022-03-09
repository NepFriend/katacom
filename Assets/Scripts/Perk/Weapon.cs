using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : IUpdate
{
    public enum ID
    {
        AssertRifle1 = 10000,
        AssertRifle2 = 10001,
        AssertRifle3 = 10002,

        SniperRifle1 = 20000,
        SniperRifle2 = 20001
    }

    public ID id;

    public int CurrentAmmo { get; protected set; }

    public WeaponData Data { get; protected set; } = new WeaponData();

    protected bool shotTrigger = false;
    protected bool reloadTrigger = false;
    public virtual bool CanUse => !shotTrigger;
    public virtual bool CanReload => CurrentAmmo != Data.magazineSize && Data.suppliedAmmo > 0 && !reloadTrigger;


    public Weapon(ID id)
    {
        this.id = id;
        ResetData();
    }


    public void ResetData()
    {
        WeaponDataLoader.Instance.Get(id).CopyTo(Data);
        CurrentAmmo = Data.magazineSize;
    }

    public abstract void Attack(GameObject self);
    public abstract void Reload(GameObject self);

    public virtual void OnFixedUpdate(float fdt)
    {
    }

    public virtual void OnUpdate(float dt)
    {
    }

    public virtual void OnLateUpdate(float dt)
    {
    }
}
