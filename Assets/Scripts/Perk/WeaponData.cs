using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/Data")]
public class WeaponData : ScriptableObject
{
    public enum ID
    {
        AssaultRifle,
        SubmachineGun,

    }
    public ID id;

    public int magazineSize;
    public int suppliedAmmo;

    public int shotCountPerSecond;


    [System.Serializable]
    public struct Recoil
    {
        public float upperAngle, lowerAngle;
        public float recoilIncPerSecond, recoilDecPerSecond;
    }

    public Recoil hipFireRecoilData, aimingRecoilData, movingRecoilData;
    public Recoil burstHipFireRecoilData, burstAimingRecoilData, burstMovingRecoilData;

    public Vector2 damageRange;

    public float reloadDelay;

    public Recoil GetRecoilData(bool isBurst, bool isAiming, bool isMoving)
    {
        if (isBurst)
        {
            if (isAiming) return burstAimingRecoilData;
            else if (isMoving) return burstMovingRecoilData;
            else return burstHipFireRecoilData;
        }
        else
        {
            if (isAiming) return aimingRecoilData;
            else if (isMoving) return movingRecoilData;
            else return hipFireRecoilData;
        }
    }
}
