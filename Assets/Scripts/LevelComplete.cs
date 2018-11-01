using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

    //Wanneer de animatie voorbij is word de LoadNextLevel() 
    //Functie aangeroepen. Deze functie heeft als doel dat unity de volgende scene ophaalt.
    //De +1 zorgt ervoor dat de unity de eerste volgende scene volgens de build order oppakt.
    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
