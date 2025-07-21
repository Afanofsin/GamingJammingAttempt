using UnityEngine;

public class Plant : InteractObject, IInteractable
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    public Progression progression;
    bool didAction = false;
    int i = 0;
    private void Start() {
         if (progression.isPlantCardCollected)
        {
            i = 2;
            didAction = true;
        }
    }

    public override void Interact()
    {
        Talk(dialogueText[i]);
        CheckDialogueEnd();
        if (i == 1 && !didAction && dialogueController.talkingEnded)
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
        progression.isPlantCardCollected = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().InventoryDeck.Add(new Card(CardManagementSystem.Instance.GetCardByName("Healthy Meal")));
        EventManager.Instance.OnCLick();
        didAction = true;
    }
    public override void CheckDialogueEnd()
    {
        if (dialogueController.talkingEnded && i < dialogueText.Length - 1 && !dialogueController.isTyping)
        {
            i++;
        }
    }

}

