using UnityEngine;

public class Television : InteractObject, IDoAction
{
    [SerializeField] private GameObject shopWindow;
    private bool didAction = false;
    public override void Interact()
    {
        if (didAction == false)
        {
            DoAction();
        }

    }
    public override void DoAction()
    {
        SwitchShop();
    }


    public void SwitchShop()
    {
        if (!shopWindow.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            didAction = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
            shopWindow.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            didAction = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
            shopWindow.SetActive(false);
        }

    }
}
