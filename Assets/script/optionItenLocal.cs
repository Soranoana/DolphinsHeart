using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionItenLocal : MonoBehaviour {

    private optionItemAdministar parent;
    private bool haveChildContents = true;
    private GameObject childGameObject;
    private RectTransform childRect;
    private Toggle toggle;
    private Vector2 childSizeDelta;
    private Vector3 childScale;

    void Start() {
        parent = GameObject.Find("Canvas").GetComponent<optionItemAdministar>();
        toggle = transform.Find("Toggle").GetComponent<Toggle>();
        transform.Find("Text").gameObject.GetComponent<Text>().text = parent.getContentName(transform.name);
        getChildObject();
    }

    void Update() {        
        setVertical();

        if (toggle.isOn)
        {
            parent.falseToTrue(transform.name);
        }
        else if (!toggle.isOn)
        {
            if (parent.currentFish - parent.CostReturn(transform.name) < 0)
            {
                toggle.interactable = false;
            }
            else {
                toggle.interactable = true;
            }
            parent.trueToFalse(transform.name);
        }
        if (haveChildContents) {
            if (toggle.isOn) {
                childRect.sizeDelta = childSizeDelta;
                childRect.localScale = childScale;
            } else if (!toggle.isOn)
            {
                childRect.sizeDelta = new Vector2(0, 0);
                childRect.localScale = new Vector3(0, 0, 0);
                childGameObject.transform.Find("Toggle").GetComponent<Toggle>().isOn = false;
            }
        }
    }
    //heightとscale.yを0にする
    void getChildObject() {
        if (transform.name == "dashSpeed1") {
            childGameObject = GameObject.Find("dashSpeed2");
            childRect = childGameObject.GetComponent<RectTransform>();
            childSizeDelta = childRect.sizeDelta;
            childScale = childRect.localScale;
        }
        else if (transform.name == "dashSpeed2") {
            childGameObject = GameObject.Find("dashSpeed3");
            childRect = childGameObject.GetComponent<RectTransform>();
            childSizeDelta = childRect.sizeDelta;
            childScale = childRect.localScale;
        }
        else if (transform.name == "dashSpeed3") {
            childGameObject = GameObject.Find("dashSpeed4");
            childRect = childGameObject.GetComponent<RectTransform>();
            childSizeDelta = childRect.sizeDelta;
            childScale = childRect.localScale;
        }
        else haveChildContents = false;
        return;
    }
    private void setVertical() {
        Vector3 a=Input.mousePosition;
        if (a.x >= transform.position.x) {
            if (a.y <= transform.position.y) {
                if (a.x <= transform.position.x + GetComponent<RectTransform>().sizeDelta.x)
                {
                    if (a.y >= transform.position.y - GetComponent<RectTransform>().sizeDelta.y)
                    {
                        parent.contentTouch(transform.name);
                    }
                }
            }
        }
    }
}
