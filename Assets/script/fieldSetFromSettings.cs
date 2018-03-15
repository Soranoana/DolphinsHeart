using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldSetFromSettings : MonoBehaviour {
    public GameObject wall;
	void Start () {
        //完全描画のとき
        if (PlayerPrefs.GetInt("byougaValue") == 0) {
            wall.gameObject.SetActive(false);
        }
        //簡易描画のとき
        else {

        }
	}
	
	void Update () {
		
	}
}
