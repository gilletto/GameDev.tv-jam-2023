using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int _deaths;
    [SerializeField] bool _hasKey;
    [SerializeField] Vector3 _startPosition;
    [SerializeField] Transform _player;
    [SerializeField] GemSpawner _gemSpawner;
    [SerializeField] int _gems;

    public bool HasKey { get { return _hasKey; } }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _deaths = 0;
        _gems = 0;
        _hasKey = false;
        _startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _gemSpawner.GenerateGems();

    }

    public void TakeKey()
    {
        _hasKey = true;
    }

    public void Restart()
    {
        _deaths = 0;
        _gems = 0;
        _hasKey = false;
        _player.position = _startPosition;
    }

    public void IncreaseDeath()
    {
        _deaths++;
    }
    public void IncreaseGem()
    {
        _gems++;
    }

}
