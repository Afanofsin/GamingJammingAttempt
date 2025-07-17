using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Television : InteractObject, IDoAction
{
    [SerializeField] private DialogueText[] dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private GameObject shopWindow;
    private bool didAction = false;
    public override void Interact()
    {
        if (didAction == false)
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
        SwitchShop();
    }


    public void SwitchShop()
    {
        if (!shopWindow.activeSelf)
        {
            didAction = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
            shopWindow.SetActive(true);
        }
        else
        {
            didAction = false;
            shopWindow.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
        }

    }
}
