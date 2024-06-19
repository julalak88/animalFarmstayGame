using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    public List<Sprite> sprites;

    CloudObject[] clouds;

    private void Awake() {
        clouds = GetComponentsInChildren<CloudObject>();
        for (int i = 0; i < clouds.Length; i++) {
            clouds[i].manager = this;
        }
    }

    public void UpdateClouds() {
        for (int i = 0; i < clouds.Length; i++) {
            clouds[i].UpdateCloud();
        }
    }

    public Sprite RandomSprite() {
        int ind = UnityEngine.Random.Range(0, sprites.Count);
        return sprites[ind];
    }


}
