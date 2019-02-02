using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    //De grootte van het bord
    public int cardRows = 2;
    public int cardCols = 4;
    public float cardWait = 0.5f;
    //Aantal kaarten op het bord 
    public int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4 };

    //Afstand van de kaarten op het bord
    public float cardPositionX = 4f;
    public float cardPositionY = 5f;

    //Aantal kogels
    public int ammo;
    [SerializeField] public TextMesh ammoLabel;
    [SerializeField] private TextMesh cardsLeftLabel;

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;
    public TextMesh GameScore;
    private int _score = 0;
    public int AmmoCost = 1;
    //Game sound effects
    public AudioSource Gun;
    public AudioSource Score;
    [SerializeField] private MainCard originalCard;
    //Aantal verschillende kaarten op het bord
    [SerializeField] private Sprite[] images;
    //
    public bool FinishedLevel = false;

    private void Start() {
        ammo = PlayerPrefs.GetInt("Ammo");
        if (PlayerPrefs.GetInt("HighscoreKey") != 0) {
            GameScore.text = "Highscore: " + PlayerPrefs.GetInt("HighscoreKey");
        }
        ammoLabel.text = "Ammo: " + PlayerPrefs.GetInt("Ammo");
        //De positie van de eerste kaart. Alle andere kaarten zijn vanaf hier verschoven.
        Vector3 startPos = originalCard.transform.position;
        //Binnen deze functie worden de nummers (kaarten) van positie veranderd op het bord willekeurig
        numbers = ShuffleArray(numbers);
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
        //Bereken op basis van kaarten kolom en rijen /2 -_score hoeveel combinaties nog gemaakt
        //moeten worden voordat het level voorbij is
        var cardsLeft = cardCols * cardRows / 2 - _score;
        cardsLeftLabel.text = "Kaarten over: " + cardsLeft;
    }

    //Binnen deze functie worden de nummers van de initalisatie door elkaar heen gehusseld
    private int[] ShuffleArray(int[] numbers) {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            //de willekeurige waarde word bepaald door de lengte van de nieuwe array
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    //Toon de tweede waarde _secondRevealed als null
    public bool canReveal {
        get { return _secondRevealed == null; }
    }

    //Hierin word bepaald of de kaarten een combinatie vormen en of dit ook zo is
    //Het bekijken van de combinatie word afgehandeld wanneer beide kaarten getoond zijn
    //In de functie StartCoroutine(CheckMatch())
    public void CardRevealed(MainCard card) {
        if (_firstRevealed == null && ammo != 0) {
            _firstRevealed = card;
            Gun.Play();
            ammo = ammo - AmmoCost;

            ammoLabel.text = "Ammo: " + ammo;
            PlayerPrefs.SetInt("Ammo", ammo);
        } else if (ammo != 0) {
            _secondRevealed = card;
            Gun.Play();
            ammo = ammo - AmmoCost;
            ammoLabel.text = "Ammo: " + ammo;
            PlayerPrefs.SetInt("Ammo", ammo);
            StartCoroutine(CheckMatch());
        }
    }
    //Dit word aangeroepen wanneer er geen kaarten meer over zijn

    private IEnumerator CheckMatch() {
        //Wanneer de 2 id's met elkaar matchen dan word de score met +1 verhoogd
        if (_firstRevealed.id == _secondRevealed.id) {
            Score.Play();
            _score++;

            //Playerprefs highscore bijwerken ophalen converten bijwerken! 

            int score = PlayerPrefs.GetInt("HighscoreKey") + 10;

            PlayerPrefs.SetInt("HighscoreKey", score);
            GameScore.text = "Highscore: " + PlayerPrefs.GetInt("HighscoreKey");
            //bekijken of de score gelijk is aan het aantal kaarten omgeslagen per level
            //Hierin word het aantal kaarten beschikbaar per combinatie in het level getoond
            //En vervolgens vergeleken met aantal behaalde score (+1 per combinatie)
            //Wanneer dit overeenkomt is het bekend dat er geen kaarten meer beschikbaar zijn
            //En is het level hierbij gehaald
            var cardsLeft = cardCols * cardRows / 2;
            if (_score == cardsLeft) {
                FinishedLevel = true;


                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        } else {
            //Wacht 0,5 seconden en beide kaarten worden weer veranderd naar de achterkant van de kaart. 
            yield return new WaitForSeconds(cardWait);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        //De initalisatie van aantal geklikte kaarten op het moment word hierbij ook op null gezet
        _firstRevealed = null;
        _secondRevealed = null;

    }
}