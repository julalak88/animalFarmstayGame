using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerDatabase", menuName = "AnimalFarmstay/CustomerDatabase", order = 3)]
public class CustomerDatabase : DescriptionDatabase
{
    public CharacterType type;
    [TabGroup("TH")]
    [HideLabel]
    public StoryData StoryTH;
    [TabGroup("US")]
    [HideLabel]
    public StoryData StoryUS;
    public List<UnlockCondition> unlock_condition;
 
}


[Serializable]
public class StoryData
{
    [Multiline(15)]
    public string story;
    public string hint;
}

[Serializable]
//[InlineProperty]
public class UnlockCondition
{
    [HorizontalGroup("condition", 150)]
    [HideLabel]
    public string itemName;
    [HorizontalGroup("condition", 10)]
    [HideLabel]
    public int count = 1;
}

 