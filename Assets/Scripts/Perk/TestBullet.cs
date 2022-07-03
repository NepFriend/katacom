using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // Destroy(gameObject, 1F);
        GetComponent<Rigidbody2D>().AddForce(transform.right * -40000f *Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }

      
    }

}
