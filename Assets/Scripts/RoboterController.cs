using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class RoboterController : MonoBehaviour
{
    Rigidbody2D rigidbody2;

    /*
     * Only one of the Following 3 should be set to true!
     * Otherwise it will come to errors that will cause the
     * Robot to move wierdly!
     */
    public bool moveRectangular = false; //The Robot will move in a square shape!
    public bool moveCounterClockWise = false; //Only relevant if the Robot moves in a Square-Shape
    public bool moveHorizontal = true; //Per default true, the robot will move left and rigth!
    public bool moveVertical = false; //The Robot will move up and down!
    public bool moveByWaypoints = false; //The Robot will follow waypoints

    public float speed = 1.0f; //How fast the Robot moves
    public float coverUnits = 1.0f; //How many units the robot should go at his speed
    private float time;

    //Waypoint Section for the Robot, in case the robot should follow waypoints
    public Transform[] waypoints;
    private WaypointMover waypointMover;
    private bool started = true; //Another Variable that stops the Method from beeing called again after beeing called once!

    // Robot Move Variable Section:
    private int rectPhase = 0;
    private int direction = 1;
    private int rectDirection = 1; //Acts differently as the normal direction var and is only used for Rectangular Movement!
    private bool moveVert = true;
    int count = 0;


    /*
     * Animation Variable Section
     */
    public float checkInterval = 0.1f; // How many Times the Position from the Robot is checked!

    private Vector2 lastPosition;
    private Animator animator;
    private float checkTimer = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        time = coverUnits / speed; //This comes from the Formula v = s * t
        waypointMover = gameObject.AddComponent<WaypointMover>();

        lastPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void SetDirectionBooleans(Vector2 delta)
    {
        // Standardm‰ﬂig alle Richtungen deaktivieren
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);

        if (delta.magnitude < 0.01f)
        {
            // Keine Bewegung = Idle, alle Richtungsflags bleiben false
            return;
        }

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
                animator.SetBool("Right", true);
            else
                animator.SetBool("Left", true);
        }
        else
        {
            if (delta.y > 0)
                animator.SetBool("Up", true);
            else
                animator.SetBool("Down", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Just sets and resets the timer
        Timer();

        //This is the section where the Animation of the Robot is getting startet
        checkTimer += Time.deltaTime;

        if (checkTimer >= checkInterval)
        {
            Vector2 currentPosition = transform.position;
            Vector2 delta = currentPosition - lastPosition;

            SetDirectionBooleans(delta);

            lastPosition = currentPosition;
            checkTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        /*
         * started = true means, that another Movemethod has been called
         * Because of that, we can call movebyWaypoints again. 
         */
        if (moveByWaypoints && started)
        {
            waypointMover.MoveToWaypoints(waypoints, speed);
            started = false;
        }
        if (moveHorizontal)
        {
            HorizontalMovement();
            waypointMover.StopMoving();
            started = true;
        }
        if (moveVertical)
        {
            VerticalMovement();
            waypointMover.StopMoving();
            started = true;
        }
        if (moveRectangular)
        {
            RectangularMovement();
            waypointMover.StopMoving();
            started = true;
        }
    }

    private void HorizontalMovement()
    {
        Vector2 position = rigidbody2.position; //Gets the current Position from the objekt the Script is attached to
        position.x = position.x + speed * direction * Time.deltaTime;
        rigidbody2.MovePosition(position);
    }

    private void VerticalMovement()
    {
        Vector2 position = rigidbody2.position;
        position.y = position.y + speed * direction * Time.deltaTime;
        rigidbody2.MovePosition(position);
    }
    
    private void RectangularMovement()
    {
        if (moveVert)
        {
            rectVerticalMovement();
        } else {
            rectHorizontalMovement();
        }
    }
    //Uses the rectDirection Variable to move the Robot
    private void rectHorizontalMovement()
    {
        Vector2 position = rigidbody2.position; //Gets the current Position from the objekt the Script is attached to
        position.x = position.x + speed * rectDirection * Time.deltaTime;
        rigidbody2.MovePosition(position);
    }
    private void rectVerticalMovement()
    {
        Vector2 position = rigidbody2.position;
        position.y = position.y + speed * rectDirection * Time.deltaTime;
        rigidbody2.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.Pt.Teleport();
        }
    }

    private void Timer()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            time = coverUnits / speed;
            direction = -direction;
            if(moveVertical)
            {
                moveVert =  !moveVert;
            }

            rectPhase = (rectPhase + 1) % 4;

            if (!moveCounterClockWise)
            {
                // Clockwise: Oben, Rechts, Unten, Links
                switch (rectPhase)
                {
                    case 0:
                        moveVert = true;
                        rectDirection = 1; // Hoch
                        break;
                    case 1:
                        moveVert = false;
                        rectDirection = 1; // Rechts
                        break;
                    case 2:
                        moveVert = true;
                        rectDirection = -1; // Runter
                        break;
                    case 3:
                        moveVert = false;
                        rectDirection = -1; // Links
                        break;
                }
            }
            else
            {
                // CounterClockWise: Rechts, Oben, Links, Unten
                switch (rectPhase)
                {
                    case 0:
                        moveVert = false;
                        rectDirection = 1; // Rechts
                        break;
                    case 1:
                        moveVert = true;
                        rectDirection = 1; // Hoch
                        break;
                    case 2:
                        moveVert = false;
                        rectDirection = -1; // Links
                        break;
                    case 3:
                        moveVert = true;
                        rectDirection = -1; // Runter
                        break;
                }
            }

            // Debug.Log("Phase: " + rectPhase);
            // Debug.Log("Vertical: " + moveVert + " | Direction: " + rectDirection);
        }
    }
}
