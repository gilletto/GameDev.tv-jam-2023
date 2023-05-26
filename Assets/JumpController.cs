using System.Collections.Generic;
using System.Collections;
using CustomInputSystem;
using UnityEngine;


namespace UnityEngine
{
    #region USING          

    using ShowInInspector = SerializeField;
    using SpriteRender = SpriteRenderer;
    using Text = TMPro.TextMeshProUGUI;
    using HIDE = HideInInspector;
    using SHOW = SerializeField;
    using O = HideInInspector;
    using I = SerializeField;

    #endregion

    public class JumpController : MonoBehaviour
    {
        // -----------------------------------------+
        //                                          |
        [O] Rigidbody2D rb;                 //      |
        [O] bool isJumping;                 //      |
        [O] bool onGround;                  //      |
        //                                          |
        [O] const float MIN = 0.8f;         //      |
        [O] const float MAX = 1.5f;         //      |
        [O] const float MAX_HEIGHT = +3;    //      |
        [O] const float MIN_HEIGHT = -3;    //      |
        //                                          |
        // -----------------------------------------+
        //                                          |
        [I] Transform m_JumpLimit;  //              |
        [I] Transform m_GroundPos;  //              |
        //                                          |
        [I][Range(MIN, MAX)] float jumpBoost;   //  |
        [I][Range(05f, 20f)] float jumpForce;   //  |
        //                                          |
        // -----------------------------------------+


        public float Gravity
        {
            get { return 7; } // <- default value
            set => rb.gravityScale = value;
        }



        void Start()
        {
            if (!TryGetComponent(out rb))
            {
                Debug.LogWarning("Rigidbody non presente.", gameObject);
                Debug.LogWarning("Rigidbody aggiunto ora.", gameObject);

                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.freezeRotation = true;

                Gravity = Gravity;
            }

            if (!m_JumpLimit) GameObject.Find("Player's Jump Limit");
            if (!m_GroundPos) GameObject.Find("Player's OnGround Position");
        }






        private void Update()
        {
            // limita posizione sull'asse Y per evitare il rimbalzo
            var pos = transform.position;
            transform.position = new(pos.x, Mathf.Clamp(pos.y, MIN_HEIGHT, MAX_HEIGHT));



            if (onGround)
            {
                if (GetInput.Press_Jump)
                {
                    //AudioManager.Instance.PlaySoundJump();
                    //AnimController.Instance.ChangeState(AnimController.AnimState.Jump);
                }
                else if (!isJumping)
                {
                    // play step sound
                    //AnimController.Instance.ChangeState(AnimController.AnimState.Walk);
                }
            }



            onGround = pos.y <= m_GroundPos.position.y;


            if (onGround && GetInput.Hold_Jump)
            {
                jumpBoost = MAX;
                isJumping = true;
            }


            // se rilasci il tasto o raggiungi il limite smette di andare in alto
            if (GetInput.Release_Jump || pos.y >= m_JumpLimit.position.y)
            {
                isJumping = false;

                // riattiva a terra se premi di nuovo, per evitare il rimbalzo
                // canJump = false;
            }
        }



        void FixedUpdate()
        {
            if (isJumping)
            {
                rb.velocity = jumpBoost * jumpForce * Vector2.up;
                jumpBoost = Mathf.Clamp(jumpBoost - Time.fixedDeltaTime, MIN, MAX);
            }
        }



        public static float SmoothJump(float current, float target, ref float curVel, float smoothTime, float maxSpeed = Mathf.Infinity)
        {
            float deltaTime = Time.deltaTime;
            float vector = target;


            smoothTime = Mathf.Max(0.0001f, smoothTime);


            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            float num4 = current - target;
            float num5 = maxSpeed * smoothTime;
            float num6 = num5 * num5;
            float num7 = num4 * num4;


            if (num7 > num6) num4 = num4 / ((float)Mathf.Sqrt(num7)) * num5;
            target = current - num4;


            float num8 = (curVel + num1 * num4) * deltaTime;
            float num9 = vector - current;
            float num0 = target + (num4 + num8) * num3;


            if (num9 * (num0 - vector) > 0f)
            {
                num0 = vector;
                curVel = (num0 - vector) / deltaTime;
            }
            else curVel = (curVel - num1 * num8) * num3;

            return num0;
        }
    }
}