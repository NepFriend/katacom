using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerkTest : MonoBehaviour
{
    private Perk perk;

    private void Awake()
    {
        perk = new PerkAssertRifle();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            perk?.Use(gameObject);
        }
    }
}
