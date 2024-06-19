using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableFood : ObjectUpgrade
{
    public OrderIcon order;
    public SpriteRenderer food;
    public string foodName;

    [HideInInspector]
    public Character customer;

    Sprite foodSprite;
    float cookTime;
    bool gotOrder = false;
    SoundManager soundManager;
    BoxCollider2D bCollider;
    Tween tw;

    protected override void Awake() {

        base.Awake();

        bCollider = GetComponent<BoxCollider2D>();
        bCollider.enabled = false;

        order.gameObject.SetActive(false);
    }

    protected override void Start() {
        base.Start();
        soundManager = SoundManager.Ins;
    }

    public void SetOrder(string foodName, Sprite image, float cookTime) {
        gotOrder = false;
        this.foodName = foodName;
        foodSprite = image;
        this.cookTime = cookTime;
        order.SetImage(image);
        bCollider.enabled = true;
        tw = DOVirtual.DelayedCall(10f, GetOrder);
    }

    public void GetOrder() {
        if (tw != null) tw.Kill(); tw = null;
        if (gotOrder) return;
        gotOrder = true;
        bCollider.enabled = false;
        soundManager.PlaySFX("Select");
        order.gameObject.SetActive(false);
        DOVirtual.DelayedCall(cookTime, SendFood);
    }

    private void SendFood() {
        food.sprite = foodSprite;
        customer.Eating();
    }

    public void ClearFood() {
        food.sprite = null;
        customer = null;
    }
}
