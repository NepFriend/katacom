using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveHandler
{
    private List<Passive> passives = new List<Passive>();
    private List<Buff> buffs = new List<Buff>();

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
            var converted = data[i] as Buff;
            if (converted != null) AddBuff(converted);
            else AddPassive(data[i]);
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

    public void AddBuff(Buff b)
    {
        buffs.Add(b);
        b.OnEquipped();
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

    public void RemoveBuff(Buff b)
    {
        b.OnUnequipped();
        buffs.Remove(b);
    }
    public void RemoveBuff<T>() where T : Buff
    {
        bool Find(Buff a)
        {
            return a is T;
        }

        int index = buffs.FindIndex(Find);
        buffs[index].OnUnequipped();
        buffs[index] = buffs[buffs.Count - 1];
        buffs.RemoveAt(buffs.Count - 1);
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
    public List<Passive> GetPassives()
    {
        return passives;
    }

    public Buff GetBuff<T>() where T : Buff
    {
        bool Find(Buff a)
        {
            return a is T;
        }
        return buffs.Find(Find);
    }
    public List<Buff> GetBuffs<T>() where T : Buff
    {
        bool Find(Buff a)
        {
            return a is T;
        }
        return buffs.FindAll(Find);
    }
    public List<Buff> GetBuffs()
    {
        return buffs;
    }



    public void Update(float dt)
    {
        for (int i = 0; i < buffs.Count; ++i)
        {
            if (!buffs[i].Update(dt)) continue;

            buffs[i].OnUnequipped();
            buffs.RemoveAt(i--);
        }
    }
}