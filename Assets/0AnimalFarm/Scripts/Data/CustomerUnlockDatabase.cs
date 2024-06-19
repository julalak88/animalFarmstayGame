using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerUnlockDatabase", menuName = "AnimalFarmstay/CustomerUnlockDatabase")]
public class CustomerUnlockDatabase : SerializedScriptableObject
{
    public List<string> customerList;
}



