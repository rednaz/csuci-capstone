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

	private PlayerController player;
	private Transform playerTransform;		// Reference to the playerTransform.
	private Transform myTransform;
	private bool faceLeft = false;


	// to gain access to Phase functions
	public PlayerController phases;

	public bool hasInterrupt = false;

	/***************************************************
	 * determines if AI is turned on
	 * NOTE: isAnAI check is only there to simulate 
	 * 	game menu selection to Single playerTransform mode 
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
	// AI is doing combo
	// public currentCombo;
	public int comboPos = 0;
	public int currType = -1; // 0 for Drive, 1 for SDrive
	public int currAttack = -1;

	void Awake()
	{
		// Setting up the references.
		//ren = transform.Find("body").GetComponent<SpriteRenderer>();
		//frontCheck = transform.Find("frontCheck").transform;

		phases = gameObject.GetComponent<PlayerController>();
		player = phases.attack;
		playerTransform = player.transform;
		myTransform = transform;		
	}
	
	void FixedUpdate ()
	{
		//PHASE 0
		// if not an AI or can't act
		// exit loop
		//if (!isAnAI)
			//return;
		if (!phases.canAct)
		{
			phases.currentInput = "X";
			comboPos = 0;
			return;
		}
		//print (phases.health);
		//print (phases.blockCheck);

		print ("Combo" + comboPos);

		// updates knowledge of which way AI is facing
		float dist = myTransform.position.x - playerTransform.position.x;
		print (dist);
		if (dist > 0)
			faceLeft = true;

		//PHASE 1
		// if an interupt needs to be handled
		if (handleInterrupt ())
		{
			block (dist);
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
		if (melee (dist))
			return;

		//PHASE 5
		// decide if AI will use ranged attack
		if (ranged (dist))
			return;

		//PHASE 6
		// decide if AI will move
		if (move (dist))
			return;



	}

	public bool handleInterrupt ()
	{
		if (player.normalFrames > 0)
		{
			incAttack = true;

			return true;
		}
		else
			incAttack = false;

		return false;
	}

	public bool block (int dist)
	{
		print ("In Blocking");
		if (Random.Range (0, 10) < 5)
		{
			print ("BLOCKED");
			phases.moveX = -1;
			running = 2;
			return true;
		}

		return false;
	}

	public bool melee (int dist)
	{
		if (Mathf.Abs (dist) < 1)
		{
			if (comboPos > 0)
			{
				string current = "";

				if (currType == 0)
				{
					if (faceLeft)
						current = phases.drive[2, 0].ToString();
					else
						current = phases.drive[0, 0].ToString();
					
				}
				else if (currType == 1)
				{
					if (faceLeft)
						current = phases.Sdrive[2, currAttack].ToString();
					else
						current = phases.Sdrive[0, currAttack].ToString();
				}

				if (comboPos < current.Length)
				{
					phases.currentInput = "" + current[comboPos];
					comboPos++;
				}
				else
				{
					comboPos = 0;
					currType = -1;
					currAttack = -1;
					return false;
				}
			}

			else
			{
				print ("New Attack");
				int attack = Random.Range (0, 10);
				
				if (attack < 4)
					return false;

				else if (attack < 7)
				{
					currType = 0;
					
					if (faceLeft)
						phases.currentInput = "" + phases.drive[2, 0].ToString()[comboPos];
					else
						phases.currentInput = "" + phases.drive[0, 0].ToString()[comboPos];
					
					comboPos++;
				}

				else if (attack < 8)
				{
					currType = 1;

					currAttack = Random.Range (1, 3);

					if (faceLeft)
						phases.currentInput = "" + phases.Sdrive[2, currAttack].ToString()[comboPos];
					else
						phases.currentInput = "" + phases.Sdrive[0, currAttack].ToString()[comboPos];
					
					comboPos++;
				}

				else
				{
					attack = Random.Range (0, 4);

					switch(attack)
					{
					case 0: phases.currentInput = "A"; break;
					case 1: phases.currentInput = "B"; break;
					case 2: phases.currentInput = "C"; break;
					case 3: phases.currentInput = "D"; break;
					}
				}
			}

			return true;
		}

		return false;
	}

	public void kick ()
	{
		int attack = Random.Range (0, 10);
		if (attack < 8)
		{

		}
		else
			phases.currentInput = "C";
	}

	public void punch ()
	{
		int attack = Random.Range (0, 100);
		if (attack < 5)
		{
		}
		if (attack < 99)
		{
			currAttack = Random.Range (0, phases.drive.Length);
			phases.currentInput = phases.drive[currAttack, comboPos].ToString();
			comboPos++;
		}
		else
			phases.currentInput = "A";
	}

	public bool ranged (int dist)
	{
		return false;
	}
	
	public bool move(int dist)
	{
		int direction = Random.Range (0, 10);
		
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
		if (Mathf.Abs (myTransform.position.x - playerTransform.position.x) > 0) {
			//if (Random.Range(0,100) > 80) enemy not constantly moving towards playerTransform
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