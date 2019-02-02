using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscoreShow : MonoBehaviour {
    public Text HighScore;
    public Text BestHighScore;
    // Use this for initialization
    void Start () {
        //Als de highscore hoger is dan de huidige
        if(PlayerPrefs.GetInt("HighscoreKey") > PlayerPrefs.GetInt("ScoreHighest")) {
            //Maak variable van de oorspronkelijke highscore
            int AchievedHighscore = PlayerPrefs.GetInt("HighscoreKey");
            //Zet deze om in de huide highscore
            PlayerPrefs.SetInt("ScoreHighest", AchievedHighscore);
            //Toon de huidige highscore
            BestHighScore.text = "Beste: " + PlayerPrefs.GetInt("ScoreHighest").ToString();
            HighScore.text = PlayerPrefs.GetInt("HighscoreKey").ToString();
        } else {
            BestHighScore.text = "Beste: "+ PlayerPrefs.GetInt("ScoreHighest").ToString();
            HighScore.text = PlayerPrefs.GetInt("HighscoreKey").ToString();
        }
    }
	

}
