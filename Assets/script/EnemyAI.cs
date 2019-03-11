using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    private int myType=0;         //アクティブか、ノンアクティブか
    /* myType   type        examples
     * 0        neutral     初期化状態
     * 1        active      サメ・人間
     * 2        nonactive   マンボウ、クジラ、フグ
     * 3        goaway      船
     * 4        nonmove     釣り針、機雷、障害物、クラゲ、ごみ
     * 100      error       エラー
     */
    private Rigidbody enemyRigidbody;
	private Vector3 destination;	//目的地
	private Vector3 Sdestination;
	private float speed;	//スピード値
	private Vector3 current;			//縄張りの中心
	private bool arrived;		//目的地到着フラグ
    private float arrivedRange=1.5f; //到着フラグ発生距離
    private float chaseRange = 100;
	private float waitTime;		//待ち移行時間
	private float currentTime;	//現在時間
	private int state;		//モード
	private GameObject Player;		//ゲームオブジェクトPlayer取得
	private int flameCount;
	private Vector3 randDestination;  //ランダム座標
    /* アイコン */
    private samonIcon samonicon;

    /*モードの種類
	walk,			//徘徊		0
	wait,			//待機		1
	chase,			//Player追	2
	*/
/*------------------------------スタート-----------------------------------*/
	void Start () {
        if (transform.name == "shark" || transform.name == "human") {
            myType = 1;
        } else if (transform.name == "manbou" || transform.name == "kuzira" || transform.name == "hugu") {
            myType = 2;
        } else if (transform.name == "ship") {
            myType = 3;
        } else if (transform.name == "nidle" || transform.name == "kirai" || transform.name == "jammer" || transform.name == "kurage" || transform.name == "garbage") {
            myType = 4;
        } else if (transform.name =="enemy") {
            myType = 5;
        } else {
            myType = 100;
        }
        enemyRigidbody = GetComponent<Rigidbody>();
		Player = GameObject.FindWithTag("Player");		//ゲームオブジェクトPlayerを見つけてPlayerに格納
		speed = 10.0f;					//移動スピード設定 パーセント表記
		arrived = false;				//arrived初期化　目的地についていない
		waitTime = 200.0f;				//待ち移行時間初期化
		currentTime = 0.0f;				//現在時間初期化
		setState(/*walk*/0);			//モードをwalk、目的地の設定
		flameCount = 0;
		current = transform.position;//スポーン地点を縄張りの中心に設定
		destination = transform.position;
        /* アイコンスクリプト取得 */
        samonicon = gameObject.GetComponent<samonIcon>();
	}
