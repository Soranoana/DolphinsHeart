using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManege : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Behaviour behaviour = (Behaviour)gameObject.GetComponent("Halo");
        behaviour.enabled = true;
        if (gameObject.GetComponent<BoxCollider>() != null) {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        Destroy(this.gameObject, 40);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Destroy(this.gameObject);
        }
    }
}
