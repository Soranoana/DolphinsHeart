using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sliderAdministar : MonoBehaviour
{

    /*BGM slider
     *SE slider
     *VR slider
     *TFPS slider
     *lang dropdown
     *byouga slider
     *delete button
     */
    public GameObject bgmText;
    public GameObject BGMslider;
    public GameObject seText;
    public GameObject SEslider;
    public GameObject vrText;
    public GameObject VRslider;
    public GameObject tfpstext;
    public GameObject TFPSslider;
    public GameObject langText;
    public GameObject LangsDropdown;
    public GameObject byougaText;
    public GameObject ByougaSlider;
    public GameObject deletText;
    public GameObject DELATEbutton;

    public int bgm;
    public int se;
    public int vr;
    public int tfps;
    public int lang;
    public int byouga;

    public GameObject realyPanel;
    public GameObject titlePanel;

    /* DropDown用 */
    private Dropdown dropdown;
    List<string> list = new List<string>();

    void Start()
    {
        bgm = PlayerPrefs.GetInt("bgmValue");
        BGMslider.GetComponent<Slider>().value = (float)bgm;
        se = PlayerPrefs.GetInt("seValue");
        SEslider.GetComponent<Slider>().value = (float)se;
        vr = PlayerPrefs.GetInt("vrValue");
        VRslider.GetComponent<Slider>().value = (float)vr;
        tfps = PlayerPrefs.GetInt("tfpsValue");
        TFPSslider.GetComponent<Slider>().value = (float)tfps;
        /* lang */
        dropdown = LangsDropdown.GetComponent<Dropdown>();
        dropdown.ClearOptions();
        /* 言語一覧     識別          番号
         * アラビア     Arabic        0
         * 中国         Chinese       1
         * オランダ     Dutch         2
         * 英語         English       3
         * フランス     French        4
         * ドイツ       German        5
         * ハングル     Hangul        6
         * インド       Indian        7
         * イタリア     Italian       8
         * 日本語       Japanese      9
         * ポルトガル   Portuguese    10
         * ロシア       Russian       11
         * スペイン     Spanish       12
         */
        //言語
        list.Add("العربية");                  ///アラビア
        list.Add("中國");                      ///中国
        list.Add("Nederlands");                ///オランダ
        list.Add("English");                   ///英語
        list.Add("Français");                  ///フランス
        list.Add("Deutsch");                   ///ドイツ
        list.Add("한글");                      ///ハングル
        list.Add("हिन्दी");                      ///インド
        list.Add("italiano");                  ///イタリア
        list.Add("日本語");                    ///日本
        list.Add("Portugues");                 ///ポルトガル
        list.Add("Русский язык");   ///ロシア
        list.Add("Español");                   ///スペイン
        dropdown.AddOptions(list);
        dropdown.value = 9;
        /* lang End */

        byouga = PlayerPrefs.GetInt("byougaValue");
        ByougaSlider.GetComponent<Slider>().value = (float)byouga;

        realyPanel.SetActive(false);
        titlePanel.SetActive(false);
    }

    void Update()
    {
        bgm = (int)BGMslider.GetComponent<Slider>().value;
        bgmText.GetComponent<Text>().text = bgm.ToString();
        PlayerPrefs.SetInt("bgmValue", bgm);
        se = (int)SEslider.GetComponent<Slider>().value;
        seText.GetComponent<Text>().text = se.ToString();
        PlayerPrefs.SetInt("seValue", se);
        vr = (int)VRslider.GetComponent<Slider>().value;
        if (vr == 0)
        {
            vrText.GetComponent<Text>().text = "通常モード";
        }
        else
        {
            vrText.GetComponent<Text>().text = "VRモード";
        }
        PlayerPrefs.SetInt("vrValue", vr);
        tfps = (int)TFPSslider.GetComponent<Slider>().value;
        if (tfps == 0)
        {
            tfpstext.GetComponent<Text>().text = "TPSモード";
        }
        else
        {
            tfpstext.GetComponent<Text>().text = "FPSモード";
        }
        PlayerPrefs.SetInt("tfpsValue", tfps);
        //Lang
        byouga = (int)ByougaSlider.GetComponent<Slider>().value;
        if (byouga == 0)
        {
            byougaText.GetComponent<Text>().text = "完全描画";
        }
        else
        {
            byougaText.GetComponent<Text>().text = "簡易描画";
        }
        PlayerPrefs.SetInt("byougaValue", byouga);
    }

    public void GetButtonDownOnDelete()
    {
        //本当に？パネル表示
        realyPanel.SetActive(true);
    }

    public void GetButtonDownOnDeleteToYes()
    {
        //本当に？パネル非表示
        realyPanel.SetActive(false);
        //削除実行
        PlayerPrefs.DeleteAll();
        //タイトルへパネル呼び出し
        titlePanel.SetActive(true);
    }

    public void GetButtonDownOnDeleteToNo()
    {
        //本当に？パネル非表示
        realyPanel.SetActive(false);
    }

    public void GetButtonDownOnDeleteToYesToTitle()
    {
        //タイトルへ
        SceneManager.LoadScene("rogoOnSoranoana2");
    }

    /* DropDown用 */
    public void OnValueChanged(int result)
    {
//        Debug.Log(result + "  " + dropdown.options[result].text + "  " + dropdown.captionText.text);
        
    }
}
