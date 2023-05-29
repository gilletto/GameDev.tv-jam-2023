using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter exit trigger");

        if (GameManager.Instance.HasKey)
        {

            Debug.Log("Congratulations! You have completed the level!!!");
            GameManager.Instance.NextLevel();
            // play end level sound
            // show ui final level
            // change level
            // restart game manager
        }
        else
        {
            Debug.Log("You don't have key");
        }

      

    }
}
