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

        CheckDialogueEnd();
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().InventoryDeck.Add(new Card(CardManagementSystem.Instance.GetCardByName("Take Rest")));
        EventManager.Instance.OnCLick();
        didAction = true;
    }
    public override void CheckDialogueEnd()
    {
        if (dialogueController.CanProgress && i < dialogueText.Length - 1)
        {
            i++;
        }
    }

}
