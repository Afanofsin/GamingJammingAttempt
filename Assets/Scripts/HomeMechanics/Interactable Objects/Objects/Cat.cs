using System;
using UnityEngine;

public class Cat : InteractObject, ITalkable, IDoAction
{
    public Quest quest;
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private AudioClip clip;
    [SerializeField] private Progression progression;
    int dialogueIndex = 0;
    private void Start()
    {

        if (!progression.isCompletedCatQuest)
        {
            quest.Initialize();
        }
        else
        {
            dialogueIndex = 3;
        }
        if (progression.isFirstBossKilled)
        {
            dialogueIndex = 4;
        }
    }
    public override void Interact()
    {
          if (quest.Completed)
        {
            progression.isCompletedCatQuest = true;
        }
        
        if (dialogueController.talkingEnded && !dialogueController.isTyping)
        {
            CheckDialogueEnd();
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
        
    }

    public override void CheckDialogueEnd()
    {
        if (dialogueController.CanProgress && dialogueIndex < 2 )
        {
            dialogueIndex++;
        }
        if (quest.Completed && dialogueIndex < dialogueText.Length - 1)
        {
            dialogueIndex++;
        }

    }

}
