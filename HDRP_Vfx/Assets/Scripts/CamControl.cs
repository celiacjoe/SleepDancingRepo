using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Animator AC_Cam01;
    public Animator AC_Cam02;
    public GameObject Cam01;
    public GameObject Cam02;
    public bool BCam01;
   // public bool BCam02;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            AC_Cam01.SetTrigger("RotationHorizontal");
            AC_Cam02.SetTrigger("RotationHorizontal");
        }
        if (Input.GetKeyDown("v"))
        {
            Debug.Log("verticalok");
            AC_Cam01.SetTrigger("RotationVertical");
            AC_Cam02.SetTrigger("RotationVertical");
        }
        if (Input.GetKeyDown("t"))
        {
            AC_Cam01.SetTrigger("Througt");
            AC_Cam02.SetTrigger("Througt");
        }

        if (Input.GetKeyDown("s"))
        {
            if (BCam01)
            {
                BCam01 = false;
                Cam01.SetActive(false);
                Cam02.SetActive(true);
            }
            else
            {
                BCam01 = true;
                Cam01.SetActive(true);
                Cam02.SetActive(false);
            }

        }






        ///// OLD STUFF 
        if (Input.GetKeyDown("o"))
        {
            //Cam01.camera.orthographic = true;
        }
        if (Input.GetKeyDown("p"))
        {
           // Cam01.orthographic = false;
        }
    }
}