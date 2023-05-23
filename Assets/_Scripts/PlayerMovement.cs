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
    [SerializeField] GameObject _humanShape;
    [SerializeField] GameObject _ghostShape;
    [SerializeField] int _life_essence;
    private DimensionType _shapeType;
    private Rigidbody2D _rb;
    private Vector2 _movement;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _life_essence = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(_shapeType == DimensionType.Human)
        {
            _movement = new Vector2(Input.GetAxisRaw("Horizontal") * _speed, _rb.velocity.y);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _movement = new Vector2(_rb.velocity.x, 3);
            }
        }
        else
        {
            _movement = new Vector2(Input.GetAxisRaw("Horizontal") * _speed, Input.GetAxisRaw("Vertical") * _speed);
        }
      
        
    }

    private void FixedUpdate()
    {
        
            _rb.velocity = _movement;
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("enter collision");
        if (collision.gameObject.CompareTag("Trap"))
        {
            ChangeDimension("ghost");
        }else if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter trigger");
        if (collision.gameObject.CompareTag("LifeEssence"))
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
