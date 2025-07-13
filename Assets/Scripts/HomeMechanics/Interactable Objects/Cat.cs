using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Cat : InteractObject, ITalkable, IDoAction
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private AudioClip clip;
    bool didAction = false;
    int i = 0;
    public override void Interact()
    {
        Talk(dialogueText[i]);
        CheckDialogueEnd();
        if (i == dialogueText.Length - 1 && !didAction && dialogueController.talkingEnded)
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
        didAction = true;
        SoundFXManager.Instance.PlayFXClip(clip, transform, 0.008f);
    }
    public void CheckDialogueEnd()
    {
        if (dialogueController.talkingEnded && i < dialogueText.Length - 1)
        {
            i++;
        }
    }

}
