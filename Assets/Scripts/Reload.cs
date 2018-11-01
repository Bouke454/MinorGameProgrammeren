using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour {
    public Color highlightColor = Color.cyan;
    //Haalt het object SceneController op deze bevat immers het aantal kogels per scene
    [SerializeField] private SceneController controller;
    public AudioSource reload;
    public void OnMouseOver() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = highlightColor;
    }
    //Wanneer de gebruik klikt dan zal een geluidje afgespeeld worden en
    //de munitie van de speler met +1 bijgevuld worden
    public void OnMouseDown() {
        reload.Play();
        controller.ammo = controller.ammo + 1;
        controller.ammoLabel.text = "Ammo: " + controller.ammo;
    }

    public void OnMouseExit() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) {
            sprite.color = Color.white;
        }
    }

}
