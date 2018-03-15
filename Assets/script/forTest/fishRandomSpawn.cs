using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*ランダムな座標に小魚を配置*/
public class fishRandomSpawn : MonoBehaviour
{
    /*X -250~250
     *Y 0~150
     *Z -250~250
     *
     */
    private Vector3 posi;
    private int fishMax;
    private int fieldx;
    private int fieldy;
    private int fieldz;
    private float posiX;
    private float posiY;
    private float posiZ;
    private bool generated;
    /* 対象オブジェクト */
    public GameObject fishG1;
    public GameObject fishG2;
    public GameObject fishG3;
    public GameObject fishG4;
    public GameObject fishG5;
    public GameObject fishG6;
    public GameObject fishG7;
    public GameObject fishG8;
    private GameObject fish;

    GameObject[] existFish;

    void Start()
    {
        generated = false;
        fieldx = /*(int)Terrain.activeTerrain.terrainData.size.x*/500;
        fieldy = 150;
        fieldz = /*(int)Terrain.activeTerrain.terrainData.size.z*/500;
        fishMax = 100;
        existFish = new GameObject[fishMax];
    }

    void Update()
    {
        //	Random.Range
        Generate();

    }

    void Generate()
    {
        if (!generated)
        {
            for (int fishCount = 0; fishCount < existFish.Length; fishCount++)
            {
                if (existFish[fishCount] == null)
                {
                    posi = new Vector3(Random.Range(-fieldx / 2 + 10, fieldx / 2 - 10), Random.Range(10, fieldy - 10), Random.Range(-fieldz / 2 + 10, fieldz / 2 - 10));
                    /*       posiX = Random.Range(-fieldx / 2 + 10, fieldx / 2 - 10);
                           posiZ = Random.Range(-fieldz / 2 + 10, fieldz / 2 - 10);
                           posiY = Random.Range(Terrain.activeTerrain.terrainData.GetInterpolatedHeight(posiX, posiZ)+10,fieldy-10);
                           posi = new Vector3(posiX, posiY, posiZ);
                      */     //敵を作成する
                    fishGrade();
                    existFish[fishCount] = Instantiate(fish, posi, transform.rotation) as GameObject;
                    return;
                }
            }
        }
        generated = true;
    }

    void fishGrade() {
        int i;
        i = Random.Range(0, 1000);
        if (i < 50) {
            fish = fishG3;
        }else if (i < 200)
        {
            fish = fishG2;
        }
        else{
            fish = fishG1;
        }
        
    }
}
