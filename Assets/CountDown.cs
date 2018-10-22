using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour {
    [SerializeField] public TextMesh countDownLabel;
    [SerializeField] private float mainTimer;
    private float timer;
    private bool canCount = false;
    private bool doOnce = false;
    // Use this for initialization
    void Start () {
        timer = mainTimer;
    }

    // Update is called once per frame
    void Update() {
        if (timer >= 0.0f && canCount) {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            countDownLabel.text = timer.ToString("F");
        } else if (timer <= 0.0f && !doOnce) {
            canCount = false;
            doOnce = true;
            countDownLabel.text = "0.00";
            timer = 0.0f;
            Gameover();
        }
    }
    void Gameover() {
        SceneManager.LoadScene("Scene_01");
    }
}
