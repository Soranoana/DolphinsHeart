using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poseMode : MonoBehaviour {

    public GameObject sceneloader;

	void Start () {
		
	}
	
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            sceneloader.GetComponent<SceneLoader>().loadtest();
            GameObject.FindWithTag("Player").GetComponent<playerControl>().toPoseMode(true);
        }
    }
}
