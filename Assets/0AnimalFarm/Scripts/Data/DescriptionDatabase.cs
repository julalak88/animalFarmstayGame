using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DescriptionDatabase", menuName = "AnimalFarmstay/DescriptionDatabase", order = 2)]
public class DescriptionDatabase : SerializedScriptableObject
{
    [PreviewField(150, ObjectFieldAlignment.Left)]
    public Sprite image;
    [PreviewField(40, ObjectFieldAlignment.Left)]
    public Sprite thumbnail;
    public string id;
    public NameList.Level Level = NameList.Level.LV1;
    [TabGroup("TH")]
    [HideLabel]
    public DescriptionData TH;
    [TabGroup("US")]
    [HideLabel]
    public DescriptionData US;
    public DialogDetail dialogTH;
    public DialogDetail dialogUS;

}

[System.Serializable]
public class DescriptionData
{
    public string name;
    [Multiline(5)]
    public string desc;
}


[Serializable]
public class DialogDetail
{
    public string firstmeet;
    public string hint;
}
