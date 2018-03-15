using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour {

    /* UI一覧         表記例                                 内容
     * life           命：★★★                             命。攻撃、衝突などで減る。ゼロになると死ぬ。
     * stamina        満腹度：★★★                         スタミナ。時間で減少。ゼロになると速く泳げなくなる
     * speed          スピード：★★★                       スピード。今のスピード
     * fieldName      マップ：太平洋の海面付近（朝）         マップ名。（）が時間帯。
     * fish           エサ：700                              保有している小魚数。
     * fieldFishNum   近くのエサ：25                         マップ中にいる小魚数
     * enemy          天敵：5                                サメなどのエネミーの数。
     * o2             酸素：70                               残り酸素
     */

    // private GameObject pose;
    //private GameObject active;
    private int fishText;
    private string fieldNameString;
    private string speedString;
    public GameObject life;
    public GameObject stamina;
    public GameObject speed;
    public GameObject fieldName;
    public GameObject fish;
    public GameObject fieldFishNum;
    public GameObject o2Num;
    public GameObject enemy;
    private playerControl PlayerControl;

	void Start () {
        PlayerControl = GetComponent<playerControl>();
        fieldNameString = getCurrentFieldName();
    }
	
	void Update () {
        life.GetComponent<TextMesh>().text = "test★★★";
        stamina.GetComponent<TextMesh>().text = getHungryString();
        speed.GetComponent<TextMesh>().text = getSpeedString();
        fieldName.GetComponent<TextMesh>().text = fieldNameString;
        fish.GetComponent<TextMesh>().text= PlayerPrefs.GetInt("currentFish").ToString();
        fieldFishNum.GetComponent<TextMesh>().text = "test50";
        o2Num.GetComponent<TextMesh>().text = "test50";
        //enemy.GetComponent<TextMesh>().text = "5";
        /*
        pose = GameObject.Find("pose");
        active = GameObject.Find("active");
        if (pose!=null&& pose.activeInHierarchy)
        {

        }
        else if (active.activeInHierarchy) {
            this.gameObject.transform.Find("active").gameObject.transform.Find("Text").GetComponent<Text>().text = fishText.ToString();
        }*/
    }

    private string getCurrentFieldName() {
        string fieldName = SceneManager.GetActiveScene().name;
        if (fieldName == "cameraTest2") return "太平洋の海面付近（朝）";
        else if (fieldName == "test2") return "test2";
        else return "Error : Field name is not found.";
        
        /* 太平洋（波普通、浅瀬～深瀬）、
         * 日本海（浅瀬~沖、波強い）、
         * 地中海（きれい、浅瀬～沖）、カリブ海（きれい浅瀬～深瀬）、
         * 北極海（深瀬のみ、氷に当たると死ぬ、波はないが、息継ぎのインターバルが短い、オットセイやペンギンがよく落ちてきて当たると死）
         * 洞窟（狭い、スピード感、壁に当たると死、窒息死なし、魚多い、ムズめのボーナスステージ）
         * 川（狭く浅くごつい道、流れ強い、下流から上流へ、イルカ不可）
         * 湖（浅瀬、きれい、波なし、魚多め、ボーナスステージ）
         * など
         */
    }
    private string getSpeedString() {
        string speedText = "";
        for (int i = 0; i < PlayerControl.speedModeChagable; i++) {
            speedText += "★";
        }
        return speedText;
    }
    private string getHungryString() {
        string hungryText = "";
        for (int i = 0; (float)i < PlayerPrefs.GetFloat("hungryPoint"); i++) {
            hungryText += "★";
        }
        return hungryText;
    }
}
