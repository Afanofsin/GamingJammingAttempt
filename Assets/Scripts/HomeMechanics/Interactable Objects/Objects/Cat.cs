using System;
using UnityEngine;

public class Cat : InteractObject, ITalkable, IDoAction
{
    public Quest quest;
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private AudioClip clip;
    int dialogueIndex = 0;
    private void Start()
    {
        quest.Initialize();
    }
    public override void Interact()
    {
        if (dialogueController.talkingEnded && !dialogueController.isTyping)
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
        if (dialogueController.talkingEnded && dialogueIndex < dialogueText.Length - 1 && !dialogueController.isTyping)
        {
            dialogueIndex++;
        }
    }

}
