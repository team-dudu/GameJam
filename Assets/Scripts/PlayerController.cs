using System;
using UnityEngine;

namespace GameJam
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public float jumpForce = 20f;
        public State state = State.Alive;
        public IAttack attack;
        
        Rigidbody2D rigidBody;
        AudioSource audioSource;

        private float moveInput;
        private bool facingRight = true;

        public enum State
        {
            Alive,
            Dying
        };

        bool IsGrounded()
        {
            Vector2 position = transform.position;
            Vector2 direction = Vector2.down;
            float distance = 3.5f;

            RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);

            Debug.DrawRay(position, direction, Color.green);

            if (hit.collider != null)
            {
                return true;
            }

            return false;
        }

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            attack = GetComponent<IAttack>();
        }

        // Update is called once per frame
        void Update()
        {
            moveInput = Input.GetAxis("Horizontal");
            rigidBody.velocity = new Vector2(moveInput * moveSpeed, rigidBody.velocity.y);

            if (!facingRight && moveInput > 0 || facingRight && moveInput < 0)
            {
                Flip();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                attack?.Shoot();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    print("ok");
                    break;
                case "End":
                    StartLevelTransition();
                    break;
            }
        }

        private void StartLevelTransition()
        {
            print("Next Level");
            // throw new NotImplementedException();
        }

        private void Jump()
        {
            if (!IsGrounded())
            {
                return;
            }
            else
            {
                rigidBody.AddRelativeForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }
}