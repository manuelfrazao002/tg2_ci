using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CardsManager : MonoBehaviour
{
    [SerializeField]
    private List<CardScript> listOfCards;

    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private AudioSource victoryMusic;

    [SerializeField]
    private TimerScript timerScript;

    private CardScript firstSelectedItem;
    private CardScript secondSelectedItem;
    private int numberOfMatches = 0;
    private CanvasGroup canvasGroup;

    private bool canSelect = true;


    public void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("⚠️ CanvasGroup não encontrado no GameObject pai. Verifica se está presente!");
        }

        if (listOfCards.Count / 2 != sprites.Count)
        {
            throw new ApplicationException("O GameManager está mal configurado");
        }

        List<Sprite> duplicatedSprites = new List<Sprite>();
        foreach (var sprite in sprites)
        {
            duplicatedSprites.Add(sprite);
            duplicatedSprites.Add(sprite);
        }

        Shuffle(duplicatedSprites);

        for (int i = 0; i < listOfCards.Count; i++)
        {
            listOfCards[i].SetBelowImage(duplicatedSprites[i]);
        }

        for (int i = 0; i < listOfCards.Count; i++)
        {
            listOfCards[i].transform.SetSiblingIndex(i);
        }


        void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
    public void OnCardClick()
    {

        if (!canSelect)
            return;

        if (EventSystem.current.currentSelectedGameObject == null)
            return;
        

        var clickedItem = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CardScript>();

        if (clickedItem == null)
            return;

        if (clickedItem.IsFaceUp || firstSelectedItem == clickedItem)
            return;

        StartCoroutine(HandleCardClick(clickedItem));
    }

    private IEnumerator HandleCardClick(CardScript clickedItem){
        canSelect = false;

        yield return StartCoroutine(clickedItem.FlipCardCoroutine());

        if (firstSelectedItem == null)
        {
            firstSelectedItem = clickedItem;
            canSelect = true;
            yield break;
        }

        secondSelectedItem = clickedItem;

        yield return new WaitForSeconds(0.3f);

        if (firstSelectedItem.Below.sprite.name == secondSelectedItem.Below.sprite.name)
        {
            numberOfMatches++;

            firstSelectedItem = null;
            secondSelectedItem = null;

            if (numberOfMatches == listOfCards.Count / 2)
            {
                StartCoroutine(LoadFinalScene());
            }
            canSelect = true;
        }
        else{
            yield return new WaitForSeconds(1.0f);

            yield return StartCoroutine(firstSelectedItem.FlipCardCoroutine());
            yield return StartCoroutine(secondSelectedItem.FlipCardCoroutine());

            firstSelectedItem = null;
            secondSelectedItem = null;

            canSelect = true;
        }
    }


    private IEnumerator LoadFinalScene()
        {
         GameManager.SetSeconds(timerScript.GetTimerAndStop());

        //Toca o audio de vitória
         victoryMusic.Play();
         //Espera que o audio termine
            yield return new WaitForSeconds(victoryMusic.clip.length);

              NivelManager.ultimoNivel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
              //Carrega a outra cena
             SceneManager.LoadScene("Nivel4-5");
 }

}
