using DG.Tweening;
using HedgehogTeam.EasyTouch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPull : MonoBehaviour
{
    public Ease easePull;
    public float timePull = .2f, timeMove = 1.2f;
    [HideInInspector]
    public Plot pl;
    bool pressed = false, pulling = false;
    float pY = 0;
    BoxCollider2D col;
    QuickTouch touch;
    Tween tw, tw2;

    public Plant[] products;
    void Awake()
    {
        touch = GetComponent<QuickTouch>();
        col = GetComponent<BoxCollider2D>();

        SettingPlant();
    }

    void SettingPlant()
    {
        pl = GetComponentInParent<Plot>();
        if (pl.index != 1)
        {
            col.enabled = false;
            touch.enabled = false;
        }
    }

    void Update()
    {
        if (!pressed) return;

        pY = Mathf.Lerp(Input.mousePosition.y, pY, Time.deltaTime * .5f);
        float a = (Input.mousePosition.y - pY) * 12f * Time.deltaTime;
        if (a < 0) a = 0;

        if (a > 0 && !pulling)
        {
            pulling = true;
            //SoundGameManager.ins.PlaySFX("Pull");
        }

        Vector3 scale = transform.localScale;
        scale.y += a;
        if (scale.y < 0) scale.y = 0;
        transform.localScale = scale;

        if (scale.y > 1f)
        {
            if (pl == null) return;
            if (pl.index == 1)
            {
                col.enabled = false;
                touch.enabled = false;
                pressed = false;

                if (tw != null) tw.Kill(); tw = null;
                tw = transform.DOScaleY(1f, 1f).SetEase(Ease.OutElastic);
                float y = transform.localPosition.y + 0.5f;
                tw2 = transform.DOLocalMoveY(y, timePull).SetEase(easePull).OnComplete(GatherProduct);

                return;
            } else
            {
                tw = transform.DOScaleY(1f, .5f).SetEase(Ease.InOutElastic);
                pressed = false;
                pulling = false;
                return;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            tw = transform.DOScaleY(1f, .5f).SetEase(Ease.InOutElastic);
            pressed = false;
            pulling = false;
        }
    }

    public void GatherProduct()
    {
        DOVirtual.DelayedCall(.01f, products[0].GatherProduct);
        DOVirtual.DelayedCall(.025f, products[1].GatherProduct);
        DOVirtual.DelayedCall(.035f, products[2].GatherProduct);
        DOVirtual.DelayedCall(.045f, products[3].GatherProduct);
        
    }

    public void OnStartDrag()
    {
        if (pressed) return;

        pressed = true;
        if (tw != null) tw.Kill(); tw = null;
        pY = Input.mousePosition.y;
    }
}
