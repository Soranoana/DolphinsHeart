using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

	private GameObject[] MapPoint;
	public GameObject ActivePoint;
	public GameObject worldMap;
	public GameObject CanvasStartWeather;
	public GameObject CanvasStartDayTime;
	public GameObject CanvasStartOptions;
	public GameObject CanvasStartUIpositionStart;
	public GameObject CanvasStartUIposition;
	private GameObject goMap;
	private GameObject MoveObject1;
	private GameObject MoveObject2;
	private GameObject MoveObject3;
	private GameObject MoveTarget1;
	private GameObject MoveTarget2;
	private GameObject MoveTarget3;
	private Vector3 startVector1;
	private Vector3 startVector2;
	private float distanceVector;
	private bool UIfloor;
	//UIが右方向に動くか左方向に動くか。。。左がtrue、右がfalse
	private bool titleTextAlphaChange = false;
	private float uiSpeed = 25f;//UIの移動速度
	private float uiRotate = 70f;//UIが左に行くときの角度
								 //タッチ、クリック操作のためのUIlayer貫通
	public LayerMask UIlayer;

	/* mapinfo用 */
	[SerializeField]
	private GameObject mapName;
	[SerializeField]
	private GameObject mapInfo;
	[SerializeField]
	private GameObject mapGoButton;
	private bool mapInfoUpdate = false;
	private string mapInfoName = "";
	private FieldDataBase dataBase = new FieldDataBase();

	void Start() {
		MapPoint = GameObject.FindGameObjectsWithTag("MapPoint");
		CanvasStartOptions.transform.position = CanvasStartUIposition.transform.position;
		SetStartPosition(CanvasStartDayTime);
		SetStartPosition(CanvasStartWeather);
		goMap = transform.gameObject;
	}

	void Update() {
		if (MoveTarget1 != null || MoveTarget2 != null || MoveTarget3 != null) {
			CanvasMove();
		}
		if (Vector3.Distance(transform.position, ActivePoint.transform.position) <= 0.1) {
			if (worldMap.gameObject.GetComponent<MeshCollider>().enabled == false) {
				worldMap.gameObject.SetActive(true);
				CanvasStartOptions.gameObject.SetActive(true);
				OnMapPointSetActive(true);
			}
			if (Input.GetMouseButtonDown(0)) {
				goMap = GetClickObject();
				if (goMap?.tag != null) {
					if (goMap?.tag == "MapPoint") {
						LoadNextScene();
					} else if (goMap?.tag == "MapOption") {
						LoadNextSceneOption();
					}
				}
			}
		} else {
			if (worldMap.gameObject.GetComponent<MeshCollider>().enabled == true) {
				OnMapPointSetActive(false);
				//worldMap.gameObject.SetActive(false);
				//CanvasStartWeather.gameObject.SetActive(false);
				//CanvasStartDayTime.gameObject.SetActive(false);
				//CanvasStartOptions.gameObject.SetActive(false);
			}
		}
		/* マップ情報書き変え */
		if (mapInfoUpdate) {
			mapInfoDisplay();
		}

	}

	private void OnMapPointSetActive(bool active) {
		for (int i = 0; i < MapPoint.Length; i++) {
			MapPoint[i].SetActive(active);
			MapPoint[i].gameObject.GetComponent<SphereCollider>().enabled = active;
		}
		worldMap.gameObject.GetComponent<MeshCollider>().enabled = active;
	}

	private GameObject GetClickObject() {
		GameObject result = null;
		// 左クリックされた場所のオブジェクトを取得
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit, 1000, UIlayer)) {
			result = hit.collider.gameObject;
		}
		//Debug.Log(result.name);
		return result;
	}

	private void LoadNextScene() {
		if (goMap.name == "MapPointDemo") {
			SceneManager.LoadScene("cameraTest2");
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "") {
			SceneManager.LoadScene("");
			PlayerPrefs.SetInt("", 0);
		}
	}

	private void LoadNextSceneOption() {
		if (goMap.name == "sunny") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "clowdy") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "rainy") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "storm") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "snow") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "morning") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "noon") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "evening") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "night") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "dawn") {
			PlayerPrefs.SetInt("", 0);
		} else if (goMap.name == "MapOptionWeather") {
			OnClickFromOptionsToWeather();
		} else if (goMap.name == "MapOptionDayTime") {
			OnClickFromOptionsToDaytime();
		} else if (goMap.name == "back1") {
			OnClickFromWeatherToOptions();
		} else if (goMap.name == "back2") {
			OnClickFromDaytimeToOptions();
		} else {
			Debug.LogWarning("Unknown Option was selected on Canvas Start in Title Scene.");
		}
	}

	private void SetStartPosition(GameObject gameObject) {
		gameObject.transform.position = CanvasStartUIpositionStart.transform.position;
		gameObject.GetComponent<CanvasGroup>().alpha = 0;
		gameObject.GetComponent<CanvasGroup>().interactable = false;
		gameObject.SetActive(false);
	}

	private void SetPosition(GameObject gameObject) {
		gameObject.transform.position = CanvasStartUIposition.transform.position;
		gameObject.GetComponent<CanvasGroup>().alpha = 1;
		gameObject.GetComponent<CanvasGroup>().interactable = true;
	}

	private void CanvasMove() {
		if (MoveTarget1 != null) {
			distanceVector = Vector3.Distance(MoveTarget1.transform.position, MoveObject1.transform.position);
			MoveObject1.transform.position += ( MoveTarget1.transform.position - MoveObject1.transform.position ) / 100 * uiSpeed;
			alphaRotateChange(distanceVector, 1);
			if (distanceVector <= 0.1f) {
				if (MoveTarget1.name == "CanvasStartUIpositionStart") {
					SetStartPosition(MoveObject1);
				} else if (MoveTarget1.name == "CanvasStartUIposition") {
					SetPosition(MoveObject1);
				}
				MoveTarget1 = null;
				MoveObject1 = null;
			} else {
				if (MoveObject1 != null) {
					MoveObject1.SetActive(true);
				}
			}
		}

		if (MoveTarget2 != null) {
			distanceVector = Vector3.Distance(MoveObject2.transform.position, MoveTarget2.transform.position);
			MoveObject2.transform.position += ( MoveTarget2.transform.position - MoveObject2.transform.position ) / 100 * uiSpeed;
			alphaRotateChange(distanceVector, 2);
			if (distanceVector <= 0.1f) {
				if (MoveTarget2.name == "CanvasStartUIpositionStart") {
					SetStartPosition(MoveObject2);
				} else if (MoveTarget2.name == "CanvasStartUIposition") {
					SetPosition(MoveObject2);
				}
				MoveTarget2 = null;
				MoveObject2 = null;
			} else {
				if (MoveObject2 != null) {
					MoveObject2.SetActive(true);
				}
			}
		}
	}

	public void OnClickFromOptionsToWeather() {
		MoveTarget1 = CanvasStartUIpositionStart;
		MoveObject1 = CanvasStartOptions;
		startVector1 = MoveObject1.transform.position;
		MoveObject1.GetComponent<CanvasGroup>().interactable = false;
		MoveTarget2 = CanvasStartUIposition;
		MoveObject2 = CanvasStartWeather;
		startVector2 = MoveObject2.transform.position;
		MoveObject2.GetComponent<CanvasGroup>().interactable = false;
		UIfloor = false;
		titleTextAlphaChange = true;
	}

	public void OnClickFromOptionsToDaytime() {
		MoveTarget1 = CanvasStartUIpositionStart;
		MoveObject1 = CanvasStartOptions;
		startVector1 = MoveObject1.transform.position;
		MoveObject1.GetComponent<CanvasGroup>().interactable = false;
		MoveTarget2 = CanvasStartUIposition;
		MoveObject2 = CanvasStartDayTime;
		startVector2 = MoveObject2.transform.position;
		MoveObject2.GetComponent<CanvasGroup>().interactable = false;
		UIfloor = false;
		titleTextAlphaChange = true;
	}

	public void OnClickFromWeatherToOptions() {
		MoveTarget1 = CanvasStartUIpositionStart;
		MoveObject1 = CanvasStartWeather;
		startVector1 = MoveObject1.transform.position;
		MoveObject1.GetComponent<CanvasGroup>().interactable = false;
		MoveTarget2 = CanvasStartUIposition;
		MoveObject2 = CanvasStartOptions;
		startVector2 = MoveObject2.transform.position;
		MoveObject2.GetComponent<CanvasGroup>().interactable = false;
		UIfloor = false;
		titleTextAlphaChange = true;
	}

	public void OnClickFromDaytimeToOptions() {
		MoveTarget1 = CanvasStartUIpositionStart;
		MoveObject1 = CanvasStartDayTime;
		startVector1 = MoveObject1.transform.position;
		MoveObject1.GetComponent<CanvasGroup>().interactable = false;
		MoveTarget2 = CanvasStartUIposition;
		MoveObject2 = CanvasStartOptions;
		startVector2 = MoveObject2.transform.position;
		MoveObject2.GetComponent<CanvasGroup>().interactable = false;
		UIfloor = false;
		titleTextAlphaChange = true;
	}

	private void alphaRotateChange(float distance, int moveNumber) {
		if (moveNumber == 1) {
			if (UIfloor) {
				MoveObject1.GetComponent<CanvasGroup>().alpha = (float)( ( Vector3.Distance(startVector1, MoveTarget1.transform.position) - distance ) / Vector3.Distance(startVector1, MoveTarget1.transform.position) );
			} else {
				MoveObject1.GetComponent<CanvasGroup>().alpha = (float)( distance / Vector3.Distance(startVector1, MoveTarget1.transform.position) );
			}
		} else if (moveNumber == 2) {
			if (UIfloor) {
				MoveObject2.GetComponent<CanvasGroup>().alpha = (float)( distance / Vector3.Distance(startVector2, MoveTarget2.transform.position) ) * 0.5f + 0.5f;
				//            MoveObject2.transform.localEulerAngles = new Vector3(0, ((Vector3.Distance(startVector2, MoveTarget2.transform.position) - distance) / Vector3.Distance(startVector2, MoveTarget2.transform.position)) * uiRotate, 0);
			} else {
				MoveObject2.GetComponent<CanvasGroup>().alpha = (float)( ( Vector3.Distance(startVector2, MoveTarget2.transform.position) - distance ) / Vector3.Distance(startVector2, MoveTarget2.transform.position) ) * 0.5f + 0.5f;
				//            MoveObject2.transform.localEulerAngles = new Vector3(0, (distance / Vector3.Distance(startVector2, MoveTarget2.transform.position)) * uiRotate, 0);
			}/*
            if (titleTextAlphaChange) {
                titleText.GetComponent<TextMesh>().color = new Color(titleText.GetComponent<TextMesh>().color.r,
                                                                     titleText.GetComponent<TextMesh>().color.g,
                                                                     titleText.GetComponent<TextMesh>().color.b,
                                                                     1 - MoveObject2.transform.localEulerAngles.y / uiRotate);
            }*/
		} else {
			Debug.LogWarning("Syntax Error; AlphaValue and RotateValue are changed by unknown MoveNumber.");
		}
	}

	/* メニュー戻したとき選択を初期化 */

	/* マップ情報を受け取り、変数上書き */
	public void reciveMapInfo(string mapName) {
		Debug.Log(mapName);
		mapInfoUpdate = true;
		mapInfoName = mapName;
	}

	/* マップ情報の表示 */
	private void mapInfoDisplay() {
		mapName.GetComponent<Text>().text = dataBase.dataOfFieldName(mapInfoName);
		mapInfo.GetComponent<Text>().text = dataBase.dataOfFieldInfo(mapInfoName);
	}

	/* マップ情報をPlayerprefsに保存 */
	public void saveMapAllInfo() {

	}

	/* マップに行くボタン動作 */
	public void goThisMapButton() {

	}

}
