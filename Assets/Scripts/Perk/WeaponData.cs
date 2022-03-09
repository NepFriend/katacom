using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/Data")]
public class WeaponData : ScriptableObject
{
    public Weapon.ID ID;

    public int magazineSize;
    public int suppliedAmmo;

    // 한 번에 발사 가능한 탄수
    public int shotCount;

    public Vector2 damageRange;

    public float shotDelay;
    public float reloadDelay;

    public void CopyTo(WeaponData data)
    {
        data.magazineSize = this.magazineSize;
        data.suppliedAmmo = this.suppliedAmmo;

        data.shotCount = this.shotCount;

        data.damageRange = this.damageRange;

        data.shotDelay = this.shotDelay;
        data.reloadDelay = this.reloadDelay;
    }
}
