using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataLoader
{
    private WeaponDataLoader()
    {
        Initialize();
    }
    private static WeaponDataLoader instance = null;
    public static WeaponDataLoader Instance => instance ?? (instance = new WeaponDataLoader());

    private bool isInitialized = false;

    private Dictionary<WeaponData.ID, WeaponData> dataDictionary = new Dictionary<WeaponData.ID, WeaponData>();


    public void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;

        var data = Resources.LoadAll<WeaponData>("ScriptableObjects/WeaponData");
        for (int i = 0; i < data.Length; ++i)
        {
            dataDictionary.Add(data[i].id, data[i]);
        }
    }

    public WeaponData Get(WeaponData.ID id)
    {
        if (!dataDictionary.TryGetValue(id, out var data))
        {
            return null;
        }
        return data;
    }
}
