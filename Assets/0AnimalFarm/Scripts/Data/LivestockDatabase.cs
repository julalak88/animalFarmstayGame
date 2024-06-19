using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LivestockDatabase", menuName = "AnimalFarmstay/LivestockDatabase")]
public class LivestockDatabase : ItemDatabase
{
    public float eatTime = 5f;
    public int numProduct = 10;
}
