using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
using System;
using static System.Net.Mime.MediaTypeNames;

public class LanguageSelector : SerializedMonoBehaviour {

    TextMeshProUGUI text;
    public FontType fontType;
    TextStyle currentStyle;
    string lang = "TH";
 
    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        if (SaveManager.Ins) {
            if (lang != SaveManager.Ins.lang) {
                lang = SaveManager.Ins.lang;
                currentStyle = GameManager.Ins.loc.lang_style[lang];
                if (fontType.Equals(FontType.head)) {
                    text.font = currentStyle.fontTmp[FontType.head];
                } else if (fontType.Equals(FontType.subHead)) {
                    text.font = currentStyle.fontTmp[FontType.subHead];
                } else {
                    text.font = currentStyle.fontTmp[FontType.content];
                }
              
                //text.fontSize = currentStyle.size;
            }
        }
    }

    string _str;
    public string Text {
        set {
            _str = value;
            if (!string.IsNullOrEmpty(_str))
            {
                text.text = ThaiFontAdjuster.Adjust(_str);
            } else
            {
                text.text = "";
            }
        }
        get {
            return ThaiFontAdjuster.Adjust(_str);
        }
    }


 
}


