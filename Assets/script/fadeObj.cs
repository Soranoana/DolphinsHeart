using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//任意のオブジェクトのマテリアルを操作して、プレイヤーが近づくにつれてフェードインする
//フェードアウトする方や、フェード距離をいじれる昨日は実装できたらする

public class fadeObj : MonoBehaviour {

    private float fadePoint;
    private GameObject player;  
    private int fade;       //フェード値
    private int paintArea;//描画を始める範囲
    private Vector3 target; //playerとtransform距離ベクトル
    private Material mcolor;    //コンポーネント
    private Color myColor;  //元の色
    private float fadeStop; //フェードの上限値

    void Start() {
        player = GameObject.FindWithTag("Player");
        mcolor = gameObject.GetComponent<MeshRenderer>().material;
        myColor = mcolor.color;
       // Debug.Log(myColor);
        fade = 0;
        paintArea = 10;
        fadeStop = 50f;
    }

    void Update() {
        //fadePoint = paintArea - Mathf.Abs(player.transform.position.z - transform.position.z) + 5;
        target = player.transform.position - transform.position;
        fadePoint = (float)paintArea - target.magnitude + 5;

        if (fadePoint >= 0) {
            fade = (int)(100 * (fadePoint / paintArea));
        } else {
            fade = 0;//透明
        }
        mcolor.color = new Color(myColor.r, myColor.g, myColor.b, 1.0f * fade / 100 * (fadeStop / 100f));
    }
}
