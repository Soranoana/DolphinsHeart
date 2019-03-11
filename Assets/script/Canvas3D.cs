using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas3D : MonoBehaviour {
    
    private GameObject hukidasi1;
    private GameObject hukidasi2;
    private GameObject hukidasi3;
    private GameObject hukidasi4;
    private GameObject hukidasi5;
    private GameObject centerObject;
    private float normalizedHukidasiRange;
    private float hukidasiPositionY=1.1f;
	void Start () {
        if (PlayerPrefs.GetInt("tfpsValue") == 0) {
            transform.position = GameObject.Find("Canvas3DpostionTPS").transform.position;
        }
        else {
            transform.position = GameObject.Find("Canvas3DpostionFPS").transform.position;
        }
        hukidasi1 = transform.Find("PlaneLeft").gameObject;
        hukidasi2 = transform.Find("PlaneLeftTop").gameObject;
        hukidasi3 = transform.Find("PlaneTop").gameObject;
        hukidasi4 = transform.Find("PlaneRightTop").gameObject;
        hukidasi5 = transform.Find("PlaneRight").gameObject;
        centerObject = transform.Find("playerPoint").gameObject;
        /* hukidasiの位置決定 */
        normalizedHukidasiRange = Vector3.Distance(centerObject.transform.position, hukidasi1.transform.position);
        Debug.Log(normalizedHukidasiRange);
        hukidasi1.transform.localPosition = new Vector3(-normalizedHukidasiRange, hukidasiPositionY,0);
        hukidasi2.transform.localPosition = new Vector3(-normalizedHukidasiRange / Mathf.Sqrt(2), hukidasiPositionY, normalizedHukidasiRange / Mathf.Sqrt(2));
        hukidasi3.transform.localPosition = new Vector3(0, hukidasiPositionY, normalizedHukidasiRange);
        hukidasi4.transform.localPosition = new Vector3(normalizedHukidasiRange / Mathf.Sqrt(2), hukidasiPositionY, normalizedHukidasiRange / Mathf.Sqrt(2));
        hukidasi5.transform.localPosition = new Vector3(normalizedHukidasiRange, hukidasiPositionY, 0);
     /*   hukidasi1.GetComponent<MeshRenderer>().enabled = false;
        hukidasi2.GetComponent<MeshRenderer>().enabled = false;
        hukidasi3.GetComponent<MeshRenderer>().enabled = false;
        hukidasi4.GetComponent<MeshRenderer>().enabled = false;
        hukidasi5.GetComponent<MeshRenderer>().enabled = false;*/
    }

    void Update() {
        hukidasi1.transform.Find("Canvas").transform.Find("Text").gameObject.GetComponent<Text>().text = LifeHukidasi();
    }

    private string LifeHukidasi() {
        if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax") / 2) {
            return "^o^";
        } else if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax") / 5) {
            return ">o<";
        }else {
            return "XoX";
        }
    }

    private string StaminaHukidasi() {
        if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax") / 2) {
            return "^o^";
        } else if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax") / 5) {
            return ">o<";
        }else {
            return "XoX";
        }
    }

    private string SpeedHukidasi() {
        return "";
    }

    private string FishHukidasi() {
        if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax") / 2) {
            return "^o^";
        } else if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax") / 5) {
            return ">o<";
        }else {
            return "XoX";
        }
    }

    private string EnemyHukidasi() {
        if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax") / 2) {
            return "^o^";
        } else if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax") / 5) {
            return ">o<";
        }else {
            return "XoX";
        }
    }

    private string CurrentFish() {
        return "";
    }

    private string FieldName() {
        return "";
    }
}
