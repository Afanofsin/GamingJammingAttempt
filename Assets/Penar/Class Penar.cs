using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class ClassPenar : MonoBehaviour
{
    public GameObject _gameObject;  // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _gameObject.transform.position += Vector3.forward * 0.5f * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {

            CreateLagSpike();
        }
    }
    private void CreateLagSpike()
    {
        for (int i = 0; i < 1000000000; i++)
        {
            Vector3 lag = new Vector3(Mathf.Sin(i), Mathf.Cos(i), Mathf.Tan(i));
        }
    }
}
