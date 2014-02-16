using UnityEngine;
using System.Collections;

public class CplayerControllerScript : MonoBehaviour 
{
	public float maxspeed2 = 10f;
	bool facingLeft = true;

	Animator anim2;

	bool grounded2 = false;
	public Transform groundCheck2;
	float groundRadius2 = 0.2f;
	public LayerMask whatIsGround2;
	public float jumpforce2 = 700f;

	// Use this for initialization
	void Start () 
	{
		anim2 = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		grounded2 = Physics2D.OverlapCircle (groundCheck2.position, groundRadius2, whatIsGround2);
		anim2.SetBool ("Ground2", grounded2);

		anim2.SetFloat ("vSpeed2", rigidbody2D.velocity.y);

		float move2 = Input.GetAxis ("Horizontal2");
		anim2.SetFloat( "speed2", Mathf.Abs( move2 ) );


		rigidbody2D.velocity = new Vector2 ( move2 * maxspeed2, rigidbody2D.velocity.y );

		if ( move2 > 0 && !facingLeft ) 
		{
			Flip2 ();
		} 
		else if ( move2 < 0 && facingLeft ) 
		{
			Flip2();
		}
	}

	void Flip2()
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// Update is called once per frame
	void Update () 
	{
		if (grounded2 && ( Input.GetAxis("Jump2") < 0 )) 
		{
			anim2.SetBool ("Ground2", false);
			rigidbody2D.AddForce (new Vector2(0, jumpforce2));
		}
	}
}
