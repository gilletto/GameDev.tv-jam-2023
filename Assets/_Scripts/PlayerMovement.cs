using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// in questo caso, avendo solo due stati, potresti anche solo usare una booleana
// ad esempio "isGhost", per alleggerire i vari controlli e soprattutto per semplificare
// la scrittura del codice, ma non fa tutta sta differenza, trattandosi di uno stato
// è comunque più "elegante" usare gli enum anche se solo per due opzioni
public enum DimensionType
{
    Human,
    Ghost
}
public class PlayerMovement : MonoBehaviour
{
    private const byte GHOST_SPEED = 12;
    private const byte HUMAN_SPEED = 07;
    [SerializeField] float _speed = 100f;
    [SerializeField] float _jumpForce = 10f;
    [SerializeField] float _maxFallHeight;
    [SerializeField] GameObject _humanShape;
    [SerializeField] GameObject _ghostShape;
    [SerializeField] byte _life_essence; // se la quantità è piccola, meglio il byte dell'int
    [SerializeField] byte _life_essence_to_respawn; // contatore di gemme per respawnare
    private const byte LIFE_ESSENCE_TO_RESPAWN = 3; // quantità di gemme necessarie

    private DimensionType _shapeType;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    // usa bool al posto di Boolean, altrimenti utilizzi la libreria System solo per quello
    // in questo modo invece puoi proprio toglierla essendo inutilizzata
    private bool _facingRight;
    private bool _isJumping;
    private float _currentFallHeight;

    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _life_essence = 0;
        _facingRight = true;
        _isJumping = false;

        // inizializza valore di _life_essence_to_respawn
        InitializeLifeEssenceCounter();
    }

    
    void Update()
    {


        float movX = Input.GetAxisRaw("Horizontal") * _speed;


        if (_shapeType == DimensionType.Human)
        {

            // _isFalling = _rb.velocity.y < 0;


            if (_isJumping && _rb.velocity.y == 0)
            {
                _isJumping = false;
            }
            else if (_isJumping || (_rb.velocity.y < 0))
            {
                if(_rb.velocity.y < _currentFallHeight)
                {
                    _currentFallHeight = _rb.velocity.y;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            {
                _isJumping = true;
                _currentFallHeight = 0;
                _rb.velocity = Vector2.up * _jumpForce;
            }

            _movement = new Vector2(movX, _rb.velocity.y);

        }
        else
        {
            _movement = new Vector2(movX, Input.GetAxisRaw("Vertical") * _speed);
        }
      
        if ((!_facingRight && movX > 0) ||
            (_facingRight && movX < 0)) { Flip(); }
    }


    private void InitializeLifeEssenceCounter()
    {
        // inizializza allo start e ogni volta che arriva a zero
        _life_essence_to_respawn = LIFE_ESSENCE_TO_RESPAWN;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale *= new Vector2(-1, 1);
    }

    private void FixedUpdate()
	{
		_rb.position += _movement * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player onEnterCollision");

        /* In una serie di if è meglio mettere le condizioni in ordine di probabilità
         * nella maggior parte delle volte la collisione avverrà con il ground, è inutile fargli
         * controllare ogni volta due condizioni false per poi arrivare all'ultima giusta, in
         * questo modo fa un solo controllo ed esce subito dalla catena di if saltando i successivi.
         * 
         * Stessa cosa con Key, può avvenire solo una volta quella collisione, è la meno probabile,
         * meglio lasciarla per ultima in modo che i due controlli non necessari avvengano solo una volta.
        */

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Current Fall height " + _currentFallHeight);

            if (_currentFallHeight < -_maxFallHeight)
            {
                ChangeDimension(DimensionType.Ghost);
                _currentFallHeight = 0;
            } 
        }
        
        else if (collision.gameObject.CompareTag("Trap"))
        {
            ChangeDimension(DimensionType.Ghost);
        }

        else if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.TakeKey();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player onEnterTrigger");
        if (collision.gameObject.CompareTag("LifeEssence") && _shapeType == DimensionType.Ghost)
        {
            _life_essence++;
            --_life_essence_to_respawn;

            /* è più leggero come controllo rispetto a fare % 3
             * in più non andrebbero mai scritti numeri direttamente nel codice
             * meglio usare una costante o un readonly 
             */
            if (_life_essence_to_respawn == 0)
            {
                // inizializza valore di _life_essence_to_respawn
                InitializeLifeEssenceCounter();
                ChangeDimension(DimensionType.Human);
            }

            Destroy(collision.gameObject);
        }
    }


    private void ChangeDimension(DimensionType type)
    {
        // lo switch usalo solo per controllare gli enum, meglio non usarlo per altre cose
        // inoltre controllare un enum è più leggero che controllare una stringa
        switch (type)
        {
            case DimensionType.Human:
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 1;
                _rb.isKinematic = false;
                _humanShape.SetActive(true);
                _ghostShape.SetActive(false);
                _speed = HUMAN_SPEED;
                
                break;
            case DimensionType.Ghost:
                _rb.gravityScale = 0;
                _rb.isKinematic = true;
                _humanShape.SetActive(false);
                _ghostShape.SetActive(true);
                _speed = GHOST_SPEED;

                break;
        }
        
        // invece di scrivere due volte il codice che cambia l'enum, nei vari case
        // meglio scriverlo una volta sola per semplificare. Ho fatto la stessa cosa
        // anche in altre parti del codice. L'enum in questo caso va modificato DOPO
        // aver controllato lo switch, lo specifico solo perchè non ci ho pensato subito
        // e quindi l'avevo messo prima, invertendo i risultati
        _shapeType = type;


        // disable human sprite
        // enable ghost sprite
        // switch movement from human to ghost
    }
}
