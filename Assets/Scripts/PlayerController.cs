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


    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float PlayerSpeed = 3.0f;
    float lookLeft = 180f;
    float lookRight = 0f;

    public int maxCredits = 3;
    int currentCredits = 0;


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

        if (move.x < 0)
        {
            rotateObject(lookLeft);
        }
        else if (move.x > 0)
        {
            rotateObject(lookRight);
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