using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class UpgradeFX : MonoBehaviour
{

    public TweenCallback OnComplete;

    ParticlesManager particles;
    SpriteRenderer[] sprites;
    string layerTemp1;
    List<string> layerTemp = new List<string>();
    private void Awake() {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++) {
            layerTemp.Add(sprites[i].sortingLayerName);
            sprites[i].sortingLayerName = "OverPlayArea";
        }
        transform.localScale = new Vector3(1, 1.2f, 1f);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.y);
    }

    void Start()
    {
        particles = GameManager.Ins.particles;
        transform.DOLocalMoveY(0, .3f).SetEase(Ease.InExpo).OnComplete(CreateFX);
        transform.DOScaleY(1, .8f).SetDelay(.3f).SetEase(Ease.OutElastic).OnComplete(moveComplete);
    }

    void CreateFX() {
        for (int i = 0; i < sprites.Length; i++) sprites[i].sortingLayerName = layerTemp[i];
        particles.CreateParticles("Firework").position = transform.position;
        SoundManager.Ins.PlaySFX("Collect");
    }

    private void moveComplete() {
        DOVirtual.DelayedCall(1.5f, DestroyThis);
    }

    void DestroyThis() {
        OnComplete.Invoke();
        Destroy(this);
    }
}
