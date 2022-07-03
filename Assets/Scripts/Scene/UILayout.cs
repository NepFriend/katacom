using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIEventParam
{

}

public abstract class UILayout : MonoBehaviour
{
    public abstract Task OnEnter(UIEventParam param);
    public abstract Task OnExit();
}
