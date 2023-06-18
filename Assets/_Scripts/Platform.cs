using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] bool _hasGem;


    public bool Gem { get { return _hasGem; } }
    // Start is called before the first frame update
    void Start()
    {
        int num = Random.Range(0, 2);
        _hasGem = num > 0 ? true : false;
    }

   
}
