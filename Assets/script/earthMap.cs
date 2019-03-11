using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class earthMap : MonoBehaviour {

    private GameObject parent;
    private Vector3 euler;
    private float sensitivity = 10;
    Vector3 mouse;
    bool drag = false;
    float localX=0;
    float localY=0;
    float attenuation = 1f;             //回転の減衰量
    float stopThreshold = 0.01f;        //停止の閾値

    /* マップ選択、表示 */
    private string selectingMapName = "";
    private string mapInfo = "";
    private bool[] mapDepth = new bool[5];
    private int mapWeather = 0;
    private int mapTime = 0;
    [SerializeField]
    private GameObject dispObject;

    void Start () {
        parent=transform.parent.gameObject;
	}
	
	void Update () {
        if (drag) {
            OnDrag();
        } else {
            if (localY>0) {
                transform.Rotate(new Vector3(0, localY/sensitivity*-1, 0), Space.Self);
                localY-=attenuation;
                if (localY<=stopThreshold) {
                    localY=0;
                }
            } else if (localY<0) {
                transform.Rotate(new Vector3(0, localY/sensitivity*-1, 0), Space.Self);
                localY+=attenuation;
                if (localY>=-stopThreshold) {
                    localY=0;
                }
            }
        }
        euler = transform.localEulerAngles;
        mouse = Input.mousePosition;
    }

    public void OnDrag() {
        localX = Input.mousePosition.y - mouse.y;
        if (localX/sensitivity + transform.localEulerAngles.x > 360) localX -= 360*sensitivity;
        else if (localX/sensitivity + transform.localEulerAngles.x < 0) localX += 360*sensitivity;

        localY = Input.mousePosition.x - mouse.x;
        if (localY / sensitivity + transform.localEulerAngles.x > 360) localY -= 360 * sensitivity;
        else if (localY / sensitivity + transform.localEulerAngles.x < 0) localY += 360 * sensitivity;

        parent.transform.Rotate(new Vector3(localX/sensitivity, 0, 0),Space.Self);
        transform.Rotate(new Vector3(0,localY / sensitivity*-1, 0), Space.Self);
    }

    public void OnDragBegin() {
        drag = true;
    }

    public void OnDragEnd() {
        drag = false;
    }
    

}
