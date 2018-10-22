using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTouch : MonoBehaviour {
    public Color highlightColor = Color.cyan;
    public void OnMouseOver() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) {
            sprite.color = highlightColor;
        }
    }

    public void OnMouseExit() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) {
            sprite.color = Color.white;
        }
    }


}
