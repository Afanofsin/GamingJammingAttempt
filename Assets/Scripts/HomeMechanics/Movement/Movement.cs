using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D _rb;
    Vector2 _moveDirection;
    public bool canMove = true;
    void Update()
    {
        if (canMove)
        {
            MovePlayer();
        }

        if (_moveDirection == Vector2.zero)
        {
            SnapToGrid();
        }

    }
    private void MovePlayer()
    {
        _moveDirection = (transform.right * Input.GetAxisRaw("Horizontal") + transform.up * Input.GetAxisRaw("Vertical")).normalized;
        _rb.linearVelocity = _moveDirection * speed;
        /*Vector2 pos = _rb.position;
        if (_moveDirection.x != 0)
        {
            pos.y = Mathf.Round(pos.y / 0.0625f) * 0.0625f;
            _rb.position = pos;
        }*/

    }
    private void SnapToGrid()
    {
        float snapX = Mathf.Round(transform.position.x / 0.0625f) * 0.0625f;
        float snapY = Mathf.Round(transform.position.y / 0.0625f) * 0.0625f;
        transform.position = new Vector3(snapX, snapY);
    }
    public void StopPlayer()
    {
        if (canMove)
        {
            SnapToGrid();
            canMove = false;
        }
        else if(!canMove)
        {
            canMove = true;
        }
        _rb.linearVelocity = new Vector2(0,0);
    }
}
