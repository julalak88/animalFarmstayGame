using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "FontDatabase", menuName = "AnimalFarmstay/FontStyleDatabase")]
public class FontStyleDatabase : SerializedScriptableObject
{
  
    public Dictionary<FontType, TMP_FontAsset> fontTmp = new Dictionary<FontType, TMP_FontAsset>();
    public int size = 30;
}
