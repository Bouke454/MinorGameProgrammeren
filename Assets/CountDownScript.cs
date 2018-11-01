using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour {
    [SerializeField] public TextMesh CountDownLabel;
    [SerializeField] private float mainTimer;
    [SerializeField] public SceneController controller;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    void Start() {
        timer = mainTimer;
    }

    void Update() {
        if (timer >= 0.0f && canCount) {
            timer -= Time.deltaTime;
            CountDownLabel.text = timer.ToString("F");
        } else if (timer <= 0.0f && !doOnce) {
            canCount = false;
            doOnce = true;
            CountDownLabel.text = "0.00";
            timer = 0.0f;
            if(controller.lives != 0) {
                LoseLive();
            } else {
                GameOver();
            }
        }
    }
    void LoseLive() {
        controller.lives = controller.lives - 1;
        timer = mainTimer;
        canCount = true;
        doOnce = false;
    }

    void GameOver() {
        SceneManager.LoadScene("Menu");
    }
}
