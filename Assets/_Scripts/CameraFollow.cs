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
        transform.position = _player.position + _offset;
    }
}
