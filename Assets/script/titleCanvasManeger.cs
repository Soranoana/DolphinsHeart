using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleCanvasManeger : MonoBehaviour {

    public GameObject titleText;
    public GameObject uiPosition;
    public GameObject uiPositionEnd;
    public GameObject uiPositionEnd2;
    public GameObject uiPositionStart;
    public GameObject CanvasTitle;
    public GameObject CanvasMenu;
    public GameObject CanvasTutorial;
    public GameObject CanvasGrow;
    public GameObject CanvasShop;
    public GameObject CanvasSettings;
    public GameObject CanvasStart;
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
    private bool titleTextAlphaChange=false;
    private float uiSpeed = 25f;//UIの移動速度
    private float uiRotate = 70f;//UIが左に行くときの角度

    void Start () {
        CanvasTitle.transform.position = uiPosition.transform.position;
        SetStartPosition(CanvasMenu);
        SetStartPosition(CanvasTutorial);
        SetStartPosition(CanvasGrow);
        SetStartPosition(CanvasShop);
        SetStartPosition(CanvasSettings);
        SetStartPosition(CanvasStart);
    }
	
	void Update () {
        if (MoveTarget1 != null|| MoveTarget2 != null|| MoveTarget3 != null) {
            CanvasMove();
        }
	}

    private void SetStartPosition(GameObject gameObject) {
        gameObject.transform.position = uiPositionStart.transform.position;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.SetActive(false);
    }

    private void SetPosition(GameObject gameObject) {
        gameObject.transform.position = uiPosition.transform.position;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
    }

    private void SetEndPosition(GameObject gameObject) {
        gameObject.transform.position = uiPositionEnd.transform.position;
        gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
    }

    private void SetEnd2Position(GameObject gameObject) {
        gameObject.transform.position = uiPositionEnd2.transform.position;
        gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
    }

    private void CanvasMove() {
        if (MoveTarget1 != null) {
            distanceVector = Vector3.Distance(MoveTarget1.transform.position, MoveObject1.transform.position);
            MoveObject1.transform.position += (MoveTarget1.transform.position - MoveObject1.transform.position) / 100 * uiSpeed;
            alphaRotateChange(distanceVector, 1);
            if (distanceVector <= 0.1f) {
                if (MoveTarget1.name == "uiPositionStart") {
                    SetStartPosition(MoveObject1);
                }else if (MoveTarget1.name == "uiPosition") {
                    SetPosition(MoveObject1);
                }else if (MoveTarget1.name == "uiPositionEnd") {
                    SetEndPosition(MoveObject1);
                }else if (MoveTarget1.name == "uiPositionEnd2") {
                    SetEnd2Position(MoveObject1);
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
            MoveObject2.transform.position += (MoveTarget2.transform.position - MoveObject2.transform.position) / 100 * uiSpeed;
            alphaRotateChange(distanceVector, 2);
            if (distanceVector <= 0.1f) {
                if (MoveTarget2.name == "uiPositionStart") {
                    SetStartPosition(MoveObject2);
                }else if (MoveTarget2.name == "uiPosition") {
                    SetPosition(MoveObject2);
                }else if (MoveTarget2.name == "uiPositionEnd") {
                    SetEndPosition(MoveObject2);
                }else if (MoveTarget2.name == "uiPositionEnd2") {
                    SetEnd2Position(MoveObject2);
                }
                MoveTarget2 = null;
                MoveObject2 = null;
            } else {
                if (MoveObject1 != null) {
                    MoveObject1.SetActive(true);
                }
            }
        }

        if (MoveTarget3 != null) {
            MoveObject3.transform.position += (MoveTarget3.transform.position - MoveObject3.transform.position) / 100 * uiSpeed;
            if (Vector3.Distance(MoveObject3.transform.position, MoveTarget3.transform.position) <= 0.1f) {
                if (MoveTarget3.name == "uiPositionStart") {
                    SetStartPosition(MoveObject3);
                }else if (MoveTarget3.name == "uiPosition") {
                    SetPosition(MoveObject3);
                }else if (MoveTarget3.name == "uiPositionEnd") {
                    SetEndPosition(MoveObject3);
                }else if (MoveTarget3.name == "uiPositionEnd2") {
                    SetEnd2Position(MoveObject3);
                }
                MoveTarget3 = null;
                MoveObject3 = null;
            }
        }
    }

    private void alphaRotateChange(float distance, int moveNumber) {
        if (moveNumber == 1) {
            if (UIfloor) {
                MoveObject1.GetComponent<CanvasGroup>().alpha = (float)((Vector3.Distance(startVector1, MoveTarget1.transform.position)-distance) / Vector3.Distance(startVector1,MoveTarget1.transform.position));
            }else {
                MoveObject1.GetComponent<CanvasGroup>().alpha = (float)(distance / Vector3.Distance(startVector1, MoveTarget1.transform.position));
            }
        }
        else if (moveNumber == 2) {
            if (UIfloor) {
                MoveObject2.GetComponent<CanvasGroup>().alpha = (float)(distance / Vector3.Distance(startVector2, MoveTarget2.transform.position)) * 0.5f + 0.5f;
                MoveObject2.transform.localEulerAngles = new Vector3(0, ((Vector3.Distance(startVector2, MoveTarget2.transform.position) - distance) / Vector3.Distance(startVector2, MoveTarget2.transform.position)) * uiRotate, 0);
            }else {
                MoveObject2.GetComponent<CanvasGroup>().alpha = (float)((Vector3.Distance(startVector2, MoveTarget2.transform.position) - distance) / Vector3.Distance(startVector2, MoveTarget2.transform.position)) * 0.5f + 0.5f;
                MoveObject2.transform.localEulerAngles = new Vector3(0, (distance / Vector3.Distance(startVector2, MoveTarget2.transform.position)) * uiRotate, 0);
            }
            if (titleTextAlphaChange) {
                titleText.GetComponent<TextMesh>().color = new Color(titleText.GetComponent<TextMesh>().color.r,
                                                                     titleText.GetComponent<TextMesh>().color.g,
                                                                     titleText.GetComponent<TextMesh>().color.b,
                                                                     1 - MoveObject2.transform.localEulerAngles.y / uiRotate);
            }
        }else {
            Debug.LogWarning("Syntax Error; AlphaValue and RotateValue are changed by unknown MoveNumber.");
        }
    }

    public void OnClickFromTitleToMenu() {
        //CanvasMenuを手前に
        MoveTarget1 = uiPosition;
        MoveObject1 = CanvasMenu;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        //Titleを端に
        MoveTarget2 = uiPositionEnd;
        MoveObject2 = CanvasTitle;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        //canvasmenu 色付け
        UIfloor = true;
        titleTextAlphaChange=false;
    }

    public void OnClickFromMenuToTitle() {
        //CanvasMenuを後ろに
        MoveTarget1 = uiPositionStart;
        MoveObject1 = CanvasMenu;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        //Titleを手前に
        MoveTarget2 = uiPosition;
        MoveObject2 = CanvasTitle;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        //canvasmenu 色付け
        UIfloor = false;
        titleTextAlphaChange = false;
    }

    public void OnClickFromMenuToStart() {
        MoveTarget1 = uiPosition;
        MoveObject1 = CanvasStart;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPositionEnd;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd2;
        MoveObject3 = CanvasTitle;
        UIfloor = true;
        titleTextAlphaChange = true;
    }

    public void OnClickFromMenuToGrow() {
        MoveTarget1 = uiPosition;
        MoveObject1 = CanvasGrow;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPositionEnd;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd2;
        MoveObject3 = CanvasTitle;
        UIfloor = true;
        titleTextAlphaChange = true;
    }

    public void OnClickFromMenuToShop() {
        MoveTarget1 = uiPosition;
        MoveObject1 = CanvasShop;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPositionEnd;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd2;
        MoveObject3 = CanvasTitle;
        UIfloor = true;
        titleTextAlphaChange = true;
    }

    public void OnClickFromMenuToSetting() {
        MoveTarget1 = uiPosition;
        MoveObject1 = CanvasSettings;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPositionEnd;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd2;
        MoveObject3 = CanvasTitle;
        UIfloor = true;
        titleTextAlphaChange = true;
    }

    public void OnClickFromMenuToTutorial() {
        MoveTarget1 = uiPosition;
        MoveObject1 = CanvasTutorial;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPositionEnd;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd2;
        MoveObject3 = CanvasTitle;
        UIfloor = true;
        titleTextAlphaChange = true;
    }

    public void OnClickFromTutorialToMenu() {
        MoveTarget1 = uiPositionStart;
        MoveObject1 = CanvasTutorial;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPosition;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd;
        MoveObject3 = CanvasTitle;
        UIfloor = false;
        titleTextAlphaChange = true;
    }

    public void OnClickFromGrowToMenu() {
        MoveTarget1 = uiPositionStart;
        MoveObject1 = CanvasGrow;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPosition;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd;
        MoveObject3 = CanvasTitle;
        UIfloor = false;
        titleTextAlphaChange = true;
    }

    public void OnClickFromShopToMenu() {
        MoveTarget1 = uiPositionStart;
        MoveObject1 = CanvasShop;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPosition;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd;
        MoveObject3 = CanvasTitle;
        UIfloor = false;
        titleTextAlphaChange = true;
    }

    public void OnClickFromSettingToMenu() {
        MoveTarget1 = uiPositionStart;
        MoveObject1 = CanvasSettings;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPosition;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd;
        MoveObject3 = CanvasTitle;
        UIfloor = false;
        titleTextAlphaChange = true;
    }

    public void OnClickFromStartToMenu() {
        MoveTarget1 = uiPositionStart;
        MoveObject1 = CanvasStart;
        startVector1 = MoveObject1.transform.position;
        MoveObject1.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget2 = uiPosition;
        MoveObject2 = CanvasMenu;
        startVector2 = MoveObject2.transform.position;
        MoveObject2.GetComponent<CanvasGroup>().interactable = false;
        MoveTarget3 = uiPositionEnd;
        MoveObject3 = CanvasTitle;
        UIfloor = false;
        titleTextAlphaChange = true;
    }
}
