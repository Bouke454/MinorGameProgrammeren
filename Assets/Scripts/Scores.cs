using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scores : MonoBehaviour {

    public void StartGame() {
        GameObject Mig = GameObject.Find("MusicInGame");
        Destroy(Mig);
        SceneManager.LoadScene("Menu");
    }
}
