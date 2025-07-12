using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D _rb;
    Vector2 _moveDirection;
    private
    void Update()
    {
        MovePlayer();
        if (_moveDirection == Vector2.zero)
        {
            SnapToGrid();
        }
       
        Debug.Log(_moveDirection);
    }
    private void MovePlayer()
    {
        _moveDirection = (transform.right * Input.GetAxisRaw("Horizontal") + transform.up * Input.GetAxisRaw("Vertical")).normalized;
        Mathf.Round(_moveDirection.x);
        Mathf.Round(_moveDirection.y);
        _rb.linearVelocity = _moveDirection * speed;
    }
    private void SnapToGrid()
    {
        float snapX = Mathf.Round(transform.position.x / 0.0625f) * 0.0625f;
        float snapY = Mathf.Round(transform.position.y / 0.0625f) * 0.0625f;
        transform.position = new Vector3(snapX, snapY);
    }
}
