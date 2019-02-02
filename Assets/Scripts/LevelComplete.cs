using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {
    public GameObject completeLevelUI;
    public SceneController controller;
    public AudioSource Complete;
    //Wanneer de animatie voorbij is word de LoadNextLevel() 
    //Functie aangeroepen. Deze functie heeft als doel dat unity de volgende scene ophaalt.
    //De +1 zorgt ervoor dat de unity de eerste volgende scene volgens de build order oppakt.

    //private void Start() {
    //    Scene scene = SceneManager.GetActiveScene();
    //    if (scene.name == "Scene_Init") {
    //        SceneManager.LoadScene("Scene_01");
    //    }
    //}
    private void Update() {
       if( controller.FinishedLevel == true) {
            Complete.Play();
            completeLevelUI.SetActive(true);
        }
    }
    public void LoadNextLevel() {
        Scene scene = SceneManager.GetActiveScene();
        completeLevelUI.SetActive(false);
        if(scene.name != "Scene_05") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            SceneManager.LoadScene("Scene_02");
        }
    }
}
