using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform _player;
    [SerializeField] Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = new Vector3(0 , 0, transform.position.z);
    }

    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(_player.position.x, _player.position.y, 0f);
        transform.position = newPosition + _offset;
        
    }
}
