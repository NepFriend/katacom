using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


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
    public void EquipWeapon(int slot)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.Value.slotIndex != slot) continue;
            CurrentWeapon = weapon.Value;
            return;
        }
    }

    public void Use(GameObject self)
    {
        CurrentWeapon?.AttackLogic(self);
    }

    public void Update(float dt)
    {
        if (ReferenceEquals(CurrentWeapon, null)) EquipWeapon(0);
        foreach (var weapon in weapons)
        {
            weapon.Value.OnUpdate(dt);
        }
    }
}


public class PerkAssaultRifle : Perk
{
    protected override Dictionary<Weapon.ID, Weapon> weapons { get; } = new Dictionary<Weapon.ID, Weapon>()
    {
        { Weapon.ID.AssaultRifle1, new TestGun(0, Weapon.ID.AssaultRifle1) }
    };

    public override void OnEquiped()
    {
    }

    public override void OnUnequiped()
    {
    }
}

public class PerkSniperRifle : Perk
{
    protected override Dictionary<Weapon.ID, Weapon> weapons { get; } = new Dictionary<Weapon.ID, Weapon>()
    {
        { Weapon.ID.SniperRifle1, new WeaponGun(0, Weapon.ID.SniperRifle1) }
    };

    public override void OnEquiped()
    {

    }

    public override void OnUnequiped()
    {

    }
}
