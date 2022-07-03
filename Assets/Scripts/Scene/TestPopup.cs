using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestPopup : Popup
{
    public override async Task OnEnter(UIEventParam param)
    {
        Debug.Log("Popup Start");
        await Task.Delay(3000);
        Debug.Log("Popup Start End");
    }

    public override async Task OnExit()
    {
        Debug.Log("?");
    }
}
