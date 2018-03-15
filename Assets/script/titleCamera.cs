using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleCamera : MonoBehaviour {
    void Start () {
    }
	
	void Update () {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 0.1f, transform.localEulerAngles.z);
	}
}
