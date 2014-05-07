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
		phases = gameObject.GetComponent<PlayerController>();
		player = phases.attack;
		playerTransform = player.transform;
		myTransform = transform;		
	}
	
	void FixedUpdate ()
	{
		//PHASE 0
		// if can't act exit loop
		if (!phases.canAct)
		{
			phases.currentInput = "X";
			comboPos = 0;
			return;
		}

		// updates knowledge of which way AI is facing
		float dist = myTransform.position.x - playerTransform.position.x;
		if (dist > 0)
			faceLeft = true;

		//PHASE 1
		// if an interupt needs to be handled
		switch (handleInterrupt ())
		{
		case 0: block (dist); break;
		case 1: contCombo (); return;
		case -1: print ("NO INTERRUPTS"); break;
		}

		/*if (handleInterrupt ())
		{
			block (dist);
		}*/

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
		if (incAttack)
			return;

		//PHASE 4
		// decide if AI can and will use a melee attack
		if (melee (dist))
			return;

		//PHASE 5
		// decide if AI will use ranged attack
		if (ranged ())
			return;

		//PHASE 6
		// decide if AI will move
		if (move ())
			return;



	}

	public int handleInterrupt ()
	{
		print ("Current Attack" + currAttack);

		if (player.normalFrames > 0)
			return 0;
		else if (currAttack > -1)
			return 1;

		// nothing to handle
		else
			return -1;
	}

	public bool block (float dist)
	{
		print ("In Blocking");
		if (Random.Range (0, 10) < 3)
		{
			print ("BLOCKED");
			phases.moveX = -1;
			running = 2;
			return true;
		}

		return false;
	}

	public void contCombo ()
	{
		print ("IN CONTINUE COMBO");

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
		print ("ATTACK LENGTH " + current.Length);
		print ("CURRENT COMBO " + comboPos);
		if (comboPos < current.Length - 1)
		{
			phases.currentInput = "" + current[comboPos];
			comboPos++;
		}
		else
		{
			comboPos = 0;
			currType = -1;
			currAttack = -1;
			
			print ("RESET");
			print ("COMBOPOS " + comboPos);
			print ("CURRATTACK " + currAttack);
		}
	}

	public bool melee (float dist)
	{
		if (Mathf.Abs (dist) < 1)
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
				switch(Random.Range (0, 4))
				{
				case 0: phases.currentInput = "A"; break;
				case 1: phases.currentInput = "B"; break;
				case 2: phases.currentInput = "C"; break;
				case 3: phases.currentInput = "D"; break;
				}
			}

			return true;
		}

		return false;
	}

	public bool ranged ()
	{
		print ("RANGED ATTACK");

		if (Random.Range (0, 100) < 5)
		{
			currAttack = 0;

			print ("FIRE");
			
			if (faceLeft)
				phases.currentInput = "" + phases.Sdrive[2, currAttack].ToString()[comboPos];
			else
				phases.currentInput = "" + phases.Sdrive[0, currAttack].ToString()[comboPos];
			
			comboPos++;
		}

		return false;
	}
	
	public bool move()
	{
		int direction = Random.Range (0, 10);


		if (direction < 3)
		{
			running = (Random.Range (0,2) + 1) * 10;
			return true;
		}
		// move away from player
		else if (direction < 5)
		{
			if (faceLeft)
				phases.moveX = 1;
			else
				phases.moveX = -1;
		}
		// move towards player
		else
		{
			if (faceLeft)
				phases.moveX = -1;
			else
				phases.moveX = 1;
		}
		
		running = (Random.Range (0,3) + 1) * 10;

		return true;
	}
}