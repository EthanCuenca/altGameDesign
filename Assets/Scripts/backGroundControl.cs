using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundControl : MonoBehaviour
{
    [SerializeField] private GameObject background;
    void FixedUpdate()
    {
        background.transform.Rotate(0,0,Input.GetAxisRaw("Horizontal"));

    }
}
