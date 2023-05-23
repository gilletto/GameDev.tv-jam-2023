using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DimensionType
{
    Human,
    Ghost
}
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float _speed = 100f;
    [SerializeField] float _jumpForce = 10f;
    [SerializeField] GameObject _humanShape;
    [SerializeField] GameObject _ghostShape;
    [SerializeField] int _life_essence;
    private DimensionType _shapeType;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Boolean _facingRight;
    private Boolean _isJumping;
    private Boolean _isFalling;
    private float maxFallHeight;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _life_essence = 0;
        _facingRight = true;
        _isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {


        float movX = Input.GetAxisRaw("Horizontal");
        if (_shapeType == DimensionType.Human)
        {


            _isFalling = _rb.velocity.y < 0;
            
            if(_isJumping && _rb.velocity.y == 0)
            {
                _isJumping = false;

            }
            else if (_isJumping || _isFalling)
            {
                if(_rb.velocity.y < maxFallHeight)
                {
                    maxFallHeight = _rb.velocity.y;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            {
                _isJumping = true;
                maxFallHeight = 0;
                _rb.velocity = Vector2.up * _jumpForce;
            }

            _movement = new Vector2(movX * _speed, _rb.velocity.y);

        }
        else
        {
            _movement = new Vector2(movX * _speed, Input.GetAxisRaw("Vertical") * _speed);
        }
      
        if(!_facingRight && movX > 0)
        {
            _facingRight = !_facingRight;
            Flip();
        }
        else if(_facingRight && movX < 0)
        {
            _facingRight = !_facingRight;
            Flip();
            
        }
    }

    private void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void FixedUpdate()
    {
        
            _rb.velocity = _movement;
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player onEnterCollision");
        if (collision.gameObject.CompareTag("Trap"))
        {
            ChangeDimension("ghost");

        }else if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
        }else if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("maxFallHeight " + maxFallHeight);

            if (maxFallHeight < -20)
            {
                ChangeDimension("ghost");
            }

            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player onEnterTrigger");
        if (collision.gameObject.CompareTag("LifeEssence") && _shapeType == DimensionType.Ghost)
        {
            _life_essence++;
            Destroy(collision.gameObject);
            if (_life_essence % 3 == 0)
            {
                ChangeDimension("human");
            }
        }
    }

    private void ChangeDimension(string type)
    {
        switch (type)
        {
            case "human":
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 1;
                _rb.isKinematic = false;
                _humanShape.SetActive(true);
                _ghostShape.SetActive(false);
                _shapeType = DimensionType.Human;
                _speed = 7f;
                
                break;
            case "ghost":
                _rb.gravityScale = 0;
                _rb.isKinematic = true;
                _humanShape.SetActive(false);
                _ghostShape.SetActive(true);
                _shapeType = DimensionType.Ghost;
                _speed = 12f;

                break;
        }
        
        // disable human sprite
        // enable ghost sprite
        // switch movement from human to ghost
    }
}
