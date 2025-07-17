using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Lamp : InteractObject, IDoAction
{
    [SerializeField] private GameObject lampLight;
    public override void Interact()
    {
            DoAction();
    }
    public override void DoAction()
    {
        SwitchLamp();
    }

 public void SwitchLamp()
    {
        if (lampLight.activeSelf)
        {
            lampLight.SetActive(false);
        }
        else lampLight.SetActive(true);

    }

}
