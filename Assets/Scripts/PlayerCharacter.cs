using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{

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
    [SerializeField]
    private Collider2D playerGroundCollider;
    [SerializeField]
    private PhysicsMaterial2D playerMovePhys, playerStopPhys;

    private float horzInput;
    private bool onGround;
    private Collider2D[] groundHitDetector = new Collider2D[16];
    private Checkpoint currentCheck;

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
    
    //physics calls should be here so performance is optimized
    void FixedUpdate()
    {
        UpdatePhysMat();
        Move();

    }

    private void UpdatePhysMat()
    {
        if(Mathf.Abs(horzInput)>0)
            playerGroundCollider.sharedMaterial = playerMovePhys;
        
        else
            playerGroundCollider.sharedMaterial = playerStopPhys;
    }

    private void UpdateOnGround()
    {
        onGround = groundDetectTrig.OverlapCollider(groundContactFilter, groundHitDetector) > 0;
        //Debug.Log("Grounded: " + onGround);

    }

    private void UpdateHorzInput()
    {
        horzInput = Input.GetAxisRaw("Horizontal"); //raw ignores unity's smoothing filter which makes movement more responsive
    }

    private void JumpInputHandler()
    {
        if (Input.GetButtonDown("Jump") && onGround)
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);//Impulse adds immediate action while force is not
        
    }
    private void Move()
    {
        rb2d.AddForce(Vector2.right * horzInput * acceleration);
        Vector2 clampedVelocity = rb2d.velocity;//set clamp for velocity
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);//clamp the max speed on the x axis
        rb2d.velocity = clampedVelocity;
    }
    public void Respawn()
    {
        if(currentCheck == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//reload current scene on death

        else
        {
            rb2d.velocity = Vector2.zero;
            transform.position = currentCheck.transform.position;
        }
        
    }
    public void setCurrentCheck(Checkpoint newCurrentCheck)
    {
        if(currentCheck !=null)
        {
            currentCheck.setActive(false);
            currentCheck = newCurrentCheck;
            currentCheck.setActive(true);

        }
    }
    
}
