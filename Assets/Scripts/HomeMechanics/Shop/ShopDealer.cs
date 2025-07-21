using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDealer : InteractObject, IDoAction
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] public List<ShopCard> cardsToGive = new List<ShopCard>();
    [SerializeField] private AudioClip bellRing;
    [SerializeField] private AudioClip[] talkSound;
    bool didAction = false;
    int i = 0;
   
    void OnEnable()
    {
        StartCoroutine("RingTheBell");
    }
    public override void Interact()
    {
        Talk(dialogueText[i]);

        if (i == 3 && !didAction && dialogueController.talkingEnded)
        {
            DoAction();
        }

    }
    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
        if (dialogueController.isTyping)
        {
            SoundFXManager.Instance.PlayRandomFXClip(talkSound, gameObject.transform, 0.1f);
        }
    }
    public override void DoAction()
    {
        for (int c = 0; c < cardsToGive.Count; c++)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().InventoryDeck.Add(new Card(CardManagementSystem.Instance.GetCardByName(cardsToGive[c].Title)));
        }
        StartCoroutine("KillShopDealer");
        cardsToGive.Clear();
        didAction = true;
    }
    public override void CheckDialogueEnd()
    {
        if (dialogueController.talkingEnded && i < dialogueText.Length - 1 && !dialogueController.isTyping)
        {
            i++;
        }
    }
    public IEnumerator KillShopDealer()
    {
        yield return new WaitForSeconds(4f);
        dialogueController.gameObject.SetActive(false);
        dialogueController.talkingEnded = true;
        dialogueController.isTyping = false;
        dialogueController.paragpaphs.Clear();
        Talk(dialogueText[i]);
        gameObject.SetActive(false);
        i = 0;
        didAction = false;
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().canMove == false)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
        }
    }
    public IEnumerator RingTheBell()
    {
        yield return new WaitForSeconds(1);
        SoundFXManager.Instance.PlayFXClip(bellRing, gameObject.transform, 0.1f);
    }

}


