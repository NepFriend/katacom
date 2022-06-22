using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGunTest : WeaponGun
{
    private GameObject bullet;

    public WeaponGunTest(int slotIndex, ID id) : base(slotIndex, id)
    {
        bullet = Resources.Load("TestBullet") as GameObject;
    }

    private float spreadRatio = 0F;

    protected override void OnAttack(GameObject self)
    {
        if (Time.realtimeSinceStartup - lastUse >= Mathf.Max(Data.shotDelay * 2F, Time.deltaTime * 2F))
        {
            spreadRatio = 0F;
        }

        float spreadThreshold = Data.spreadThreshold.Evaluate(spreadRatio);
        spreadRatio += 1F / Data.magazineSize;

        base.OnAttack(self);

        float angle = Random.Range(Data.spreadRange.x, Data.spreadRange.y) * spreadThreshold;
        Vector3 shotDir = Quaternion.AngleAxis(angle, self.transform.right) * self.transform.forward;

        var obj = GameObject.Instantiate(bullet);
        obj.transform.position = self.transform.position;
        obj.transform.rotation = Quaternion.AngleAxis(angle, obj.transform.right);

        if (Physics.Raycast(obj.transform.position, shotDir, out var hit, 1000F))
        {
            Managers.Interactor.AddEvent(EventPool.Get<HitEvent>(), new EventParam() { sender = self, receiver = hit.transform.gameObject });
        }
        Debug.DrawRay(self.transform.position, shotDir * 100F, Color.red, Data.shotDelay * 0.5f);
    }

    protected override void OnEndReload(GameObject self)
    {
        base.OnEndReload(self);
        spreadRatio = 0F;
    }
}
