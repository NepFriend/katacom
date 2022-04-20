using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkHandler
{
    private List<Perk> perkPool = new List<Perk>();
    public Perk Current { get; private set; }

    public void AddPerk(params Perk[] perks)
    {
        perkPool.AddRange(perks);
    }


    public void Equip(int index)
    {
        if (perkPool.Count <= index) return;
        Current?.OnUnequiped();
        Current = perkPool[index];
        Current?.OnEquiped();
    }

    public void Unequip()
    {
        Current?.OnUnequiped();
        Current = null;
    }
}
