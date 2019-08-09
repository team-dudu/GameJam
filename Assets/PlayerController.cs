using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed= 10f;
    [SerializeField] float jumpForce= 500f;
    [SerializeField] State state = State.Alive;

    public LayerMask groundLayer;

    Rigidbody2D rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying};

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            MoveLeft();
        }
        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
       if(!IsGrounded())
       {
            return;
       }
       else
       {
            rigidBody.AddRelativeForce(Vector3.up * jumpForce);
       }
        
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}