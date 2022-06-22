using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public string TopDawnAxisName = "Vertical";
    public string moveAxisName = "Horizontal";
    public string fireButtonName = "Fire1";
    public string moveSideName = "Horizontal";


    public float move { get; private set; }
    public float topDawn { get; private set; }
    public float moveSide { get; private set; }

    public bool fire { get; private set; }

    public bool moveDir { get; private set; }

    public bool lookDown { get; private set; }


    // Start is called before the first frame update
    public void Start()
    {
        move = 0f;
        moveDir = false;
        lookDown = false;
    }

    // Update is called once per frame
    public void Update()
    {
        
        moveSide = 0f;
        fire = false;


        move = Input.GetAxisRaw(moveAxisName);

        if (move > 0)
        {
            //오른쪽으로 가면 참
            moveDir = true;
        }
        else if (move < 0)
        {
            moveDir = false;
        }

        topDawn = Input.GetAxis(TopDawnAxisName);

        if (topDawn < 0)
        {
            lookDown = true;
        }
        else
        {
            lookDown = false;
        }

        fire = Input.GetButton(fireButtonName);

    }
}
