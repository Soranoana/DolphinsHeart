using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeTextTakeOffOrTake : MonoBehaviour {

    public GameObject no1Text;
    public GameObject no2Text;
    public GameObject fadeButton;
    private float alpha;
    private float beta;
    private float count;
    private int speed;

	void Start () {
        count = 0;
        alpha = 255;
        beta = 0;
        speed = 50;
	}
	
	void Update () {
        count++;
        alpha = Mathf.Sin(count/50);        //    1~-1 
        beta = -alpha;
        no1Text.GetComponent<TextMesh>().color = new Vector4(1, 1, 1, alpha);
        no2Text.GetComponent<TextMesh>().color = new Vector4(1, 1, 1, beta);
        fadeButton.GetComponent<Image>().color = new Vector4(1, 1, 1, Mathf.Abs(alpha));
    }
}
