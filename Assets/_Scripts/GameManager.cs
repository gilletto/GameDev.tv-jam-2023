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
    [SerializeField] int _gems;
    [SerializeField] UIManager _uimanager;

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

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        _startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _deaths = 0;
        _gems = 0;
        _hasKey = false;
    }


     // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

     // called when the game is terminated
    void OnDestroy()
    {
        Debug.Log("OnDestroy");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game manager start");
       
        
        _uimanager.UpdateDeaths(_deaths);
        _uimanager.UpdateGems(_gems);
        _uimanager.UpdateLevel(1);
    }

    public void TakeKey()
    {
        _hasKey = true;
        _uimanager.UpdateKey(true);
    }

    public void Restart()
    {
        Debug.Log("Restart level");
        _deaths = 0;
        _gems = 0;
        _hasKey = false;
        _player.position = _startPosition;

    }

    public void IncreaseDeath()
    {
        _deaths++;
        _uimanager.UpdateDeaths(_deaths);
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
        StartCoroutine(LoadSceneAsync());
        
        
        
    }

    IEnumerator LoadSceneAsync()
    {
        yield return null;
        
        Scene scene = SceneManager.GetActiveScene();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.buildIndex + 1);
        asyncOperation.allowSceneActivation = false;
        while(!asyncOperation.isDone){

            if(asyncOperation.progress >= 0.9f){
                Restart();
                _uimanager.UpdateLevel(2);
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }   
        
    }

    private void CheckLevelCompletion()
    {
        int stars = 0;
        if (_deaths <= MAX_LEVEL_DEATH) { stars++; }
        if(_gems <= 3) { stars++; }
    }
}
