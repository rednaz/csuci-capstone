using UnityEngine;
using System.Collections;

public class BplayerControllerScript : MonoBehaviour 
{
	public float maxspeed = 10f;
	bool facingRight = true;

	Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	public float jumpForce;
	private int health;
	public GUIText healthUI;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		health = 100;
		healthUI.text = "P1 Health: " + health;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		float move = Input.GetAxis ("Horizontal1");

		anim.SetFloat("speed", Mathf.Abs(move));

		rigidbody2D.velocity = new Vector2 (move * maxspeed, rigidbody2D.velocity.y);
	
		if ( Input.GetAxis("Horizontal1") > 0 && !facingRight) 
		{
			Flip ();
		} 
		else if (Input.GetAxis("Horizontal1") < 0 && facingRight) 
		{
			Flip ();
		}
	}
	//Input.GetKeyDown (KeyCode.Space))
	void Update()
	{
		if ( grounded && ( Input.GetAxis("Jump1") < 0 ) ) 
		{
			anim.SetBool ("Ground", false);
			rigidbody2D.AddForce( new Vector2(0,jumpForce));
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		modifyHealth (-1);
	}

	void modifyHealth(int deltaHealth)
	{
		if (health > 0)
		{
			health += deltaHealth;
			healthUI.text = "P1 Health: " + health;
		}
	}

	int getHealth()
	{
		return this.health;
	}
}
