using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "AnimalFarmstay/ItemDatabase")]
public class ItemDatabase : DescriptionDatabase
{
    public ItemsType itemsType;
    public Currency currency;
    public float actionTime = 5f;
    public List<string> unlock_name;
    public List<Vector2> points;
}
