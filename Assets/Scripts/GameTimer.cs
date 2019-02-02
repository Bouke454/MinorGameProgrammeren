using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour {
    //Timer
    [SerializeField] public TextMesh CountDownLabel;
    public float mainTimer;
    public float timer;
    private bool canCount = true;
    private bool doOnce = false;

    //
    private bool FinishedLevel = false;

    private void Start() {
        timer = mainTimer;
    }
    public void Update() {
        //Countdown timer die voordurend bijgewerkt moet worden vandaar dat deze in de update staat
        if (timer >= 0.0f && canCount && FinishedLevel == false) {
            timer -= Time.deltaTime;
            //de F staat voor dat de float word omgezet in een string waarde d.m.v. ToString()
            CountDownLabel.text = "Tijd: "+ timer.ToString("F");
        } else if (timer <= 0.0f && !doOnce) {
            canCount = false;
            doOnce = true;
            CountDownLabel.text = "Tijd: 0.00";
            timer = 0.0f;
            GameOver();
        }
    }
    //Wanneer de tijd is verstreken begeleid de speler naar het menu
    void GameOver() {
        GameObject CurrentTimer = GameObject.Find("CountDown");
        Destroy(CurrentTimer);
        SceneManager.LoadScene("Scores");
    }

}