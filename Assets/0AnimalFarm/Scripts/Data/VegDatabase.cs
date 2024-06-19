using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "VegDatabase", menuName = "AnimalFarmstay/VegDatabase")]
public class VegDatabase : DescriptionDatabase
{
    public PlantType plantType;
    public Sprite product;
    public Currency currency;
    public float vegExpDrop;
    public float actionTime = 5f;
    [Header("0.5f for 30 sec,1 for 1miu")]
    public float growthTime = 5f;
    //public int collect_num = 4;
}
