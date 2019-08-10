using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed= 10f;
    [SerializeField] float jumpForce= 20f;
    [SerializeField] State state = State.Alive;

    public LayerMask groundLayer;

    Rigidbody2D rigidBody;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Animator animator;

    enum State { Alive, Dying};

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isMoving", true);
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            animator.SetBool("isMoving", true);
            MoveLeft();
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
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
       if(!IsGrounded())
       {
            return;
       }
       else
       {
            rigidBody.AddRelativeForce(Vector3.up * jumpForce,ForceMode2D.Impulse);
       }
    }

    // Voir s'il faut bloquer le flipX pendant une action (attaque)
    private void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        spriteRenderer.flipX = true;
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        spriteRenderer.flipX = false;
    }
}
