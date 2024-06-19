using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderIcon : MonoBehaviour
{
    public Image image;

    public void SetImage(Sprite sprite) {
        image.sprite = sprite;
        image.SetNativeSize();
        gameObject.SetActive(true);
    }
}
