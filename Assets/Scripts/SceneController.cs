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
    [SerializeField] private TextMesh scoreLabel;

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;


    public AudioSource Gun;
    public AudioSource Score;
    [SerializeField] private MainCard originalCard;
    //Aantal verschillende kaarten op het bord
    [SerializeField] private Sprite[] images;
    

    private void Start() {
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

    private IEnumerator CheckMatch() {
        if (_firstRevealed.id == _secondRevealed.id) {
            Score.Play();
            _score++;
            scoreLabel.text = "Combo: " + _score;
            //bekijken of de score gelijk is aan het aantal kaarten omgeslagen per level
            var cardsLeft = cardCols * cardRows / 2;
            if (_score == cardsLeft) {
                Debug.Log("einde van het level");
            }
        } else {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;

    }

    public void Restart() {
        SceneManager.LoadScene("Menu");
    }
}
