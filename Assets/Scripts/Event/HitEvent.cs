using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : BaseEvent
{
    public override void Invoke(EventParam param)
    {
        if (!param.IsValid()) return;

        Debug.Log($"{param.sender.name} hit {param.receiver.name}");
    }
}
