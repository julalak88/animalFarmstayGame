using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Product : MonoBehaviour
{

    public float time;

    [HideInInspector]
    public int coin = 1;
    [HideInInspector]
    public float exp = 1;
    [HideInInspector]
    public float vegExp = 1;
    [HideInInspector]
    public Transform target;
    SpriteRenderer spRenderer;
    Tween tx, ty;
    SoundManager soundManager;
    GameManager gm;
    public AnimationCurve curveX1, curveX2;
    private void Awake() {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        soundManager = SoundManager.Ins;
        gm = GameManager.Ins;
        DOVirtual.DelayedCall(.1f, OnCollect);
    }

    void OnCollect() {

        if (UnityEngine.Random.Range(0, 2) == 0) tx = transform.DOMoveY(target.position.y, time).SetEase(curveX1);
        else tx = transform.DOMoveY(target.position.y, time).SetEase(curveX2);
        ty = transform.DOMoveX(target.position.x, time).SetEase(Ease.InQuad).OnComplete(GotIt);

        if (soundManager == null) soundManager = SoundManager.Ins;
       // if (spRenderer.isVisible) soundManager.PlaySFX("Pull");
       
    }

    void  GotIt() {
        //spRenderer.sortingLayerName = "PlayArea";
       // gm.unlockManager.AddRecord(gameObject.name);
       // customer.CalculateCoinDrop(coin, exp);
       // customer.ActivityComplete(false);
        Destroy(gameObject);
    }
}
