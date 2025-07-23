using UnityEngine;

public class Computer : InteractObject, IDoAction
{
    [SerializeField] private GameObject pcWindow;
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
        SwitchPc();
    }


    public void SwitchPc()
    {
        if (!pcWindow.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            didAction = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
            pcWindow.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            didAction = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().StopPlayer();
            pcWindow.SetActive(false);
        }

    }

    public override void CheckDialogueEnd()
    {
    }
}
