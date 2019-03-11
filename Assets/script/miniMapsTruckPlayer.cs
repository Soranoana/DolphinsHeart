using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapsTruckPlayer : MonoBehaviour {
    private GameObject playerObj;
    private GameObject seaUpper;
    private Vector3 playerPos;
    private Vector3 seaUpperPos;
    private Quaternion cameraQ;

	void Start () {
        playerObj = GameObject.FindWithTag("Player");
        seaUpper = GameObject.FindWithTag("SeaUpper");
        cameraQ = transform.rotation;
	}
	
	void Update () {
        playerPos = playerObj.transform.position;
        seaUpperPos = seaUpper.transform.position;
        playerPos.y = 170;// seaUpperPos.y;
        transform.position = playerPos;
        transform.rotation = cameraQ;
	}
}
