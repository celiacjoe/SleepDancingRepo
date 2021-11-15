using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Animator AC_Cam;
    public Camera Cam;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            AC_Cam.SetTrigger("RotationHorizontal");
        }
        if (Input.GetKeyDown("v"))
        {
            Debug.Log("verticalok");
            AC_Cam.SetTrigger("RotationVertical");
        }
        if (Input.GetKeyDown("t"))
        {
            AC_Cam.SetTrigger("Througt");
        }

        if (Input.GetKeyDown("o"))
        {
            Cam.orthographic = true;
        }
        if (Input.GetKeyDown("p"))
        {
            Cam.orthographic = false;
        }
    }
}