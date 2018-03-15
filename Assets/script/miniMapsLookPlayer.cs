using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapsLookPlayer : MonoBehaviour {

    public GameObject TPSCamera;

	void Start () {
        Vector3 TPSCameraPos;
        TPSCameraPos = TPSCamera.transform.position;
        transform.LookAt(TPSCameraPos);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z+180);
	}
	
	void Update () {
		
	}
}
