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

	public bool waiting = false;
	
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	
	private Transform player;		// Reference to the player.
	private Transform myTransform;
	private bool faceLeft = false;


	// to gain access to Phase functions
	public PlayerController phases;

	public bool hasInterrupt = false;

	/***************************************************
	 * determines if AI is turned on
	 * NOTE: isAnAI check is only there to simulate 
	 * 	game menu selection to Single Player mode 
	 * 	If that feature is implemented, isAnAI would 
	 * 	be removed along with EnemyAI component from 
	 * 	the character object and would be added 
	 * 	through the menu selection ONLY.
	 ***************************************************/
	public bool isAnAI;

	// frames to run
	public int running;
	// used for entering PHASE 3
	public bool incAttack;
	
	void Awake()
	{
		// Setting up the references.
		//ren = transform.Find("body").GetComponent<SpriteRenderer>();
		//frontCheck = transform.Find("frontCheck").transform;
		
		phases = gameObject.GetComponent<PlayerController>();
		player = phases.attack.transform;
		myTransform = transform;	
	}
	
	void FixedUpdate ()
	{
		//PHASE 0
		// if not an AI or can't act
		// exit loop
		if (!isAnAI)
			return;
		if (!phases.canAct)
		{
			phases.currentInput = "X";
			return;
		}


		//PHASE 1
		// if an interupt needs to be handled
		if (hasInterrupt)
		{
			handleInterrupt ();
		}

		//PHASE 2
		if (running > 0)
		{
			running--;
			return;
		}
		else
		{
			phases.moveX = 0;
		}

		//PHASE 3
		// decide if AI wants to block
		if (incAttack && block ())
			return;

		//PHASE 4
		// decide if AI can and will use a melee attack
		if (melee ())
			return;

		//PHASE 5
		// decide if AI will use ranged attack
		if (ranged ())
			return;

		//PHASE 6
		// decide if AI will move
		if (move ())
			return;



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
		
		/*print ("my" + myTransform.position.x + " " + Random.Range(0,10));
		//print (player.position.x);
		
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
		if (!waiting)
		{
			startState ();
		}
		
		// Set the enemy's velocity to moveSpeed in the x direction.
		//rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);*/	
	}

	public void handleInterrupt ()
	{
		running = 0;
	}

	public bool block ()
	{
		return false;
	}

	public bool melee ()
	{
		if (Mathf.Abs (myTransform.position.x - player.position.x) < 1)
		{
			int attack = Random.Range (0, 10);

			if (attack < 8)
				return false;

			phases.currentInput = "A";

			print (phases.currentInput);

			return true;
		}

		return false;
	}

	public bool ranged ()
	{
		return false;
	}
	
	public bool move()
	{
		int direction = Random.Range (0, 10);
		print (direction);
		
		if (direction < 3)
		{
			running = (Random.Range (0,2) + 1) * 10;
			return true;
		}
		else if (direction < 5)
		{
			phases.moveX = -1;
		}
		else
		{
			phases.moveX = 1;
		}
		
		running = (Random.Range (0,3) + 1) * 10;

		return true;
	}

	public void isAI()
	{
		print ("I'm an AI");
	}

	public void startState()
	{
		if (Mathf.Abs (myTransform.position.x - player.position.x) > 0) {
			//if (Random.Range(0,100) > 80) enemy not constantly moving towards player
			//{
			//}

			rangeState ();
			print ("ranged");
		} 
		else
		{
			print ("melee");
			meleeState ();
		}
	}

	public void meleeState()
	{

	}

	public void rangeState()
	{
		move ();
	}

	public void moveState()
	{

	}

	public void stop()
	{
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);

		waiting = false;
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