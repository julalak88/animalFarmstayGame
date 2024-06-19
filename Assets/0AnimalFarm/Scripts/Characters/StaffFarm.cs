using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffFarm : Staff
{
    public PointTarget restTarget;
    public List<Sprite> watering, rest;
    public GameObject water;

    int work = 0;

    protected override void Awake() {
        base.Awake();
        water.SetActive(false);
    }

    public override void OnReached() {
        _reached = true;
        if (target.type == PointTarget.PointTargetType.STAFF_OPERATE)  {
            curSpriteSet = stand;
            DOVirtual.DelayedCall(20, RandomTarget);
        } else if (target.type == PointTarget.PointTargetType.STAFF_STAND) {
            sprite.transform.localScale = new Vector3(target.direction * -1, 1);
            curSpriteSet = stand;
            DOVirtual.DelayedCall(2, RandomTarget);
        } else if (target.type == PointTarget.PointTargetType.STAFF_WATERING) {
            sprite.transform.localScale = new Vector3(target.direction * -1, 1);
            if (target.aUpgrade && target.aUpgrade.isAvailable) {
                curSpriteSet = watering;
                water.SetActive(true);
            }else {
                curSpriteSet = stand;
            }
            DOVirtual.DelayedCall(5, RandomTarget);
        }else if(target.type == PointTarget.PointTargetType.STAFF_REST) {
            sprite.transform.localScale = new Vector3(target.direction * -1, 1);
            curSpriteSet = rest;
            DOVirtual.DelayedCall(20, RandomTarget);
        }
        ind_anim = 0;

    }

    protected override void RandomTarget() {
        water.SetActive(false);
        if (work > 10) {
            work = 0;
            target = restTarget;
            curSpriteSet = walk;
            GoToTarget();
        } else {
            work++;
            base.RandomTarget();
        }
    }
}
