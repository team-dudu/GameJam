using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed= 10f;
    [SerializeField] float jumpForce= 500f;
    [SerializeField] State state = State.Alive;

    Rigidbody2D rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Jumping };

    //champ calculé "IsGrounded"

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
        if(Input.GetKey(KeyCode.Z))
        {
            Jump();
        }
    }

    private void Jump()
    {
        //Detecter collision avec un bloc de terrain horizontal
       if(state == State.Alive)
       {
            state = State.Jumping;
            rigidBody.AddRelativeForce(Vector3.up * jumpForce);
       }
        
    }

    private void MoveLeft()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    private void MoveRight()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}