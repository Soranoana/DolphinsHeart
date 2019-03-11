using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    private AsyncOperation ope;
    public GameObject LoadingUI;
    private GameObject mainCameraUiPosition;
    public Slider Slider;
    private string NextScene;
    public bool testBool=false;

	void Start () {
        LoadingUI.SetActive(false);
        mainCameraUiPosition = GameObject.Find("uiPosition");
	}
	
	void Update () {
        if (testBool) {
//            LoadNextScene("title");
            LoadNextScene("cameraTest2");
        }
	}

    public void LoadNextScene(string SceneName) {
        testBool = false;
        NextScene = SceneName;
        LoadingUI.SetActive(true);
        LoadingUI.transform.parent = mainCameraUiPosition.transform;
        LoadingUI.transform.localPosition = mainCameraUiPosition.transform.localPosition + new Vector3(0,0,1) * 1.5f;
        LoadingUI.transform.localEulerAngles = new Vector3(0, 0, 0);
        StartCoroutine(LoadSceneAndWait());
    }

    IEnumerator LoadSceneAndWait() {
        //Time.timeScale = 0;
        ope = SceneManager.LoadSceneAsync(NextScene);
        /* ロード画面 ローディングバーの表示 */
        while (ope.progress<0.9f) {
            Debug.Log("loading");
            Slider.value = ope.progress/0.9f;
            yield return 0;
            //yield break;
        }
        /* 次シーンの自動アクティベートを無効に */
        ope.allowSceneActivation = false;
//        Slider.value = 1.0f;
        /* ロード後に一秒待つ */
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - start < 5) {
            /* 次シーンの自動アクティベートを有効に */
            /*while (ope.progress < 1)
            {
                Debug.Log("run");
                Slider.value = ope.progress;
            }*/
            yield return 0;
        }
        ope.allowSceneActivation = true;
//        yield return new WaitForSecondsRealtime(5.0f);
        yield return null;
    }

    public void loadtest() {
        testBool = true;
    }
}
