using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerkTest : MonoBehaviour
{
    private Perk perk;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        perk = new PerkAssertRifle();
        perk.EquipWeapon(Weapon.ID.AssertRifle1);
    }

    private void Update()
    {
        perk?.Update(Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            perk?.Use(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            perk?.EquipWeapon(Weapon.ID.AssertRifle1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            perk?.EquipWeapon(Weapon.ID.AssertRifle2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            perk?.EquipWeapon(Weapon.ID.AssertRifle3);
        }
    }
}
