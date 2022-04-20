using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1F);
        GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
    }
}
