using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameJam
{
	public abstract class Character : MonoBehaviour, IDamageable
	{
		public int MaxHealth = 100;
		public int health = 5;
		public bool IsMoving = false;
		public bool IsAttacking = false;

        public bool ShouldDisappear = true;

        protected Animator _animator;
        protected bool _isDead = false;

        protected const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        protected bool m_Grounded; // Whether or not the player is grounded.
        protected const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        protected Rigidbody2D m_Rigidbody2D;
        protected bool m_FacingRight = true; // For determining which way the player is currently facing.
        protected Vector3 m_Velocity = Vector3.zero;

        [SerializeField] protected float m_JumpForce = 400f; // Amount of force added when the player jumps.

        [SerializeField] protected float m_DashForce = 25f;

        [Range(0, 1)] [SerializeField]
        protected float m_CrouchSpeed = .36f; // Amount of maxSpeed applied to crouching movement. 1 = 100%

        [Range(0, .3f)] [SerializeField]
        protected float m_MovementSmoothing = .05f; // How much to smooth out the movement


        [SerializeField]
        protected Transform m_GroundCheck; // A position marking where to check if the player is grounded.

        [SerializeField] protected Transform m_CeilingCheck; // A position marking where to check for ceilings

        [SerializeField]
        protected Collider2D m_CrouchDisableCollider; // A collider that will be disabled when crouching

        [SerializeField] protected LayerMask m_WhatIsGround; // A mask determining what is ground to the character
        [SerializeField] protected bool m_AirControl = false; // Whether or not a player can steer while jumping;

        [Header("Events")] [Space] public UnityEvent OnLandEvent;

        [Serializable]
        public class BoolEvent : UnityEvent<bool>
        {
        }

        public BoolEvent OnCrouchEvent;
        protected bool m_wasCrouching = false;

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();

            if (OnCrouchEvent == null)
                OnCrouchEvent = new BoolEvent();
        }

        public void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
        }

        public void TakeDamage(int damage)
        {
            if (health <= 0)
            {
                return;
            }

            health -= damage;
            if (health <= 0)
            {
                Die();
            }
            else
            {
                _animator.SetAnimation(AnimationParameter.Damaged);
            }
        }

		public virtual void Die()
        {
            _animator.SetAnimation(AnimationParameter.Death);
            _isDead = true;
        }

        public void OnDeathEnds()
        {
            if (ShouldDisappear)
            {
                gameObject.SetActive(false);
                Destroy(this);
            }
            else
            {
                _animator.SetAnimation(AnimationParameter.DeathIdle);
            }
        }

        private void FixedUpdate()
        {
            bool wasGrounded = m_Grounded;
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
        }


        protected void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch)
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            if (m_Grounded && Math.Abs(move) > 0.01)
            {
                _animator.SetAnimation(AnimationParameter.IsMoving, true);
            }
            else
            {
                _animator.SetAnimation(AnimationParameter.IsMoving, false);
            }

            if (m_Grounded)
            {
                _animator.SetAnimation(AnimationParameter.IsJumping, false);
            }
            else
            {
               if(_animator.TryGetAnimation(AnimationParameter.IsJumping))
               {
                    _animator.SetAnimation(AnimationParameter.IsJumping, true);
               }
            }

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // If crouching
                if (crouch)
                {
                    if (!m_wasCrouching)
                    {
                        m_wasCrouching = true;
                        OnCrouchEvent.Invoke(true);
                    }

                    // Reduce the speed by the crouchSpeed multiplier
                    move *= m_CrouchSpeed;

                    // Disable one of the colliders when crouching
                    if (m_CrouchDisableCollider != null)
                        m_CrouchDisableCollider.enabled = false;
                }
                else
                {
                    // Enable the collider when not crouching
                    if (m_CrouchDisableCollider != null)
                        m_CrouchDisableCollider.enabled = true;

                    if (m_wasCrouching)
                    {
                        m_wasCrouching = false;
                        OnCrouchEvent.Invoke(false);
                    }
                }

                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
                // And then smoothing it out and applying it to the character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity,
                    m_MovementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
            }
        }

        protected void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            transform.Rotate(0, 180, 0);
        }
    }
}