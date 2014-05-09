﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	//random number generator for sounds
	private System.Random rand = new System.Random();

	public bool ReadySet = false;

	public bool owAirTrigger = false;

	public string enemyScript;
	public PlayerController attack;
	public FreezeCode freeze;
	
	//debug string
	public string debugString;

	//animator declaration
	public Animator anim;
	public string facingLeftstring;
	public string velocityXString;
	public string blockingString;
	public string crouchingString;
	public string normalFramesString;
	public string LPStringTrigger;
	public string HPStringTrigger;
	public string LKStringTrigger;
	public string HKStringTrigger;
	public string groundString;
	public string velocityYString;
	public string hurtLockStringLight;
	public string hurtLockStringHeavy;
	public string hitFramesString;
	public string blockLockStandingString;
	public string blockLockCrouchingString;
	public string gettingUpString;
	public string gettingTrippedString;
	public string owAirTriggerString;
	public string alreadyAirString;
	public string D1triggerString;
	public string SD1triggerString;		
	public string SD2triggerString;
	public string driveFramesString;
	public string stellarDriveFramesString;

	
	//maxSpeed declaration
	public float maxspeed;

	//opposing player declaration
	public string OtherPlayer; //declaring who the enemy is
	public Transform OtherWhere; //spying on the enemy position
	public float enemyX; //grabbing the exact position of the enemy at the exact frame

	//button pushes: only one button can be registered in the system at a time per frame
	//turns true when the attack is in range (hitboxes use this)
	//standard attacks
	public bool SLP = false;		public bool ALP = false;		public bool CLP = false;
	public bool	SHP = false;		public bool AHP = false;		public bool CHP = false;
	public bool SLK = false;		public bool ALK = false;		public bool CLK = false;
	public bool SHK = false;		public bool AHK = false;		public bool CHK = false;
	public bool D1 = false;											//supers are now drives
	public bool SD1 = false;		public bool SD2 = false;		//hypers are now stellar drives
	//note: S = standing, A = air, C = crouching

	//drive declarations (who has access to what)
	public bool Snap;					public bool Strike;

	//stellar drive declarations (who has access to what)
	public bool YouAreUnderArrest;		public bool DownBoy;
	public bool OmniBlast;				public bool OmniBarrage;

	//normal frames declaration
	public int normalFrames = 0; //how many frames total the normal lasts
	public int startNormal; //when the frame actually starts damage
	public int finishNormal; //the last frame of damage call
	public bool atariDesu;  //when active, the finishNormal doesn't matter anymore
	//note: a normal once landed is called off from anymore calls to
	//      avoid multiple hits of the same individual normal

	//array values for normals
	public int[ , ]attackValues = new int[ 7, 12 ];
	//Rows:	0: totalFrames		3: Xforce			6: hitType			Columns: 	00 - 03: standing values
	//		1: startFrame		4: Yforce											04 - 07: air values
	//		2: finishFrame		5: damageAmount										08 - 11: crouching values

	//key for hitType
	//0 = hurts upper body only, crouching opponent means it completely misses
	//1 = hurts lower body only, crouch blocking only successful
	//2 = hurts either upper or lower, any blocking will do
	//3 = hurts overhead, only standing blocking will work
	//4 = unblockable
	//5 = trip, hurts lower body only, crouch blocking only successful

	//normal triggers
	public bool LPtrigger = false;		public bool HPtrigger = false;		public bool LKtrigger = false;		public bool HKtrigger = false;

	//drive triggers
	public bool D1trigger = false;

	//stellar drive triggers
	public bool SD1trigger = false;		public bool SD2trigger = false;		public bool SD3trigger = false;

	//button declarations
	public string LPgrabber;
	public string HPgrabber;
	public string LKgrabber;
	public string HKgrabber;

	//crouching or not
	public bool crouchCheck = false;

	//for attacks that suck in their target
	//downBoy is one of them
	public bool flipDamage = false;

	//blocking or not
	public bool blockCheck = false;

	//checking if player is on the ground
	public bool groundCheck = false;
	public LayerMask whatIsGround;
	public float groundRadius = 0.2f;
	public Transform daGround;
		
	//needed because only one attack can be done in the air
	//player must land before another air attack can be done again
	public bool airLock = false; 
	public bool alreadyAir = false;

	//declaration of up force for jumping
	public float jumpForce;

	//countDown is needed when the player is recovering and getting up
	//a value of 0 means the player is up and ready for more punishment
	public int countDown; //set equal to countDownSetter when the player starts gettting up
	public int countDownSetter; //how long it takes the player to get up

	//declaring stellar drive variable
	public int stellarDriveMeter = 300;
	//100 = level 1
	//200 = level 2
	//300 = level 3 (max level)
	public int bullets = 6;
	
	//checking what direction the player is facing at the time of the frame
	//note: player can't turn when in the air
	public bool facingLeft = false;

	//button combinations listener
	public string commands = "XXXXXXX";
	public string currentInput;
	public string nextInput; //this is compared to previousInput to ensure doubles aren't registered into the system
	public string previousInput = "x";  //the x is done because the first input doesn't have anything to match up to

	//directional movement
	public float moveX; //grabbing X directional input
	public float moveY; //grabbing Y directional input
	public string moveXgrabber;
	public string moveYgrabber;

	//drive frames declaration
	public int drive1Frames;

	//stellar drive frames declaration
	public int stellarDriveFrames;

	//drive declarations
	public string[ , ]drive = new string[ 4, 1 ];
	public int driveDelay1;		//how many frames drive 1 takes

	//stellar drives declarations
	public string[ , ]Sdrive = new string[ 4, 3 ];
	public int stellarDrivedelay1;	 //how many frames stellar drive 1 takes
	public int stellarDrivedelay2;	 //how many frames stellar drive 2 takes
	public int stellarDrivedelay3;	 //how many frames stellar drive 3 takes
	public int stellarEat1; //how much meter stellar drive 1 eats
	public int stellarEat2; //how much meter stellar drive 2 eats
	public int stellarEat3; //how much meter stellar drive 3 eats

	//combo declaration
	int beingComboed = 0;

	//KO declaration (knocked out)
	bool KO = false;

	//player health declaration
	public int health;
	public int maxHealth;

	//player being hurt
	public bool beingHurt = false;
	public bool beingHurtLow;
	public int recieveHurtX;
	public int recieveHurtY;
	public int damageAmountRecieved;
	public bool recievedAttackLow = false;  //true if the attack is low
	public int chipDamage;
	public int damageType; //what type of damage to send
	public int damageTypeRecieved;
	public bool beingTripped = false; //trigger for being tripped
	public int trippedCountdown;  //set equal to trippedAmount when trip happens
	public int trippedAmount;	//how many frames the tripped animation lasts

	//hurt launch variables
	public int hurtX;  //how much force a punch does to you in X direction
	public int hurtY;  //how much force a punch does to you in Y direction
	public int currentDamage;
	public bool hurtLow;
	public int damageThreshold; //determines if the hit recieved is soft or hard hit
	public int lightHitFrames;  //stun lasts this long when ground damage is equal or less than damageThreshold
	public int heavyHitFrames;  //stun lasts this long when ground damage is greater than damageThreshold
	public int hitFrames;       //hitFrames grabs from lightHitFrames/heavyHitFrames depending on damageThreshold
	public bool hurtLockLight = false;
	public bool hurtLockHeavy = false;
	public bool blockStandingLock = false;
	public bool blockCrouchingLock= false;


	// variable for AI code to interact with
	// variable is set to true after reaching PHASE 13
	// variable is set to false if not allowed to act
	public bool canAct = true;

	//called every frame
	//the heart of all actions-------------------------------------------------------
	void FixedUpdate () 
	{
		//this line checks if the character is on the ground at this frame
		groundCheck = Physics2D.OverlapCircle (daGround.position, groundRadius, whatIsGround);
		//setting up the animation for the frame
		animationCalls ();
		//Phase Start (counter going to 0 then fight begins)
		if ( ReadySet == false )
		{
			determineFlip ();
			return;
		}
						
		//int randomNum = rand.Next(0,100);
		//Debug.Log ( randomNum );




		//this is nothing but debug code, feel free to uncomment 
		//at your pleasure to see game activity
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			//Debug.Log ( debugString + " health: " + health + " / " + maxHealth );
			freeze.tempFreeze();
		}




		//Phase 0 
		//Player is hit and got to 0 health, player falls to ground
		//Control is relinquished from the player here
		if ( health < 1 )
		{
			KO = true;
			canAct = false;
		}
		if ( KO == true ) 
		{
			return;
		}

		//Phase 1
		//Player is on the ground, no control, and recovering from hurt (getting up)
		//Flush the status here when countDown reaches 0
		if ( countDown > 1 && groundCheck == true ) 
		{
			countDown--;
			canAct = false;
			return;
		} 
		else if ( countDown == 1 )
		{
			countDown--;
			Debug.Log ( debugString + " " + beingComboed );
			beingComboed = 0;
			flush ();
		}
		if( trippedCountdown > 1 )
		{
			trippedCountdown--;
			canAct = false;
			//the player is in the middle of being tripped animation
			return;
		}
		if ( trippedCountdown == 1 ) 
		{
			//the player is finished being tripped, now must get up
			trippedCountdown--;
			canAct = false;
			countDown = countDownSetter;
			return;
		}
		if( beingTripped == true )
		{
			canAct = false;
			beingTripped = false;
			return;
		}


		//Player is in the air and no control, being hurt
		if( groundCheck == false )
		{
			hurtWhileAir();
		}
		if( owAirTrigger == true )
		{
			if( groundCheck == true )
			{
				owAirTrigger = false;
				countDown = countDownSetter;
			}
			return;
		}


		//Phase 3
		//Player is on the ground and no control, being hurt
		//Set countDown from the hit
		if( groundCheck == true )
		{
			canAct = false;
			hurtWhileStanding ();
		}
		//turn off beingHurt when the player recovers from the previous ground hit
		if ( beingHurt == true )
		{
			beingHurt = false;
			return;
		}
		if ( trippedCountdown > 1 )
		{
			return;
		}
		if ( hitFrames > 1 )
		{
			blockCrouchingLock = false;
			blockStandingLock = false;
			hurtLockLight = false;
			hurtLockHeavy = false;
			hitFrames--;
			canAct = false;
			return;
		}
		else if ( hitFrames == 1 )
		{
			Debug.Log ( debugString + " " + beingComboed );
			beingComboed = 0;
			hitFrames--;
			flush ();
		}


		//Phase 5
		//Player is in the middle of a super/hyper and cannot move till
		//the move completes, prevents spamming of supers/hypers
		if( stellarDriveFrames > 1 )
		{
			if ( YouAreUnderArrest == true && SD1trigger == true )  //stellar drive is only available to Barrett
			{
				YouAreUnderArrestMethod ();
			}
			if ( DownBoy == true && SD1trigger == true )	//stellar drive is only available to Olivia
			{
				DownBoyMethod ();
			}
			if ( OmniBlast == true && SD2trigger == true ) 	//stellar drive is only available to Barrett and Olivia
			{
				OmniBlastMethod ();
			}
			if ( OmniBarrage == true && SD3trigger == true )	//stellar drive is only available to Barrett and Olivia
			{
				OmniBarrageMethod ();
			}
			stellarDriveFrames--;
			return;
		}
		if( stellarDriveFrames == 1 )
		{
			SD1trigger = false;
			SD2trigger = false;
			SD3trigger = false;
			stellarDriveFrames--;
			flush ();
			return;
		}


		//Phase 6
		//Player is in the air, left and right control not possible
		//Only one normal air attack in the air is possible
		//One super and hyper and only one of each are possible if allows
		//Blocking isn't possible in the air
		//This the starting Phase where player can put input into the system to be registered
		buttonRegistration ();
		if( normalFrames > 1 && airLock == true && alreadyAir == false )
		{
			blockCheck = false;
			if( groundCheck == true )
			{
				normalFrames = 0;
				airLock = false;
				atariDesu = false;
				normalReset();
				alreadyAir = false;
				return;
			}
			normalAirCalls();
			return;
		}
		if( normalFrames == 1 && airLock == true )
		{
			normalFrames--;
			alreadyAir = true;
			return;
		}
		if ( groundCheck == false )
		{
			blockCheck = false;
			if( alreadyAir == false )
			{
				airNormal ();
			}
			return;
		}
		if( groundCheck == true )
		{
			alreadyAir = false;
		}
		
		
		//Phase 7
		//Player is on the ground and attacking, left and right control not possible
		//Super and hyper are possible here
		//Jumping is not possible
		//Blocking isn't possible here


		//Phase 8
		//player successfully implemented a stellar drive, game executes stellar drive
		//hyper 1, 2, and 3 checked if entered at this moment
		hyper1Check ();
		if( SD1trigger == true )
		{
			stellarDriveFrames = stellarDrivedelay1;
			return;
		}
		if( SD2trigger == true )
		{
			stellarDriveFrames = stellarDrivedelay2;
			return;
		}


		//Phase 9
		//player successfully implemented a super, game executes super
		//super 1 and 2 checked if entered a this moment


		//Phase 10 (normals being executed)
		if( normalFrames > 0 && groundCheck == true )
		{
			canAct = false;
			normalCalls();
			return;
		}
		normalReset ();
		atariDesu = false;


		//Phase 11 (crouching)




		//Phase 12 (crouching normals)
		if ( crouchCheck == true )
		{
			canAct = false;
			crouchingNormal();
		}


		//Phase 13 (normals listening)
		if ( crouchCheck == false )
		{
			canAct = false;
			standingNormal();
		}
		if ( normalFrames > 0 )
		{
			return;
		}

		canAct = true;

		//Phase 14 (walking and flipping)
		//Player is ready to take any commands without interruptions
		//moving left and right

		if( moveY < -.01f )
		{
			crouchCheck = true;
		}
		else
		{
			crouchCheck = false;
		}

		//Phase 15 (walking)
		walking ();
		determineFlip ();

		//Phase 16 (jumping)
		//code to jump
		jumpCall ();

		//Phase 17 (blocking)
		blockInput ();




		//Debug.Log ( moveX + ", " + moveY );
	}
	//end FixedUpdate() -------------------------------------------------------------
	

	void determineFlip()
	{
		//getting other player's position
		enemyX = OtherWhere.position.x;
		//deciding if turning is nessessary on this frame
		if ( enemyX  < transform.position.x && facingLeft == false ) 
		{
			Flip ();
		} 
		else if ( enemyX > transform.position.x && facingLeft == true ) 
		{
			Flip ();
		}
	}

	//command called to turn the character around
	void Flip()
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	string buttonListener()
	{
		//buttons get priority to be pushed
		if (Input.GetButton (LPgrabber) == true) 
		{
			currentInput = "A";
		} 
		else if (Input.GetButton (HPgrabber) == true) 
		{
			currentInput = "B";
		} 
		else if (Input.GetButton (LKgrabber) == true) 
		{
			currentInput = "C";
		} 
		else if (Input.GetButton (HKgrabber) == true) 
		{
			currentInput = "D";
		} 

		//no buttons, so joystick inputs are next
		else if( moveX < - 0.5f && moveY < -.5f ) //down + left 
		{
			currentInput = "1";
		}
		else if( moveX > .5f && moveY < -.5f ) //down + right
		{
			currentInput = "3";
		}
		else if ( moveY < - 0.5f ) // up (this pretty much cancels out anything since nothing requires up)
		{
			currentInput = "2";
		}
		else if ( moveY > .5f ) //down
		{
			currentInput = "8";
		}
		else if( moveX < - 0.5f ) //left
		{
			currentInput = "4";
		}
		else if( moveX > .5f ) //right
		{
			currentInput = "6";
		}
		else if (gameObject.GetComponent<EnemyAI>() == null) //no input && no AI
		{
			currentInput = "X";
		}
		//
		return currentInput;
	}

	//method to stack what button combinations have been done
	void buttonRegister ()
	{
		if ( Equals( previousInput, nextInput ) || Equals(nextInput, "X" )) //preventing reading twitching in the commands  
		{ 
			return;
		}

		else if( commands.Length == 7 ) //string is at 7 characters, it's time to trim it so it doesn't exceed 7 characters
		{
			//shifting, commands[0] is tossed out
			char placeHolder1 = commands[ 1 ];
			char placeHolder2 = commands[ 2 ];
			char placeHolder3 = commands[ 3 ];
			char placeHolder4 = commands[ 4 ];
			char placeHolder5 = commands[ 5 ];
			char placeHolder6 = commands[ 6 ];
			commands = ""; //flushed
			commands = placeHolder1.ToString() + placeHolder2.ToString() + placeHolder3.ToString() + 
				placeHolder4.ToString() + placeHolder5.ToString() + placeHolder6.ToString() + nextInput;
			previousInput = nextInput;
			//Debug.Log ( placeHolder1 + placeHolder3 + placeHolder4 + placeHolder5 + placeHolder6 );
			Debug.Log (commands);
		}

		else 
		{
			//Debug.Log ("adder" + nextInput);
			commands = commands + nextInput;
			previousInput = nextInput;
			//Debug.Log (commands);
		}
	}


	//checkpoint if the hyper1 has been executed at this frame
	void hyper1Check ()
	{

		//person was in the air at the time, hyper1 input doesn't count
		if ( Equals (commands, Sdrive[ 0, 0 ] ) && facingLeft == false && groundCheck == false || 
		    Equals (commands, Sdrive[ 0, 0 ] ) && facingLeft == false && groundCheck == false ) 
		{
			nextInput = "X";
			buttonRegister ();
		}
		else if(Equals (commands, Sdrive[ 0, 0 ] ) && facingLeft == true && groundCheck == true || 
		        Equals (commands, Sdrive[ 0, 0 ] ) && facingLeft == true && groundCheck == true )		
		{
			char placeHolder3 = commands[ 5 ];
			nextInput = "X";
			buttonRegister ();
		}

		//all conditions for hyper1 has been met
		if ( Equals (commands, Sdrive[ 0, 0 ] ) && facingLeft == false && groundCheck == true || 
		    Equals (commands, Sdrive[ 1, 0 ] ) && facingLeft == false && groundCheck == true ) 
		{
			SD1trigger = true;
			return;
			
		}
		else if(Equals (commands, Sdrive[ 2, 0 ] ) && facingLeft == true && groundCheck == true || 
		        Equals (commands, Sdrive[ 3, 0 ] ) && facingLeft == true && groundCheck == true )
		{
			SD1trigger = true;
			return;
		}
	}

	//all memory from player control and status is wiped after being hurt and is recovering
	void flush()
	{
		drive1Frames = 0;
		stellarDriveFrames = 0;
		commands = "XXXXXXX";
		previousInput = "";
		nextInput = "";
		beingHurt = false;
		normalFrames = 0;
		airLock = false;
		hitFrames = 0;

	}

	void modifyHealth(int deltaHealth)
	{
		if (health > 0)
		{
			health += deltaHealth;
		}
	}
	
	int getHealth()
	{
		return this.health;
	}

	int getMaxHealth()
	{
		return this.maxHealth;
	}


	void minusHealth( int currentDamage )
	{
		health = health - currentDamage;
		//Debug.Log (health);
	}

	public void minusOther()
	{
		health = health - 1;
		Debug.Log (health + " " + debugString);
	}

	public void standingNormal()
	{
		if ( currentInput == "A" )
		{
			//standing light punch executed
			normalFrames = attackValues[ 0, 0 ];
			startNormal = attackValues[ 1, 0 ];
			finishNormal = attackValues[ 2, 0 ];
			hurtX = attackValues[ 3, 0 ];
			hurtY = attackValues[ 4, 0 ];
			currentDamage = attackValues[ 5, 0 ];
			damageType = attackValues[ 6, 0 ];
			LPtrigger = true;
		}
		else if ( currentInput == "B" )
		{
			//standing heavy punch executed
			normalFrames = attackValues[ 0, 1 ];
			startNormal = attackValues[ 1, 1 ];
			finishNormal = attackValues[ 2, 1 ];
			hurtX = attackValues[ 3, 1 ];
			hurtY = attackValues[ 4, 1 ];
			currentDamage = attackValues[ 5, 1 ];
			damageType = attackValues[ 6, 1 ];
			HPtrigger = true;
		}
		else if ( currentInput == "C" )
		{
			//standing light kick executed
			normalFrames = attackValues[ 0, 2 ];
			startNormal = attackValues[ 1, 2 ];
			finishNormal = attackValues[ 2, 2 ];
			hurtX = attackValues[ 3, 2 ];
			hurtY = attackValues[ 4, 2 ];
			currentDamage = attackValues[ 5, 2 ];
			damageType = attackValues[ 6, 2 ];
			LKtrigger = true;
		}
		else if ( currentInput == "D" )
		{
			//standing heavy kick executed
			normalFrames = attackValues[ 0, 3 ];
			startNormal = attackValues[ 1, 3 ];
			finishNormal = attackValues[ 2, 3 ];
			hurtX = attackValues[ 3, 3 ];
			hurtY = attackValues[ 4, 3 ];
			currentDamage = attackValues[ 5, 3 ];
			damageType = attackValues[ 6, 3 ];
			HKtrigger = true;
		}
	}

	public void crouchingNormal()
	{
		if ( currentInput == "A" )
		{
			//crouching light punch executed
			normalFrames = attackValues[ 0, 8 ];
			startNormal = attackValues[ 1, 8 ];
			finishNormal = attackValues[ 2, 8 ];
			hurtX = attackValues[ 3, 8 ];
			hurtY = attackValues[ 4, 8 ];
			currentDamage = attackValues[ 5, 8 ];
			damageType = attackValues[ 6, 8 ];
			LPtrigger = true;
		}
		else if ( currentInput == "B" )
		{
			//crouching heavy punch executed
			normalFrames = attackValues[ 0, 9 ];
			startNormal = attackValues[ 1, 9 ];
			finishNormal = attackValues[ 2, 9 ];
			hurtX = attackValues[ 3, 9 ];
			hurtY = attackValues[ 4, 9 ];
			currentDamage = attackValues[ 5, 9 ];
			damageType = attackValues[ 6, 9 ];
			HPtrigger = true;
		}
		else if ( currentInput == "C" )
		{
			//crouching light kick executed
			normalFrames = attackValues[ 0, 10 ];
			startNormal = attackValues[ 1, 10 ];
			finishNormal = attackValues[ 2, 10 ];
			hurtX = attackValues[ 3, 10 ];
			hurtY = attackValues[ 4, 10 ];
			currentDamage = attackValues[ 5, 10 ];
			damageType = attackValues[ 6, 10 ];
			LKtrigger = true;
		}
		else if ( currentInput == "D" )
		{
			//crouching heavy kick executed
			normalFrames = attackValues[ 0, 11 ];
			startNormal = attackValues[ 1, 11 ];
			finishNormal = attackValues[ 2, 11 ];
			hurtX = attackValues[ 3, 11 ];
			hurtY = attackValues[ 4, 11 ];
			currentDamage = attackValues[ 5, 11 ];
			damageType = attackValues[ 6, 11 ];
			HKtrigger = true;
		}
	}

	public void airNormal()
	{
		if ( currentInput == "A" )
		{
			//air light punch executed
			normalFrames = attackValues[ 0, 4 ];
			startNormal = attackValues[ 1, 4 ];
			finishNormal = attackValues[ 2, 4 ];
			hurtX = attackValues[ 3, 4 ];
			hurtY = attackValues[ 4, 4 ];
			currentDamage = attackValues[ 5, 4 ];
			damageType = attackValues[ 6, 4 ];
			LPtrigger = true;
			airLock = true;
		}
		else if ( currentInput == "B" )
		{
			//air heavy punch executed
			normalFrames = attackValues[ 0, 5 ];
			startNormal = attackValues[ 1, 5 ];
			finishNormal = attackValues[ 2, 5 ];
			hurtX = attackValues[ 3, 5 ];
			hurtY = attackValues[ 4, 5 ];
			currentDamage = attackValues[ 5, 5 ];
			damageType = attackValues[ 6, 5 ];
			HPtrigger = true;
			airLock = true;
		}
		else if ( currentInput == "C" )
		{
			//air light kick executed
			normalFrames = attackValues[ 0, 6 ];
			startNormal = attackValues[ 1, 6 ];
			finishNormal = attackValues[ 2, 6 ];
			hurtX = attackValues[ 3, 6 ];
			hurtY = attackValues[ 4, 6 ];
			currentDamage = attackValues[ 5, 6 ];
			damageType = attackValues[ 6, 6 ];
			LKtrigger = true;
			airLock = true;
		}
		else if ( currentInput == "D" )
		{
			//air heavy kick executed
			normalFrames = attackValues[ 0, 7 ];
			startNormal = attackValues[ 1, 7 ];
			finishNormal = attackValues[ 2, 7 ];
			hurtX = attackValues[ 3, 7 ];
			hurtY = attackValues[ 4, 7 ];
			currentDamage = attackValues[ 5, 7 ];
			damageType = attackValues[ 6, 7 ];
			HKtrigger = true;
			airLock = true;
		}
	}

	public void normalReset()
	{
		LPtrigger = false;		HPtrigger = false;		LKtrigger = false;		HKtrigger = false;
	}

	public void buttonRegistration()
	{
		if (gameObject.GetComponent<EnemyAI>() == null)
		{
			moveX = Input.GetAxis ( moveXgrabber );
			moveY = Input.GetAxis ( moveYgrabber );
		}
		nextInput = buttonListener ();
		buttonRegister ();
	}

	//determines if the player was hit or not on the ground $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
	public void hurtWhileStanding()
	{
		//damageTypeRecieved values key
		//0 = hurts upper body only, crouching opponent means it completely misses
		//1 = hurts lower body only, crouch blocking only successful
		//2 = hurts either upper or lower, any blocking will do
		//3 = hurts overhead, only standing blocking will work
		//4 = unblockable
		//5 = trip, hurts lower body only, crouch blocking only successful
		if( beingHurt == true )
		{
			//triping
			if( damageTypeRecieved == 5 )
			{
				if( blockCheck == true && crouchCheck == true )
				{
					//blocked
					blockCrouchingLock = true;
					minusHealth( chipDamage );
					normalForce();
					frameSetting();
				}
				//damaged and tripped
				else
				{
					normalForce();
					trippedCountdown = trippedAmount;
					minusHealth( damageAmountRecieved );
					beingTripped = true;
					beingComboed++;
				}
			}
			//unblockable
			else if( damageTypeRecieved == 4 )
			{
				normalForce();
				hurtLockSetting();
				minusHealth( damageAmountRecieved );
				frameSetting();
				beingComboed++;
			}
			//hurts overhead, only standing blocking will work
			//usually meant for air attacks
			else if( damageTypeRecieved == 3 )
			{
				if( blockCheck == true && crouchCheck == false )
				{
					//blocked
					blockStandingLock = true;
					minusHealth( chipDamage );
					normalForce();
					frameSetting();
				}
				else
				{
					normalForce();
					hurtLockSetting();
					minusHealth( damageAmountRecieved );
					frameSetting();
					beingComboed++;
				}
			}
			//hurts either upper or lower, any blocking will do
			else if( damageTypeRecieved  == 2 )
			{
				if( blockCheck == true )
				{
					if( crouchCheck == true )
					{
						blockCrouchingLock = true;
					}
					else
					{
						blockStandingLock = true;
					}
					minusHealth( chipDamage );
					normalForce();
					frameSetting();
				}
				else
				{
					normalForce();
					hurtLockSetting();
					minusHealth( damageAmountRecieved );
					frameSetting();
					beingComboed++;
				}
			}
			//hurts lower body only, crouch blocking only successful
			else if( damageTypeRecieved == 1 )
			{
				if( blockCheck == true && crouchCheck == true )
				{
					//blocked
					blockCrouchingLock = true;
					minusHealth( chipDamage );
					normalForce();
					frameSetting();
				}
				//damaged
				else
				{
					normalForce();
					hurtLockSetting();
					minusHealth( damageAmountRecieved );
					frameSetting();
					beingComboed++;
				}
			}
			//hurts upper body only, crouching from attack means it completely misses
			else if( damageTypeRecieved == 0 )
			{
				//blocked
				if( blockCheck == true && crouchCheck == false )
				{
					blockStandingLock = true;
					minusHealth( chipDamage );
					normalForce();
					frameSetting();
				} 
				//damaged
				else if( crouchCheck == false && blockCheck == false )
				{
					normalForce();
					hurtLockSetting();
					minusHealth( damageAmountRecieved );
					frameSetting();
					beingComboed++;
				}
				//else miss completely
			}
			//rigidbody2D.AddForce (new Vector2 (0, 90000f));  //currently launches into SPAAAAAAAAAAAAACE
		}
	}
	//end hit on ground determination sector $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

	public void blockInput()
	{
		if( enemyX  < transform.position.x && moveX > .01f )
		{
			blockCheck = true;
		}
		else if( enemyX  > transform.position.x && moveX < -.01f )
		{
			blockCheck = true;
		}
		else
		{
			blockCheck = false;
		}
	}

	public void normalCalls()
	{
		//standingLPcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false 
		   && LPtrigger == true && groundCheck == true && crouchCheck == false )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitSLP( hurtX, hurtY, currentDamage, damageType );
		}
		//standingHPcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false 
		   && HPtrigger == true && groundCheck == true && crouchCheck == false )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitSHP( hurtX, hurtY, currentDamage, damageType );
		}
		//standingLKcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false 
		   && LKtrigger == true && groundCheck == true && crouchCheck == false )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitSLK( hurtX, hurtY, currentDamage, damageType );
		}
		//standingHKcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false 
		   && HKtrigger == true && groundCheck == true && crouchCheck == false )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitSHK( hurtX, hurtY, currentDamage, damageType );
		}
		//crouchingLPcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false 
		   && LPtrigger == true && groundCheck == true && crouchCheck == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitCLP( hurtX, hurtY, currentDamage, damageType );
		}
		//crouchingHPcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false 
		   && HPtrigger == true && groundCheck == true && crouchCheck == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitCHP( hurtX, hurtY, currentDamage, damageType );
		}
		//crouchingLKcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false 
		   && LKtrigger == true && groundCheck == true && crouchCheck == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitCLK( hurtX, hurtY, currentDamage, damageType );
		}
		//crouchingHKcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false 
		   && HKtrigger == true && groundCheck == true && crouchCheck == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitCHK( hurtX, hurtY, currentDamage, damageType );
		}
		normalFrames--;
	}

	public void jumpCall()
	{
		if ( groundCheck == true && moveY  > 0 ) 
		{
			rigidbody2D.AddForce (new Vector2 (0, jumpForce));
		}
	}

	public void walking()
	{
		if( crouchCheck == false ) 
		{
			rigidbody2D.velocity = new Vector2 ( moveX * maxspeed, rigidbody2D.velocity.y );
		}
	}


	//hitboxes--------------------------------------------------------------------------------------------------------
	//when a hitbox successfully touches a player
	void OnTriggerStay2D ( Collider2D other )
	{
		//standing values
		if ( other.gameObject.tag == "LowPunchStanding" )
		{
			SLP = true;
		}
		if ( other.gameObject.tag == "LowKickStanding" )
		{
			SLK = true;
		}
		if ( other.gameObject.tag == "HardPunchStanding" )
		{
			SHP = true;
		}
		if ( other.gameObject.tag == "HardKickStanding" )
		{
			SHK = true;
		}

		//crouching values
		if ( other.gameObject.tag == "LowPunchCrouch" )
		{
			CLP = true;
		}
		if ( other.gameObject.tag == "LowKickCrouch" )
		{
			CLK = true;
		}
		if ( other.gameObject.tag == "HardPunchCrouch" )
		{
			CHP = true;
		}
		if ( other.gameObject.tag == "HardKickCrouch" )
		{
			CHK = true;
		}

		//air values
		if ( other.gameObject.tag == "LowPunchAir" )
		{
			ALP = true;
		}
		if ( other.gameObject.tag == "LowKickAir" )
		{
			ALK = true;
		}
		if ( other.gameObject.tag == "HardPunchAir" )
		{
			AHP = true;
		}
		if ( other.gameObject.tag == "HardKickAir" )
		{
			AHK = true;
		}

		//drive values
		if( other.gameObject.tag == "drive1" )
		{
			D1 = true;
		}

		//stellar drive values
		if( other.gameObject.tag == "stellarDrive1" )
		{
			SD1 = true;
		}

		if( other.gameObject.tag == "stellarDrive2" )
		{
			SD2 = true;
		}
	}

	//when a hitbox leaves a player no longer touching
	void OnTriggerExit2D( Collider2D other )
	{
		//standing values
		if ( other.gameObject.tag == "LowPunchStanding" )
		{
			SLP = false;
		}
		if ( other.gameObject.tag == "LowKickStanding" )
		{
			SLK = false;
		}
		if ( other.gameObject.tag == "HardPunchStanding" )
		{
			SHP = false;
		}
		if ( other.gameObject.tag == "HardKickStanding" )
		{
			SHK = false;
		}

		//crouching values
		if ( other.gameObject.tag == "LowPunchCrouch" )
		{
			CLP = false;
		}
		if ( other.gameObject.tag == "LowKickCrouch" )
		{
			CLK = false;
		}
		if ( other.gameObject.tag == "HardPunchCrouch" )
		{
			CHP = false;
		}
		if ( other.gameObject.tag == "HardKickCrouch" )
		{
			CHK = false;
		}
		
		//air values
		if ( other.gameObject.tag == "LowPunchAir" )
		{
			ALP = false;
		}
		if ( other.gameObject.tag == "LowKickAir" )
		{
			ALK = false;
		}
		if ( other.gameObject.tag == "HardPunchAir" )
		{
			AHP = false;
		}
		if ( other.gameObject.tag == "HardKickAir" )
		{
			AHK = false;
		}

		//drive values
		if( other.gameObject.tag == "drive1" )
		{
			D1 = false;
		}
		
		//stellar drive values
		if( other.gameObject.tag == "stellarDrive1" )
		{
			SD1 = false;
		}
		
		if( other.gameObject.tag == "stellarDrive2" )
		{
			SD2 = false;
		}
	}
	//end hitboxes---------------------------------------------------------------------------------------------------




	//amIgettingHit calls********************************************************************************************
	public bool amIgettingHitSLP( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		//standing light punch connected on call
		if( SLP == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;

	}

	public bool amIgettingHitSHP( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		//standing heavy punch connected on call
		if( SHP == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitSLK( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		//standing light kick connected on call
		if( SLK == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitSHK( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		//standing heavy kick connected on call
		if( SHK == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitCLP( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		//crouching light punch connected on call
		if( CLP == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitCHP( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		//crouching heavy punch connected on call
		if( CHP == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitCLK( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		//crouching light kick connected on call
		if( CLK == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitCHK( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		//crouching light kick connected on call
		if( CHK == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitALP( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		if( ALP == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}
	public bool amIgettingHitAHP( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		if( AHP == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}


	public bool amIgettingHitALK( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		if( ALK == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitAHK( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		if( AHK == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}
	public bool amIgettingHitDownBoy( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		if( SD1 == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}

	public bool amIgettingHitYAUA( int sendingHurtX, int sendingHurtY, int damageAmountSent, int damageTypeSent )
	{
		if( SD1 == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			damageTypeRecieved = damageTypeSent;
			return beingHurt = true;
		}
		return false;
	}



	//amIgettingHit ends********************************************************************************************


	//animationCalls &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
	public void animationCalls()
	{
		anim.SetFloat ( velocityXString, moveX ); //telling the animator what 
		//horizontal direction the character is going
		anim.SetFloat ( velocityYString, rigidbody2D.velocity.y ); //telling the animator what 
		//vertical direction the character is going
		anim.SetInteger ( normalFramesString, normalFrames );
		anim.SetBool ( LPStringTrigger, LPtrigger );
		anim.SetBool ( HPStringTrigger, HPtrigger );
		anim.SetBool ( LKStringTrigger, LKtrigger );
		anim.SetBool ( HKStringTrigger, HKtrigger );
		anim.SetBool ( groundString, groundCheck );
		anim.SetBool ( blockingString, blockCheck );
		anim.SetBool ( facingLeftstring, facingLeft );  //telling the animator if facing left or not
		anim.SetBool ( crouchingString, crouchCheck ); //telling the animator if crouching or not
		anim.SetBool ( hurtLockStringLight, hurtLockLight );
		anim.SetBool ( hurtLockStringHeavy, hurtLockHeavy );
		anim.SetInteger ( hitFramesString, hitFrames );
		anim.SetBool ( blockLockStandingString, blockStandingLock );
		anim.SetBool ( blockLockCrouchingString, blockCrouchingLock );
		anim.SetInteger ( gettingUpString, countDown );
		anim.SetInteger ( gettingTrippedString, trippedCountdown );
		anim.SetBool ( owAirTriggerString, owAirTrigger );
		anim.SetBool ( alreadyAirString, alreadyAir );
		anim.SetBool ( D1triggerString, D1trigger  );
		anim.SetBool ( SD1triggerString, SD1trigger  );
		anim.SetBool ( SD2triggerString, SD2trigger );
		anim.SetInteger ( driveFramesString, drive1Frames );
		anim.SetInteger ( stellarDriveFramesString, stellarDriveFrames );
	}
	//animationCalls ends &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

	public void normalForce()
	{
		if( enemyX  < transform.position.x )
		{
			rigidbody2D.AddForce ( new Vector2 ( recieveHurtX , 0 ) );
		}
		else
		{
			rigidbody2D.AddForce ( new Vector2 ( -recieveHurtX , 0 ) );//recieveHurtX
		}
	}

	public void normalForceAir()
	{
		if( enemyX  < transform.position.x )
		{
			rigidbody2D.AddForce ( new Vector2 ( recieveHurtX , recieveHurtY ) );
		}
		else
		{
			rigidbody2D.AddForce ( new Vector2 ( -recieveHurtX , recieveHurtY ) );//recieveHurtX
		}
	}

	public void frameSetting()
	{
		if ( damageAmountRecieved <= damageThreshold )  //how long is the player in block lock
		{
			hitFrames = lightHitFrames;
		}
		else
		{
			hitFrames = heavyHitFrames; 
		}
	}

	public void hurtLockSetting()
	{
		if ( damageAmountRecieved <= damageThreshold ) //how long the player is in hit stun
		{
			hurtLockLight = true; 
			hitFrames = lightHitFrames;
		}
		else
		{
			hurtLockHeavy = true;
			hitFrames = heavyHitFrames;
		}
	}

	//checking if player is hurt in the air ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
	public void hurtWhileAir()
	{
		if( beingHurt == true )
		{
			beingHurt = false;
			owAirTrigger = true;
			normalForceAir();
			minusHealth( damageAmountRecieved );
			beingComboed++;
		}
	}
	//end method of being hurt in the air ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

	public void normalAirCalls()
	{
		//air lp registered
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false && LPtrigger == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitALP( hurtX, hurtY, currentDamage, damageType );
		}

		//air lp registered
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false && HPtrigger == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitAHP( hurtX, hurtY, currentDamage, damageType );
		}

		//air lk registered
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false && LKtrigger == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitALK( hurtX, hurtY, currentDamage, damageType );
		}
		//air hk registered
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false && HKtrigger == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitAHK( hurtX, hurtY, currentDamage, damageType );
		}
		normalFrames--;
	}

	//stellar drive routing instructions 88888888888888888888888888888888888888888888888888888888888888888888888888888888888

	public void didStellarDriveActivate()
	{

		//countDelayHyper
	}

	//end stellar drive routing instructions 8888888888888888888888888888888888888888888888888888888888888888888888888888888

	//stellar drive instructions!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	public void YouAreUnderArrestMethod()
	{

		if( facingLeft == false )
		{
			rigidbody2D.AddForce ( new Vector2 ( 30 , 0 ) );
		}
		else
		{
			rigidbody2D.AddForce ( new Vector2 ( -30 , 0 ) );
		}

		if( stellarDriveFrames % 10 == 0 )
		{
			attack.amIgettingHitYAUA( 200, 0, 20, 2 );
		}
	}

	public void DownBoyMethod()
	{
		if( stellarDriveFrames % 10 == 0 )
		{
			//flipDamage is used here so the target is bounced back and forth
			//within the whip zone
			if( flipDamage == false )
			{
				attack.amIgettingHitDownBoy( 200, 0, 20, 2 );
				flipDamage = true;
			}
			else if ( flipDamage == true )
			{
				attack.amIgettingHitDownBoy( -200, 0, 20, 2 );
				flipDamage = false;
			}
		}
	}

	public void OmniBlastMethod()
	{

	}

	public void OmniBarrageMethod()
	{

	}

	//end drive instructions!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
}

