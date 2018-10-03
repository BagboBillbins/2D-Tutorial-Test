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
    private float horzInput;
    

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
    //Leave input to call as fast as possible
	void Update ()
    {
        horzInput = Input.GetAxis("Horizontal");  
        
	}

    //physics calls should be here so performance is optimized
    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * horzInput * acceleration);
        //set clmap for velocity
        Vector2 clampedVelocity = rb2d.velocity;
        //clamp the max speed on the x axis
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;

    }
}
