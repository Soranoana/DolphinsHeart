using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownCallBack : MonoBehaviour {

    private Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.ClearOptions();
        List<string> list = new List<string>();
        list.Add("jap");
        list.Add("eng");
        list.Add("ita");
        list.Add("cha");
        dropdown.AddOptions(list);
        dropdown.value = 1;

        string deviceLanguage = Application.systemLanguage.ToString();
        Debug.Log(deviceLanguage);
        if (deviceLanguage == "Japanese")
        {
            Debug.Log("日本語だよ");
        }
        else if (deviceLanguage == "English")
        {
            Debug.Log("英語だよ");
        }
        else
        {
            Debug.Log("他の言語だよ: " + deviceLanguage);
        }
    }

    public void OnValueChanged(int result) {
        Debug.Log(result +"  "+ dropdown.options[result].text + "  " + dropdown.captionText.text);
        Debug.Log(result);
    }
}
