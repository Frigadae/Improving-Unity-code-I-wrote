# Implementing Platformer behaviour
### It takes a bit of work
```cs
public class PlatformerMoveScript : MonoBehaviour
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

        //Jump
        if (Input.GetKey("up") && playableObject.velocity.y == 0)
        {
            playableObject.velocity = Vector2.up * speed;
        }

        //Horizontal Movement
        if (Input.GetKey("left"))
        {
            playableObject.velocity += Vector2.left * speed * Time.deltaTime;
        }

        if (Input.GetKey("right"))
        {
            playableObject.velocity += Vector2.right * speed * Time.deltaTime;
        }

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
}
```
Implementing platformer behaviour is definitely more time consuming than it first appears. 
Setting and toggling the gravity is easy, it can be done in the Unity UI. However the hard part is implementing the jump function and get it to cooperate with the horizontal movement.
In various rewrites of the script which is not shown, either I have good horizontal movement but a measly weak or unresponsive jump, or a desirable jump but with horizontal movement that sends the player zipping off the platform.


Another implementation I did was using a list of ``if`` statements similar to this but while yielding desirable movement behaviour, it is missing diagonal movement.
I believe the reason is I initialised new vectors and assigning it to the velocity property. This means my old vector directions are overwritten and thus I cannot move diagonally.
My solution was to increment the horizontal velocities into my gameObject velocity property and finally, I was able to get the desirable platformer behaviour.

### A flawed jump implementation
I would like to direct your attention to the code responsible for making the player jump.
```
//Jump
if (Input.GetKey("up") && playableObject.velocity.y == 0)
{
  playableObject.velocity = Vector2.up * speed;
}
```
While the Youtube tutorials implement raycasting or some form of colliders to check if the player is grounded. I was initially hesitant to implement one myself.
I don't want to copy another person's code immediately. Additionally, I was already satisfied with my platformer movement behaviour, I wanted to relish the moment.
Acknowledging the flaws of my implementation did begin right away, while a simple vertical velocity check is basic and workable. 
It does have a flaw that while I did not test did convince me to look into raycasting or colliders.


Suppose the player is on a moving platform. A vertical moving platform could potentiallyu disable the ability to jump. 
I should stress that while this problem is unverified as I did not test it out. It could be possible, as a moving platform would push the player around and thus the velocity is never really set to 0. If it is possible, then the game experience would be ruined.

And so enter [this video about implementing a simple platformer game](https://www.youtube.com/watch?v=1bHVsxw_o7o)

### A much better implementation
```cs
public class NewPlatformerScript : MonoBehaviour
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
```
With some simplifying, such as deleting a function call. I was able to achieve the desired platformer behaviour by implementing a collider check using a cirlce overlap.
The circle overlap checks for a layer named "Ground" which represents the platforms the player can stand on.

Reading the line ``Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, Ground);``, the OverlapCircle takes in three arguments:
* A Vector2 representing the center of the circle.
* A float representing the radius of the circle.
* A layer to check for objects.

The feet is a child gameObject of the player gameObject, this thus marks the center of the circle. When it is not in contact, it would return ``null`` as a result.
Initially I copied the video tutorial of checking if the ``gameObject`` property of ``groundCheck`` is null, but it threw a null exception as the entire variable is stored with a ``null``.
I did not initially realise I made a mistake until I removed checking property and checked the variable itself.

With the mistakes fixed, I now got a perfect implementation of a platformer movement behaviour. With the basics of collisions and scripts for movement now written. 
I am in a position to implement very simple games. However, there are still things to do, such as researching and implementing game overs and game win conditions.
