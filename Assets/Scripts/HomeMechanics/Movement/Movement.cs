using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public HeroDataSO defaultHero;
    public float speed;
    public Rigidbody2D _rb;
    public Animator _animator;
    Vector2 _moveDirection;
    public bool canMove = true;
    public List<Card> BattleDeck;
    public List<Card> InventoryDeck;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InventoryDeck = new();
        for (int i = 0; i < defaultHero.InventoryDeck.Count; i++)
        {
            Card card = new Card(CardManagementSystem.Instance.GetCardByID(defaultHero.InventoryDeck[i].ID));
            BattleDeck.Add(card);
        }
        BattleDeck = new();
        for (int i = 0; i < defaultHero.Deck.Count; i++)
        {
            Card card = new Card(CardManagementSystem.Instance.GetCardByID(defaultHero.Deck[i].ID));
            BattleDeck.Add(card);
        }
    }
    void Update()
    {
        if (canMove)
        {
            _moveDirection = (transform.right * Input.GetAxisRaw("Horizontal") + transform.up * Input.GetAxisRaw("Vertical")).normalized;
            MovePlayer();
        }
        if (_moveDirection == Vector2.zero)
        {
            SnapToGrid();
        }

        if (_moveDirection != Vector2.zero)
        {
            _animator.SetFloat("LH", _moveDirection.x);
            _animator.SetFloat("LV", _moveDirection.y);
        }


    }
    private void MovePlayer()
    {
        _rb.linearVelocity = _moveDirection * speed;
        _animator.SetFloat("Horizontal", _moveDirection.x);
        _animator.SetFloat("Vertical", _moveDirection.y);
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
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 0);
            SnapToGrid();
            canMove = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (!canMove)
        {
            canMove = true;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
