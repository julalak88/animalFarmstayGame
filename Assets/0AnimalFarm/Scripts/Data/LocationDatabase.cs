using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LocationDatabase", menuName = "AnimalFarmstay/LocationDatabase")]
public class LocationDatabase : DescriptionDatabase
{
    public int price;
    public List<ItemDatabase> itemsFirstUnlock;
    public PostcardDatabase postcard;
}
