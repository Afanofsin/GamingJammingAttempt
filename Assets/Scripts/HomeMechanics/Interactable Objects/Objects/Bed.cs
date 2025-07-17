using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Bed : InteractObject, IDoAction
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    bool didAction = false;
    int i = 0;
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
