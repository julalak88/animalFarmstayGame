using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Remover : MonoBehaviour
{
    public float delayTime = 2f;

    private void Start() {
        DOVirtual.DelayedCall(delayTime, () => Destroy(gameObject));
    }
}
