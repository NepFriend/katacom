using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public enum ID
    {
        AssaultRifle1 = 10000,

        SniperRifle1 = 20000,
        SniperRifle2 = 20001
    }

    public ID id;
    public int slotIndex;

    public int CurrentAmmo { get; protected set; }
    public int SuppliedAmmo { get; protected set; }

    public WeaponData Data { get; protected set; } = new WeaponData();

    protected bool shotTrigger = false;
    protected bool reloadTrigger = false;

    protected float lastUse = -1F;

    public virtual bool CanUse => !shotTrigger;
    public virtual bool CanReload => CurrentAmmo != Data.magazineSize && Data.suppliedAmmo > 0 && !reloadTrigger;
    public bool IsReloading => reloadTrigger;


    public Weapon(int slotIndex, ID id)
    {
        this.slotIndex = slotIndex;
        this.id = id;
        ResetData();
    }


    public void ResetData()
    {
        Data = WeaponDataLoader.Instance.Get(id);//.CopyTo(Data);

        CurrentAmmo = Data.magazineSize;
        SuppliedAmmo = Data.suppliedAmmo;
    }

    public abstract void AttackLogic(GameObject self);
    public abstract void ReloadLogic(GameObject self);

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
