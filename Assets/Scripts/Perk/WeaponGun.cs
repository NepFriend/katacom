using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponGun : Weapon
{
    public WeaponGun(ID id) : base(id) { }

    public override async void Attack(GameObject self)
    {
        if (!CanUse) return;

        if (CurrentAmmo == 0)
        {
            if (!CanReload) return;
            Reload(self);
            return;
        }

        Debug.Log($"{id}: Shot!");
        CurrentAmmo = Mathf.Max(CurrentAmmo - Data.shotCount, 0);

        shotTrigger = true;
        await Task.Delay((int)(Data.shotDelay * 1000));
        shotTrigger = false;
    }

    public override async void Reload(GameObject self)
    {
        Debug.Log("Reloading...");
        reloadTrigger = true;
        await Task.Delay((int)(Data.reloadDelay * 1000));
        reloadTrigger = false;
        int newMag = Data.suppliedAmmo - Data.magazineSize < 0 ? Data.suppliedAmmo : Data.magazineSize;
        CurrentAmmo = newMag;
        Data.suppliedAmmo = Mathf.Max(Data.suppliedAmmo - Data.magazineSize, 0);
        Debug.Log("Done Reloading");
    }
}
