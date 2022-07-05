using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static bool isInitialized = false;

    public static Interactor Interactor { get; private set; }


    private void Awake()
    {
        if (isInitialized)
        {
            Destroy(gameObject);
            return;
        }
        isInitialized = true;
        DontDestroyOnLoad(gameObject);


        // Add more if you need...
        Interactor = gameObject.AddComponent<Interactor>();
    }
}
