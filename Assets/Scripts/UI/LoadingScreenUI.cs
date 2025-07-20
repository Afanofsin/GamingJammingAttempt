using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreenUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Animator animatorSpinner;
    [SerializeField]
    private Animator animatorCharacter;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
    }

    public IEnumerator FadeIn()
    {
        animatorSpinner.enabled = true;
        animatorCharacter.enabled = true;
        float t = 0f;
        while(t < 1f)
        {
            t += Time.unscaledDeltaTime * 2f;
            canvasGroup.alpha = Mathf.Lerp(0,1,t);
            yield return null;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public IEnumerator FadeOut()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime * 2f;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        animatorSpinner.enabled = false;
        animatorCharacter.enabled = false;
    }
}
