using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public CreditController creditBar;
    private PlayerTeleporter playerTeleporter;
    public InputAction MoveAction;

    [SerializeField] private Animator _animator;


    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float PlayerSpeed = 3.0f;
    float lookLeft = 180f;
    float lookRight = 0f;

    public int maxCredits = 3;
    public int currentCredits = 0;


    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        creditBar.SetMaxCredits(maxCredits);
        playerTeleporter = GetComponent<PlayerTeleporter>();
    }

    public PlayerTeleporter Pt { get { return playerTeleporter; } }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        //Start Move-Up-Animation
        if (move.y == -1f && move.x == 0)
        {
            _animator.SetBool("isRunningForward", true);

            _animator.SetBool("isRunningBackwards", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isRunningRight", false);
        }

        //Start Move-Down-Animation
        if(move.y == 1f && move.x == 0)
        {
            _animator.SetBool("isRunningBackwards", true);

            _animator.SetBool("isRunningForward", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isRunningRight", false);
        }

        /*
         * Move-Left-Animation Starts whenever x is smaler 0. 
         * The y-Variable value is irrelevant for the sidewards movement because it's got priority
         */

        if(move.x < 0)
        {
            _animator.SetBool("isRunningLeft", true);

            _animator.SetBool("isRunningBackwards", false);
            _animator.SetBool("isRunningForward", false);
            _animator.SetBool("isRunningRight", false);
        }

        /*
         * Move-Right-Animation Starts whenever x is smaler 0. 
         * The y-Variable value is irrelevant for the sidewards movement because it's got priority
         */
        if (move.x > 0)
        {
            _animator.SetBool("isRunningRight", true);
            
            _animator.SetBool("isRunningBackwards", false);
            _animator.SetBool("isRunningForward", false);
            _animator.SetBool("isRunningLeft", false);
        }

        //If the Player stands still, all animations should stop!
        if (move.y == 0 && move.x == 0)
        {
            _animator.SetBool("isRunningBackwards", false);
            _animator.SetBool("isRunningForward", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isRunningRight", false);
        }
        
             Debug.Log(move);
    }

        void FixedUpdate()
        {
            Vector2 position = (Vector2)rigidbody2d.position + move * PlayerSpeed * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }

        void rotateObject(float angle)
        {
            Vector3 currentRotation = transform.eulerAngles;
            float x = currentRotation.x;
            float y = angle;
            float z = currentRotation.z;
            transform.eulerAngles = new Vector3(x, y, z);
        }

        public void ChangeCredits(int amount)
        {

            currentCredits = Mathf.Clamp(currentCredits +  amount, 0, maxCredits);
            creditBar.SetCredits(currentCredits);
            Debug.Log(currentCredits + "/" + maxCredits);
        }

    public int credits {  get { return currentCredits; } }
}