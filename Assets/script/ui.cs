using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour {

    /* UI一覧         表記例                                 内容                                                  実装状態
     * life           命：★★★                             命。攻撃、衝突などで減る。ゼロになると死ぬ。          表示、回復、ダメージは実装。ゲームオーバーと成長システムは未実装。
     * stamina        満腹度：★★★                         スタミナ。時間で減少。ゼロになると速く泳げなくなる    減少は実装済み。回復はシーンの実行。
     * speed          スピード：★★★                       スピード。今のスピード                                実装済み。成長システムの分のデバッグ必要
     * fieldName      マップ：太平洋の海面付近（朝）         マップ名。（）が時間帯。                              仮実装。時間の概念を入れてない。量産前。
     * fish           エサ：700                              保有している小魚数。                                  実装済み。データはデモ
     * fieldFishNum   近くのエサ：25                         マップ中にいる小魚数                                  実装済み。デバッグ用に数字表示中
     * enemy          天敵：5                                サメなどのエネミーの数。                              実装済み。デバッグ用に数字表示中
     * o2             酸素：70                               残り酸素                                              実装済み。デバッグ用に数字表示中
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
        if (PlayerPrefs.GetInt("tfpsValue")<=0) {
            GameObject.Find("leftUIPanel").transform.localPosition = GameObject.Find("leftUIPosisionForTPS").transform.localPosition;
            GameObject.Find("PlaneForMinimaps").transform.localPosition = GameObject.Find("minimapPosisionForTPS").transform.localPosition;
        } else if (PlayerPrefs.GetInt("tfpsValue")>=1) {
            GameObject.Find("leftUIPanel").transform.localPosition = GameObject.Find("leftUIPosisionForFPS").transform.localPosition;
            GameObject.Find("PlaneForMinimaps").transform.localPosition = GameObject.Find("minimapPosisionForFPS").transform.localPosition;
        }
    }
	
	void Update () {
        //life.GetComponent<TextMesh>().text = "test★★★";
        life.GetComponent<TextMesh>().text = getCurrentLifeString();
        stamina.GetComponent<TextMesh>().text = getHungryString();
        speed.GetComponent<TextMesh>().text = getSpeedString();
        fieldName.GetComponent<TextMesh>().text = fieldNameString;
        fish.GetComponent<TextMesh>().text= PlayerPrefs.GetInt("currentFish").ToString();
        fieldFishNum.GetComponent<TextMesh>().text = getCurrentFieldFishSum();//"test50";
        o2Num.GetComponent<TextMesh>().text = getCurrentO2();
        enemy.GetComponent<TextMesh>().text = getCurrentEnemySum();
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

    private string getCurrentLifeString() {
        string lifeText = "";
        for (int i = 0; (float)i < (float)(PlayerPrefs.GetInt("lifePoint") /20); i++)
        {
            lifeText += "★";
        }
        return lifeText + PlayerPrefs.GetInt("lifePoint").ToString();
    }

    private string getCurrentFieldFishSum() {
        GameObject[] fish;
        fish = GameObject.FindGameObjectsWithTag("Fish");
        string fishText = "";
        for (int i = 0; (float)i < fish.Length / 20; i++)
        {
            fishText += "★";
        }
        return fishText+fish.Length.ToString();
    }

    private string getCurrentO2() {
        string o2Text = "";
        for (int i = 0; (float)i < GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>().o2 / 20; i++)
        {
            o2Text += "★";
        }
        return o2Text+ GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>().o2.ToString();
    }

    private string getCurrentEnemySum()
    {
        GameObject[] enemy;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        string enemyText = "";
        for (int i = 0; (float)i < enemy.Length; i++)
        {
            enemyText += "★";
        }
        return enemyText + enemy.Length.ToString();
    }
}
