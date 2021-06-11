using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float speed;
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
        float yMove = Input.GetAxis("Vertical");

        playableObject.velocity = new Vector2(xMove * speed, yMove * speed) * Time.deltaTime;

        if (xMove > 0)
        {
            playableSprite.flipX = false;
        } 
        else if (xMove < 0)
        {
            playableSprite.flipX = true;
        }
    }
}
