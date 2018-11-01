
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {

    //De button heeft een verwijzing naar de functie StartGame()
    //Deze functie maakt het mogelijk om het eerst volgende level op te starten
    public void StartGame() {
        SceneManager.LoadScene("Scene_01");
    }

}
