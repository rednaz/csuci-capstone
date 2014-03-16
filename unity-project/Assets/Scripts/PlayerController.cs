using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public string enemyScript;
	public PlayerController attack;
	
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

	
	//maxSpeed declaration
	public float maxspeed;

	//opposing player declaration
	public string OtherPlayer; //declaring who the enemy is
	public Transform OtherWhere; //spying on the enemy position
	public float enemyX; //grabbing the exact position of the enemy at the exact frame

	//button pushes: only one button can be registered in the system at a time per frame
	//turns true when the attack is in range
	//standard attacks
	public bool SLP = false;		public bool ALP = false;		public bool CLP = false;
	public bool	SHP = false;		public bool AHP = false;		public bool CHP = false;
	public bool SLK = false;		public bool ALK = false;		public bool CLK = false;
	public bool SHK = false;		public bool AHK = false;		public bool CHK = false;
	//note: S = standing, A = air, C = crouching

	//normal frames declaration
	public int normalFrames = 0; //how many frames total the normal lasts
	public int startNormal; //when the frame actually starts damage
	public int finishNormal; //the last frame of damage call
	public bool atariDesu;  //when active, the finishNormal doesn't matter anymore
	//note: a normal once landed is called off from anymore calls to
	//      avoid multiple hits of the same individual normal

	//normal standing frames values
	public int SLPtotalFrames;		public int SHPtotalFrames;		public int SLKtotalFrames;		public int SHKtotalFrame;
	public int SLPstartFrame;		public int SHPstartFrame;		public int SLKstartFrame;		public int SHKstartFrame;
	public int SLPfinishFrame;		public int SHPfinishFrame;		public int SLKfinishFrame;		public int SHKfinishFrame;
	public int SLPXforce;			public int SHPXforce;			public int SLKXforce;			public int SHKXforce;
	public int SLPYforce;			public int SHPYforce;			public int SLKYforce;			public int SHKYforce;
	public int SLPdamage;			public int SHPdamage;			public int SLKdamage;			public int SHKdamage;
	public bool SLPlow;				public bool SHPlow;				public bool SLKlow;				public bool SHKlow;

	//normal air frames values
	public int ALPtotalFrames;		public int AHPtotalFrames;		public int ALKtotalFrames;		public int AHKtotalFrame;
	public int ALPstartFrame;		public int AHPstartFrame;		public int ALKstartFrame;		public int AHKstartFrame;
	public int ALPfinishFrame;		public int AHPfinishFrame;		public int ALKfinishFrame;		public int AHKfinishFrame;
	public int ALPXforce;			public int AHPXforce;			public int ALKXforce;			public int AHKXforce;
	public int ALPYforce;			public int AHPYforce;			public int ALKYforce;			public int AHKYforce;
	public int ALPdamage;			public int AHPdamage;			public int ALKdamage;			public int AHKdamage;
	public bool ALPlow;				public bool AHPlow;				public bool ALKlow;				public bool AHKlow;

	//normal crouching frames values
	public int CLPtotalFrames;		public int CHPtotalFrames;		public int CLKtotalFrames;		public int CHKtotalFrame;
	public int CLPstartFrame;		public int CHPstartFrame;		public int CLKstartFrame;		public int CHKstartFrame;
	public int CLPfinishFrame;		public int CHPfinishFrame;		public int CLKfinishFrame;		public int CHKfinishFrame;
	public int CLPXforce;			public int CHPXforce;			public int CLKXforce;			public int CHKXforce;
	public int CLPYforce;			public int CHPYforce;			public int CLKYforce;			public int CHKYforce;
	public int CLPdamage;			public int CHPdamage;			public int CLKdamage;			public int CHKdamage;
	public bool CLPlow;				public bool CHPlow;				public bool CLKlow;				public bool CHKlow;

	//normal triggers
	public bool LPtrigger = false;		public bool HPtrigger = false;		public bool LKtrigger = false;		public bool HKtrigger = false;

	//button declarations
	public string LPgrabber;
	public string HPgrabber;
	public string LKgrabber;
	public string HKgrabber;

	//crouching or not
	public bool crouchCheck = false;

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

	//declaration of up force for jumping
	public float jumpForce;

	//countDown is needed when the player is recovering and getting up
	//a value of 0 means the player is up and ready for more punishment
	public int countDown = 0;

	//countDelayHyper is needed when the player is in a state of action
	//this prevents the spamming of an attack per frame
	public int countDelayHyper = 0;

	//countDelayHyper is needed when the player is in a state of action
	//this prevents the spamming of an attack per frame
	public int countDelaySuper = 0;

	//declaring hyper variable
	public int hyperMeter = 300;
	//100 = level 1
	//200 = level 2
	//300 = level 3
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

	//super and hyper locks
	//can't execute normals when super is locked
	//can't execute supers when hyper is locked
	public bool superLock = false;
	public bool hyperLock = false;

	//super 1 declarations
	public string super1Aleft;
	public string super1Bleft;
	public string super1Aright;
	public string super1Bright;
	public int super1delay;


	public string super2;

	//hyper 1 declarations
	public string hyper1Aleft;   //these
	public string hyper1Bleft;   //recognize
	public string hyper1Aright;  //input
	public string hyper1Bright;  //patterns
	public int hyper1delay;	 //how many frames the hyper takes to execute
	public int hyper1eat; //how much meter the hyper takes

	//hyper 2 declarations
	public string hyper2Aleft;	 //these
	public string hyper2Bleft;   //recognize
	public string hyper2Aright;  //input
	public string hyper2Bright;  //patterns
	public int hyper2delay;	//how many frames the hyper takes to execute
	public int hyper2eat; //how much meter the hyper takes

	//hyper 3 declarations
	public string hyper3Aleft;  //these
	public string hyper3Bleft;  //recognize
	public string hyper3Aright; //input
	public string hyper3Bright; //patterns
	public int hyper3delay; //how many frames the hyper takes to execute
	public int hyper3eat; //how much meter the hyper takes

	//combo declaration
	int beingComboed = 0;

	//KO declaration (knocked out)
	bool KO = false;

	//player health declaration
	public int health;
	public GUIText healthUI;

	//player being hurt
	public bool beingHurt = false;
	public bool beingHurtLow;
	public int recieveHurtX;
	public int recieveHurtY;
	public int damageAmountRecieved;
	public bool recievedAttackLow = false;  //true if the attack is low
	public int chipDamage;

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

	//called every frame
	//the heart of all actions-------------------------------------------------------
	void FixedUpdate () 
	{
		//this line checks if the character is on the ground at this frame
		groundCheck = Physics2D.OverlapCircle (daGround.position, groundRadius, whatIsGround);


		//this is nothing but debug code, feel free to uncomment at your pleasure to
		//see game activity
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			Debug.Log ( debugString + " health: " + health );
		}
		//Debug.Log ( debugString + " " + LP);
		//if ( Input.GetButtonDown( LPgrabber ) )
		//{
		//	Debug.Log ( debugString + " check" );
		//}
		//Debug.Log ( debugString + " " + Input.GetAxis ( moveXgrabber ) );
		//Debug.Log (health);

		//setting up the animation for the frame
		animationCalls ();


		//Phase 0 
		//Player is hit and got to 0 health, player falls to ground
		//Control is relinquished from the player here
		if ( health < 1 )
		{
			KO = true;
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
			return;
		} 
		else if ( countDown == 1 )
		{
			countDown--;
			flush ();
		}


		//Player is in the air and no control, being hurt
		//Set countDown from the hit

		//if( groundCheck == false && beingHurt == true )
		//{
		//	return;
		//}


		//Phase 2
		//Player is on the ground and no control, being hurt
		//Set countDown from the hit
		if( groundCheck == true )
		{
			hurtWhileStanding ();
		}
		//turn off beingHurt when the player recovers from the previous ground hit
		if ( beingHurt == true )
		{
			return;
		}


		//Phase 3
		//Player is on the ground, no control, and recovering from hurt (getting up)
		//Flush the status here when countDown reaches 0
		/*
		if ( countDown > 1 && groundCheck == true ) 
		{
			countDown--;
			return;
		} 
		else if ( countDown == 1 )
		{
			countDown--;
			flush ();
		}
		*/


		//Phase 4
		//Player is in the middle of a super/hyper and cannot move till
		//the move completes, prevents spamming of supers/hypers
		/*
		if ( countDelayHyper > 1 ) 
		{
			countDelayHyper--;
			return;
		}
		else if( countDelayHyper == 1 ) 
		{
			countDelayHyper--;
			flush ();
		}
		if ( countDelaySuper > 1 ) 
		{
			countDelaySuper--;
			return;
		}
		else if( countDelaySuper == 1 ) 
		{
			countDelaySuper--;
			flush ();
		}
		*/


		//Phase 5
		//Player is in the air, left and right control not possible
		//Only one normal air attack in the air is possible
		//One super and hyper and only one of each are possible if allows
		//Blocking isn't possible in the air
		//This the starting Phase where player can put input into the system to be registered
		buttonRegistration ();


		//implemented when jumping is put back in
		//if ( airLock == false )
		//{
		//	airNormal ();
		if ( groundCheck == false )
		{
			return;
		}

			
		//Phase 6
		//Player is on the ground and attacking, left and right control not possible
		//Super and hyper are possible here
		//Jumping is not possible
		//Blocking isn't possible here


		//Phase 7
		//player successfully implemented a hyper, game executes hyper
		//hyper 1, 2, and 3 checked if entered at this moment
		//hyper1Check ();



		//Phase 8
		//player successfully implemented a super, game executes super
		//super 1 and 2 checked if entered a this moment


		//Phase 9 (normals being executed)
		//normalFramesString

		if( normalFrames > 0 && groundCheck == true )
		{
			normalCalls();
			return;
		}
		normalReset ();
		atariDesu = false;


		//Phase 10 (crouching)




		//Phase 11 (crouching normals)
		if ( crouchCheck == true )
		{
			crouchingNormal();
		}


		//Phase 12 (normals listening)
		if ( crouchCheck == false )
		{
			standingNormal();
		}
		if ( normalFrames > 0 )
		{
			return;
		}



		//Phase 13 (walking and flipping)
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

		//Phase 14 (walking)
		walking ();
		determineFlip ();

		//Phase 14 (jumping)
		//code to jump
		jumpCall ();

		//Phase 14 (blocking)
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
		else if( moveX < - 0.5f && moveY > .5f ) //down + left 
		{
			currentInput = "1";
		}
		else if( moveX > .5f && moveY > .5f ) //down + right
		{
			currentInput = "3";
		}
		else if ( moveY < - 0.5f ) // up (this pretty much cancels out anything since nothing requires up)
		{
			currentInput = "8";
		}
		else if ( moveY > .5f ) //down
		{
			currentInput = "2";
		}
		else if( moveX < - 0.5f ) //left
		{
			currentInput = "4";
		}
		else if( moveX > .5f ) //right
		{
			currentInput = "6";
		}
		else //no input
		{
			currentInput = "X";
		}
		//Debug.Log (currentInput + " " + debugString);
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
			//Debug.Log (commands);
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
		if ( Equals (commands, hyper1Aleft ) && facingLeft == false && groundCheck == false || 
		    Equals (commands, hyper1Bleft ) && facingLeft == false && groundCheck == false ) 
		{
			nextInput = "X";
			buttonRegister ();
		}
		else if(Equals (commands, hyper1Aright ) && facingLeft == true && groundCheck == true || 
		        Equals (commands, hyper1Bright ) && facingLeft == true && groundCheck == true )		
		{
			char placeHolder3 = commands[ 5 ];
			nextInput = "X";
			buttonRegister ();
		}

		//all conditions for hyper1 has been met
		if ( Equals (commands, hyper1Aleft ) && facingLeft == false && groundCheck == true || 
		    Equals (commands, hyper1Bleft ) && facingLeft == false && groundCheck == true ) 
		{
			Debug.Log ("HADOUKEN");
			countDelayHyper = hyper1delay;
			flush ();
			return;
			
		}
		else if(Equals (commands, hyper1Aright ) && facingLeft == true && groundCheck == true || 
		        Equals (commands, hyper1Bright ) && facingLeft == true && groundCheck == true )
		{
			Debug.Log ("HADOUKEN");
			countDelayHyper = hyper1delay;
			flush ();
			return;
		}
	}

	//all memory from player control and status is wiped after being hurt and is recovering
	void flush()
	{
		countDelayHyper = 0;
		countDelaySuper = 0;
		commands = "XXXXXXX";
		previousInput = "";
		nextInput = "";
		beingHurt = false;
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
			normalFrames = SLPtotalFrames;
			startNormal = SLPstartFrame;
			finishNormal = SLPfinishFrame;
			hurtX = SLPXforce;
			hurtY = SLPYforce;
			hurtLow = SLPlow;
			currentDamage = SLPdamage;
			LPtrigger = true;
		}
		else if ( currentInput == "B" )
		{
			//standing heavy punch executed
			//normalFrames = HLPtotalFrames;
			//startNormal = HLPstartFrame;
			//finishNormal = HLPfinishFrame;
			HPtrigger = true;
		}
		else if ( currentInput == "C" )
		{
			//standing light kick executed
			normalFrames = SLKtotalFrames;
			startNormal = SLKstartFrame;
			finishNormal = SLKfinishFrame;
			hurtLow = SLKlow;
			LKtrigger = true;
		}
		else if ( currentInput == "D" )
		{
			//standing heavy kick executed
			//normalFrames = SHKtotalFrames;
			//startNormal = SHKstartFrame;
			//finishNormal = SHKfinishFrame;
			HKtrigger = true;
		}

	}

	public void crouchingNormal()
	{
		if ( currentInput == "A" )
		{
			//crouching light punch executed
		}
		else if ( currentInput == "B" )
		{
			//crouching heavy punch executed
		}
		else if ( currentInput == "C" )
		{
			//crouching light kick executed
		}
		else if ( currentInput == "D" )
		{
			//crouching heavy kick executed
		}
	}

	public void airNormal()
	{
		if ( currentInput == "A" )
		{
			//air light punch executed
			airLock = true;
		}
		else if ( currentInput == "B" )
		{
			//air heavy punch executed
			airLock = true;
		}
		else if ( currentInput == "C" )
		{
			//air light kick executed
			airLock = true;
		}
		else if ( currentInput == "D" )
		{
			//air heavy kick executed
			airLock = true;
		}
	}

	public void normalReset()
	{
		LPtrigger = false;		HPtrigger = false;		LKtrigger = false;		HKtrigger = false;
	}




	public void buttonRegistration()
	{
		moveX = Input.GetAxis ( moveXgrabber );
		moveY = Input.GetAxis ( moveYgrabber );
		nextInput = buttonListener ();
		buttonRegister ();
	}

	public void hurtWhileStanding()
	{
		if( beingHurt == true )
		{
			if( enemyX  < transform.position.x )
			{
				rigidbody2D.AddForce ( new Vector2 ( recieveHurtX , 0 ) );
			}
			else
			{
				rigidbody2D.AddForce ( new Vector2 ( -recieveHurtX , 0 ) );//recieveHurtX
			}

			if( blockCheck == true && crouchCheck == beingHurtLow )
			{
				minusHealth( chipDamage );
			}
			else //player isn't blocking, recieve amount of damage
			{
				minusHealth( damageAmountRecieved );
			}
			//rigidbody2D.AddForce (new Vector2 (0, 90000f));  //currently launches into SPAAAAAAAAAAAAACE
		}
	}

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
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false && LPtrigger == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitSLP( hurtX, hurtY, currentDamage, hurtLow );
		}
		//standingLKcalls
		if( normalFrames <= startNormal && normalFrames >= finishNormal && atariDesu == false && LKtrigger == true )
		{
			//send an attack signal
			atariDesu = attack.amIgettingHitSLK( hurtX, hurtY, currentDamage, hurtLow );
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
		if ( other.gameObject.tag == "LowPunchStanding" )
		{
			SLP = true;
		}
		if ( other.gameObject.tag == "LowKickStanding" )
		{
			SLK = true;
		}
	}

	//when a hitbox leaves a player no longer touching
	void OnTriggerExit2D( Collider2D other )
	{
		if ( other.gameObject.tag == "LowPunchStanding" )
		{
			SLP = false;
		}
		if ( other.gameObject.tag == "LowKickStanding" )
		{
			SLK = false;
		}
	}
	//end hitboxes---------------------------------------------------------------------------------------------------




	//amIgettingHit calls********************************************************************************************
	public bool amIgettingHitSLP( int sendingHurtX, int sendingHurtY, int damageAmountSent, bool isHitLow )
	{
		//light punch connected on call
		if( SLP == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			beingHurtLow = isHitLow;
			return beingHurt = true;
		}
		return false;

	}

	public bool amIgettingHitSLK( int sendingHurtX, int sendingHurtY, int damageAmountSent, bool isHitLow )
	{
		//light kick connected on call
		if( SLK == true )
		{
			recieveHurtX = sendingHurtX;
			recieveHurtY = sendingHurtY;
			damageAmountRecieved = damageAmountSent;
			beingHurtLow = isHitLow;
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
	}
	//animationCalls ends &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
}