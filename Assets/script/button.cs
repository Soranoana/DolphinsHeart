using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour {

    /* buttonToTile */
    private optionItemAdministar parent;
    private sliderAdministar parentSlider;

    void Start () {
        /* buttonToTile */
        parent = GameObject.Find("Canvas").GetComponent<optionItemAdministar>();
        parentSlider = GameObject.Find("Canvas").GetComponent<sliderAdministar>();
    }

    void Update () {
        /* buttonToTile */
    }

    public void Clicking() {
        if (transform.name == "buttonToTitle") {     //ショップシーンからタイトルへ
            /* データセーブ */
            PlayerPrefs.SetInt("currentFish", parent.currentFish);
            /* 能力など */
            PlayerPrefs.SetInt("dashSpeed1", parent.dashSpeed1); 
            PlayerPrefs.SetInt("dashMax1", parent.dashMax1);
            PlayerPrefs.SetInt("o2max1", parent.o2max1);
            PlayerPrefs.SetInt("o2use1", parent.o2use1);
            PlayerPrefs.SetInt("o2recovery1", parent.o2recovery1);
            PlayerPrefs.SetInt("luck1", parent.luck1);
            PlayerPrefs.SetInt("enemyArea1", parent.enemyArea1);
            PlayerPrefs.SetInt("life1", parent.life1);
            PlayerPrefs.SetInt("jumpAble1", parent.jumpAble1);
            PlayerPrefs.SetInt("trick1", parent.trick1);
            PlayerPrefs.SetInt("stearing1", parent.stearing1);
            PlayerPrefs.SetInt("stearSpeedDownNothing1", parent.stearSpeedDownNothing1);
            PlayerPrefs.SetInt("sharkKill1", parent.sharkKill1);
            PlayerPrefs.SetInt("nextMap1", parent.nextMap1);
            PlayerPrefs.SetInt("dolphinColor", parent.dolphinColor);
            PlayerPrefs.SetInt("fishColor1", parent.fishColor1);
            PlayerPrefs.SetInt("sharkColor1", parent.sharkColor1);
            PlayerPrefs.SetInt("nextGame1", parent.nextGame1);
            PlayerPrefs.SetInt("nextGameTuyokute1", parent.nextGameTuyokute1);
            PlayerPrefs.SetInt("cheatMode1", parent.cheatMode1);
            PlayerPrefs.SetInt("DLCcontents1", parent.DLCcontents1);
            PlayerPrefs.Save();         //完全セーブ

            /* シーン遷移 */
            SceneManager.LoadScene("title");
        }
        if (transform.name == "buttonToTitleOnOption"){//オプションシーンからタイトルへ
            /* データセーブ */
            PlayerPrefs.SetInt("bgmValue", parentSlider.bgm);    //音量
            PlayerPrefs.Save();

            /* シーン遷移 */
            SceneManager.LoadScene("title");
        }
        if (transform.name == "ButtonGame")         //タイトルからかぶってー画面、ゲームへ
        {
            if (PlayerPrefs.GetInt("vrValue") == 0)
            {           //通常モード
                SceneManager.LoadScene("cameraTest2");
            }
            else
            {           //VRモード
                        //                SceneManager.LoadScene("pleaseTakeWearVR");
                SceneManager.LoadScene("cameraTest2");

            }
        }
        if (transform.name == "ButtonShop")         //タイトルからショップへ
        {
            SceneManager.LoadScene("shop");
        }
        if (transform.name == "ButtonOption")       //タイトルからオプションへ
        {
            SceneManager.LoadScene("option");
        }
        if (transform.name == "ButtonChutoreal")    //タイトルからチュートリアルへ
        {
            SceneManager.LoadScene("lecture");
        }
        if (transform.name == "ButtonFromTakeWear")    //vrとってーからタイトルへ
        {
            SceneManager.LoadScene("title");
        }
        if (transform.name == "ButtonFromTakeOff")    //vrかぶってーからタイトルへ
        {
            SceneManager.LoadScene("title");
        }
    }
}
