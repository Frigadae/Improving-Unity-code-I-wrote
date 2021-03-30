# Implementing movement using RigidBody2D
### Discussion
```cs
public class myFirstScript : MonoBehaviour 
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
    float xCoord = Input.GetAxis("Horizontal");
    float yCoord = Input.GetAxis("Vertical");

    playableObject.velocity = new Vector2(xCoord * speed, yCoord * speed) * Time.deltaTime;
  }
}
```
In this implementation, I researched into Rigid Bodies and Collisions. I can definitely say this is very helpful as using RigidBody2d eliminated the shaking when I press against another solid object.
It turns out the implementation of the code using RigidBody velocity also reduced the amount of checking I need to do for the keys too. 
Unity has an input manager which keeps track of the default control keys. I only need to check the vertical and horizontal inputs which are given as floats.
If the float is on the vertical axis and is a negative number, then it is very definitely a down key.
  
### Sprite Flipping
```cs
if (Input.GetKey("left") || Input.GetKey("a"))
{
  playableSprite.flipX = false;
}
  if (Input.GetKey("right") || Input.GetKey("d"))
{
  playableSprite.flipX = true;
}
if (!Input.anyKey)
{
  playableObject.velocity = Vector2.zero;
}
```
A comfort feature I implemented includes sprite flipping if I were to go left or right. 
I initialised a variable called ``private SpriteRenderer playableSprite;`` and fetched the sprite properties of the gameObject on ``Start()``.
By flipping the script through a simple conditional check, I can point my sprite towards the direction I wish to go.
