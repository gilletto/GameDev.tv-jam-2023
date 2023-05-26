using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;


public class TestingMovement : MonoBehaviour
{
    bool rightDirection = true;
    [SerializeField] float speed = 1;

    void Update()
    {
        if (rightDirection)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                transform.position + transform.right,
                Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                transform.position - transform.right,
                Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            float curr = rightDirection ? 1 : -1;
            float next = rightDirection ? -1 : 1;


            rightDirection = !rightDirection;
        }
    }
}
