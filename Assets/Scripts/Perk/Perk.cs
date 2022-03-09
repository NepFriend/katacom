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



public abstract class Perk
{
    protected abstract Dictionary<Weapon.ID, Weapon> weapons { get; }
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

    public void Update(float dt)
    {
        foreach (var weapon in weapons)
        {
            weapon.Value.OnUpdate(dt);
        }
    }
}


public class PerkAssertRifle : Perk
{
    protected override Dictionary<Weapon.ID, Weapon> weapons { get; } = new Dictionary<Weapon.ID, Weapon>()
    {
        { Weapon.ID.AssertRifle1, new WeaponGun(Weapon.ID.AssertRifle1) },
        { Weapon.ID.AssertRifle2, new WeaponGun(Weapon.ID.AssertRifle2) },
        { Weapon.ID.AssertRifle3, new WeaponGun(Weapon.ID.AssertRifle3) },
    };

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
    protected override Dictionary<Weapon.ID, Weapon> weapons { get; } = new Dictionary<Weapon.ID, Weapon>()
    {
        { Weapon.ID.SniperRifle1, new WeaponGun(Weapon.ID.SniperRifle1) }
    };

    public override void OnEquiped()
    {
        Debug.Log("Player\'s Perk is SniperRifle");
    }

    public override void OnUnequiped()
    {
        Debug.Log("Player\'s Perk is NOT SniperRifle");
    }
}
