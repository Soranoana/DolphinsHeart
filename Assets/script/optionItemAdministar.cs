using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionItemAdministar : MonoBehaviour {

    /* 魚量 */
    private int fishPoint;
    /* 残り魚 */
    public int currentFish;
    /* アイテム使用用int */    ///アイテム追加時追加
    public int dashSpeed1;
    public int dashSpeed2;
    public int dashSpeed3;
    public int dashSpeed4;
    public int dashMax1;
    public int o2max1;
    public int o2use1;
    public int o2recovery1;
    public int luck1;
    public int enemyArea1;
    public int life1;
    public int jumpAble1;
    public int trick1;
    public int stearing1;
    public int stearSpeedDownNothing1;
    public int sharkKill1;
    public int nextMap1;
    public int dolphinColor;
    public int fishColor1;
    public int sharkColor1;
    public int nextGame1;
    public int nextGameTuyokute1;
    public int cheatMode1;
    public int DLCcontents1;
    /* アイテムのコスト */    ///アイテム追加時追加
    private int dashSpeedCost1;
    private int dashSpeedCost2;
    private int dashSpeedCost3;
    private int dashSpeedCost4;
    private int dashMaxCost1;
    private int o2maxCost1;
    private int o2useCost1;
    private int o2recoveryCost1;
    private int luckCost1;
    private int enemyAreaCost1;
    private int lifeCost1;
    private int jumpAbleCost1;
    private int trickCost1;
    private int stearingCost1;
    private int stearSpeedDownNothingCost1;
    private int sharkKillCost1;
    private int nextMapCost1;
    private int dolphinColorCost1;
    private int fishColorCost1;
    private int sharkColorCost1;
    private int nextGameCost1;
    private int nextGameTuyokuteCost1;
    private int cheatModeCost1;
    private int DLCcontentsCost1;
    /* トグル取得用 */
    private GameObject content;
    /* アイテム情報用 */
    private int nowItem;
    private GameObject textArea;

    void Start () { 
        /* 魚数初期化 */
        fishPoint = 200;
        currentFish = PlayerPrefs.GetInt("currentFish");
        transform.Find("TextMaxNumber").GetComponent<Text>().text = fishPoint.ToString();
        transform.Find("TextCurrentNumber").GetComponent<Text>().text = currentFish.ToString();
        /* 各bool初期化 */            ///アイテム追加時追加
        /* 0 未使用 1 使用中 */
        dashSpeed1 = PlayerPrefs.GetInt("dashSpeed1");
        dashSpeed2 = PlayerPrefs.GetInt("dashSpeed2");
        dashSpeed3 = PlayerPrefs.GetInt("dashSpeed3");
        dashSpeed4 = PlayerPrefs.GetInt("dashSpeed4");
        dashMax1 = PlayerPrefs.GetInt("dashMax1");
        o2max1 = PlayerPrefs.GetInt("o2max1");
        o2use1 = PlayerPrefs.GetInt("o2use1");
        o2recovery1 = PlayerPrefs.GetInt("o2recovery1");
        luck1 = PlayerPrefs.GetInt("luck1");
        enemyArea1 = PlayerPrefs.GetInt("enemyArea1");
        life1 = PlayerPrefs.GetInt("life1");
        jumpAble1 = PlayerPrefs.GetInt("jumpAble1");
        trick1 = PlayerPrefs.GetInt("trick1");
        stearing1 = PlayerPrefs.GetInt("stearing1");
        stearSpeedDownNothing1 = PlayerPrefs.GetInt("stearSpeedDownNothing1");
        sharkKill1 = PlayerPrefs.GetInt("sharkKill1");
        nextMap1 = PlayerPrefs.GetInt("nextMap1");
        dolphinColor = PlayerPrefs.GetInt("dolphinColor");
        fishColor1 = PlayerPrefs.GetInt("fishColor1");
        sharkColor1 = PlayerPrefs.GetInt("sharkColor1");
        nextGame1 = PlayerPrefs.GetInt("nextGame1");
        nextGameTuyokute1 = PlayerPrefs.GetInt("nextGameTuyokute1");
        cheatMode1 = PlayerPrefs.GetInt("cheatMode1");
        DLCcontents1 = PlayerPrefs.GetInt("DLCcontents1");
        /* トグル初期化 */            ///アイテム追加時追加
        content = transform.Find("ScrollView").gameObject.transform.Find("Content").gameObject;
        /* アイテム情報用 */
        nowItem = 0;
        textArea = transform.Find("itemInformation").gameObject.transform.Find("Text").gameObject;
        /* nowItem
         * 000 未使用
         * 100 dashSpeed1
         * 101 dashSpeed2
         * 200 dashMax1
         *      :
         */
        textArea.GetComponent<Text>().text = "";

        if (dashSpeed1 == 1)
        {
            content.transform.Find("dashSpeed1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (dashSpeed1 == 0) {
            content.transform.Find("dashSpeed1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (dashSpeed2 == 1)
        {
            content.transform.Find("dashSpeed2").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (dashSpeed2 == 0)
        {
            content.transform.Find("dashSpeed2").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (dashSpeed3 == 1)
        {
            content.transform.Find("dashSpeed3").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (dashSpeed3 == 0)
        {
            content.transform.Find("dashSpeed3").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (dashSpeed4 == 1)
        {
            content.transform.Find("dashSpeed4").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (dashSpeed4 == 0)
        {
            content.transform.Find("dashSpeed4").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (dashMax1 == 1)
        {
            content.transform.Find("dashMax1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (dashMax1 == 0)
        {
            content.transform.Find("dashMax1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (o2max1 == 1)
        {
            content.transform.Find("o2max1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (o2max1 == 0)
        {
            content.transform.Find("o2max1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (o2use1 == 1)
        {
            content.transform.Find("o2use1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (o2use1 == 0)
        {
            content.transform.Find("o2use1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (o2recovery1 == 1)
        {
            content.transform.Find("o2recovery1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (o2recovery1 == 0)
        {
            content.transform.Find("o2recovery1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (luck1 == 1)
        {
            content.transform.Find("luck1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (luck1 == 0)
        {
            content.transform.Find("luck1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (enemyArea1 == 1)
        {
            content.transform.Find("enemyArea1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (enemyArea1 == 0)
        {
            content.transform.Find("enemyArea1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (life1 == 1)
        {
            content.transform.Find("life1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (life1 == 0)
        {
            content.transform.Find("life1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (jumpAble1 == 1)
        {
            content.transform.Find("jumpAble1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (jumpAble1 == 0)
        {
            content.transform.Find("jumpAble1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (trick1 == 1)
        {
            content.transform.Find("trick1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (trick1 == 0)
        {
            content.transform.Find("trick1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (stearing1 == 1)
        {
            content.transform.Find("stearing1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (stearing1 == 0)
        {
            content.transform.Find("stearing1").Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (stearSpeedDownNothing1 == 1)
        {
            content.transform.Find("stearSpeedDownNothing1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (stearSpeedDownNothing1 == 0)
        {
            content.transform.Find("stearSpeedDownNothing1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (sharkKill1 == 1)
        {
            content.transform.Find("sharkKill1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (sharkKill1 == 0)
        {
            content.transform.Find("sharkKill1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (nextMap1 == 1)
        {
            content.transform.Find("nextMap1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (nextMap1 == 0)
        {
            content.transform.Find("nextMap1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (dolphinColor == 1)
        {
            content.transform.Find("dolphinColor1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (dolphinColor == 0)
        {
            content.transform.Find("dolphinColor1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (fishColor1 == 1)
        {
            content.transform.Find("fishColor1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (fishColor1 == 0)
        {
            content.transform.Find("fishColor1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (sharkColor1 == 1)
        {
            content.transform.Find("sharkColor1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (sharkColor1 == 0)
        {
            content.transform.Find("sharkColor1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (nextGame1 == 1)
        {
            content.transform.Find("nextGame1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (nextGame1 == 0)
        {
            content.transform.Find("nextGame1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (nextGameTuyokute1 == 1)
        {
            content.transform.Find("nextGameTuyokute1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (nextGameTuyokute1 == 0)
        {
            content.transform.Find("nextGameTuyokute1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (cheatMode1 == 1)
        {
            content.transform.Find("cheatMode1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (cheatMode1 == 0)
        {
            content.transform.Find("cheatMode1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
        if (DLCcontents1 == 1)
        {
            content.transform.Find("DLCcontents1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = true;
        }
        else if (DLCcontents1 == 0)
        {
            content.transform.Find("DLCcontents1").gameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }

        /* コスト設定 */         ///アイテム追加時追加
        dashSpeedCost1 = 10;
        dashSpeedCost2 = 10;
        dashSpeedCost3 = 10;
        dashSpeedCost4 = 10;
        dashMaxCost1 = 10;
        o2maxCost1 = 10;
        o2useCost1 = 10;
        o2recoveryCost1 = 10;
        luckCost1 = 10;
        enemyAreaCost1 = 10;
        lifeCost1 = 10;
        jumpAbleCost1 = 10;
        trickCost1 = 10;
        stearingCost1 = 10;
        stearSpeedDownNothingCost1 = 10;
        sharkKillCost1 = 10;
        nextMapCost1 = 10;
        dolphinColorCost1 = 10;
        fishColorCost1 = 10;
        sharkColorCost1 = 10;
        nextGameCost1 = 10;
        nextGameTuyokuteCost1 = 10;
        cheatModeCost1 = 10;
        DLCcontentsCost1 = 10;
    }

    void Update () {
        UpdateCurrentFish();
        dateSave();
        contentInfoUpdata();
    }

    void UpdateCurrentFish() {                            ///アイテム追加時追加
        //残りの魚数を更新
        currentFish = fishPoint - (dashSpeed1 * dashSpeedCost1
                                    + dashSpeed2 * dashSpeedCost2
                                    + dashSpeed3 * dashSpeedCost3
                                    + dashSpeed4 * dashSpeedCost4
                                    + dashMax1 * dashMaxCost1
                                    + o2max1 * o2maxCost1
                                    + o2use1 * o2useCost1
                                    + o2recovery1 * o2recoveryCost1
                                    + luck1 * luckCost1
                                    + enemyArea1 * enemyAreaCost1
                                    + life1 * lifeCost1
                                    + jumpAble1 * jumpAbleCost1
                                    + trick1 * trickCost1
                                    + stearing1 * stearingCost1
                                    + stearSpeedDownNothing1 * stearSpeedDownNothingCost1
                                    + sharkKill1 * sharkKillCost1
                                    + nextMap1 * nextMapCost1
                                    + dolphinColor * dolphinColorCost1
                                    + fishColor1 * fishColorCost1
                                    + sharkColor1 * sharkColorCost1
                                    + nextGame1 * nextGameCost1
                                    + nextGameTuyokute1 * nextGameTuyokuteCost1
                                    + cheatMode1 * cheatModeCost1
                                    + DLCcontents1 * DLCcontentsCost1);
        transform.Find("TextCurrentNumber").GetComponent<Text>().text = currentFish.ToString();
    }

    public void falseToTrue(string name) {                  ///アイテム追加時追加
        //ローカルからのtrue変更用 
        if (name == "dashSpeed1") { dashSpeed1 = 1; }
        else if (name == "dashSpeed2") { dashSpeed2 = 1; }
        else if (name == "dashSpeed3") { dashSpeed3 = 1; }
        else if (name == "dashSpeed4") { dashSpeed4 = 1; }
        else if (name == "dashMax1") { dashMax1 = 1; }
        else if (name == "o2max1") { o2max1 = 1; }
        else if (name == "o2use1") { o2use1 = 1; }
        else if (name == "o2recovery1") { o2recovery1 = 1; }
        else if (name == "luck1") { luck1 = 1; }
        else if (name == "enemyArea1") { enemyArea1 = 1; }
        else if (name == "life1") { life1 = 1; }
        else if (name == "jumpAble1") { jumpAble1 = 1; }
        else if (name == "trick1") { trick1 = 1; }
        else if (name == "stearing1") { stearing1 = 1; }
        else if (name == "stearSpeedDownNothing1") { stearSpeedDownNothing1 = 1; }
        else if (name == "sharkKill1") { sharkKill1 = 1; }
        else if (name == "nextMap1") { nextMap1 = 1; }
        else if (name == "dolphinColor1") { dolphinColor = 1; }
        else if (name == "fishColor1") { fishColor1 = 1; }
        else if (name == "sharkColor1") { sharkColor1 = 1; }
        else if (name == "nextGame1") { nextGame1 = 1; }
        else if (name == "nextGameTuyokute1") { nextGameTuyokute1 = 1; }
        else if (name == "cheatMode1") { cheatMode1 = 1; }
        else if (name == "DLCcontents1") { DLCcontents1 = 1; }
        else return;
        }

    public void trueToFalse(string name){          ///アイテム追加時追加
        //ローカルからのfalse変更用 
        if (name == "dashSpeed1") { dashSpeed1 = 0; }
        else if (name == "dashSpeed2") { dashSpeed2 = 0; }
        else if (name == "dashSpeed3") { dashSpeed3 = 0; }
        else if (name == "dashSpeed4") { dashSpeed4 = 0; }
        else if (name == "dashMax1") { dashMax1 = 0; }
        else if (name == "o2max1") { o2max1 = 0; }
        else if (name == "o2use1") { o2use1 = 0; }
        else if (name == "o2recovery1") { o2recovery1 = 0; }
        else if (name == "luck1") { luck1 = 0; }
        else if (name == "enemyArea1") { enemyArea1 = 0; }
        else if (name == "life1") { life1 = 0; }
        else if (name == "jumpAble1") { jumpAble1 = 0; }
        else if (name == "trick1") { trick1 = 0; }
        else if (name == "stearing1") { stearing1 = 0; }
        else if (name == "stearSpeedDownNothing1") { stearSpeedDownNothing1 = 0; }
        else if (name == "sharkKill1") { sharkKill1 = 0; }
        else if (name == "nextMap1") { nextMap1 = 0; }
        else if (name == "dolphinColor1") { dolphinColor = 0; }
        else if (name == "fishColor1") { fishColor1 = 0; }
        else if (name == "sharkColor1") { sharkColor1 = 0; }
        else if (name == "nextGame1") { nextGame1 = 0; }
        else if (name == "nextGameTuyokute1") { nextGameTuyokute1 = 0; }
        else if (name == "cheatMode1") { cheatMode1 = 0; }
        else if (name == "DLCcontents1") { DLCcontents1 = 0; }
        else return;
    }

    public int CostReturn(string name) {                    ///アイテム追加時追加
        if (name == "dashSpeed1") { return dashSpeedCost1; }
        else if (name == "dashSpeed2") { return dashSpeedCost2; }
        else if (name == "dashSpeed3") { return dashSpeedCost3; }
        else if (name == "dashSpeed4") { return dashSpeedCost4; }
        else if (name == "dashMax1") { return dashMaxCost1; }
        else if (name == "o2max1") { return o2maxCost1; }
        else if (name == "o2use1") { return o2useCost1; }
        else if (name == "o2recovery1") { return o2recoveryCost1; }
        else if (name == "luck1") { return luckCost1; }
        else if (name == "enemyArea1") { return enemyAreaCost1; }
        else if (name == "life1") { return lifeCost1; }
        else if (name == "jumpAble1") { return jumpAbleCost1; }
        else if (name == "trick1") { return trickCost1; }
        else if (name == "stearing1") { return stearingCost1; }
        else if (name == "stearSpeedDownNothing1") { return stearSpeedDownNothingCost1; }
        else if (name == "sharkKill1") { return sharkKillCost1; }
        else if (name == "nextMap1") { return nextMapCost1; }
        else if (name == "dolphinColor1") { return dolphinColorCost1; }
        else if (name == "fishColor1") { return fishColorCost1; }
        else if (name == "sharkColor1") { return sharkColorCost1; }
        else if (name == "nextGame1") { return nextGameCost1; }
        else if (name == "nextGameTuyokute1") { return nextGameTuyokuteCost1; }
        else if (name == "cheatMode1") { return cheatModeCost1; }
        else if (name == "DLCcontents1") { return DLCcontentsCost1; }
        else {
            return -1;
        }
    }

    public string getContentName(string name) {    ///アイテム追加時追加
        if (name == "dashSpeed1") { return "基礎スピードアップ"; }
        else if (name == "dashSpeed2") { return "基礎スピードアップ2"; }
        else if (name == "dashSpeed3") { return "基礎スピードアップ3"; }
        else if (name == "dashSpeed4") { return "基礎スピードアップ4"; }
        else if (name == "dashMax1") { return "スピード限界アップ"; }
        else if (name == "o2max1") { return "酸素取得量アップ"; }
        else if (name == "o2use1") { return "酸素消費量ダウン"; }
        else if (name == "o2recovery1") { return "酸素回復速度アップ"; }
        else if (name == "luck1") { return "運アップ"; }
        else if (name == "enemyArea1") { return "敵探索範囲ダウン"; }
        else if (name == "life1") { return "命1アップ"; }
        else if (name == "jumpAble1") { return "海面ジャンプ開放"; }
        else if (name == "trick1") { return "海面トリック開放"; }
        else if (name == "stearing1") { return "回転速度アップ"; }
        else if (name == "stearSpeedDownNothing1") { return "回転時速度ダウン量緩和"; }
        else if (name == "sharkKill1") { return "サメへの攻撃開放"; }
        else if (name == "nextMap1") { return "マップ開放1"; }
        else if (name == "dolphinColor1") { return "肌　赤"; }
        else if (name == "fishColor1") { return "エサ　赤"; }
        else if (name == "sharkColor1") { return "サメ　赤"; }
        else if (name == "nextGame1") { return "強くなくてニューゲーム"; }
        else if (name == "nextGameTuyokute1") { return "強くてニューゲーム"; }
        else if (name == "cheatMode1") { return "チートモード"; }
        else if (name == "DLCcontents1") { return "DLコンテンツその1"; }
        else return "Error : Content name is not found.";
    }

    private void dateSave() {    ///アイテム追加時追加
        /* データセーブ */
        PlayerPrefs.SetInt("currentFish", currentFish);
        /* 能力など */
        PlayerPrefs.SetInt("dashSpeed1", dashSpeed1);
        PlayerPrefs.SetInt("dashSpeed2", dashSpeed2);
        PlayerPrefs.SetInt("dashSpeed3", dashSpeed3);
        PlayerPrefs.SetInt("dashSpeed4", dashSpeed4);
        PlayerPrefs.SetInt("dashMax1", dashMax1);
        PlayerPrefs.SetInt("o2max1", o2max1);
        PlayerPrefs.SetInt("o2use1", o2use1);
        PlayerPrefs.SetInt("o2recovery1", o2recovery1);
        PlayerPrefs.SetInt("luck1", luck1);
        PlayerPrefs.SetInt("enemyArea1", enemyArea1);
        PlayerPrefs.SetInt("life1", life1);
        PlayerPrefs.SetInt("jumpAble1", jumpAble1);
        PlayerPrefs.SetInt("trick1", trick1);
        PlayerPrefs.SetInt("stearing1", stearing1);
        PlayerPrefs.SetInt("stearSpeedDownNothing1", stearSpeedDownNothing1);
        PlayerPrefs.SetInt("sharkKill1", sharkKill1);
        PlayerPrefs.SetInt("nextMap1", nextMap1);
        PlayerPrefs.SetInt("dolphinColor", dolphinColor);
        PlayerPrefs.SetInt("fishColor1", fishColor1);
        PlayerPrefs.SetInt("sharkColor1", sharkColor1);
        PlayerPrefs.SetInt("nextGame1", nextGame1);
        PlayerPrefs.SetInt("nextGameTuyokute1", nextGameTuyokute1);
        PlayerPrefs.SetInt("cheatMode1", cheatMode1);
        PlayerPrefs.SetInt("DLCcontents1", DLCcontents1);
    }

    public void contentTouch(string name) {    ///アイテム追加時追加
        if (name == "dashSpeed1") nowItem=100;
        else if (name == "dashSpeed2") { nowItem = 101; }
        else if (name == "dashSpeed3") { nowItem = 102; }
        else if (name == "dashSpeed4") { nowItem = 103; }
        else if (name == "dashMax1") { nowItem = 200; }
        else if (name == "o2max1") { nowItem = 300; }
        else if (name == "o2use1") { nowItem = 400; }
        else if (name == "o2recovery1") { nowItem = 500; }
        else if (name == "luck1") { nowItem = 600; }
        else if (name == "enemyArea1") { nowItem = 700; }
        else if (name == "life1") { nowItem = 800; }
        else if (name == "jumpAble1") { nowItem = 900; }
        else if (name == "trick1") { nowItem = 1000; }
        else if (name == "stearing1") { nowItem = 1100; }
        else if (name == "stearSpeedDownNothing1") { nowItem = 1200; }
        else if (name == "sharkKill1") { nowItem = 1300; }
        else if (name == "nextMap1") { nowItem = 1400; }
        else if (name == "dolphinColor1") { nowItem = 1500; }
        else if (name == "fishColor1") { nowItem = 1600; }
        else if (name == "sharkColor1") { nowItem = 1700; }
        else if (name == "nextGame1") { nowItem = 1800; }
        else if (name == "nextGameTuyokute1") { nowItem = 1900; }
        else if (name == "cheatMode1") { nowItem = 2000; }
        else if (name == "DLCcontents1") { nowItem = 2100; }
        else nowItem = -1; ;
    }

    private void contentInfoUpdata() {    ///アイテム追加時追加
        if (nowItem == 0) {
            textArea.GetComponent<Text>().text = "";
        }
        else if (nowItem == 100)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 101)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 102)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 103)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 200)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 300)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 400)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 500)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 600)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 700)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 800)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 900)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1000)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1100)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1200)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1300)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1400)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1500)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1600)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1700)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1800)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 1900)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 2000)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else if (nowItem == 2100)
        {
            textArea.GetComponent<Text>().text = "移動速度が少し上がる。必要なエサの数：10";
        }
        else
        {
            textArea.GetComponent<Text>().text = "Error : ItemInfometion not found.";
        }

    }
}
