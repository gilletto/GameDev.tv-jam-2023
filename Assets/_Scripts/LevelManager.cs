using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] Level _currentLevel;
    Dictionary<int, Level> levelDictionary;

    


    void Awake()
    {
        levelDictionary = new Dictionary<int, Level>();
    }

    public void Init()
    {
        // create 5 levels 
        for(int i = 0; i < 5; i++)
        {
            levelDictionary.Add(i, new Level(i + 1));
        }

        _currentLevel = levelDictionary[0];
        
    }

    public Level GetCurrentLevel()
    {
        return _currentLevel;
    }
}
