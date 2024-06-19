using DG.Tweening;
using HedgehogTeam.EasyTouch;
using System;
using System.Collections;
using System.Collections.Generic;
using unityad;
using UnityEngine;
using static Pathfinding.Util.RetainedGizmos;

public class Plant : MonoBehaviour
{
    public Ease easePull;
    public float timePull = .2f, timeMove = 1.2f;
     [HideInInspector]
    public Plot pl;
    bool pressed = false;
    float pY = 0;
    BoxCollider2D col;
    QuickTouch touch;
    Tween tw, tw2;
    void Awake()
    {
        touch = GetComponent<QuickTouch>();
        col = GetComponent<BoxCollider2D>();
       
        SettingPlant();
    }

    void SettingPlant()
    {
        pl = GetComponentInParent<Plot>();
        if(pl.index != 1)
        {
            col.enabled = false;
            touch.enabled = false;
        }
    }
   
    
    public void GatherProduct()
    {

        pl.Gather(gameObject,gameObject.transform.position);
    }

    public void OnStartDrag()
    {
        if (pressed) return;
       
        pressed = true;
        if (tw != null) tw.Kill(); tw = null;
        pY = Input.mousePosition.y;
    }
}
