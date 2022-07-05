using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestPlayer : MonoBehaviour
{
    Status status = new Status();

    PassiveHandler ph = new PassiveHandler();


    private void Start()
    {
    }
    private void Update()
    {
        ph.Update(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ph.AddBuff(Buff.Create<BurnBuff>(gameObject, status, 5F, 1F));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ph.AddPassive(Passive.Create<HPHighPassive>(gameObject, status));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ph.AddPassive(Passive.Create<HPPlusPassive>(gameObject, status));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ph.RemovePassive<HPHighPassive>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ph.RemovePassive<HPPlusPassive>();
        }
    }
}
