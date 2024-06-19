using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float moveSpeed;
    public List<Sprite> movement;
    float time;
    int countAnim;
    public void OnMovement() {
        if (movement.Count > 0) {
            time += Time.deltaTime;
            if (time > moveSpeed) {
                time = 0;
                sprite.sprite = movement[(++countAnim) % movement.Count];
            }
        }
    }

}
