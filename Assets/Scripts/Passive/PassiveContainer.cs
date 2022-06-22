using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveContainer
{
    private List<Passive> passives = new List<Passive>();

    public void Load()
    {
        for (int i = 0; i < passives.Count; ++i)
        {
            var passive = passives[i];
            if (!passive.IsOnGoing) continue;
            passive.OnUnequipped();
        }
        passives.Clear();

        // Load in data

        var data = new List<Passive>();
        for (int i = 0; i < data.Count; ++i)
        {
            passives.Add(data[i]);
            data[i].OnEquipped();
        }
    }
    public void Save()
    {
        for (int i = 0; i < passives.Count; ++i)
        {
            // Save
        }
    }

    public void AddPassive(Passive p)
    {
        passives.Add(p);
        p.OnEquipped();
    }

    public void RemovePassive(Passive p)
    {
        p.OnUnequipped();
        passives.Remove(p);
    }
    public void RemovePassive<T>() where T : Passive
    {
        bool Find(Passive a)
        {
            return a is T;
        }

        int index = passives.FindIndex(Find);
        passives[index].OnUnequipped();
        passives[index] = passives[passives.Count - 1];
        passives.RemoveAt(passives.Count - 1);
    }

    public Passive GetPassive<T>() where T : Passive
    {
        bool Find(Passive a)
        {
            return a is T;
        }
        return passives.Find(Find);
    }
    public List<Passive> GetPassives<T>() where T : Passive
    {
        bool Find(Passive a)
        {
            return a is T;
        }
        return passives.FindAll(Find);
    }
}