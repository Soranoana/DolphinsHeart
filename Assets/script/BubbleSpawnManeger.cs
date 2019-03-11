using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* ランダムな位置に泡を配置 */
public class BubbleSpawnManeger : MonoBehaviour {

    /*X -250~250
     *Y 0~150
     *Z -250~250
     *
     */
    private Vector3 posi;
    private int bubbleMax;
    private int fieldx;
    private int fieldy;
    private int fieldz;
    private float posiX;
    private float posiY;
    private float posiZ;
    /* 対象オブジェクト */
    public GameObject bubble;

    GameObject[] existBubble;

    void Start () {
        fieldx = 500;
        fieldy = 150;
        fieldz = 500;
        bubbleMax = 100;
        existBubble = new GameObject[bubbleMax];
    }

    void Update() {
        bubbleGenerate();

    }

    private void bubbleGenerate() {
        for (int bubbleCount = 0; bubbleCount < bubbleMax; bubbleCount++) {
            if (existBubble[bubbleCount] == null) {
                if (Random.Range(0, 1000) < 20) {
                    posi = new Vector3(Random.Range(-fieldx / 2 + 10, fieldx / 2 - 10), Random.Range(10, fieldy - 10), Random.Range(-fieldz / 2 + 10, fieldz / 2 - 10));
                    existBubble[bubbleCount] = Instantiate(bubble, posi, transform.rotation) as GameObject;
                    //自立スクリプト付与
                    existBubble[bubbleCount].gameObject.AddComponent<BubbleManege>();
                }
            }
        }
    }
}
