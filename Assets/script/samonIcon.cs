using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class samonIcon : MonoBehaviour {

    public GameObject minimapIcon1;
    public GameObject minimapIcon2;
    private GameObject samonIcon1;
    private GameObject samonIcon2;
    private MeshRenderer samonIcon1Mesh;
    private MeshRenderer samonIcon2Mesh;
    private float icon1Y;
    private float icon2Y;
    private GameObject seaUpper;
    private bool iconFlash=false;
    private bool iconSwitch=false;
    private float flashTime;

    void Start () {
        iconFlash = false;
        iconSwitch = false;
        seaUpper = GameObject.FindWithTag("SeaUpper");
        flashTime = 0;
        samonIcon1 = Instantiate(minimapIcon1) as GameObject;
        samonIcon1Mesh = samonIcon1.GetComponent<MeshRenderer>();
        samonIcon1Mesh.enabled = true;
        icon1Y = samonIcon1.transform.eulerAngles.y;
        if (minimapIcon2 != null) {
            samonIcon2 = Instantiate(minimapIcon2) as GameObject;
            samonIcon2Mesh = samonIcon2.GetComponent<MeshRenderer>();
            samonIcon2Mesh.enabled = false;
            icon2Y = samonIcon2.transform.eulerAngles.y;
        }
    }
	
	void Update () {
        samonIcon1.transform.position = new Vector3(transform.position.x,seaUpper.transform.position.y, transform.position.z);
        samonIcon1.transform.eulerAngles = new Vector3(samonIcon1.transform.eulerAngles.x, icon1Y+transform.eulerAngles.y, samonIcon1.transform.eulerAngles.z);
        if (minimapIcon2 != null) {
            samonIcon2.transform.position = new Vector3(transform.position.x, seaUpper.transform.position.y, transform.position.z);
            samonIcon2.transform.eulerAngles = new Vector3(samonIcon1.transform.eulerAngles.x, icon2Y + transform.eulerAngles.y, samonIcon1.transform.eulerAngles.z);

            if (iconFlash) {
                IconFlash();
            }
            if (iconSwitch) {
                IconSwitch();
            }
        }
    }

    private void OnDestroy() {
        Destroy(samonIcon1);
        if (samonIcon2 != null) {
            Destroy(samonIcon2);
        }
    }

    private void IconFlash() {
        flashTime += Time.deltaTime;
        if (samonIcon1Mesh.enabled == true && flashTime>=1) {
            samonIcon1Mesh.enabled = false;
            samonIcon2Mesh.enabled = true;
            flashTime = 0;
        }
        else if (samonIcon2Mesh.enabled == true && flashTime>=1) {
            samonIcon2Mesh.enabled = false;
            samonIcon1Mesh.enabled = true;
            flashTime = 0;
        }
    }

    private void IconSwitch() {
        if (samonIcon1Mesh.enabled == true) {
            samonIcon1Mesh.enabled = false;
            samonIcon2Mesh.enabled = true;
        }
        else if (samonIcon2Mesh.enabled == true) {
            samonIcon2Mesh.enabled = false;
            samonIcon1Mesh.enabled = true;
        }
        iconSwitch = false;
    }

    public void IconFlashFlg(bool flg) {
        iconFlash = flg;
    }

    public void IconSwitchFlg(int icon, bool flg) {
        if (icon == 1 && samonIcon1Mesh.enabled!=flg) {
            iconSwitch = flg;
        }
        if (icon == 2 && samonIcon2Mesh.enabled==flg) {
            iconSwitch = flg;
        }
    }
}
