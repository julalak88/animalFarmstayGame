using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Character
{
    public List<Sprite> stand;
    public List<PointTarget> pointTargets;

    internal List<Sprite> curSpriteSet;

    protected override void Awake() {
        base.Awake();
        movement.character = this;

        ind_anim = 0;
        curSpriteSet = stand;

        transform.position = pointTargets[0].transform.position;
    }

    protected override void Start() {
        RandomTarget();
    }

    public override void OnUpdate() {

        if (sprite.gameObject.activeInHierarchy) {

            t_anim += Time.deltaTime;
            if (t_anim > .3f) {
                t_anim = 0;
                sprite.sprite = curSpriteSet[(++ind_anim) % curSpriteSet.Count];
                if (!_reached) {
                    int dir = movement.GetDirection();
                    if (dir == -1 || dir == 1) sprite.transform.localScale = new Vector3(dir, 1);
                }
            }

        }

        movement.OnUpdate();

    }

    public override void OnReached() {
        _reached = true;
        if (target.type == PointTarget.PointTargetType.STAFF_STAND || target.type == PointTarget.PointTargetType.STAFF_WATERING || target.type == PointTarget.PointTargetType.STAFF_REST)
            sprite.transform.localScale = new Vector3(target.direction * -1, 1);
        ind_anim = 0;
        curSpriteSet = stand;
        if (target.type == PointTarget.PointTargetType.STAFF_OPERATE) DOVirtual.DelayedCall(20, RandomTarget);
        else DOVirtual.DelayedCall(2, RandomTarget);
    }

    protected virtual void RandomTarget() {
        int ind = UnityEngine.Random.Range(0, pointTargets.Count);
        target = pointTargets[ind];
        curSpriteSet = walk;
        GoToTarget();
    }
}
