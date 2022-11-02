using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public Rigidbody2D body;
    public Vector3 velocity = Vector3.zero;

    public bool isJumping;
    public bool isGrounded;

    public Animator animator;

    public Transform LFootCheck;
    public Transform RFootCheck;

    public SpriteRenderer spriteRenderer;

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(LFootCheck.position, RFootCheck.position);

        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
        
        MovePlayer(horizontalMovement);

        Flip(body.velocity.x);

        float characterVelocity = Math.Abs(body.velocity.x);
        animator.SetFloat("Speed", characterVelocity);

    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, body.velocity.y);
        body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping == true)
        {
            body.AddForce(new Vector2(0f, jumpForce));
            isJumping=false;
        }
    }

    void Flip( float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
