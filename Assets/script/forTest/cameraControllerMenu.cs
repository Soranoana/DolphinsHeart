using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* ポーズ画面なら上下左右に
 * かぶってー画面なら上下に首を振れる
 * 実装はplayerオブジェクトに行う
 * playerオブジェクトの子供に同座標のカメラが存在する
 * 首の動いた判定はplayerオブジェクトの回転角を用いる
 * 
 * かぶってー画面においてのみ、次のゲームシーンへの移行もこのスクリプトで行う
 * */
public class cameraControllerMenu : MonoBehaviour
{
    /* 首振り角度 */
    Quaternion gyro;
    /* かぶってー画面用 */
    private bool shakeFlg = false;
    private int count = 0;
    private bool timeCounting = false;
    private int timecount = 0;
    /* lateStart用 */
    private bool late = true;
    private int countLate = 0;


    private GUIStyle labelStyle;

    void Start()
    {
#if UNITY_EDITOR
        this.enabled = false;
#endif


        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 22;
        this.labelStyle.normal.textColor = Color.white;
    }

    void Update()
    {
        if (late)
        {
            lateStart();
        }
        else
        {
            /*そもそもの実装はかぶってー画面とゲーム画面、チュートリアル画面のみ
             * その他のシーンには実装しない
             * ゲーム画面など、今後の拡張などで変わるため、かぶってー画面の時のみ特殊処理を行う
             */
            if (SceneManager.GetActiveScene().name == "pleaseTakeWearVR")
            {
                Input.gyro.enabled = true;
                if (Input.gyro.enabled)
                {
                    gyro = Input.gyro.attitude;
                    gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
                    this.transform.localRotation = gyro;
                }

                /* 首を振った瞬間のみ発動 */
                if ((this.gameObject.transform.localEulerAngles.x >= 25 && this.gameObject.transform.localEulerAngles.x <= 335) && !shakeFlg)
                {
                    count+=1;        //首振りました
                    shakeFlg = true;
                    timeCounting = true;//計測開始or続行
                }
                if (this.gameObject.transform.localEulerAngles.x < 25 || this.gameObject.transform.localEulerAngles.x > 335)
                {
                    shakeFlg = false;
                }

                if (timeCounting)
                {     //計測中かつ
                    if (count >= 3)
                    {   //3回以上首を振っている   ならゲームシーンへ移行
                        SceneManager.LoadScene("cameraTest");
                    }
                    else if (timecount > 300)
                    {
                        count = 0;
                        timeCounting = false;
                    }

                    timecount++;
                }
                else if (!timeCounting)
                {
                    count = 0;
                    timecount = 0;
                }
            }
            else
            {
                Input.gyro.enabled = true;
                if (Input.gyro.enabled)
                {
                    gyro = Input.gyro.attitude;
                    gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
                    this.transform.localRotation = gyro;
                }
            }
        }
    }

    private void lateStart()
    {
        countLate++;
        if (countLate >= 10)
        {
            late = false;
        }
    }

    //ジャイロセンサの値を表示するプログラム
    void OnGUI()
    {
        float x = Screen.width / 10;
        float y = 0;
        float w = Screen.width * 8 / 10;
        float h = Screen.height / 20;

        for (int i = 0; i < 9; i++)
        {
            y = Screen.height / 9 + h * i;
            string text = string.Empty;

            switch (i)
            {
                case 0://X
                    text = string.Format("local-X:{0}", this.gameObject.transform.localEulerAngles.x);
                    break;
                case 1://Y
                    text = string.Format("local-Y:{0}", this.gameObject.transform.localEulerAngles.y);
                    break;
                case 2://Z
                    text = string.Format("local-Z:{0}", this.gameObject.transform.localEulerAngles.z);
                    break;
                case 3://W
                    text = string.Format("count:{0}", count);
                    break;
                case 4://スピード
                    text = string.Format("timecount:{0}", timecount);
                    break;
                case 5://加速
                    text = string.Format("timeCounting:{0}", timeCounting);
                    break;
                case 6://X
                    text = string.Format("shakeFlg:{0}", shakeFlg);
                    break;
                case 7://Y
                    text = string.Format("late:{0}", late);
                    break;
                case 8://Z
                    text = string.Format("countLate:{0}", countLate);
                    break;/*
                case 9://X
                    text = string.Format("startWorld-x:{0}", startWorldRote.x);
                    break;
                case 10://Y
                    text = string.Format("startWorld-Y:{0}", startWorldRote.y);
                    break;
                case 11://Z
                    text = string.Format("startWorld-Z:{0}", startWorldRote.z);
                    break;
                case 12://W
                    text = string.Format("   gear:{0}", speedModeChagable);
                    break;*/
                default:
                    throw new System.InvalidOperationException();
            }

            GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);
        }

    }
}
