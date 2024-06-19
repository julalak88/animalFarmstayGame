using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMap : MonoBehaviour
{
    public bool unlock;
    public int index;
    [HideInInspector]
    public LocationDatabase location;
    public Image image;
    public Image button;
    public Sprite[] sing;
    MiniMapController miniMap;
    SoundManager soundManager;
    private void Awake() {
        miniMap = GetComponentInParent<MiniMapController>();
        if (soundManager == null) soundManager = SoundManager.Ins;
    }

    public void SetupMap() {
      
        if (unlock) {
            button.sprite = sing[0];
        } else {
            button.sprite = sing[1];
        }
        image.enabled = unlock;
    }

    public void Selected() {
        soundManager.PlaySFX("Click");
        if (unlock) {
            miniMap.MoveToScene(index);
        } else {
            miniMap.OnClickUnlock(this);
        }
    }
}
