using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerprefsStart : MonoBehaviour {

	void Start () {
        prefsSet();
    }

    void Update () {
		
	}

    private void prefsSet() {
        /* データがあるならそのまま
         * ないなら初期化
         * 有無は"data"で確認
         * 中身はintの"1"
         */
        if (!PlayerPrefs.HasKey("data")) {
            //初期化
            PlayerPrefs.SetInt("data", 1);
            //空腹度 float
            PlayerPrefs.SetFloat("hungryPoint", 0);
            //現在のエサ量 int
            PlayerPrefs.SetInt("currentFish", 0);
            //設定系   int
            PlayerPrefs.SetInt("bgmValue", 50);
            PlayerPrefs.SetInt("seValue", 50);
            PlayerPrefs.SetInt("vrValue", 0);
            PlayerPrefs.SetInt("tfpsValue", 0);
            PlayerPrefs.SetInt("byougaValue", 0);
            //以下ショップ画面 すべてint 
            PlayerPrefs.SetInt("dashSpeed1", 0);
            PlayerPrefs.SetInt("dashSpeed2", 0);
            PlayerPrefs.SetInt("dashSpeed3", 0);
            PlayerPrefs.SetInt("dashSpeed4", 0);
            PlayerPrefs.SetInt("dashMax1", 0);
            PlayerPrefs.SetInt("o2max1", 0);
            PlayerPrefs.SetInt("o2use1", 0);
            PlayerPrefs.SetInt("o2recovery1", 0);
            PlayerPrefs.SetInt("luck1", 0);
            PlayerPrefs.SetInt("enemyArea1enemyArea1", 0);
            PlayerPrefs.SetInt("life1", 0);
            PlayerPrefs.SetInt("jumpAble1", 0);
            PlayerPrefs.SetInt("trick1", 0);
            PlayerPrefs.SetInt("stearing1", 0);
            PlayerPrefs.SetInt("stearSpeedDownNothing1", 0);
            PlayerPrefs.SetInt("sharkKill1", 0);
            PlayerPrefs.SetInt("nextMap1", 0);
            PlayerPrefs.SetInt("dolphinColor", 0);
            PlayerPrefs.SetInt("fishColor1", 0);
            PlayerPrefs.SetInt("sharkColor1", 0);
            PlayerPrefs.SetInt("nextGame1", 0);
            PlayerPrefs.SetInt("nextGameTuyokute1", 0);
            PlayerPrefs.SetInt("cheatMode1", 0);
            PlayerPrefs.SetInt("DLCcontents1", 0);

            PlayerPrefs.Save();
        }
    }
}
