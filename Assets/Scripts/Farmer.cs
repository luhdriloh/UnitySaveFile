using System.Collections.Generic;
using UnityEngine;

public delegate void FlowerChange(Dictionary<FlowerColor, int> flowerValues);

public class Farmer : MonoBehaviour
{
    public static event FlowerChange _flowerChangeEvent;
    public float _speed;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private GameData _gameData;
    private static bool _toUpdate;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _gameData = GetComponent<GameData>();
    }


    private void Update()
    {
        if (_toUpdate)
        {
            _toUpdate = false;
            OnFlowerChangeEvent();
        }

        // get input from player
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        Vector2 movement = (new Vector2(xMovement, yMovement)).normalized * _speed;
        _rigidbody.velocity = movement;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }

        // set animation
        if (movement.magnitude > Mathf.Epsilon)
        {
            _animator.SetBool("Moving", true);
            _spriteRenderer.flipX = xMovement <= 0;
        }
        else
        {
            _animator.SetBool("Moving", false);
        }

        // for trees and shit
        float yPos = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y, -3f + (yPos * .01f));
    }

    private void PickUpItem()
    {
        Flower flower = CheckIfFlowerOverlap();

        if (flower == null)
        {
            return;
        }

        if (_gameData._numberOfFlowers.ContainsKey(flower._color))
        {
            _gameData._numberOfFlowers[flower._color] = _gameData._numberOfFlowers[flower._color] + flower._numberOfFlowers;
        }
        else
        {
            _gameData._numberOfFlowers.Add(flower._color, flower._numberOfFlowers);
        }

        flower.PickFlower();
        OnFlowerChangeEvent();
    }

    private Flower CheckIfFlowerOverlap()
    {
        // set up contact filter and results array
        ContactFilter2D colliderFilter = new ContactFilter2D();
        colliderFilter.SetLayerMask(LayerMask.GetMask("Flower"));
        colliderFilter.useTriggers = true;

        Collider2D[] results = new Collider2D[1];

        // find collider overlap
        int numberOfContacts = _collider.OverlapCollider(colliderFilter, results);
        return numberOfContacts == 0 ? null : results[0].GetComponent<Flower>();
    }

    private void OnFlowerChangeEvent()
    {
        if (_flowerChangeEvent != null)
        {
            _flowerChangeEvent(_gameData._numberOfFlowers);
        }
    }

    public static void AddFlowerEventObserver(FlowerChange observer)
    {
        _flowerChangeEvent += observer;
        _toUpdate = true;
    }
}
