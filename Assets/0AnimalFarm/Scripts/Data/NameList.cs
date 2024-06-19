using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameList : MonoBehaviour
{
    public enum Activities
    {
        Watch, Fishing, Eat, Rest, Egg, Milk, Gather
    }

    public enum Level
    {
        LV0 = 0,
        LV1 = 1,
        LV2 = 2,
        LV3 = 3,
        LV4 = 4,
        LV5 = 5,
        LV6 = 6,
        LV7 = 7,
        LV8 = 8,
        LV9 = 9,
        LV10 = 10
       // ,LV1, LV2, LV3, LV4, LV5, LV6, LV7, LV8, LV9, LV10
    }

    public enum DailyRoutines
    {

        Villager_Watch, Villager_Fishing
    }
}
