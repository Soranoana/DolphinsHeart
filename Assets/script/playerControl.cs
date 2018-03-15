using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class playerControl : MonoBehaviour {

    /* camera初期化用 */
    public GameObject FPScamera;
    public GameObject TPScamera;
//    public GameObject GvrViewMain;

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

    void Start () {
        /* test用 */
        PlayerPrefs.SetFloat("hungryPoint", 15.0f);
        /* camera初期化 */
        if (PlayerPrefs.GetInt("vrValue") == 0)
        {
            /* 通常カメラ */
            //GvrViewMain.GetComponent<GvrViewer>().VRModeEnabled = false;
        }
        else {
            /* VRモード */
            //GvrViewMain.GetComponent<GvrViewer>().VRModeEnabled = true;
        }
        if (PlayerPrefs.GetInt("tfpsValue") == 0)
        {
            /* TPS */
            FPScamera.SetActive(false);
            TPScamera.SetActive(true);
        }
        else {
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
    }

    void Update()
    {
        if (late)
        {
            lateStart();
        }

        underTheSea();

        /* ポーズ状態用首振り確認 */
        isShakeHead();
        if (pose)
        {
            if (shaked)
            {
                toPoseMode(false);
                shaked = false;
            }
        }
        /* 操作方法 */
#if UNITY_EDITOR
        ControllerOnUnityEditer();
#elif UNITY_IOS
        ControllerOniOS();
#elif UNITY_STANDALONE_OSX
        ControllerOnOSX();
#elif UNITY_STANDALONE_WIN
        ControllerOnWin();
#elif UNITY_PS4
        ControllerOnPS4();
#elif UNITY_ANDROID
        ControllerOnAndroid();
#else
        Debug.Log("Any other platform");
#endif

        //勝手に前に進む(ただし、海面より下の時のみ)
        if (!notAddForce)
        {
            playerRigid.velocity = transform.forward  * modeSpeed * changableSpeed;
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
        float a = 300 - 300 * (160 - transform.position.y) / 160;
        RenderSettings.fogStartDistance = a;
        RenderSettings.fogEndDistance = a + 300;

        /* 移動距離算出 */
        runDistance += Vector3.Distance(wasPosition, transform.position);
        wasPosition = transform.position;
        if (runDistance >= hungryDistance) {
            PlayerPrefs.SetFloat("hungryPoint", PlayerPrefs.GetFloat("hungryPoint") - 1f);
            runDistance = 0;
        }
    }

    /* 海面越え対策 */
    private void underTheSea() {
        /* 海面より上なら重力+加速禁止 */
        if (transform.position.y >= sea.transform.position.y)
        {
            playerRigid.useGravity = true;
            Physics.gravity = new Vector3(0, -9.81f,0);
            notAddForce = true;
        }
        else {
            playerRigid.useGravity =false;
            //Physics.gravity = new Vector3(0, -9.81f * 0.4f,0);
            notAddForce = false;
        }
    }


    private void ControllerOnAndroid() {
        /* Android用 */
        localRota = this.gameObject.transform.localEulerAngles;     //x,z軸用
        worldRote = this.gameObject.transform.rotation;             //y軸用

        /* localRote.ｘは、初期値0
         * 端末を上に傾けると360～350などになる
         * 端末を下に傾けると0～10などになる
         * /
        /* 動いた処理（減速モードへ） */
        if ((localRota.x > 10 || localRota.x < 350)|| Mathf.Abs(worldRote.y - startWorldRote.y) >= 0.2){ 
            speedMode = 0;
            count = 0;
        }

        /* localRote.zは、初期値0
         * 端末を右に傾けると360～350などになる
         * 端末を左に傾けると0～10などになる
         * /
        /* 加速処理 */
        if (localRota.z >= 180 && localRota.z <340) { 
            if (!waitForAdd)
            {
                waitForAdd = true;
            }
            else if (waitForAdd) {
                if (countForAdd >= 30 && !changed) {
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
        if(localRota.z <= 180 && localRota.z > 20)
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
        if (localRota.z < 10 || localRota.z>180) {
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
    private void ControllerOniOS() {

    }
    private void ControllerOnPS4()
    {

    }
    private void ControllerOnWin()
    {

    }
    private void ControllerOnOSX()
    {

    }

    private void ControllerOnUnityEditer()
    {
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
            changableSpeed = 40f;
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
                        if (true/**ショップ拡張有効か**/) {
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
                if (speedModeChagable < 3/**ショップ拡張より*/)
                {
                    speedModeChagable++;
                }
            }

            //左入力で左回転
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.gameObject.transform.localEulerAngles += new Vector3(0, -1, 0)*(1+0.5f*/**ショップ拡張の回転角*/0);
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

    private void lateStart() {
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
    public void toPoseMode(bool inOut) {
        if (inOut)
        {
            pose = true;
            speedModeChagable = 0;
        }
        else if (!inOut) {
            pose = false;
        }
    }

    /* うなずいているか？ */
    public void isShakeHead() {

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
}
