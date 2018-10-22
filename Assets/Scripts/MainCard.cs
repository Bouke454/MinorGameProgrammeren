using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour {

    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;

    public AudioSource Empty;
    public void OnMouseDown() {
        if (Card_Back.activeSelf && controller.canReveal && controller.ammo != 0) {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
        else if(controller.ammo == 0) {
            Empty.Play();
        }
    }

    private int _id;
    public int id {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite image) {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image; //This gets the sprite renderer component and changes the property of it's sprite!
    }

    public void Unreveal() {
        Card_Back.SetActive(true);
    }


}