/*------------------------------アップデート-----------------------------------*/
	void Update () {
        if (myType == 0) {
            neutralUpdate();
        }else if (myType == 1) {
            activeUpdate();
        }else if (myType == 2) {
            nonactiveUpdate();
        }else if (myType == 3) {
            goawayUpdate();
        }else if (myType == 4) {
            nonmoveUpdate();
        }else if (myType == 100) {
            errorUpdate();
        }else if (myType==5){
            activeUpdate();
        } else {
            return;
        }
    }

    private void neutralUpdate() {
        Debug.Log("There is neutral enemy.");
        Destroy(gameObject);
    }

    private void activeUpdate() {
        flameCount++;
        chaseArea();
        if (Vector3.Distance(destination, transform.position) <= 3) {
            arrived = true;                     //目的地到着
        } else {
            arrived = false;
        }
        if (!arrived) {   //目的地に到着していない
            if (state == /*walk*/0) {           //モードはwalkか
                walk(destination);              //　見回り、目的地を再設定
            } else if (state == 2) {    //モードがchase
                walk(Player.transform.position);                //　プレイヤーを目的地に設定
            }
        } else if (arrived) {                       //目的地に到着している
            if (state == 0) {
                setState(1);
            } else if (state == 2) {
                setState(1);
            }
        }
        if (state == /*wait*/1) {           //モードがwaitか
            wait();             //待ち関数呼び出し
        }
    }

    private void nonactiveUpdate() {
        flameCount++;
        //chaseArea();
        if (Vector3.Distance(destination, transform.position) <= 3)
        {
            arrived = true;                     //目的地到着
        }
        else
        {
            arrived = false;
        }
        if (!arrived)
        {   //目的地に到着していない
            if (state == /*walk*/0)
            {           //モードはwalkか
                walk(destination);              //　見回り、目的地を再設定
            }
            else if (state == 2)
            {    //モードがchase
                walk(Player.transform.position);                //　プレイヤーを目的地に設定
            }
        }
        else if (arrived)
        {                       //目的地に到着している
            if (state == 0)
            {
                setState(1);
            }
            else if (state == 2)
            {
                setState(1);
            }
        }
        if (state == /*wait*/1)
        {           //モードがwaitか
            wait();             //待ち関数呼び出し
        }
    }

    private void goawayUpdate() {
        //未実装
    }

    private void nonmoveUpdate() {
        //未実装
    }

    private void errorUpdate() {
        Debug.Log("There is Error Object.");
        Destroy(gameObject);
    }

    /*------------------------------モードチェンジ-----------------------------------*/
    void setState(int nextState) {
		if(nextState == /*walk*/0) {		//walkで呼び出されたか
			arrived = false;				//目的地についていない
			state = /*walk*/0;				//モードをwalkに
			destination = setDestination();		//目的地再設定
            if (myType == 1) {
                samonicon.IconSwitchFlg(2, true);   //アイコンを２番に設定
                samonicon.IconFlashFlg(false);
            }
		} else if(nextState == /*chase*/2) {			//chaseで呼び出されたか
			state = /*chase*/2;				//モードをchaseに設定
			arrived = false;				//　待機状態から追いかける場合もあるのでOff		目的地についていない
			destination = setDestination(Player.transform.position);		//目的地再設定
            samonicon.IconFlashFlg(true);   //アイコンをフラッシュさせる
		} else if(nextState == /*wait*/1) {			//waitで呼び出されたか
			state=/*wait*/1;				//モードをwaitに設定
			arrived = true;					//目的地に到着
			currentTime = 0.0f;				//currentTime初期化
            if (myType == 1) {
                samonicon.IconSwitchFlg(1, true);   //アイコンを１番に設定
                samonicon.IconFlashFlg(false);
            }
			wait();
		}
	}
/*------------------------------歩き回る-----------------------------------*/
	void walk(Vector3 position) {				//歩行関数
		Vector3 target = setDestination(position);        //目的地設定
        transform.LookAt(target);           //移動方向を向く
        if (flameCount%50!=0){
            enemyRigidbody.velocity = transform.forward * speed;
		}else{
			enemyRigidbody.velocity = Vector3.zero;
		}
		Debug.Log ("walk");
	}
/*------------------------------止まる-----------------------------------*/
	void wait (){
		state=/*wait*/1;						//モードをwaitに
		currentTime+=1; 							//時間経過
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Debug.Log ("wait");
		if(waitTime < currentTime) {			//waitTimeを過ぎたか
			currentTime = 0.0f;				//currentTime初期化
			setState(/*walk*/0);			//モードをwalkに移行
			Debug.Log("toWalk");
		}
	}
/*------------------------------縄張り-----------------------------------*/
	void chaseArea(){
		if(Vector3.Distance(Player.transform.position, transform.position) < arrivedRange || Vector3.Distance(Player.transform.position, transform.position) >= chaseRange){
            if (state == 2)
            {
                setState(1);            //waitになる
            }
        }else if(Vector3.Distance(Player.transform.position, transform.position) < chaseRange){
            if (state == 0)
            {
                setState(2);            //chaseになる
            }
        }
	}
/*------------------------------ランダムな目的地を設定-----------------------------------*/
	public Vector3 setDestination() {
		Sdestination = current; //目的地の基準を縄張りの中心に設定
		randDestination = Random.insideUnitSphere * 20;//ランダムな位置の設定
		Sdestination += randDestination; //目的地を設定
        return Sdestination;//目的地を設定
	}
/*------------------------------入力値を目的地にする-----------------------------------*/
	public Vector3 setDestination(Vector3 position) {
		Sdestination = position;//入力値を目的地に設定
		return Sdestination;//目的地を設定
	}
}