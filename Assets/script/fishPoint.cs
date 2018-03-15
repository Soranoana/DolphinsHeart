using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishPoint : MonoBehaviour {

    private GameObject parent;
    private int fishSum;
    private int g1Cost=1;
    private int g2Cost=2;
    private int g3Cost=5;
    private int g4Cost=10;
    private int g5Cost=15;
    private int g6Cost=30;
    private int g7Cost=50;
    private int g8Cost=100;

    void Start () {
        parent = gameObject.transform.parent.gameObject;
	}
	
	void Update () {
        fishSum = PlayerPrefs.GetInt("currentFish");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            //point++
            if (parent.name.Substring(0, 6) == "fishG1")
            {
                PlayerPrefs.SetInt("currentFish", fishSum+g1Cost);
            } else if (parent.name.Substring(0, 6) == "fishG2")
            {
                PlayerPrefs.SetInt("currentFish", fishSum+g2Cost);
            }
            else if(parent.name.Substring(0, 6) == "fishG3")
            {
                PlayerPrefs.SetInt("currentFish", fishSum+g3Cost);
            } else if (parent.name.Substring(0, 6) == "fishG4")
            {
                PlayerPrefs.SetInt("currentFish", fishSum+g4Cost);
            }
            else if(parent.name.Substring(0, 6) == "fishG5")
            {
                PlayerPrefs.SetInt("currentFish", fishSum+g5Cost);
            } else if (parent.name.Substring(0, 6) == "fishG6")
            {
                PlayerPrefs.SetInt("currentFish", fishSum+g6Cost);
            }
            else if(parent.name.Substring(0, 6) == "fishG7")
            {
                PlayerPrefs.SetInt("currentFish", fishSum+g7Cost);
            } else if (parent.name.Substring(0, 6) == "fishG8")
            {
                PlayerPrefs.SetInt("currentFish", fishSum+g8Cost);
            }
            Destroy(parent);
        }
    }
}
