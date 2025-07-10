using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class ClassPenar : MonoBehaviour
{
    public GameObject _gameObject;
    Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i =0; i < 2; i++) Instantiate(_gameObject, gameObject.transform.position, gameObject.transform.rotation);
    }
}
