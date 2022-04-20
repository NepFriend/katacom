using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// ����, �������, ����, ��ź��, ������, ������
// �� ��ũ�� �нú� ��ų ����
// �ֹ���, ��������, ��������, ��ô����, Ư������, ȸ�Ǳ�

// �Ѿ� �� ���� �ϴ� ������ �н�Ʈ ����
// ���Ǹ� �� ��.

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


public class PerkAssertRifle : Perk
{
    protected override Dictionary<Weapon.ID, Weapon> weapons { get; } = new Dictionary<Weapon.ID, Weapon>()
    {
        { Weapon.ID.AssertRifle1, new WeaponGunTest(0, Weapon.ID.AssertRifle1) },
        { Weapon.ID.AssertRifle2, new WeaponGunTest(1, Weapon.ID.AssertRifle2) },
        { Weapon.ID.AssertRifle3, new WeaponGunTest(2, Weapon.ID.AssertRifle3) }
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
        { Weapon.ID.SniperRifle1, new WeaponGun(0, Weapon.ID.SniperRifle1) }
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
