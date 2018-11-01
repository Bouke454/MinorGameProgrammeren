using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTouch : MonoBehaviour {
    //Bepaal de kleur 
    public Color highlightColor = Color.cyan;

    //Verander de kaart van kleur indien deze nog niet veranderd was 
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
