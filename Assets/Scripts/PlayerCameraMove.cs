using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMove : MonoBehaviour
{
    GameObject parent;

    Vector3 mousePosition;

    Camera cam;

    // Start is called before the first frame update
    public void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    public void Update()
    {
        //mousePosition = Input.mousePosition;

        //mousePosition = cam.ScreenToWorldPoint(mousePosition);


        //if (mousePosition.x > parent.transform.position.x)
        //{
        //    transform.position = new Vector3(2, 6.3f, 0);

        //}
        //else
        //{
        //    transform.position = new Vector3(-2, 6.3f, 0);
        //}
    }
}
