﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityStandardAssets;
using UnityStandardAssets.ImageEffects;
//VRサポート
using UnityEngine.XR;

/* ジャイロで回転させる + playerの移動 */
public class playerControl : MonoBehaviour
{

    /*private GUIStyle labelStyle;
    Quaternion start_gyro;*/
    Quaternion gyro;

    /* camera初期化用 */
    public GameObject FPScamera;
    public GameObject TPScamera;
    private bool shiftchanged;
    private float HMDShiftKaku;
    private float HMDCurveKaku;
    private float HMDkaku;
    private float HMDpercent;

    private Rigidbody playerRigid;
    private int speedCount;
    private int count;//時間計測
    private int speedMode;//勝手に進むときの速度
    // 0 通常
    // 1 加速状態
    private float modeSpeed;
    public int speedModeChagable;//首右傾き、E入力時の加速　首左傾き、Q入力時の減速
    // 0　停止
    // 1　ゆっくり
    // 2　普通
    // 3　はやい
    private float changableSpeed;

    /* 速度制限 */
    private float maxV;
    private float maxSqrV;

    /* Android用 */
    private int countForAdd;
    private bool waitForAdd;
    private int countForSub;
    private bool waitForSub;
    private GUIStyle labelStyle;
    private bool changed;
    private Vector3 localRota;
    private Vector3 startLocalRote;
    private Quaternion worldRote;
    private Quaternion startWorldRote;

    /* lateStart用 */
    private bool late;
    private int countLate;

    /* ポーズモード */
    public bool pose;

    /* 海面加速不可用 */
    private GameObject sea;
    private bool notAddForce;

    /* ポーズ時用頷き確認 */
    private int unazukiCount;
    private bool shakeFlg;
    private bool timeCounting;
    private int timecount;
    private bool shaked;

    /* ミニマップ配置用 */
    public GameObject miniMap;

    /* 空腹度 */
    private float runDistance;      //移動距離
    private float hungryDistance;   //おなかがすく距離（一定距離でおなかがすく）
    private Vector3 wasPosition;    //前の座標（今の座標と比較して移動距離を算出）

    /* ライフ */
    //    protected int lifePoint;          //体力の総量
    //    private int lifeMax;            //体力上限

    /* 酸素 */
    private int o2Max = 100;
    public int o2 = 100;
    private float o2time = 0;

    /* ダメージノックバック */
    //private bool knockBacking = false;
    private Vector3 knockBackVec = Vector3.zero;

    /* プレイモード */
    private string playMode;
    /* vivekey  vive + キーボード                実装済み
     * vivecon  vive + viveコントローラー        未実装
     * vivepad  vive + ゲームパッド              未実装
     * oclukey  oculus + キーボード              実装済み
     * oclucon  oculus + oculusコントローラー    未実装
     * oclupad  oculus + ゲームパッド            未実装
     * pconkey  pc + キーボード                  実装済み
     * pconpad  pc + ゲームパッド                未実装
     * iphovri  iphone + vr + ジョイスティック   未実装
     * iphojoi  iphone + ジョイスティック        未実装
     * iphojai  iphone + ジャイロ操作            未実装
     * andrvri  android + vr + ジョイスティック  未実装
     * andrjoi  android + ジョイスティック       未実装
     * andrjai  android + ジャイロ操作           未実装
     */

    /* Canvas3D 吹き出しUI表示、ポーズ用 */
    private GameObject canvas3D;

    /* joyコン用 */
    public joystick _joystick_move = null;
    public joystickbottun _joystickbottun_down = null;
    public joystickbottun _joystickbottun_up = null;

