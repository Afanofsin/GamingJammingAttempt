using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;
public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectName;
    [SerializeField] private TextMeshProUGUI objectDialogueText;
    [SerializeField] private float typeSpeed = 10;
    [HideInInspector] public bool talkingEnded;
    [HideInInspector] public bool isTyping = false;
    public Queue<string> paragpaphs = new Queue<string>();
    private string p;
    private Coroutine typeDialogueCoroutine;
    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;
    public bool CanProgress = false;

    private void OnEnable()
    {
        CanProgress = false;
    }

    private void OnDisable()
    {
        CanProgress = true;
    }

    public void DisplayNextParagraph(DialogueText dialogueText)
    {

        if (paragpaphs.Count == 0)
        {
            if (!talkingEnded)
            {
                StartTalking(dialogueText);
            }
            else if (talkingEnded && !isTyping)
            {
                StopTalking();
                return;
            }
        }
        
        if (!isTyping)
        {
            p = paragpaphs.Dequeue();
            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }
        else
        {
            FInishTypingEarly();
        }
    }
    private void StartTalking(DialogueText dialogueText)
    {
        talkingEnded = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
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
        talkingEnded = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
        paragpaphs.Clear();
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

    }
    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;
        objectDialogueText.text = "";
        string originalText = p;
        string displayText = "";
        int aplhaText = 0;
        foreach (char character in p.ToCharArray())
        {
            aplhaText++;
            objectDialogueText.text = originalText;
            displayText = objectDialogueText.text.Insert(aplhaText, HTML_ALPHA);
            objectDialogueText.text = displayText;
            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }
        isTyping = false;
    }
    private void FInishTypingEarly()
    {
        StopCoroutine(typeDialogueCoroutine);
        objectDialogueText.text = p;
        isTyping = false;
    }
}
