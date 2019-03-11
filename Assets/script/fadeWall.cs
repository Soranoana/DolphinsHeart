using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeWall : MonoBehaviour {

    private float fadePoint;
    private GameObject player;
    private int fade;
    private int paintArea;//描画を始める範囲
  
	void Start () {
        player = GameObject.FindWithTag("Player");
        fade = 0;
        paintArea = 100;
    }
	
	void Update () {
        if (transform.name == "wallSouth" || transform.name == "wallNorth") {
            fadePoint = paintArea - Mathf.Abs(player.transform.position.z - transform.position.z)+5;
            if (fadePoint >= 0)
            {
                fade = (int)(255 * (fadePoint/paintArea));
            }
            else {
                fade = 0;//透明
            }
        }
        if (transform.name == "wallWest" || transform.name == "wallEast")
        {
            fadePoint = paintArea - Mathf.Abs(player.transform.position.x - transform.position.x)+5;
            if (fadePoint >= 0)
            {
                fade = (int)(255 * (fadePoint / paintArea));
            }
            else
            {
                fade = 0;//透明
            }
        }
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1.0f*fade/255);
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (transform.name == "wallSouth") {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y,248);
            }else if (transform.name == "wallNorth")
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -248);
            }else if (transform.name == "wallWest")
            {
                other.transform.position = new Vector3(248,other.transform.position.y, other.transform.position.z);
            }
            else if (transform.name == "wallEast")
            {
                other.transform.position = new Vector3(-248, other.transform.position.y, other.transform.position.z);
            }
        }
    }
}