    void Start()
    {
        /* カメラ処理 */
#if UNITY_EDITOR
        Input.gyro.enabled = false;
#elif UNITY_IOS
            Input.gyro.enabled = true;
#elif UNITY_STANDALONE_OSX
            Input.gyro.enabled = false;
#elif UNITY_STANDALONE_WIN
            Input.gyro.enabled = false;
#elif UNITY_ANDROID
            Input.gyro.enabled = true;        
#else
            Debug.Log("Any other platform");
#endif
        shiftchanged = false;
        //加減速時の頭の傾きの遊び
        HMDShiftKaku = 7f;
        //曲がるときの頭の傾きの遊び
        HMDCurveKaku = 10f;
        /* カメラ処理end */

        /* test用 */
        PlayerPrefs.SetFloat("hungryPoint", 100.0f);
        /* camera初期化 */
        if (PlayerPrefs.GetInt("vrValue") == 0)
        {
            /* 通常カメラ */
            XRSettings.enabled = false;
        }
        else
        {
            /* VRモード */
            /* VR有効化 */
            XRSettings.enabled = true;
            /* カメラの位置をプレイヤーの位置にする */
            XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
            /* ヘッドセット位置をカメラの位置に変更 */
            InputTracking.Recenter();
            /* 位置トラッキング無効 */
            InputTracking.disablePositionalTracking = true;
        }
        if (PlayerPrefs.GetInt("tfpsValue") == 0)
        {
            /* TPS */
            FPScamera.SetActive(false);
            TPScamera.SetActive(true);
        }
        else
        {
            /* FPS */
            FPScamera.SetActive(true);
            TPScamera.SetActive(false);
        }

        playerRigid = GetComponent<Rigidbody>();
        modeSpeed = 1f;
        changableSpeed = 1f;
        speedCount = 100;

        speedMode = 0;
        speedModeChagable = 1;

        /* Android用 */
        countForAdd = 0;
        waitForAdd = false;
        countForSub = 0;
        waitForSub = false;
        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 22;
        this.labelStyle.normal.textColor = Color.white;
        changed = false;
        /* Android用回転 */
        localRota = this.gameObject.transform.localEulerAngles;
        startLocalRote = this.gameObject.transform.localEulerAngles;
        worldRote = this.gameObject.transform.rotation;
        startWorldRote = this.gameObject.transform.rotation;
        /* 初期化の値（実測値） */
        /* vector3.x        0
         * vector3.y        270
         * vector3.z        0
         * quatanion.x      0
         * quatanion.y      0.7071068
         * quatanion.z      0
         */
        /* startLate用 */
        late = true;
        countLate = 0;

        /* ポーズ */
        pose = false;

        /* 海面加速不可用 */
        sea = GameObject.FindWithTag("SeaUpper");
        notAddForce = false;

        /* ポーズ時用頷き確認 */
        unazukiCount = 0;
        shakeFlg = false;
        timeCounting = false;
        timecount = 0;
        shaked = false;

        /* 空腹度 */
        runDistance = 0.0f;
        wasPosition = transform.position;
        hungryDistance = 100f;       //腹が減る距離（腹のすきやすさ）

        /* ライフ */
        PlayerPrefs.SetInt("lifeMax", 100);      // 20ずつ数えるゆえにMAX300（一応それ以上も動く）
        PlayerPrefs.SetInt("lifePoint", PlayerPrefs.GetInt("lifeMax"));

        /* Canvas3D */
        canvas3D = GameObject.Find("Canvas3D");
        if (true/* Canvas3D使用か */)
        {
            canvas3D.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position + new Vector3(0, 0, 4);
        }
        else
        {
            canvas3D.SetActive(false);
        }
    }

    void Update()
    {
        if (late)
        {
            lateStart();
        }

        underTheSea();

        /* ポーズ状態用首振り(VR)・解除(nonVR)確認 */
        if (pose)
        {
            if (PlayerPrefs.GetInt("vrValue") >= 1/* VR */)
            {
                isShakeHead();

                if (shaked)
                {
                    toPoseMode(false);
                    shaked = false;
                }
            }
            else if (PlayerPrefs.GetInt("vrValue") <= 0/* nonVR */)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    toPoseMode(false);
                    shaked = false;
                }
            }
        }
        /* 操作方法 */
#if UNITY_EDITOR
        ControllerOnUnityPC();
#elif UNITY_ANDROID
            if(/* Non VRかつジャイロ操作 */true || /* VR */ true){
                cameraController();
            }else if(/* Non VR かつコントローラー操作 */false){
            }
            ControllerOnAndroid();
#elif UNITY_IOS
            if(/* Non VRかつジャイロ操作 */true || /* VR */ true){
                cameraController();
            }else if(/* Non VR かつコントローラー操作 */false){
            }
            ControllerOniOS();
#elif UNITY_STANDALONE_OSX
            ControllerOnUnityPC();
#elif UNITY_STANDALONE_WIN
            ControllerOnUnityPC();
#elif UNITY_PS4
            ControllerOnPS4();
#else
            Debug.Log("Any other platform");
        
            ControllerOnWin();
            ControllerOnOSX();
