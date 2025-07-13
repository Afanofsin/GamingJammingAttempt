using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform cameraPos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.transform.position + new Vector3(0,0, -10);
    }
}
