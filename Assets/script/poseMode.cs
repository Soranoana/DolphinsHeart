using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poseMode : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            GameObject.FindWithTag("Player").GetComponent<playerControl>().toPoseMode(true);
        }
    }
}
