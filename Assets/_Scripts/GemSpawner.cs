using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] Transform _gems;
    [SerializeField] Platform[] _platformsList;

    private void Awake()
    {
        Debug.Log("GemSpawner awake");
        GetPlatforms();
        GenerateGems();
    }

    private void GetPlatforms()
    {
        _platformsList = FindObjectsOfType<Platform>();
    }

    public void GenerateGems()
    {
        
        foreach (Platform platform in _platformsList)
        {
            platform.Init();
            if (!platform.Gem) continue;
            Collider2D _shapeCollider = platform.gameObject.GetComponent<Collider2D>();
            float posX = UnityEngine.Random.Range(_shapeCollider.bounds.min.x, _shapeCollider.bounds.max.x);
            Vector3 newPos = new Vector3(posX, _shapeCollider.bounds.max.y + 2, 1.0f);
            Instantiate(_gems, newPos, transform.rotation);
        }
    }

    private void OnDestroy() {
        Array.Clear(_platformsList, 0, _platformsList.Length);
    }

}
