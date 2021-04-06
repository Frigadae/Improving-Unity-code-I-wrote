# Basic Directional Code for Unity

### First Iteration
My first movescript script had two implementations of the code.
The first implementation being expressed as such:
```cs
public class myFirstScript : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            Debug.Log("Going Up");
            transform.Translate(0, speed, 0);
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            Debug.Log("Going Down");
            transform.Translate(0, -speed, 0);
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            Debug.Log("Going Left");
            transform.Translate(-speed, 0, 0);
        }
        else if (Input.GetKey("right") || Input.GetKey("d"))
        {
            Debug.Log("Going Right");
            transform.Translate(speed, 0, 0);
        }
        else if (!Input.anyKey)
        {
            Debug.Log("Stopping");
            transform.Translate(0, 0, 0);
        }
    }
```
If I press an arrow key (or the WASD alternative) it translates the gameObject. If no key is pressed, which is expressed as ``else if (!Input.anyKey)``. Then the gameObject stops, if I were to simplify the code, the last block would be axed. 
However I kept it as I want to ensure that the gameObject does not travel into infinity, so I kept the last block just in case.

This is well and good and it does indeed move the gameObject that the script is attached to. However using a list of ``else if``s statements means only one of them will be true.
This means that there is no diagonal movement at all. You can go north, south, east, west but not northwest, southeast as such.

### Second Iteration
My next iteration of the basic directional code involves removing the ``else if``s in favour of ``if`` statements. 
With a list ``if`` statements, one or multiple can be recognised as true instead of only a single one.

```cs
public class myFirstScript : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            transform.Translate(0, speed, 0);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            transform.Translate(0, -speed, 0);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            transform.Translate(-speed, 0, 0);
        }
        if (!Input.anyKey)
        {
            transform.Translate(0, 0, 0);
        }
    }
```
With this, I am able to now move diagonally. My first script on Unity is now done.
This implementation however is not perfect. Upon adding objects with collisions, if I press my gameObject against another gameObject with collisions enabled, it bounces around.
A more detailed description would be oscillating or shaking around when I press against it. 


The gameObject I am controlling briefly sinks into the other object, then bounces back out before pressing back in. This is not an ideal behaviour.
While some minor changes to the code such as replacing ``Update()`` with ``FixedUpdate()`` helped reduce the shaking, it only minimises it if I were to translate at a very high speed. The shaking is only made non-existent if I set the speed to an extremely low value, such as ``0.1f``. 
A behaviour I noted if I set the speed to a very high value, such as 100 or greater. The object would pass through another object completely.

Thus, my next iteration involves using RigidBody2D, which implements some physics into the game objects.
