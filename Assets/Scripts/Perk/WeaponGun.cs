using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponGun : Weapon
{
    public WeaponGun(int slotIndex, ID id) : base(slotIndex, id) { }

    public UnityEngine.Events.UnityEvent<GameObject> onAttackEvent, onBeginReloadEvent, onEndReloadEvent;

    public override async void AttackLogic(GameObject self)
    {
        if (!CanUse) return;

        if (CurrentAmmo == 0)
        {
            if (!CanReload) return;
            ReloadLogic(self);
            return;
        }

        OnAttack(self);
        CurrentAmmo = Mathf.Max(CurrentAmmo - Data.shotCount, 0);

        lastUse = Time.realtimeSinceStartup;

        shotTrigger = true;
        await Task.Delay((int)(Data.shotDelay * 1000));
        shotTrigger = false;
    }

    public override async void ReloadLogic(GameObject self)
    {
        OnBeginReload(self);

        reloadTrigger = true;
        await Task.Delay((int)(Data.reloadDelay * 1000));
        reloadTrigger = false;

        int newMag = SuppliedAmmo - Data.magazineSize < 0 ? SuppliedAmmo : Data.magazineSize;
        CurrentAmmo = newMag;
        SuppliedAmmo = Mathf.Max(SuppliedAmmo - Data.magazineSize, 0);

        OnEndReload(self);
    }


    protected virtual void OnAttack(GameObject self)
    {
        onAttackEvent?.Invoke(self);
    }

    protected virtual void OnBeginReload(GameObject self)
    {
        onBeginReloadEvent?.Invoke(self);
    }

    protected virtual void OnEndReload(GameObject self)
    {
        onEndReloadEvent?.Invoke(self);
    }
}
