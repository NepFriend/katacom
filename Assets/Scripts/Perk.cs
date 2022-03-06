using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// 소총, 기관단총, 권총, 산탄총, 저격총, 근접류
// 각 퍼크는 패시브 스킬 있음
// 주무기, 보조무기, 근접무기, 투척무기, 특수무기, 회피기


public interface IUpdate
{
    void OnFixedUpdate(float fdt);
    void OnUpdate(float dt);
    void OnLateUpdate(float dt);
}

public abstract class Weapon : IUpdate
{
    public enum ID
    {
        AssertRifle1 = 10000,
        SniperRifle1 = 20000
    }

    public ID WeaponID;

    public int CurrentAmmo { get; protected set; }

    public WeaponData InitialData { get; protected set; }
    public WeaponData Data { get; set; }

    protected float delayCounter = 0F;
    public virtual bool CanUse => delayCounter <= 0F;
    public virtual bool CanReload => Data.suppliedAmmo > 0;


    public void Reset()
    {
        InitialData.CopyTo(Data);
    }

    public abstract void Attack(GameObject self);
    public abstract void Reload(GameObject self);

    public virtual void OnFixedUpdate(float fdt)
    {
    }

    public virtual void OnUpdate(float dt)
    {
        delayCounter = Mathf.Max(delayCounter - dt, 0F);
    }

    public virtual void OnLateUpdate(float dt)
    {
    }
}

public class WeaponData
{
    public int magazineSize;
    public int suppliedAmmo;

    // 한 번에 발사 가능한 탄수
    public int shotCount;

    public Vector2 damageRange;

    public float shotDelay;
    public float reloadDelay;

    public void CopyTo(WeaponData data)
    {
        data.magazineSize = this.magazineSize;
        data.suppliedAmmo = this.suppliedAmmo;

        data.shotCount = this.shotCount;

        data.damageRange = this.damageRange;

        data.shotDelay = this.shotDelay;
        data.reloadDelay = this.reloadDelay;
    }
}

public class WeaponGun : Weapon
{
    public override void Attack(GameObject self)
    {
        if (!CanUse) return;

        if (CurrentAmmo == 0)
        {
            if (!CanReload) return;
            Reload(self);
            return;
        }

        Debug.Log("Shot!");
        CurrentAmmo = Mathf.Max(CurrentAmmo - Data.shotCount, 0);
        delayCounter = Data.shotDelay;
    }

    public override async void Reload(GameObject self)
    {
        Debug.Log("Reloading...");
        await Task.Delay((int)(Data.reloadDelay * 1000));
        int newMag = Data.suppliedAmmo - Data.magazineSize < 0 ? Data.suppliedAmmo : Data.magazineSize;
        CurrentAmmo = newMag;
        Data.suppliedAmmo = Mathf.Max(Data.suppliedAmmo - Data.magazineSize, 0);
        Debug.Log("Done Reloading");
    }
}

public abstract class Perk
{
    protected Dictionary<Weapon.ID, Weapon> weapons;
    public Weapon CurrentWeapon { get; protected set; }

    //protected Passive[] passives;

    public abstract void OnEquiped();
    public abstract void OnUnequiped();

    public void EquipWeapon(Weapon.ID id)
    {
        if (!weapons.TryGetValue(id, out var weapon))
        {
            return;
        }

        CurrentWeapon = weapon;
    }

    public void Use(GameObject self)
    {
        CurrentWeapon?.Attack(self);
    }
}


public class PerkAssertRifle : Perk
{
    public override void OnEquiped()
    {
        Debug.Log("Player\'s Perk is AssertRifle");
    }

    public override void OnUnequiped()
    {
        Debug.Log("Player\'s Perk is NOT AssertRifle");
    }
}

public class PerkSniperRifle : Perk
{
    public override void OnEquiped()
    {
        Debug.Log("Player\'s Perk is SniperRifle");
    }

    public override void OnUnequiped()
    {
        Debug.Log("Player\'s Perk is NOT SniperRifle");
    }
}
