using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private List<Buff> buffList = new List<Buff>();

    public void Register(Buff buff)
    {
        buffList.Add(buff);
        buffList[buffList.Count - 1].OnEquipped();
    }

    private void Update()
    {
        for (int i = 0; i < buffList.Count; ++i)
        {
            if (!buffList[i].Update(Time.deltaTime)) continue;

            buffList[i].OnUnequipped();
            buffList.RemoveAt(i--);
        }
    }
}