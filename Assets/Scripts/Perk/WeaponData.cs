using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/Data")]
public class WeaponData : ScriptableObject
{
    public Weapon.ID ID;

    public int magazineSize;
    public int suppliedAmmo;

    // �� ���� �߻� ������ ź��
    public int shotCount;

    public Vector2 damageRange;

    public Vector2 spreadRange;
    public AnimationCurve spreadThreshold;

    public float shotDelay;
    public float reloadDelay;

    public void CopyTo(WeaponData data)
    {
        data.magazineSize = this.magazineSize;
        data.suppliedAmmo = this.suppliedAmmo;

        data.shotCount = this.shotCount;

        data.damageRange = this.damageRange;

        data.spreadRange = this.spreadRange;

        data.spreadThreshold = new AnimationCurve(this.spreadThreshold.keys);

        data.shotDelay = this.shotDelay;
        data.reloadDelay = this.reloadDelay;
    }
}
