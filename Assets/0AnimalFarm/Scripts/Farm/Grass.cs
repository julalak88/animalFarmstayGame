using DG.Tweening;
using HedgehogTeam.EasyTouch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public Ease easePull;
    public float timePull = .2f, timeMove = 1.2f;
    public AnimationCurve curveX1, curveX2;

    [HideInInspector]
    public int value = 1;
    [HideInInspector]
    public int index;

    [HideInInspector]
    public GrassManager manager;
    [HideInInspector]
    public Livestock livestock;

    QuickTouch touch;
    SpriteRenderer sp;
    BoxCollider2D col;
    bool pressed = false, pulling = false;
    Tween tw, tw2;
    Tween tx, ty;
    float pY = 0;

    private void Awake() {
        touch = GetComponent<QuickTouch>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = true;
        touch.enabled = true;
        sp = GetComponentInChildren<SpriteRenderer>();
        //sp.sortingOrder = spriteOrder;
    }

    private void Update() {
        if (!pressed) return;

        pY = Mathf.Lerp(Input.mousePosition.y, pY, Time.deltaTime * .5f);
        float a = (Input.mousePosition.y - pY) * 12f * Time.deltaTime;
        if (a < 0) a = 0;

        if (a > 0 && !pulling) {
            pulling = true;
            //SoundGameManager.ins.PlaySFX("Pull");
        }

        Vector3 scale = transform.localScale;
        scale.y += a;
        if (scale.y < 0) scale.y = 0;
        transform.localScale = scale;

        if (scale.y > 1.4f) {
            if (livestock != null && !livestock.isFoodFull()) {
                col.enabled = false;
                touch.enabled = false;
                pressed = false;

                livestock.data.food++;
                livestock.data.stampTime = DateTime.Now;

                if (tw != null) tw.Kill(); tw = null;
                tw = transform.DOScaleY(1f, 1f).SetEase(Ease.OutElastic);
                float y = transform.localPosition.y + 0.5f;
                tw2 = transform.DOLocalMoveY(y, timePull).SetEase(easePull).OnComplete(MoveToPosition);

                return;
            }else {
                tw = transform.DOScaleY(1f, .5f).SetEase(Ease.InOutElastic);
                pressed = false;
                pulling = false;
                return;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            tw = transform.DOScaleY(1f, .5f).SetEase(Ease.InOutElastic);
            pressed = false;
            pulling = false;
        }
    }

    void MoveToPosition() {
        sp.sortingLayerName = "OverPlayArea";
        Vector3 pos = livestock.FeedPosition;
        if (UnityEngine.Random.Range(0, 2) == 0) tx = transform.DOMoveY(pos.y, .6f).SetEase(curveX1);
        else tx = transform.DOMoveY(pos.y, .6f).SetEase(curveX2);
        ty = transform.DOMoveX(pos.x, .6f).SetEase(Ease.InQuad).OnComplete(OnGet);
    }

    void OnGet() {
        manager.GetGrass(this);
        Destroy(gameObject);
    }

    public void OnStartDragGrass() {
        if (pressed) return;
        livestock = manager.farmScene.GetFeedLivestock();
        pressed = true;
        if (tw != null) tw.Kill(); tw = null;
        pY = Input.mousePosition.y;
    }
}
