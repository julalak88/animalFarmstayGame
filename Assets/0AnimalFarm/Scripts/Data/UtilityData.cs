using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
[Serializable]
public enum FontType
{
    head,
    subHead,
    content

}

[System.Serializable]
public class TextStyle
{
    public Dictionary<FontType, TMP_FontAsset> fontTmp = new Dictionary<FontType, TMP_FontAsset>();
    public int size = 30;
}

[Serializable]
public enum DirectionType
{
    Left,
    Right,
    Up,
    Down

}

[Serializable]
public enum CharacterType
{
    Guest,
    Villager,
    Kids
 
}

[Serializable]
public enum ItemsType
{
    House,
    Flowers,
    Fence,
    Shop,
    Lamp,
    Decoration,
    Mailbox,
    Barn,
    Pond,
    Bridge,
    FoodtruckLeft,
    FoodtruckRight,
    Kitchen,
    Table,
    Bench,
    Cart,
    Square
}

[Serializable]
public class Currency {
     public int price = 0;
     public int coin = 0;
     public float expReward = 0;
     public float reqExp = 0;
  
}
[Serializable]
public enum QuestType
{
    normalQuest,
    storeyQuest
}

[Serializable]
public enum QuestAction
{ COLLECT, SERVICE, WATCH, CLEANUP, GROW }


[Serializable]
public enum PlantType
{
    vegetable,
    tree,
    ivy

}
