using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGtest : MonoBehaviour
{

    public GameObject Bullet;

    public bool shoot;

    float rimi;

    // Start is called before the first frame update
    void Start()
    {
        shoot = false;

        rimi = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);

            shoot = false;

        }

        if (rimi > 0)
        {
            rimi -= Time.deltaTime;
        }
        else
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);

            rimi = 0.2f;
        }

    }


    
}
