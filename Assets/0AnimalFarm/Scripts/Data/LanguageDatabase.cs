using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
public class LanguageDatabase : SerializedMonoBehaviour
{

    public Dictionary<string, TextStyle> lang_style = new Dictionary<string, TextStyle>();


    [Button("Apply Style")]
    void ApplyFontStyle() {
        FontStyleDatabase[] langData = Resources.LoadAll<FontStyleDatabase>("FontStyle/");
        for (int i = 0; i < langData.Length; i++) {
            TextStyle style = new TextStyle();
            style.fontTmp = langData[i].fontTmp;
            style.size = langData[i].size;
            if (!lang_style.ContainsKey(langData[i].name)) lang_style.Add(langData[i].name, style);
        }
    }


}
