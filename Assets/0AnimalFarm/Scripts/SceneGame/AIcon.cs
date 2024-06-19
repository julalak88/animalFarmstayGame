using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIcon : MonoBehaviour
{
    Vector3 oriScale;

    private void Awake() {
        oriScale = transform.localScale;
    }

    private void OnEnable() {
        transform.localScale = Vector3.zero;
        transform.DOScale(oriScale, .3f).SetEase(Ease.OutBack);
    }
}
