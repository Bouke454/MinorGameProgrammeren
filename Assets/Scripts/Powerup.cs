using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Powerup : MonoBehaviour {

    public GameObject powerup;
    public GameObject special;
    [SerializeField] private SceneController controller;
    [SerializeField] private Reload reload;
    [SerializeField] public TextMesh specialLabel;
    public AudioSource positive;
    public AudioSource negative;
    //Speciale extra goede powerups
    public AudioSource infinite;
    //kleuren
    private Color red = Color.red;
    private Color green = Color.green;
    private Color magenta = Color.magenta;
    private int Gamble;
    private int Respawn;
    public float timer;
    public bool hit;
    private void Start() {
        System.Random rnd = new System.Random();
        Gamble = rnd.Next(7); 

        //respawn tijd wanneer het bonus object mag verschijnen
        Respawn = rnd.Next(7,15); 
        timer = Respawn;

    }
    private void Update() {
        if (timer <= 0) {
            System.Random rnd = new System.Random();
            gameObject.SetActive(true);
            //powerup laten spawnen
            int PositionX = rnd.Next(-7, 7);
            Instantiate(powerup, new Vector3(PositionX, 4.09f, 0), Quaternion.identity);
            //Respawn tijd veranderen
            Respawn = rnd.Next(1, 19);
            timer = Respawn;
            //powerup type veranderen
            Gamble = rnd.Next(7);
        } else {
            timer -= Time.deltaTime;
        }
    }

    // Use this for initialization
    public void OnMouseDown() {
        if (hit == false) {
            switch (Gamble) {
                //Add +5 ammunition voor de speler
                case 0:
                    positive.Play();
                    controller.ammo = controller.ammo + 5;
                    controller.ammoLabel.text = "Ammo: " + controller.ammo;
                    specialLabel.text = "+ 5 ammunitie toegevoegd";
                    special.SetActive(true);
                    powerup.SetActive(false);
                    hit = true;
                    Invoke("StopPowerUps", 5.0f);
                    break;
                case 1:
                    positive.Play();
                    controller.cardWait = 0.1f;
                    specialLabel.text = "Kaarten draaien sneller om voor 15 seconden";
                    special.SetActive(true);
                    powerup.SetActive(false);
                    hit = true;
                    Invoke("StopPowerUps", 15.0f);
                    break;
                case 2:
                    positive.Play();
                    reload.ReloadCounter = 2;
                    specialLabel.text = "Herladen gaat 2x zo snel voor 3 seconden";
                    special.SetActive(true);
                    powerup.SetActive(false);
                    hit = true;
                    Invoke("StopPowerUps", 3.0f);
                    break;
                case 3:
                    negative.Play();
                    reload.ReloadCounter = 0;
                    specialLabel.text = "Herladen is tijdelijk niet mogelijk voor 2 seconden";
                    specialLabel.color = red;
                    special.SetActive(true);
                    powerup.SetActive(false);
                    hit = true;
                    Invoke("StopPowerUps", 2.0f);
                    break;
                case 4:
                    negative.Play();
                    reload.ReloadCounter = -1;
                    specialLabel.text = "Herladen gaat fout voor 3 seconden";
                    specialLabel.color = red;
                    special.SetActive(true);
                    powerup.SetActive(false);
                    hit = true;
                    Invoke("StopPowerUps", 3.0f);
                    break;
                case 5:
                    negative.Play();
                    controller.ammo = controller.ammo - 5;
                    controller.ammoLabel.text = "Ammo: " + controller.ammo;
                    specialLabel.text = "-5 ammunitie toegevoegd";
                    specialLabel.color = red;
                    special.SetActive(true);
                    powerup.SetActive(false);
                    hit = true;
                    Invoke("StopPowerUps", 2.0f);
                    break;
                case 6:
                    infinite.Play();
                    controller.AmmoCost = 0;
                    controller.cardWait = 0f;
                    specialLabel.text = "Beast mode voor 13 seconden";
                    specialLabel.color = magenta;
                    special.SetActive(true);
                    powerup.SetActive(false);
                    hit = true;
                    Invoke("StopPowerUps", 13.0f);
                    break;
            }
        }
    }

    //In deze methode worden alle waardes weer correct terug gezet
    //Die aangepast waren tijdens de powerup
    public void StopPowerUps() {
           controller.cardWait = 0.5f;
           reload.ReloadCounter = 1;
           controller.AmmoCost = 1;
           specialLabel.color = green;
           specialLabel.text = "None";
           special.SetActive(false);
           GameObject Clone = GameObject.Find("powerup_block(Clone)");
           Destroy(Clone);
           Destroy(gameObject);

    }
}
