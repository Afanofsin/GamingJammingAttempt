using UnityEngine;

public class InterfaceViewSystem : MonoBehaviour
{
    public static InterfaceViewSystem Instance;
    [SerializeField]
    private GameObject TutorialScreen;

    private bool isTutorialOn = false;

    public void SwitchTutorial()
    {
        if (isTutorialOn) 
        {
            TutorialScreen.SetActive(true);
            isTutorialOn=false;
        }
        else
        {
            TutorialScreen.SetActive(false);
            isTutorialOn=true;
        }
            
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
