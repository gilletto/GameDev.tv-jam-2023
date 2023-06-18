using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private const byte MAX_LEVEL_DEATH = 1;

    [SerializeField] int _deaths;
    [SerializeField] bool _hasKey;
    [SerializeField] Vector3 _startPosition;
    [SerializeField] Transform _player;
    [SerializeField] GemSpawner _gemSpawner;
    [SerializeField] int _gems;
    [SerializeField] UIManager _uimanager;
    [SerializeField] LevelManager _levelManager;

    public bool HasKey { get { return _hasKey; } }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game manager start");
        _deaths = 0;
        _gems = 0;
        _hasKey = false;
        _startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _gemSpawner.GenerateGems();
        _uimanager.UpdateDeaths(_deaths);
        _uimanager.UpdateGems(_gems);
        _levelManager.Init();

    }

    public void TakeKey()
    {
        _hasKey = true;
        _uimanager.UpdateKey(true);
    }

    public void Restart()
    {
        _deaths = 0;
        _gems = 0;
        _hasKey = false;
        _player.position = _startPosition;
        _gemSpawner.GenerateGems();
    }

    public void IncreaseDeath()
    {
        _deaths++;
        _uimanager.UpdateDeaths(_deaths);
        if (_deaths > MAX_LEVEL_DEATH)
        {

        }
    }
    public void IncreaseGem()
    {
        _gems++;
        _uimanager.UpdateGems(_gems);
    }

    public void NextLevel()
    {
        /* TODO: 
         * Add level complete percentage or star
         * Add transition and loading screen
         * setup of new level variables
         */
        CheckLevelCompletion();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex  + 1);
        Restart();
        _uimanager.UpdateLevel(scene.buildIndex + 1); 
    }

    private void CheckLevelCompletion()
    {
        int stars = 0;
        if (_deaths <= MAX_LEVEL_DEATH) { stars++; }
        if(_gems <= 3) { stars++; }
    }
}
