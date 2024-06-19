using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Coin : MonoBehaviour
{

    public Sprite[] images;
    public Ease easeX, easeY1, easeY2;
    public float time = 1.2f;
    public AnimationCurve curveX1, curveX2;

    [HideInInspector]
    public int value = 1;
    [HideInInspector]
    public Vector2 targetPosition;

    SpriteRenderer spRenderer;
    Tween tx, ty1, ty2;
    bool collected = false;

    SceneGameManager sceneManager;
    GameManager gameManager;
    RectTransform collectTransform;
    SoundManager soundManager;
    MoneyController moneyController;

    bool _isCoin = true;
    public bool isCoin {
        set {
            _isCoin = value;
            if (value) spRenderer.sprite = images[0];
            else spRenderer.sprite = images[1];
        }
        get { return _isCoin; }
    }

    private void Awake() {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        soundManager = SoundManager.Ins;
        moneyController = GameManager.Ins.uiManager.money;
    }

    private void OnMouseEnter() {
        OnCollect();
    }

    public void SetFromLoad(Vector3 targetPos) {
        sceneManager = GameManager.Ins.sceneManager;
        if (collectTransform == null) collectTransform = GameManager.Ins.uiManager.money.collectPos;
        if (soundManager == null) soundManager = SoundManager.Ins;
        collected = false;
        transform.position = targetPos;
        targetPosition = targetPos;
    }

    public void MoveToPosition(Vector3 targetPos) {
        spRenderer.sortingLayerName = "OverPlayArea";
        targetPosition = targetPos;
        Vector3 pos = transform.position;
        pos.y += 1f;
        transform.position = pos;
        tx = transform.DOMoveX(targetPos.x, time).SetEase(easeX);
        float time2 = time * .2f;
        float time3 = time * .8f;
        ty1 = transform.DOMoveY(pos.y+.6f, time2).SetEase(easeY1);
        ty2 = transform.DOMoveY(targetPos.y, time3).SetDelay(time2).SetEase(easeY2).OnComplete(() => spRenderer.sortingLayerName = "PlayArea");
        sceneManager = GameManager.Ins.sceneManager;
        if (collectTransform == null) collectTransform = GameManager.Ins.uiManager.money.collectPos;
        if (soundManager == null) soundManager = SoundManager.Ins;
        collected = false;
    }


    public void MoveExp(Vector3 targetPos)
    {
        spRenderer.sortingLayerName = "OverPlayArea";
        targetPosition = targetPos;
        Vector3 pos = transform.position;
        pos.y += 1f;
        transform.position = pos;
        tx = transform.DOMoveX(targetPos.x, time).SetEase(Ease.Linear);
        float time2 = time * 0.4f;
        float time3 = time * 0.4f;
        ty1 = transform.DOMoveY(pos.y + .6f, time2).SetEase(Ease.Linear);
        ty2 = transform.DOMoveY(targetPos.y, time3).SetDelay(time2).SetEase(Ease.Linear).OnComplete(() => spRenderer.sortingLayerName = "PlayArea");
        sceneManager = GameManager.Ins.sceneManager;
        if (collectTransform == null) collectTransform = GameManager.Ins.uiManager.money.collectPos;
        if (soundManager == null) soundManager = SoundManager.Ins;
        collected = false;

        OnCollect();
    }


    public void OnCollect() {
        
        if (collected) return;
        collected = true;
        if (tx != null) tx.Kill(); tx = null;
        if (ty1 != null) ty1.Kill(); ty1 = null;
        if (ty2 != null) ty2.Kill(); ty2 = null;
        spRenderer.sortingLayerName = "OverPlayArea";
        if(spRenderer.isVisible)
            soundManager.PlaySFX("Pop2");
        Vector3 targetPos = collectTransform.position;
        targetPos.z = 0;
        if (UnityEngine.Random.Range(0, 2) == 0)
            tx = transform.DOMoveX(targetPos.x, .6f).SetEase(curveX1);
        else
            tx = transform.DOMoveX(targetPos.x, .6f).SetEase(curveX2);
        ty1 = transform.DOMoveY(targetPos.y, .6f).SetEase(Ease.InQuad).OnComplete(onMoveToMoney);
    }

    private void onMoveToMoney() {
        if (gameManager == null)
        {
            gameManager = GameManager.Ins;
        }

        sceneManager.ReturnCoin(this);
        if (_isCoin)
        {
            moneyController.AddCoin(value);
            gameManager.uiManager.panelUIManager.questController.UpdateProgress(1, QuestAction.COLLECT);


        } else
        {
            moneyController.AddExp(value);
        }

     

      


    }

    /*#if UNITY_EDITOR
        private void Update() {
            if(Input.GetMouseButtonDown(0)) {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                MoveToPosition(pos);
            }    
        }

    #endif*/
}
