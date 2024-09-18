using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class backgroundMusic : MonoBehaviour
{
    private static backgroundMusic bMusic;
    void Awake()
    {
        if(bMusic == null)
        {
            bMusic = this;
            DontDestroyOnLoad(bMusic);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
