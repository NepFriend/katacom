using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMove : MonoBehaviour
{
    GameObject Parent;

    // Start is called before the first frame update
    public void Start()
    {
        Parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    public void Update()
    {

        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        if (point.x > Parent.transform.position.x)
        {


            transform.position = new Vector3(Parent.transform.position.x + 6f, Parent.transform.position.y + 15.9f, Parent.transform.position.z);

        }
        else
        {
            transform.position = new Vector3(Parent.transform.position.x - 6f, Parent.transform.position.y + 15.9f, Parent.transform.position.z);
        }
    }
}
