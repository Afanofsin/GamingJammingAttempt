using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectName;
    [SerializeField] private TextMeshProUGUI objectDialogueText;
    private Queue<string> paragpaphs = new Queue<string>();
    private bool talkingEnded;
    private bool isRepeatable = true;
    private string p;
    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        if (paragpaphs.Count == 0)
        {
            if (!talkingEnded)
            {
                StartTalking(dialogueText);
            }
            else
            {
                StopTalking();
                return;
            }

        }
        p = paragpaphs.Dequeue();
        objectDialogueText.text = p;
        if (paragpaphs.Count == 0)
        {
            talkingEnded = true;
        }
    }
    private void StartTalking(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        objectName.text = dialogueText.speakerName;
        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragpaphs.Enqueue(dialogueText.paragraphs[i]);
        }    
    }
    private void StopTalking()
    {
        paragpaphs.Clear();
        talkingEnded = true;
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        
    }

}
