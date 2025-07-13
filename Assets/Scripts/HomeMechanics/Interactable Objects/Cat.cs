using UnityEngine;
using UnityEngine.Rendering;

public class Cat : InteractObject, ITalkable, IDoAction
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController; 
    [SerializeField] private AudioClip clip;
    public override void Interact()
    {
        Talk(dialogueText[1]);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }
    public override void DoAction()
    {
        SoundFXManager.Instance.PlayFXClip(clip, transform, 0.008f);
    }

}
