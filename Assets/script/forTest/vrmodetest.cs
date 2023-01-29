using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class vrmodetest : MonoBehaviour
{

    void Start()
    {
        UnityEngine.XR.XRSettings.enabled = false;
    }

    void Update()
    {
        Debug.Log(UnityEngine.XR.XRSettings.enabled);
    }

    public void getButtonDownVR()
    {
        UnityEngine.XR.XRSettings.enabled = true;
        Debug.Log("OK");
        Destroy(this.gameObject);
    }

    public void getButtonDownNON()
    {
        UnityEngine.XR.XRSettings.enabled = false;
        Debug.Log("OK");
        Destroy(this.gameObject);
    }
}
