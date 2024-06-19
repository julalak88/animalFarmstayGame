using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTarget : MonoBehaviour
{

    [HideInInspector]
    public SceneGame parentScene;
    [HideInInspector]
    public AUpgrade aUpgrade;
    public enum PointTargetType { ENTER, LINK, ACTIVITY, MAIN, QUEUE, FREE, STAFF_OPERATE, STAFF_STAND, STAFF_WATERING, STAFF_REST,BUS, VILLAGER_ROUTINES, VILLAGER_STAND }

    [EnumToggleButtons]
    [OnValueChanged("PointerTypeChange")]
    public PointTargetType type = PointTargetType.ENTER;

    [ShowIf("type", PointTargetType.ACTIVITY)]
    [OnValueChanged("PointerTypeChange")]
    public NameList.Activities activity;

    [ShowIf("type", PointTargetType.VILLAGER_ROUTINES)]
    [OnValueChanged("PointerTypeVillagerChange")]
    public NameList.DailyRoutines dailyRoutine;

    [ShowIf("type", PointTargetType.LINK)]
    public List<PointTarget> linkPoint;

    [ShowIf("type", PointTargetType.BUS)]
    [OnValueChanged("PointerTypeChange")]
    public bool inQueue = false;

    //[HideInInspector]
    [ShowIf("type", PointTargetType.ACTIVITY)]
    public Transform coinPos;
    //[ShowIf("type", PointTargetType.ACTIVITY)]
    [ShowIf("@this.type == PointTargetType.ACTIVITY || this.type == PointTargetType.QUEUE || this.type == PointTargetType.STAFF_STAND || this.type == PointTargetType.STAFF_WATERING || this.type == PointTargetType.STAFF_REST || this.type == PointTargetType.VILLAGER_STAND")]
    [Range(-1,1)]
    public int direction = 1;
    /*[ShowIf("type", PointTargetType.ACTIVITY)]
    [ShowIf("activity", NameList.Activities.Eat)]
    public TableFood tableFood;*/
    [ShowIf("activity", NameList.Activities.Rest)]
    public Guesthouse guesthouse;
    void PointerTypeChange() {
        if (type == PointTargetType.ACTIVITY) {
            name = activity.ToString();
            if (coinPos == null) {
                coinPos = new GameObject("coindrop").transform;
                coinPos.parent = transform;
                coinPos.localPosition = Vector3.zero;
            }
        } else {
            name = type.ToString();
            if(coinPos) {
                DestroyImmediate(coinPos.gameObject, true);
                coinPos = null;
            }
        }
    }

    void PointerTypeVillagerChange()
    {
        if (type == PointTargetType.VILLAGER_ROUTINES)
        {
            name = dailyRoutine.ToString();
        }
    }


    public Vector3 GetCoinPosition {
        get {
            Vector3 pos = coinPos.position;
            float rad = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distX = UnityEngine.Random.Range(0f, 0.7f);
            float distY = UnityEngine.Random.Range(0f, 0.5f);
            return new Vector3(pos.x + (Mathf.Cos(rad) * distX), pos.y + (Mathf.Sin(rad) * distY), 0);
        }
    }
}