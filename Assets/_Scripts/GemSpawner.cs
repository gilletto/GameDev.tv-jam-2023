using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] Transform _gems;
    [SerializeField] bool _hasGems = false;
    private GameObject[] _platformsList;

    private void Awake()
    {
        _platformsList = GameObject.FindGameObjectsWithTag("Ground");
        _hasGems = false;
    }

    public void GenerateGems()
    {
        foreach (GameObject platform in _platformsList)
        {
            Collider2D _shapeCollider = platform.GetComponent<Collider2D>();
            float posX = Random.Range(_shapeCollider.bounds.min.x, _shapeCollider.bounds.max.x);
            Vector2 newPos = new Vector2(posX, _shapeCollider.bounds.max.y + 2);
            Instantiate(_gems, newPos, transform.rotation);
        }
    }

}
