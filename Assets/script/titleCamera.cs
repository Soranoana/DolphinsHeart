using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class titleCamera : MonoBehaviour {

    void Start () {
        //通常カメラ
        XRSettings.enabled = false;
    }
	
	void Update () {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 0.1f, transform.localEulerAngles.z);
	}
}
