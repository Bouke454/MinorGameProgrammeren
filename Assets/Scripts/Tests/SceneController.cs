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
        //Countdown timer die voordurend bijgewerkt moet worden vandaar dat deze in de update staat
        if (timer >= 0.0f && canCount && FinishedLevel == false) {
            timer -= Time.deltaTime;
            //de F staat voor dat de float word omgezet in een string waarde d.m.v. ToString()
            CountDownLabel.text = timer.ToString("F");
        } else if (timer <= 0.0f && !doOnce) {
            canCount = false;
            doOnce = true;
            CountDownLabel.text = "0.00";
            timer = 0.0f;
            GameOver();
        }
    }
    //Wanneer de tijd is verstreken begeleid de speler naar het menu
    void GameOver() {
        SceneManager.LoadScene("Menu");
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
    //Dit word aangeroepen wanneer er geen kaarten meer over zijn
    public void ActivateUI() {
        completeLevelUI.SetActive(true);
    }

    private IEnumerator CheckMatch() {
        //Wanneer de 2 id's met elkaar matchen dan word de score met +1 verhoogd
        if (_firstRevealed.id == _secondRevealed.id) {
            Score.Play();
            _score++;
            //bekijken of de score gelijk is aan het aantal kaarten omgeslagen per level
            //Hierin word het aantal kaarten beschikbaar per combinatie in het level getoond
            //En vervolgens vergeleken met aantal behaalde score (+1 per combinatie)
            //Wanneer dit overeenkomt is het bekend dat er geen kaarten meer beschikbaar zijn
            //En is het level hierbij gehaald
            var cardsLeft = cardCols * cardRows / 2;
            if (_score == cardsLeft) {
                FinishedLevel = true;
                Complete.Play();
                //Wacht 4 seconden voordat de ActivateUI functie word aangeroepen d.m.v. Invoke
                Invoke("ActivateUI", 4f);


                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        } else {
            //Wacht 0,5 seconden en beide kaarten worden weer veranderd naar de achterkant van de kaart. 
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }
        //De initalisatie van aantal geklikte kaarten op het moment word hierbij ook op null gezet
        _firstRevealed = null;
        _secondRevealed = null;

    }
}
