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
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }

        if (firstSelectedItem && secondSelectedItem)
        {
            return;
        }

        var clickedItem = EventSystem.current.currentSelectedGameObject.GetComponentInParent<CardScript>();
        if (!firstSelectedItem)
        {
            firstSelectedItem = clickedItem;
            firstSelectedItem.DisableCover();
        }
        else
        {
            secondSelectedItem = clickedItem;
            secondSelectedItem.DisableCover();
            CompareChosenItems();
        }
    }

    private void CompareChosenItems()
    {
        Sprite spriteA = firstSelectedItem.Below.sprite;
        Sprite spriteB = secondSelectedItem.Below.sprite;

        if (spriteA.name == spriteB.name)
        {
            numberOfMatches++;
            StartCoroutine(ResetAndCheckFinish(0, false));
        }
        else
        {
            StartCoroutine(ResetAndCheckFinish(2, true));
        }
        
}

    IEnumerator ResetAndCheckFinish(int numberOfSecondsToWait, bool shouldReset)
 {
 canvasGroup.interactable = false;

 yield return new WaitForSeconds(numberOfSecondsToWait);

 if (shouldReset)
 {

  if (firstSelectedItem != null)
            firstSelectedItem.EnableCover();

        if (secondSelectedItem != null)
            secondSelectedItem.EnableCover();
 
 }

 firstSelectedItem = null;
 secondSelectedItem = null;

 canvasGroup.interactable = true;
 if (numberOfMatches == listOfCards.Count / 2)
 {
 StartCoroutine(LoadFinalScene());
 }
 }

IEnumerator LoadFinalScene()
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
