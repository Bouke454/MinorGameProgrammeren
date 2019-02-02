using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    //Dit script hoort bij het menu en de methodes horen bij de knoppen

    //Start het spel op en zet het aantal kogels op 0 en highscore op 0
    public void StartGame() {
        PlayerPrefs.SetInt("HighscoreKey", 0);
        PlayerPrefs.SetInt("Ammo", 0);
        SceneManager.LoadScene("Scene_01");
    }
    //Opent de highscore pagina
    public void OpenHighscore() {
        SceneManager.LoadScene("Scores");
    }
}
