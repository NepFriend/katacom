using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string Name { get; set; }

    public void Return()
    {
        PoolManager.Instance.Add(this);
    }
}