using System;
using UnityEngine;

public class Cat : InteractObject, ITalkable, IDoAction
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private AudioClip clip;
    int dialogueIndex = 0;
    public override void Interact()
    {
        if (dialogueController.talkingEnded && dialogueController.isTyping)
        {
            ProgressTalk();
        }

        Talk(dialogueText[dialogueIndex]);

        if (dialogueIndex == 1 && !dialogueController.talkingEnded)
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

        SoundFXManager.Instance.PlayFXClip(clip, gameObject.transform, 0.008f);


    }
    public void ProgressTalk()
    {

        dialogueIndex++;


    }

}
