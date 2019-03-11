using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldMapTouch : MonoBehaviour {

    [SerializeField]
    private MapSelect mapSystem;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void OnClickOrTap() {
        OnTapOrClickEvent();
    }

    private void OnTapOrClickEvent() {
        mapSystem.reciveMapInfo(transform.name);
    }
}
