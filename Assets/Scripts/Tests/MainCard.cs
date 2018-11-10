using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour {
    //Haalt het object SceneController op deze bevat immers het aantal kaarten en ammunitie
    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;

    public AudioSource Empty;

    //Wanneer de gebruiker op een kaart klik word gekeken of dezze al actief staat en of
    //deze daarnaast getoond kan worden. Met als extra of de speler nog genoeg ammunitie heeft.
    public void OnMouseDown() {
        if (Card_Back.activeSelf && controller.canReveal && controller.ammo != 0) {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
        else if(controller.ammo <= 0) {
            Empty.Play();
        }
    }

    private int _id;
    public int id {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite image) {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
        //Dit krijgt de sprite renderer component en verandert de eigenschap van de sprite!
    }

    public void Unreveal() {
        Card_Back.SetActive(true);
    }


}
