using UnityEngine;
using System.Collections;

public class BplayerControllerScript : MonoBehaviour 
{
	public float maxspeed = 10f;
	bool facingRight = true;
	float Cposition;
	private Transform Cwhere;

	bool noControl1 = false; //set to true when an event takes away control from player

	Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	public float jumpForce;
	float move;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		Cwhere = GameObject.FindGameObjectWithTag("CPlayer2").transform;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//initializers for the frame
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("speed", move);
		anim.SetBool ("facingRighty", facingRight);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		//event is happening (hyper, being juggled, etc)
		if (noControl1 == true) 
		{
			return;
		}		 

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if( grounded == false )
		{
			return;
		}
		else 
		{
			//checking if turning around is needed
			Cposition = Cwhere.position.x;
			if (Cposition > transform.position.x && facingRight == false) 
			{
				facingRight = false;
				Flip ();
			} 
			else if (Cposition < transform.position.x && facingRight == true) 
			{
				facingRight = true;
				Flip ();
			}



						


			//moving left and right
			move = Input.GetAxis ("Horizontal1");



			rigidbody2D.velocity = new Vector2 (move * maxspeed, rigidbody2D.velocity.y);

						/*
		if ( Input.GetAxis("Horizontal1") > 0 && !facingRight) 
		{
			Flip ();
		} 
		else if (Input.GetAxis("Horizontal1") < 0 && facingRight) 
		{
			Flip ();
		}
		*/

			//code to jump
			if (grounded && (Input.GetAxis ("Jump1") < 0)) 
			{
				anim.SetBool ("Ground", false);
				rigidbody2D.AddForce (new Vector2 (0, jumpForce));
			}
		}
	}


	//Input.GetKeyDown (KeyCode.Space))
	void Update()
	{

	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
