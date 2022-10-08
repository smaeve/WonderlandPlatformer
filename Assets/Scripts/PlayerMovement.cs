using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; //declaring a (null/empty) variable which we can reuse and change the values in it easily, rather than calling GetComponent every time,
                            //which would slow down our game
                            //Private means only this script can access it (reduces bugs, so other pieces of code don't access it)
                            //float decimalNumber = 4.54f; 
                            //for numbers with floating points. Usually have to add an f after the number when working with floats, e.g. in vectors, we work with
                            //floats and add f after the numbers
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim; //create private Animator called anim
    private SizeManager sizeScript;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f; //just initialise this variable with a default 0f value just so it has something when the first update is called
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce =14f; // [SerializeField] allows us to edit these values in the script editor
    //can ALSO use PUBLIC float, but it exposes these values to other scripts which could intro'd bugs, so [SerializeField]private is usually better
    public Joystick joystick; //create a reference to our joystick control

    private enum MovementState {idle, running, jumping, falling} //idle has the integer value of 0, running is 1, jumping is 2 etc. These
    //values are important for when we're working with the arrows in our animator, to set our state = 0

    [SerializeField] private AudioSource jumpSoundEffect;
    
    // Start is called before the first frame update
    private void Start() //stuff we want to happen when the game starts (in first frame)
    {
        rb = GetComponent<Rigidbody2D>(); //assign a value to our initially null variable so that it's called once at the start
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() //what we want to happen in each frame of our game (happens many times per second depending on the frame rate of your computer)
    {
        //MOVING LEFT OR RIGHT -1 TO +1 (with decimals inbetween, therefore use float)
        //COMPUTER dirX = Input.GetAxisRaw("Horizontal"); //Horizontal is the name of the x-axis as seen in the left editor
        //when we press left, it will go to -1 and when we press right, it will go towards +1
        //if we have a joystick, it will go inbetween as decimals i.e. -0.5, +0.6 etc.
        //GetAxisRaw is to stop the character from sliding when it moves left and right
        //dirX = joystick.Horizontal; //for phone, pressing joystick all the way to right will give us float value of +1 and left to -1

        /* if(joystick.Horizontal >= .2f)
        {
            dirX = rb.velocity;
        }
        else if (joystick.Horizontal <= -.2f)
        {
            dirX = -rb.velocity;
        } else
        {
            dirX = 0f;
        } */ //ERROR with rb.velocity (runSpeed on Brackey's), "Cannot implicitly convert type 'UnityEngine.Vector2' to float

        rb.velocity = new Vector2(dirX*moveSpeed, rb.velocity.y); //our horizontal force is 7f
        //We multiply by dirX, because if dirX is at a negative value, then it will multiply by this 7f force and we'll move left
        //above code is more concise than writing if (dirX>0) then move right else if (dirX<0), then move left
        //the rb.velocity.y is to keep the y-velocity that was in the previous frame so we don't start jumping in the new frame

        //float verticalMove = joystick.Vertical;
        //if (verticalMove >= .5f && IsGrounded()) //everytime we move our joystick greater than halfway up, we are going to jump
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, jumpForce); //to change velocity of the object. Vector 2() is simply a data holder for 2 values(x,y)
                                                                 //in this case, how much speed do we want to put into the x direction, y-direction etc. 0 for x as we want to jump, not move left or right,
                                                                 //for y, we set it to 14 which is like the velocity force we want player to jump, z is also 0 as we're working in 2d plane
        //} 

        //JUMP //COMPUTER 
        /* if (Input.GetButtonDown("Jump")) 
            //GetButtonDown only execute code in {} when space key is held down, otherwise if it was just GetKey("space"), then when
            //you press the space buttin and hold down, the player will just keep jumping forever.
            //use GetButtonDown instead of the GetKeyDown which is like hardcoding. GetButtonDown allows you to change the values
            //in the Unity Build settings-> Input Manager
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce); //to change velocity of the object. Vector 2() is simply a data holder for 2 values(x,y)
                //in this case, how much speed do we want to put into the x direction, y-direction etc. 0 for x as we want to jump, not move left or right,
                //for y, we set it to 14 which is like the velocity force we want player to jump, z is also 0 as we're working in 2d plane
        } */
        UpdateAnimationState();
        

    }
    private void UpdateAnimationState() //a method created by us to make things neater
                                         //void means that this method doesn't turn a result when we call it, it just executes some code and that's all
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running; //set "running" (which is from our animator in unity) to true to indicate we are running
            sprite.flipX = false; //player faces right when running right
        }
        else if (dirX < 0f) //LEFT
        {
            state= MovementState.running; //also set running to true, so we can run in the left direction
            sprite.flipX = true; //so player flips on x-axis and faces left when we're running left
        }
        else
        {
            state= MovementState.idle; //if running is false, then we return to idle state 
        }

        if (rb.velocity.y > 0.1f) //if y velocity is greater than 0.1, then we must be jumping
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f) //if there's a downward force applied, then we must be falling
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state); //int changes the enum state into it's corresponding integer representation (i.e. 0, 1, 2, or 3)
    }

    // MOVEMENT --------------------

    // Exposing (public) the Move method to the Input Controller
    public void Move(InputAction.CallbackContext context)
    {
        dirX = context.ReadValue<Vector2>().x;
    }


    // Exposing (public) the Jump method to the Input Controller
    // and also checking if the player object is standing on an object before allowing a jump action
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSoundEffect.Play();
        }

    } 

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround); 
            /*Boxcast creates a box around our player box collider- we only want to jump when we hit the ground,
              not when we hit an item floating in the air. The boxcast returns a bool, so if it is
            touching ground it returns true, and if not it returns false- this code is executed whenever the bool IsGrounded() is called*/
    }

    // USING TOUCH BUTTONS FOR MOVEMENT ---------------------------------------------------------  TOUCH CONTROLS

    // These are specifically to facilitate UI based control of the player, namely three big buttons
    // that have been overlayed on the screen - left, right and jump. It looks like we are rewriting
    // the very nice controls we made above, and yes we are, but it's one simple way to get the job done

    // Touch button for left horizontal movement
     public void MoveLeft()
    {
        dirX = -1;
    }

    // Touch button for right horizontal movement
    public void MoveRight()
    {
        dirX = 1;
    }

    // once the button is released we reset the horizontal movement value back to zero
    public void SetStationary()
    {
        dirX = 0;
    }

    public void JumpButton()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSoundEffect.Play();
        }

    } 
}
