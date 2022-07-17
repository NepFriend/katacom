using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool Recipe", fileName = "PoolRecipe")]
public class PoolRecipe : ScriptableObject
{
    public PoolObject[] objectList;


    public PoolObject Find(string name)
    {
        for (int i = 0; i < objectList.Length; ++i)
        {
            if (objectList[i].name != name) continue;
            return objectList[i];
        }
        return null;
    }
}