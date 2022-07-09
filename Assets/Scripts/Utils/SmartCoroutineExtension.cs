using System.Collections;
using UnityEngine;

public partial class SmartCoroutine
{
    public static SmartCoroutine ExecuteAfter(float delay, System.Action body)
    {
        return SmartCoroutine.Create(Delay(delay, body));

        IEnumerator Delay(float d, System.Action act)
        {
            yield return new WaitForSeconds(d);
            act?.Invoke();
        }
    }

    public static SmartCoroutine ExecuteAfterOneFrame(System.Action body)
    {
        return SmartCoroutine.Create(Delay(body));

        IEnumerator Delay(System.Action act)
        {
            yield return null;
            act?.Invoke();
        }
    }
}
