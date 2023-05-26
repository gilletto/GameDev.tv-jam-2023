using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;
using System;


namespace UnityEngine
{
    #region USING

    using HIDE = HideInInspector;
    using SHOW = SerializeField;

    #endregion


    // component name shown in inspector
    [AddComponentMenu("Movement2DPlatformer")]
    public class Movement2DPlatformer : MonoBehaviour
    {
        [SHOW] private Rigidbody2D m_rb2D;
        [SHOW] private Collider2D m_collider;


        void Start()
        {
            TryGetComponent(out m_collider);
            TryGetComponent(out m_rb2D);
        }


        [SHOW] private bool m_CanMoveOnRight;
        [SHOW] private bool m_CanMoveOnLeft;
        [SHOW] private bool facingRight;
        [SHOW] private bool isGrounded;
        [SHOW] private bool isJumping;

        [SHOW] private float m_speed;
        [SHOW] private float m_motion;
        [SHOW] private float m_MoveHorz;
        [SHOW] private float m_JumpForce;
        [SHOW] private float m_RaycastLength;


        void Update()
        {
            m_MoveHorz = Input.GetAxis("Horizontal");

            //velocity.x = Mathf.SmoothDamp(velocity.x, moveH * speed, ref velocity.x, smoothTime);
            //rb.velocity = velocity;


            m_motion = m_MoveHorz * m_speed;
            m_rb2D.velocity = new Vector2(m_motion, m_rb2D.velocity.y);



            if (m_MoveHorz > 0 && !facingRight)
            {
                m_motion = m_CanMoveOnRight ? 0 : m_MoveHorz * m_speed;
                Flip();
            }
            else if (m_MoveHorz < 0 && facingRight)
            {
                m_motion = m_CanMoveOnLeft ? 0 : m_MoveHorz * m_speed;
                Flip();
            }


            //isGrounded = Physics2D.Raycast(position, Vector2.down, groundRaycastDistance, groundLayerMask);


            if (isGrounded)
                Debug.DrawRay(transform.position, Vector2.down * m_RaycastLength, Color.green);
            else Debug.DrawRay(transform.position, Vector2.down * m_RaycastLength, Color.red);



            if (Input.GetButtonDown("Jump") && (isGrounded || !isJumping))
            {
                isJumping = true; isGrounded = false;
                m_rb2D.AddForce(new Vector2(0, m_JumpForce), ForceMode2D.Impulse);
            }

            if (isJumping && isGrounded) isJumping = false;
        }


        void Flip()
        {
            facingRight = !facingRight;
            transform.localScale *= new Vector2(-1, 1);
        }


        void OnCollisionEnter2D(Collision2D collision)
        { if (collision.transform.CompareTag("Ground")) isGrounded = true; }

        void OnCollisionExit2D(Collision2D collision)
        { if (collision.transform.CompareTag("Ground")) isGrounded = false; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            m_CanMoveOnLeft = collision.name == "Wall L";
            m_CanMoveOnRight = collision.name == "Wall R";
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            m_CanMoveOnLeft = !(collision.name == "Wall L");
            m_CanMoveOnRight = !(collision.name == "Wall R");
        }
    }
}