using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudObject : MonoBehaviour
{

    public Rect randomPositionStart;
    public float endPositionY;
    public float speedX = 0.1f, speedY = 0.15f;
    public float delayTime = 0;

    [HideInInspector]
    public CloudManager manager;

    SpriteRenderer sp;
    Tween tw;
    bool faded = false, move = false;
    Color color;

    private void Awake() {
        sp = GetComponent<SpriteRenderer>();
        color = sp.color;
    }

    private void Start() {
        if (delayTime == 0) FirstStart();
        else RandomPositionAtStart();
    }

    void FirstStart() {
        sp.sprite = manager.RandomSprite();
        move = false;
        Vector3 pos = transform.position;
        pos.x = randomPositionStart.x + UnityEngine.Random.Range(0f, randomPositionStart.width);
        float h = (endPositionY - randomPositionStart.y) * .5f;
        pos.y = (randomPositionStart.y + h*.25f) + UnityEngine.Random.Range(0f, h);
        transform.position = pos;
        sp.color = color;
        DOVirtual.DelayedCall(delayTime, StartMove);
    }

    public void UpdateCloud() {
        if (!move) return;
        Vector3 pos = transform.position;
        pos.x += speedX;
        pos.y += speedY;
        transform.position = pos;
        if(!faded && pos.y > endPositionY) {
            faded = true;
            tw = sp.DOFade(0, 1).OnComplete(RandomPositionAtStart);
        }
    }

    void StartMove() { move = true; faded = false; }

    public void RandomPositionAtStart() {
        move = false;
        Vector3 pos = transform.position;
        pos.x = randomPositionStart.x + UnityEngine.Random.Range(0f, randomPositionStart.width);
        pos.y = randomPositionStart.y + UnityEngine.Random.Range(0f, randomPositionStart.height);
        transform.position = pos;
        sp.sprite = manager.RandomSprite();
        sp.color = color;
        DOVirtual.DelayedCall(delayTime, StartMove);
    }
}
