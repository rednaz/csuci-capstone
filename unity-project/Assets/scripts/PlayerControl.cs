﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	//animator declaration
	Animator anim;

	//maxSpeed declaration
	public float maxspeed;

	//opposing player declaration
	public string OtherPlayer; //declaring who the enemy is
	public Transform OtherWhere; //spying on the enemy position
	public float enemyX; //grabbing the exact position of the enemy at the exact frame

	//button pushes: only one button can be registered in the system at a time per frame
	//turns true when the button is being pressed down, false when button is released
	//standard attacks
	public bool LP = false;		public string LPgrabber;
	public bool	HP = false;		public string HPgrabber;
	public bool LK = false;		public string LKgrabber;
	public bool HK = false;		public string HKgrabber;

	//crouching or not
	public bool crouchCheck = false;

	//blocking or not
	public bool blockCheck = false;

	//checking if player is on the ground
	public bool groundCheck = false;
		
	//needed because only one attack can be done in the air
	//player must land before another air attack can be done again
	public bool airLock = false; 

	//countDown is needed when the player is recovering and getting up
	//a value of 0 means the player is up and ready for more punishment
	public int countDown = 0;

	//countDelayHyper is needed when the player is in a state of action
	//this prevents the spamming of an attack per frame
	public int countDelayHyper = 0;

	//countDelayHyper is needed when the player is in a state of action
	//this prevents the spamming of an attack per frame
	public int countDelaySuper = 0;

	//checking what direction the player is facing at the time of the frame
	//note: player can't turn when in the air
	public bool facingRight = false;

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

	//hyper declarations
	public string hyper1Aleft;
	public string hyper1Bleft;
	public string hyper1Aright;
	public string hyper1Bright;
	public int hyper1delay;

	//combo declaration
	int beingComboed = 0;

	//KO declaration (knocked out)
	bool KO = false;

	//player health declaration
	public int health;

	//player being hurt
	public bool beingHurt = false;

	//called every frame
	//the heart of all actions-------------------------------------------------------
	void FixedUpdate () 
	{
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
		//Player is in the air and no control, being hurt
		//Set countDown from the hit
		if( groundCheck == false && beingHurt == true )
		{
			return;
		}


		//Phase 2
		//Player is on the ground and no control, being hurt
		//Set countDown from the hit
		if( groundCheck == true && beingHurt == true )
		{
			return;
		}


		//Phase 3
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


		//Phase 4
		//Player is in the middle of a super/hyper and cannot move till
		//the move completes, prevents spamming of supers/hypers
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


		//Phase 5
		//Player is in the air, left and right control not possible
		//Only one normal air attack in the air is possible
		//One super and hyper and only one of each are possible if allows
		//Blocking isn't possible here
		moveX = Input.GetAxis ( moveXgrabber );
		moveY = Input.GetAxis ( moveYgrabber );
		nextInput = buttonListener ();
		buttonRegister ();


		//Phase 6
		//Player is on the ground and attacking, left and right control not possible
		//Super and hyper are possible here
		//Jumping is not possible
		//Blocking isn't possible here


		//Phase 7
		//player successfully implemented a hyper, game executes hyper
		//hyper 1, 2, and 3 checked if entered at this moment
		hyper1Check ();



		//Phase 8
		//player successfully implemented a super, game executes super
		//super 1 and 2 checked if entered a this moment


		//Phase 9 (the neutral state)
		//Player is ready to take any commands without interruptions

		//getting other player's position
		enemyX = OtherWhere.position.x;
		//deciding if turning is nessessary on this frame
		if ( enemyX  < transform.position.x && facingRight == false ) 
		{
			Flip ();
		} 
		else if ( enemyX > transform.position.x && facingRight == true ) 
		{
			Flip ();
		}


		//Debug.Log ( moveX + ", " + moveY );
	}
	//end FixedUpdate() -------------------------------------------------------------



	//command called to turn the character around


	void Flip()
	{
		facingRight = !facingRight;
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
		//Debug.Log (currentInput);
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
		if ( Equals (commands, hyper1Aleft ) && facingRight == false && groundCheck == false || 
		    Equals (commands, hyper1Bleft ) && facingRight == false && groundCheck == false ) 
		{
			nextInput = "X";
			buttonRegister ();
		}
		else if(Equals (commands, hyper1Aright ) && facingRight == true && groundCheck == true || 
		        Equals (commands, hyper1Bright ) && facingRight == true && groundCheck == true )		
		{
			char placeHolder3 = commands[ 5 ];
			nextInput = "X";
			buttonRegister ();
		}

		//all conditions for hyper1 has been met
		if ( Equals (commands, hyper1Aleft ) && facingRight == false && groundCheck == true || 
		    Equals (commands, hyper1Bleft ) && facingRight == false && groundCheck == true ) 
		{
			Debug.Log ("HADOUKEN");
			countDelayHyper = hyper1delay;
			flush ();
			return;
			
		}
		else if(Equals (commands, hyper1Aright ) && facingRight == true && groundCheck == true || 
		        Equals (commands, hyper1Bright ) && facingRight == true && groundCheck == true )
		{
			Debug.Log ("HADOUKEN");
			countDelayHyper = hyper1delay;
			flush ();
			return;
		}
	}

	//all memory from player control and status is wiped after being hurt and recovering
	void flush()
	{
		countDelayHyper = 0;
		countDelaySuper = 0;
		commands = "XXXXXXX";
		previousInput = "";
		nextInput = "";
		beingHurt = false;
	}

	/*
	 * if(Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log ("dddddddddddddddd");
		}
	*/
}
