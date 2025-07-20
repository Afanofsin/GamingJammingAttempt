using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Bed : InteractObject, IDoAction
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private CardDataSO givableCard;
    public Progression progression;
    bool didAction = false;
    int i = 0;
    private void Start() {
        if (progression.isBedCardCollected)
        {
            i = 4;
            didAction = true;
        }
    }
    public override void Interact()
    {
        Talk(dialogueText[i]);

        ProgressTalk();
        if (i == 4 && !didAction && dialogueController.talkingEnded)
        {
            DoAction();
        }

    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }
    public override void DoAction()
    {
        progression.isBedCardCollected = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().InventoryDeck.Add(new Card(CardManagementSystem.Instance.GetCardByID(givableCard.ID)));
        Debug.Log("Added card to a player: " + new Card(CardManagementSystem.Instance.GetCardByID(givableCard.ID)).Title);
        EventManager.Instance.OnCLick();
        didAction = true;
    }
    public void ProgressTalk()
    {
        if (dialogueController.talkingEnded && i < dialogueText.Length - 1 && !dialogueController.isTyping)
        {
            i++;
        }
    }

}
