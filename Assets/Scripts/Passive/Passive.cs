using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{

}

public abstract class Skill
{
    protected GameObject caller;

    public Skill(GameObject caller)
    {
        this.caller = caller;
    }

    public abstract void Use();
}

public class Passive : System.IEquatable<Passive>
{
    protected GameObject caller;
    protected Status status;

    public bool IsOnGoing { get; protected set; }

    public Passive(GameObject caller, Status status)
    {
        this.caller = caller;
        this.status = status;
    }

    public void Initialize(GameObject caller, Status status)
    {
        this.caller = caller;
        this.status = status;
    }

    public static T Create<T>(GameObject target, Status status) where T : Passive, new()
    {
        T passive = new T();
        passive.Initialize(target, status);
        return passive;
    }

    public bool Equals(Passive other)
    {
        return this.GetType() == other.GetType();
    }

    public virtual void OnEquipped()
    {
        IsOnGoing = true;
    }
    public virtual void OnUnequipped()
    {
        IsOnGoing = false;
    }
}

public class HPPlusPassive : Passive
{
    public HPPlusPassive() : base(null, null) { }
    public HPPlusPassive(GameObject caller, Status status) : base(caller, status) { }

    public override void OnEquipped()
    {
        base.OnEquipped();
        Debug.Log($"{caller} >> HP Plus!");
    }

    public override void OnUnequipped()
    {
        base.OnUnequipped();
        Debug.Log($"{caller} >> HP Plus Done!");
    }
}

public class HPHighPassive : Passive
{
    public HPHighPassive() : base(null, null) { }
    public HPHighPassive(GameObject caller, Status status) : base(caller, status) { }

    public override void OnEquipped()
    {
        base.OnEquipped();
        Debug.Log($"{caller} >> HP High!");
    }

    public override void OnUnequipped()
    {
        base.OnUnequipped();
        Debug.Log($"{caller} >> HP High Done!");
    }
}
