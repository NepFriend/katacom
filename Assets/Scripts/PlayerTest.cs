using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerTest : MonoBehaviour
{
    public Perk2 perk;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        perk.EquipWeapon(Weapon.ID.AssaultRifle1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PopupController.Show("TestPopup", null);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PopupController.Hide();
        }



        perk?.Update(Time.deltaTime);
        if (Input.GetMouseButton(0))
        {
            perk?.Use(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            perk?.EquipWeapon(Weapon.ID.AssaultRifle1);
        }
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(PlayerTest))]
public class PlayerTestEditor : UnityEditor.Editor
{
    private void OnSceneGUI()
    {
        var self = target as PlayerTest;
        if (self.perk == null) return;

        string status = $"Ammo: {self.perk.CurrentWeapon.CurrentAmmo}/{self.perk.CurrentWeapon.Data.magazineSize}";

        status += self.perk.CurrentWeapon.IsReloading ? "\nReloading..." : "";

        var defaultColor = Handles.color;
        Handles.color = Color.red;
        Handles.Label(self.transform.position, status);
        Handles.color = defaultColor;

        Vector3 upper = Quaternion.AngleAxis(self.perk.CurrentWeapon.Data.spreadRange.x, self.transform.right) * self.transform.forward;
        Vector3 lower = Quaternion.AngleAxis(self.perk.CurrentWeapon.Data.spreadRange.y, self.transform.right) * self.transform.forward;

        Handles.color = Color.green;
        Handles.DrawLine(self.transform.position, lower, 2F);
        Handles.DrawLine(self.transform.position, upper, 2F);
        Handles.DrawWireArc(self.transform.position, self.transform.right, upper, self.perk.CurrentWeapon.Data.spreadRange.y - self.perk.CurrentWeapon.Data.spreadRange.x, 1F, 2F);
        Handles.color = defaultColor;
    }
}
#endif