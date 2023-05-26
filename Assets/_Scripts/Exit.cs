using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter exit trigger");

        if (GameManager.Instance.HasKey)
        {

            Debug.Log("Congratulations! You have completed the level!!!");
            GameManager.Instance.Restart();
            // play end level sound
            // show ui final level
            // change level
            // restart game manager
        }

      

    }
}