#endif

        //勝手に前に進む(ただし、海面より下の時のみ)
        if (!notAddForce)
        {
            if (PlayerPrefs.GetFloat("hungryPoint") > 0)
            {                                      //通常状態
                playerRigid.velocity = transform.forward * modeSpeed * changableSpeed * 1.0f;
            }
            else if (PlayerPrefs.GetFloat("hungryPoint") <= 0)
            {                              //空腹時
                playerRigid.velocity = transform.forward * modeSpeed * changableSpeed * 0.2f;
            }

        }

        //一定時間同じ方向で加速
        if (speedMode == 0)
        {
            if (speedCount > count)
            {
                count++;
            }
            else
            {
                speedMode = 1;//加速状態に変更
                count = 0;  //加速カウントをゼロ
            }
        }
        if (speedMode == 0)
        {
            modeSpeed = 2f;
        }
        else if (speedMode == 1)
        {
            modeSpeed = 2f;
        }

        /* global fog */
        /*        if (RenderSettings.fog)
                {
                    float a = 300 - 300 * (160 - transform.position.y) / 160;
                    if (transform.position.y >= 160) {
                        RenderSettings.fog = false;
                    }
                }
                else if (!RenderSettings.fog) {
                    if (transform.position.y < 160) {
                        //RenderSettings.fog = true;
                        RenderSettings.fogDensity=0.5f;
                    }
                }*/

        /* Camera Fog */
        /*if (PlayerPrefs.GetInt("vrValue") >= 1) {
            GameObject Camera =GameObject.Find("thirdPersonCamera")
        }
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (transform.position.y >= 160) {
            camera.GetComponent<> ().DistanceFog = false;
        }*/

        /* 移動距離算出 */
        runDistance += Vector3.Distance(wasPosition, transform.position);
        wasPosition = transform.position;
        if (runDistance >= hungryDistance)
        {
            PlayerPrefs.SetFloat("hungryPoint", PlayerPrefs.GetFloat("hungryPoint") - 1f);
            runDistance = 0;
        }

        /* 体力管理 */
        lifeAdminister();

        /* 酸素管理 */
        O2Administer();

        /* ノックバック */
        if (knockBackVec != Vector3.zero)
        {
            knockBack();
        }
    }

    /* 海面越え対策 */
    private void underTheSea()
    {
        /* 海面より上なら重力+加速禁止 */
        if (transform.position.y >= sea.transform.position.y)
        {
            playerRigid.useGravity = true;
            Physics.gravity = new Vector3(0, -9.81f * 2, 0);
            notAddForce = true;
            O2Administer(1);
        }
        else
        {
            playerRigid.useGravity = false;
            //Physics.gravity = new Vector3(0, -9.81f * 0.4f,0);
            notAddForce = false;
        }
    }

    private void ControllerOnAndroid()
    {
        /* Android用 */
        localRota = this.gameObject.transform.localEulerAngles;     //x,z軸用
        worldRote = this.gameObject.transform.rotation;             //y軸用

        /* localRote.ｘは、初期値0
         * 端末を上に傾けると360～350などになる
         * 端末を下に傾けると0～10などになる
         * /
        /* 動いた処理（減速モードへ） */
        if ((localRota.x > 10 || localRota.x < 350) || Mathf.Abs(worldRote.y - startWorldRote.y) >= 0.2)
        {
            speedMode = 0;
            count = 0;
        }

        /* localRote.zは、初期値0
         * 端末を右に傾けると360～350などになる
         * 端末を左に傾けると0～10などになる
         * /
        /* 加速処理 */
        if (localRota.z >= 180 && localRota.z < 340)
        {
            if (!waitForAdd)
            {
                waitForAdd = true;
            }
            else if (waitForAdd)
            {
                if (countForAdd >= 30 && !changed)
                {
                    //E
                    if (speedModeChagable < 3)
                    {
                        speedModeChagable++;
                        changed = true;
                    }
                    countForAdd = 0;
                    waitForAdd = false;
                }
            }
        }
        if (localRota.z >= 350 || localRota.z < 180)
        {
            changed = false;

        }
        if (waitForAdd)
        {
            countForAdd++;
            if (countForAdd > 200)
            {
                countForAdd = 0;
                waitForAdd = false;
            }
        }
        /* 減速処理 */
        if (localRota.z <= 180 && localRota.z > 20)
        {
            if (!waitForSub)
            {
                waitForSub = true;
            }
            else if (waitForSub)
            {
                if (countForSub >= 30 && !changed)
                {
                    //Q
                    if (speedModeChagable > 0)
                    {
                        speedModeChagable--;
                        changed = true;
                    }
                    countForSub = 0;
                    waitForSub = false;
                }
            }

        }
        if (localRota.z < 10 || localRota.z > 180)
        {
            changed = false;
        }
        if (waitForSub)
        {
            countForSub++;
            if (countForSub > 200)
            {
                countForSub = 0;
                waitForSub = false;
            }
        }
        /* 速度反映処理 */
        if (speedModeChagable == 0)
        {
            changableSpeed = 0;
            playerRigid.velocity = Vector3.zero;
        }
        else if (speedModeChagable == 1)
        {
            changableSpeed = 1f;
        }
        else if (speedModeChagable == 2)
        {
            changableSpeed = 5f;
        }
        else if (speedModeChagable == 3)
        {
            changableSpeed = 10f;
        }

    }
    private void ControllerOniOS()
    {
        /* Android用 */
        localRota = this.gameObject.transform.localEulerAngles;     //x,z軸用
        worldRote = this.gameObject.transform.rotation;             //y軸用

        /* localRote.ｘは、初期値0
         * 端末を上に傾けると360～350などになる
         * 端末を下に傾けると0～10などになる
         * /
        /* 動いた処理（減速モードへ） */
        if ((localRota.x > 10 || localRota.x < 350) || Mathf.Abs(worldRote.y - startWorldRote.y) >= 0.2)
        {
            speedMode = 0;
            count = 0;
        }

        /* localRote.zは、初期値0
         * 端末を右に傾けると360～350などになる
         * 端末を左に傾けると0～10などになる
         * /
        /* 加速処理 */
        if (localRota.z >= 180 && localRota.z < 340)
        {
            if (!waitForAdd)
            {
                waitForAdd = true;
            }
            else if (waitForAdd)
            {
                if (countForAdd >= 30 && !changed)
                {
                    //E
                    if (speedModeChagable < 3)
                    {
                        speedModeChagable++;
                        changed = true;
                    }
                    countForAdd = 0;
                    waitForAdd = false;
                }
            }
        }
        if (localRota.z >= 350 || localRota.z < 180)
        {
            changed = false;

        }
        if (waitForAdd)
        {
            countForAdd++;
            if (countForAdd > 200)
            {
                countForAdd = 0;
                waitForAdd = false;
            }
        }
        /* 減速処理 */
        if (localRota.z <= 180 && localRota.z > 20)
        {
            if (!waitForSub)
            {
                waitForSub = true;
            }
            else if (waitForSub)
            {
                if (countForSub >= 30 && !changed)
                {
                    //Q
                    if (speedModeChagable > 0)
                    {
                        speedModeChagable--;
                        changed = true;
                    }
                    countForSub = 0;
                    waitForSub = false;
                }
            }

        }
        if (localRota.z < 10 || localRota.z > 180)
        {
            changed = false;
        }
        if (waitForSub)
        {
            countForSub++;
            if (countForSub > 200)
            {
                countForSub = 0;
                waitForSub = false;
            }
        }
        /* 速度反映処理 */
        if (speedModeChagable == 0)
        {
            changableSpeed = 0;
            playerRigid.velocity = Vector3.zero;
        }
        else if (speedModeChagable == 1)
        {
            changableSpeed = 1f;
        }
        else if (speedModeChagable == 2)
        {
            changableSpeed = 5f;
        }
        else if (speedModeChagable == 3)
        {
            changableSpeed = 10f;
        }

    }
    private void ControllerOnWin()
    {
        if (speedModeChagable == 0)
        {
            changableSpeed = 0;
            playerRigid.velocity = Vector3.zero;
        }/*
        else if (speedModeChagable == 1)
        {
            changableSpeed = 1f;
        }
        else if (speedModeChagable == 2)
        {
            changableSpeed = 5f;
        }
        else if (speedModeChagable == 3)
        {
            changableSpeed = 40f;
        }*/
        else if (speedModeChagable <= 5)
        {
            changableSpeed = (float)speedModeChagable;
        }
        else if (speedModeChagable <= 10)
        {
            changableSpeed = (float)speedModeChagable * 2.5f;
        }
        else if (speedModeChagable <= 15)
        {
            changableSpeed = (float)speedModeChagable * 3 - 5;
        }

        if (!pose)
        {
            /* Q E　による加減速 */
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (speedModeChagable > 0)
                {
                    if (speedModeChagable == 1)
                    {
                        if (true/**ショップ拡張有効か**/)
                        {
                            speedModeChagable--;
                        }
                    }
                    else
                    {
                        speedModeChagable--;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (speedModeChagable < 15/**ショップ拡張より*/)
                {
                    speedModeChagable++;
                }
            }

            //左入力で左回転
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.gameObject.transform.localEulerAngles += new Vector3(0, -1, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                speedMode = 0;
                count = 0;
            }
            //右入力で右回転
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.gameObject.transform.localEulerAngles += new Vector3(0, 1, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                speedMode = 0;
                count = 0;
            }
            //下入力で下に回転
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.gameObject.transform.localEulerAngles += new Vector3(1, 0, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                speedMode = 0;
                count = 0;
            }
            //上入力で上に回転
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.gameObject.transform.localEulerAngles += new Vector3(-1, 0, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                speedMode = 0;
                count = 0;
            }
        }
    }
    private void ControllerOnOSX()
    {

    }
    private void ControllerOnUnityPC()
    {
        if (speedModeChagable == 0)
        {
            changableSpeed = 0;
            playerRigid.velocity = Vector3.zero;
        }/*
        else if (speedModeChagable == 1)
        {
            changableSpeed = 1f;
        }
        else if (speedModeChagable == 2)
        {
            changableSpeed = 5f;
        }
        else if (speedModeChagable == 3)
        {
            changableSpeed = 40f;
        }*/
        else if (speedModeChagable <= 5)
        {
            changableSpeed = (float)speedModeChagable;
        }
        else if (speedModeChagable <= 10)
        {
            changableSpeed = (float)speedModeChagable * 2.5f;
        }
        else if (speedModeChagable <= 15)
        {
            changableSpeed = (float)speedModeChagable * 3 - 5;
        }

        /* 操作系 */
        if (!pose)
        {
            /* nonVR */
            if (PlayerPrefs.GetInt("vrValue") <= 0/* nonVR */)
            {
                /* Q E　による加減速 */
                if (Input.GetKeyDown(KeyCode.Q) ||/*_joystickbottun_down.IsPushDown()*/false)
                {
                    if (speedModeChagable > 0)
                    {
                        if (speedModeChagable == 1)
                        {
                            if (true/**ショップ拡張有効か**/)
                            {
                                speedModeChagable--;
                            }
                        }
                        else
                        {
                            speedModeChagable--;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.E) ||/*_joystickbottun_up.IsPushDown()*/false)
                {
                    if (speedModeChagable < 15/**ショップ拡張より*/)
                    {
                        speedModeChagable++;
                    }
                }

                //左入力で左回転
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.gameObject.transform.localEulerAngles += new Vector3(0, -1, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //右入力で右回転
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.gameObject.transform.localEulerAngles += new Vector3(0, 1, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //下入力で下に回転
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.gameObject.transform.localEulerAngles += new Vector3(1, 0, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //上入力で上に回転
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.gameObject.transform.localEulerAngles += new Vector3(-1, 0, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //if (_joystick_move.Position.x!=0||_joystick_move.Position.y!=0) {
                //    this.gameObject.transform.localEulerAngles+=new Vector3(_joystick_move.Position.y*-1, _joystick_move.Position.x, 0)*( 1+0.5f*/**ショップ拡張の回転角*/0 );
                //}
            }
            else if (PlayerPrefs.GetInt("vrValue") >= 1/* VR */)
            {
                /* Q E　による加減速 */
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (speedModeChagable > 0)
                    {
                        if (speedModeChagable == 1)
                        {
                            if (true/**ショップ拡張有効か**/)
                            {
                                speedModeChagable--;
                            }
                        }
                        else
                        {
                            speedModeChagable--;
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    if (speedModeChagable < 15/**ショップ拡張より*/)
                    {
                        speedModeChagable++;
                    }
                }
                else if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.z >= 0 + HMDShiftKaku && InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.z <= 90)
                {
                    if (!shiftchanged)
                    {
                        if (speedModeChagable > 0)
                        {
                            if (speedModeChagable == 1)
                            {
                                if (true/* ショップ拡張でストップが有効か */)
                                {
                                    speedModeChagable--;
                                    shiftchanged = true;
                                }
                            }
                            else
                            {
                                speedModeChagable--;
                                shiftchanged = true;
                            }
                        }
                    }
                }
                else if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.z >= 270 && InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.z <= 360 - HMDShiftKaku)
                {
                    if (!shiftchanged)
                    {
                        if (speedModeChagable < 15/**ショップ拡張より*/)
                        {
                            speedModeChagable++;
                            shiftchanged = true;
                        }
                    }
                }
                else if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.z >= 360 - HMDShiftKaku || InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.z <= 0 + HMDShiftKaku)
                {
                    if (shiftchanged)
                    {
                        shiftchanged = false;
                    }
                }

                //左入力で左回転
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.gameObject.transform.localEulerAngles += new Vector3(0, -1, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //右入力で右回転
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.gameObject.transform.localEulerAngles += new Vector3(0, 1, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //下入力で下に回転
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.gameObject.transform.localEulerAngles += new Vector3(1, 0, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //上入力で上に回転
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.gameObject.transform.localEulerAngles += new Vector3(-1, 0, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }

                //HMD左入力で左に回転
                if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y >= 180 && InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y <= 360 - HMDCurveKaku && PlayerPrefs.GetInt("vrValue") >= 1)
                {
                    if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x >= 180) HMDkaku = 360 - InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x;
                    else HMDkaku = InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x;
                    HMDpercent = (360 - InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y) / (360 - InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y + HMDkaku);
                    this.gameObject.transform.localEulerAngles += new Vector3(0, -1 * HMDpercent, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //HMD右入力で右に回転
                if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y <= 180 && InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y >= 0 + HMDCurveKaku && PlayerPrefs.GetInt("vrValue") >= 1)
                {
                    if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x >= 180) HMDkaku = 360 - InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x;
                    else HMDkaku = InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x;
                    HMDpercent = InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y / (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y + HMDkaku);
                    this.gameObject.transform.localEulerAngles += new Vector3(0, 1 * HMDpercent, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //HMD下入力で下に回転
                if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x <= 180 && InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x >= 0 + HMDCurveKaku && PlayerPrefs.GetInt("vrValue") >= 1)
                {
                    if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y >= 180) HMDkaku = 360 - InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y;
                    else HMDkaku = InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y;
                    HMDpercent = InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x / (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x + HMDkaku);
                    this.gameObject.transform.localEulerAngles += new Vector3(1 * HMDpercent, 0, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
                //HMD上入力で上に回転
                if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x >= 180 && InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x <= 360 - HMDCurveKaku && PlayerPrefs.GetInt("vrValue") >= 1)
                {
                    if (InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y >= 180) HMDkaku = 360 - InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y;
                    else HMDkaku = InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.y;
                    HMDpercent = (360 - InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x) / (360 - InputTracking.GetLocalRotation(XRNode.CenterEye).eulerAngles.x + HMDkaku);
                    this.gameObject.transform.localEulerAngles += new Vector3(-1 * HMDpercent, 0, 0) * (1 + 0.5f */**ショップ拡張の回転角*/0);
                    speedMode = 0;
                    count = 0;
                }
            }
        }

    }


    /*　操作方法
     * 前ずっと向く   加速　speedMode=1
     * 曲がると       減速　speedMode=0
     * 首右傾き・E   加速
     * 首左傾き・Q   減速
     * 上を向く・↑　上を向く
     * 下を向く・↓　下を向く
     * 右を向く・→　右を向く
     * 左を向く・←　左を向く
     */
    //ジャイロセンサの値を表示するプログラム
    /*
    void OnGUI()
    {
            float x = Screen.width / 10;
            float y = 0;
            float w = Screen.width * 8 / 10;
            float h = Screen.height / 20;

        for (int i = 0; i < 14; i++)
        {
            y = Screen.height / 14 + h * i;
            string text = string.Empty;

            switch (i)
            {
                case 0://X
                    text = string.Format("local-X:{0}", localRota.x);
                    break;
                case 1://Y
                    text = string.Format("local-Y:{0}", localRota.y);
                    break;
                case 2://Z
                    text = string.Format("local-Z:{0}", localRota.z);
                    break;
                case 3://W
                    text = string.Format("world-X:{0}", worldRote.x);
                    break;
                case 4://スピード
                    text = string.Format("world-Y:{0}", worldRote.y);
                    break;
                case 5://加速
                    text = string.Format("world-Z:{0}", worldRote.z);
                    break;
                case 6://X
                    text = string.Format("startlocal-x:{0}", startLocalRote.x);
                    break;
                case 7://Y
                    text = string.Format("startlocal-Y:{0}", startLocalRote.y);
                    break;
                case 8://Z
                    text = string.Format("startlocal-Z:{0}", startLocalRote.z);
                    break;
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
                    break;
                case 13://W
//                    text = string.Format("   vrmode:{0}", GvrViewMain.GetComponent<GvrViewer>().VRModeEnabled); 
                    break;
                default:
                    throw new System.InvalidOperationException();
            }
            GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);
        }   
    }*/

    private void lateStart()
    {
        countLate++;
        if (countLate >= 10)
        {
            worldRote = this.gameObject.transform.rotation;
            late = false;
            /* ミニマップ配置（立体視システムに干渉されるため） */
            //            GameObject.Instantiate(miniMap);
        }
    }

    /* ポーズになるか解除するか */
    public void toPoseMode(bool inOut)
    {
        if (inOut)
        {
            pose = true;
            speedModeChagable = 0;
        }
        else if (!inOut)
        {
            pose = false;
        }
    }

    /* うなずいているか？ */
    public void isShakeHead()
    {

        /* 首を振った瞬間のみ発動 */
        if ((this.gameObject.transform.localEulerAngles.x >= 25 && this.gameObject.transform.localEulerAngles.x <= 335) && !shakeFlg)
        {
            unazukiCount += 1;        //首振りました
            shakeFlg = true;
            timeCounting = true;//計測開始or続行
            Debug.Log("shaked!!!!");
        }
        if (this.gameObject.transform.localEulerAngles.x < 25 || this.gameObject.transform.localEulerAngles.x > 335)
        {
            shakeFlg = false;
        }

        if (timeCounting)
        {     //計測中かつ
            if (unazukiCount >= 3)
            {   //3回以上首を振っている
                shaked = true;
            }
            else if (timecount > 300)
            {
                unazukiCount = 0;
                timeCounting = false;
            }

            timecount++;
        }
        else if (!timeCounting)
        {
            unazukiCount = 0;
            timecount = 0;
            shaked = false;
        }
        shaked = false;
    }

    private void cameraController()
    {
        Input.gyro.enabled = true;
        if (Input.gyro.enabled)
        {
            gyro = Input.gyro.attitude;
            gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
            this.transform.localRotation = gyro;
            //最初に見ていた向きとゲームの進行方向を合わせる
            //this.transform.localRotation = Quaternion.Euler(0, -start_gyro.y, 0);
        }
    }

    //ライフ管理全般
    private void lifeAdminister()
    {
        if (PlayerPrefs.GetInt("lifePoint") <= 0)
        {
            //ゲームオーバー
            Debug.Log("GameOver");
        }
    }

    /* ダメージや回復を行う */
    public void lifeAdminister(int getPoint)
    {
        PlayerPrefs.SetInt("lifePoint", getPoint + PlayerPrefs.GetInt("lifePoint"));
        if (PlayerPrefs.GetInt("lifePoint") > PlayerPrefs.GetInt("lifeMax"))
        {
            PlayerPrefs.SetInt("lifePoint", PlayerPrefs.GetInt("lifeMax"));
        }
    }

    /* 酸素管理 */
    private void O2Administer()
    {
        o2time += Time.deltaTime;
        /* 酸素消費速度 */
        if (o2time > 0.5f)
        {
            o2--;
            o2time = 0.0f;
        }
        if (o2 <= 0)
        {
            //ゲームオーバー
        }
        //Debug.Log(o2.ToString());
    }
    /* 酸素消費+回復 */
    public void O2Administer(int getO2)
    {
        o2 += getO2;
        if (o2 > o2Max)
        {
            o2 = o2Max;
        }
    }

    /* 衝突処理 対terrain */
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "terrain" || collision.gameObject.tag == "Enemy")
        {
            lifeAdminister(-10);
        }
        speedModeChagable = 0;
        playerRigid.velocity = Vector3.zero;
        foreach (ContactPoint point in collision.contacts)
        {
            print(point.normal);
            knockBackVec = point.normal;
        }
    }
    /* 衝突処理 対bubble */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bubble")
        {
            O2Administer(Random.Range(5, 20));
        }
    }

    /* ノックバック処理 */
    private void knockBack()
    {
        playerRigid.AddForce(knockBackVec * 1000, ForceMode.Acceleration);
        knockBackVec -= knockBackVec.normalized * 0.1f;
        // Debug.Log("call");
    }
}
