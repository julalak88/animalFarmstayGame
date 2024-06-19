using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Loading : MonoBehaviour
{

    public float time;
    public Transform circle;
    Tween tw;

    void OnEnable() {
        LoadingAnim();
    }

    void LoadingAnim() {
        tw = circle.DORotate(new Vector3(0, 0, 360), time, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void OnDisable() {

        if (tw != null) tw.Kill();
        tw = null;

    }
}
