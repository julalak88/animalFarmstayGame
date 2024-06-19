using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
[CreateAssetMenu(fileName = "DescriptionDatabase", menuName = "AnimalFarmstay/FoodDatabase", order = 3)]
public class FoodDatabase : DescriptionDatabase
{
    [Range(1f, 15f)]
    public float cookTime;
    public Currency currency;
    public int vegExpReq;
    public List<Ingredients> ingredients;



}


[Serializable]
public class Ingredients
{
    
    [HideLabel]
    public string itemName;
    
    [HideLabel]
    public int count = 1;
}


