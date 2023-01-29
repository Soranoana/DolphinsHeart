using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class cameraSetForWithoutField : MonoBehaviour
{

    void Start()
    {
        /* Fieldシーン以外なら基本非VRモード */
        UnityEngine.XR.XRSettings.enabled = false;
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 10f;
        UnityEngine.XR.XRSettings.renderViewportScale = 10f;
        /*
        if (SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }
        else if (SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }
        else if(SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }
        else if (SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }
        else if(SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }
        else if (SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }
        else if(SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }
        else if (SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }
        else if(SceneManager.GetActiveScene().name == "")
        {
            VRSettings.enabled = false;
        }*/
    }
}
