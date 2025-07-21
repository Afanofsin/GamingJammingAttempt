using UnityEngine;

public class ButtonLock : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    public void OnClick()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Destroy(canvas);
    }
}
