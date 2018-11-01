using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    //De grootte van het bord
    public int cardRows = 2;
    public int cardCols = 4;
    //Aantal kaarten op het bord 
    public int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4 };

    //Afstand van de kaarten op het bord
    public float cardPositionX = 4f;
    public float cardPositionY = 5f;

    //Aantal kogels

    public int ammo = 3;
    public int lives = 3;
    [SerializeField] public TextMesh ammoLabel;
    [SerializeField] private TextMesh cardsLeftLabel;

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;
    private int _score = 0;
    //animatie voor het laden van het volgende level
    public GameObject completeLevelUI;
    //Game sound effects
    public AudioSource Gun;
    public AudioSource Score;
    public AudioSource Complete;
    [SerializeField] private MainCard originalCard;
    //Aantal verschillende kaarten op het bord
    [SerializeField] private Sprite[] images;

    //Timer
    [SerializeField] public TextMesh CountDownLabel;
    [SerializeField] private float mainTimer;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    //
    private bool FinishedLevel = false;

    private void Start() {
        timer = mainTimer;
        ammoLabel.text = "Ammo: " + ammo;
        Vector3 startPos = originalCard.transform.position; //The position of the first card. All other cards are offset from here.

        numbers = ShuffleArray(numbers); //This is a function we will create in a minute!

        for (int i = 0; i < cardCols; i++) {
            for (int j = 0; j < cardRows; j++) {
                MainCard card;
                if (i == 0 && j == 0) {
                    card = originalCard;
                } else {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * cardCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (cardPositionX * i) + startPos.x;
                float posY = (cardPositionY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    public void Update() {
        var cardsLeft = cardCols * cardRows / 2 - _score;
        cardsLeftLabel.text = "Cards left: " + cardsLeft;
        if (timer >= 0.0f && canCount && FinishedLevel == false) {
            timer -= Time.deltaTime;
            CountDownLabel.text = timer.ToString("F");
        } else if (timer <= 0.0f && !doOnce) {
            canCount = false;
            doOnce = true;
            CountDownLabel.text = "0.00";
            timer = 0.0f;
            GameOver();
        }
    }

    void GameOver() {
        SceneManager.LoadScene("Menu");
    }

    private int[] ShuffleArray(int[] numbers) {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------


    public bool canReveal {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card) {
        if (_firstRevealed == null && ammo != 0) {
            _firstRevealed = card;
            Gun.Play();
            ammo = ammo - 1;
            ammoLabel.text = "Ammo: " + ammo;
        } else if(ammo != 0) {
            _secondRevealed = card;
            Gun.Play();
            ammo = ammo - 1;
            ammoLabel.text = "Ammo: " + ammo;
            StartCoroutine(CheckMatch());
        }
    }

    public void ActivateUI() {
        completeLevelUI.SetActive(true);
    }

    private IEnumerator CheckMatch() {
        if (_firstRevealed.id == _secondRevealed.id) {
            Score.Play();
            _score++;
            //bekijken of de score gelijk is aan het aantal kaarten omgeslagen per level
            var cardsLeft = cardCols * cardRows / 2;
            if (_score == cardsLeft) {
                FinishedLevel = true;
                Complete.Play();
                Invoke("ActivateUI", 5f);


                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        } else {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;

    }
}
