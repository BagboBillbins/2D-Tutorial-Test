using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private float acceleration = 5;
    [SerializeField]
    private float maxSpeed = 5;
    [SerializeField]
    private float jumpForce = 10;
    [SerializeField]
    private ContactFilter2D groundContactFilter;
    [SerializeField]
    private Collider2D groundDetectTrig;

    private float horzInput;
    private bool onGround;
    private Collider2D[] groundHitDetector = new Collider2D[16];

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
    //Leave input to call as fast as possible
	void Update()
    {
        UpdateOnGround();
        UpdateHorzInput();
        JumpInputHandler();

    }

    private void UpdateOnGround()
    {
        onGround = groundDetectTrig.OverlapCollider(groundContactFilter, groundHitDetector) > 0;
        //Debug.Log("Grounded: " + onGround);

    }

    private void UpdateHorzInput()
    {
        horzInput = Input.GetAxis("Horizontal");
    }

    private void JumpInputHandler()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);//Impulse adds immediate action while force is not
        }
    }

    //physics calls should be here so performance is optimized
    void FixedUpdate()
    {
        Move();

    }

    private void Move()
    {
        rb2d.AddForce(Vector2.right * horzInput * acceleration);
        Vector2 clampedVelocity = rb2d.velocity;//set clamp for velocity
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);//clamp the max speed on the x axis
        rb2d.velocity = clampedVelocity;
    }
}
