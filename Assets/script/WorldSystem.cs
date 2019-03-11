using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//時間経過による太陽の移動
public class WorldSystem : MonoBehaviour {

    /* 注意 */
    //世界設定上はｚ軸方向が北、ｘ軸方向が東、y軸方向が上
    /* 日本での太陽の角度は春分・秋分が55、夏至が78.4、冬至が31.6
     * 設定上の場所と、実際の時刻より決定する。
     * 実際の時刻を使うか、春夏秋冬で適当に選ぶか、指定した日付で動かすか選ばせる
     */
    private GameObject sunLight;
    private GameObject earth;
    private float dayCycle;   //分単位
    private readonly int daytime = 24;  //時間単位
    private float nowTime;
    public float sunAltitude;           //南中高度　0 <= sunAlt <= 90
    private float xRote=55;
    private float zRote=0;

	void Start () {
        sunLight = GameObject.FindGameObjectWithTag("Sunlight");
        earth = GameObject.Find("earth");
        nowTime = 0;
        dayCycle = 0.5f;
	}
	
	void Update () {
        nowTime += Time.deltaTime;
        if (nowTime >= dayCycle * 60) {
            nowTime -= dayCycle * 60;
        }
//        earth.transform.eulerAngles= new Vector3(earth.transform.localEulerAngles.x, earth.transform.localEulerAngles.y, nowTime / (dayCycle * 60) * 360);
//        earth.transform.rotation=new Quaternion(earth.transform.rotation.x, earth.transform.rotation.y, nowTime / (dayCycle * 60), earth.transform.rotation.w);
//        sunLight.transform.localEulerAngles=new Vector3(sunLight.transform.localEulerAngles.x, nowTime / (dayCycle*60)*360, sunLight.transform.localEulerAngles.z);
        Altitude();
        transform.localEulerAngles = new Vector3(xRote, 0, 0);
        earth.transform.eulerAngles = new Vector3(0, 0, zRote);
    }

    private void Altitude() {
        zRote = nowTime / (dayCycle * 60) * 360;
        xRote = Mathf.Abs(Mathf.Sin(nowTime / (dayCycle * 60) * Mathf.PI)) * (90 - sunAltitude) + sunAltitude;
    }
}
