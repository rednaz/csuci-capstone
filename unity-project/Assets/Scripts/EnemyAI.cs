using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	
	
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	
	private Transform player;		// Reference to the player.
	private Transform myTransform;
	private bool faceLeft = false;
	
	
	void Awake()
	{
		// Setting up the references.
		//ren = transform.Find("body").GetComponent<SpriteRenderer>();
		//frontCheck = transform.Find("frontCheck").transform;
		
		player = GameObject.FindGameObjectWithTag("CPlayer2").transform;
		myTransform = transform;
	}
	
	void FixedUpdate ()
	{
		// Create an array of all the colliders in front of the enemy.
		//Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);
		
		// Check each of the colliders.
		/*foreach(Collider2D c in frontHits)
		{
			// If any of the colliders is an Obstacle...
			if(c.tag == "Obstacle")
			{
				// ... Flip2 the enemy and stop checking the other colliders.
				Flip2 ();
				break;
			}
		}*/
		
		print ("my" + myTransform.position.x);
		print (player.position.x);
		
		if (myTransform.position.x - player.position.x > 0 && !faceLeft)
		{
			Flip2 ();
			faceLeft = true;
		}
		else if (myTransform.position.x - player.position.x < 0 && faceLeft)
		{
			Flip2 ();
			faceLeft = false;
		}


		
		// Set the enemy's velocity to moveSpeed in the x direction.
		//rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	
	}

	public void startState()
	{
		while (true)
		{
			if (Mathf.Abs (myTransform.position.x - player.position.x) > 6)
			{
				//if (Random.Range(0,100) > 80) enemy not constantly moving towards player
				//{
				//}

				rangeState ();
			}
			else
			{
				meleeState ();
			}
		}
	}

	public void meleeState()
	{

	}

	public void rangeState()
	{

	}

	public void moveState()
	{

	}

	public void move(float moveSpeed)
	{
		rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);

		print ("In Move");

		Invoke ("stop", 2);
	}

	public void stop()
	{
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
	}
	
	public void Hurt()
	{
		// Reduce the number of hit points by one.
		HP--;
	}
	
	
	public void Flip2()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}