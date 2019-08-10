using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private float _moveInput;

    private bool _facingRight = true;

    private bool _isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int _extraJumps;
    public int extraJumpsValue;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _extraJumps = extraJumpsValue;
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        _moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);

        if (!_facingRight && _moveInput > 0 || _facingRight && _moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (_isGrounded)
        {
            _extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Z) && _extraJumps > 0)
        {
            _rb.velocity = Vector2.up * jumpForce;
            _extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && _extraJumps == 0 && _isGrounded)
        {
            _rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }
}