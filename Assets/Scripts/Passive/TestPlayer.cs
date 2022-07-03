using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Status status = new Status();

    private List<Passive> passives = new List<Passive>();

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


    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.Buff.Register(Buff.Create<BurnBuff>(gameObject, status, 5F, 1F));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddPassive(new HPHighPassive(gameObject, status));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddPassive(new HPPlusPassive(gameObject, status));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RemovePassive<HPHighPassive>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RemovePassive<HPPlusPassive>();
        }
    }
}
