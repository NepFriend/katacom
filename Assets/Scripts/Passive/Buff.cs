using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : Passive
{
    protected float duration = 0F;
    protected float updateInterval = -1F;

    private float intervalCounter = 0F;

    public Buff() : base(null, null) { }
    public Buff(GameObject target, Status status, float duration, float updateInterval = -1F)
        : base(target, status)
    {
        Initialize(target, status, duration, updateInterval);
    }

    public void Initialize(GameObject target, Status status, float duration, float updateInterval = -1F)
    {
        this.caller = target;
        this.status = status;
        this.duration = duration;
        this.updateInterval = updateInterval;
        this.intervalCounter = updateInterval;
    }

    public static T Create<T>(GameObject target, Status status, float duration, float updateInterval = -1F) where T : Buff, new()
    {
        T buff = new T();
        buff.Initialize(target, status, duration, updateInterval);
        return buff;
    }

    public abstract void OnUpdate();

    public bool Update(float dt)
    {
        if (duration <= 0F) return true;

        if (updateInterval > 0F && intervalCounter >= updateInterval)
        {
            OnUpdate();
            intervalCounter -= updateInterval;
        }

        duration -= dt;
        intervalCounter += dt;

        return false;
    }
}

public class BurnBuff : Buff
{
    public override void OnEquipped()
    {
        base.OnEquipped();
        Debug.Log($"{caller} >> Fire on!");
    }

    public override void OnUnequipped()
    {
        base.OnUnequipped();
        Debug.Log($"{caller} >> Fire off!");
    }

    public override void OnUpdate()
    {
        Debug.Log($"{caller} >> Burn!");
    }
}