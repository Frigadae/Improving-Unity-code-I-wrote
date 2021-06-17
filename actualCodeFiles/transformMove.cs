using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoveScript : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public Transform feet;
    public LayerMask Ground;

    private Rigidbody2D playableObject;
    private SpriteRenderer playableSprite;

    // Start is called before the first frame update
    void Start()
    {
        playableSprite = GetComponent<SpriteRenderer>();
        playableObject = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");

        //Jump
        if (Input.GetKey("up") && IsGrounded())
        {
            playableObject.velocity = new Vector2(xMove * runSpeed, jumpSpeed);
        }

        //Horizontal Movement
        playableObject.velocity = new Vector2(xMove * runSpeed, playableObject.velocity.y);

        //Flip sprite
        if (xMove > 0)
        {
            playableSprite.flipX = false;
        }
        else if (xMove < 0)
        {
            playableSprite.flipX = true;
        }
    }
    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, Ground);

        if (groundCheck == null)
        {
            return false;
        } 
        else
        {
            return true;
        }
    }
}
