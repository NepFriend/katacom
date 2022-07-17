using System.Collections;
using UnityEngine;

public class Bullet : PoolObject
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float deadTimes = 3F;


    private SmartCoroutine launchCoroutine;


    public void Launch(Vector3 dir)
    {
        launchCoroutine?.Stop();

        if (launchCoroutine == null) launchCoroutine = SmartCoroutine.Create(CoLaunch);
        launchCoroutine?.Start();

        IEnumerator CoLaunch()
        {
            var nDir = dir.normalized;
            float time = 0F;
            while (time < deadTimes)
            {
                time += Time.deltaTime;
                transform.position += nDir * speed * Time.deltaTime;
                yield return null;
            }
            PoolManager.Instance.Add(this);
        }
    }
}
