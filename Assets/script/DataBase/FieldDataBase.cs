using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDataBase : MonoBehaviour {

    public string dataOfFieldName(string fieldName) {
        switch (fieldName.Substring(0,6)) {
            case "000000":
                return "";
            case "Test00":
                return "テスト";
            case "Taihei":
                return "太平洋";
            case "Nihonk":
                return "日本海";
            case "Tityuk":
                return "地中海";
            case "Karibu":
                return "カリブ海";
            case "Hokyok":
                return "北極海";
            case "Dokutu":
                return "洞窟";
            case "Kawa00":
                return "川";
            case "Mizumi":
                return "湖";
            default:
                return "Field name Error";
        }
    }
    public string dataOfFieldInfo(string fieldInfo) {
        string info="";
        switch (fieldInfo.Substring(0,6)) {
            case "000000":
                info = "";
                return "";
            case "Test00":
                info = "テストフィールドです。\n波はテストです\n";
                if (fieldInfo.Substring(6, 1) == "0") {
                    return info + "浅瀬です\n";
                }else if (fieldInfo.Substring(6, 1) == "1") {
                    return info + "深間です\n";
                }else if (fieldInfo.Substring(6, 1) == "2") {
                    return info + "沖です\n";
                }else { return "Type select Error"; }
            case "Taihei":
                info = "太平洋です。\n波は普通です\n";
                if (fieldInfo.Substring(6, 1)=="0") {
                    return info+"浅瀬です\n";
                } else if (fieldInfo.Substring(6, 1) == "1") {
                    return info+"深間です\n";
                } else if (fieldInfo.Substring(6, 1) == "2") {
                    return info+"沖です\n";
                } else { return "Type select Error"; }
            case "Nihonk":
                info = "日本海ですです。\n波は強いです\n";
                if (fieldInfo.Substring(6, 1)=="0") {
                    return info+"浅瀬です\n";
                } else if (fieldInfo.Substring(6, 1) == "1") {
                    return info+"深間です\n";
                } else if (fieldInfo.Substring(6, 1) == "2") {
                    return info+"沖です\n";
                } else { return "Type select Error"; }
            case "Tityuk":
                info = "地中海です。\n波は弱いです\n";
                if (fieldInfo.Substring(6, 1)=="0") {
                    return info+"浅瀬です\n";
                } else if (fieldInfo.Substring(6, 1) == "1") {
                    return info+"深間です\n";
                } else if (fieldInfo.Substring(6, 1) == "2") {
                    return info+"沖です\n";
                } else { return "Type select Error"; }
            case "Karibu":
                info = "カリブ海です。\n波は普通です\n";
                if (fieldInfo.Substring(6, 1)=="0") {
                    return info+"浅瀬です\n";
                } else if (fieldInfo.Substring(6, 1) == "1") {
                    return info+"深間です\n";
                } else if (fieldInfo.Substring(6, 1) == "2") {
                    return info+"沖です\n";
                } else { return "Type select Error"; }
            case "Hokyok":
                info = "北極海です。\n波は弱いです\n";
                if (fieldInfo.Substring(6, 1)=="0") {
                    return info+"浅瀬です\n";
                } else if (fieldInfo.Substring(6, 1) == "1") {
                    return info+"深間です\n";
                } else if (fieldInfo.Substring(6, 1) == "2") {
                    return info+"沖です\n";
                } else { return "Type select Error"; }
            case "Dokutu":
                return "洞窟です。\n";
            case "Kawa00":
                return "川です。\n";
            case "Mizumi":
                return "湖です。\n";
            default:
                return "Database Error";
        }
    }
	
	
	
}
