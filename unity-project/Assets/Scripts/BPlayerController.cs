using UnityEngine;
using System.Collections;

public class BPlayerController : PlayerController 
{
	//initializing for BPlayer1
	void Start () 
	{
		//debug string
		debugString = "Barrett";

		anim = GetComponent<Animator> ();
		//string animation declarations
		//this is so the PlayerController can reference to the animator's variables
		facingLeftstring = "isLeft1";
		velocityXString = "VelocityX1";
		blockingString = "blocking1";
		crouchingString = "crouching1";
		normalFramesString = "normals1";
		LPStringTrigger = "LPtrigger1";
		HPStringTrigger = "HPtrigger1";
		LKStringTrigger = "LKtrigger1";
		HKStringTrigger = "HKtrigger1";
		groundString = "isGrounded1";
		velocityYString = "VelocityY1";
		hurtLockStringLight = "hurtLockLight1";
		hurtLockStringHeavy = "hurtLockHeavy1";
		hitFramesString = "hitFrames1";
		blockLockStandingString = "blockLockStanding1";
		blockLockCrouchingString = "blockLockCrouching1";
		gettingUpString = "gettingUp1";
		gettingTrippedString = "gettingTripped1";


		//the other player declaration
		OtherPlayer = "CPlayer2"; //this could probably be another call fuction
		//but this is only a 2 character game
		OtherWhere = GameObject.FindGameObjectWithTag(OtherPlayer).transform;
		enemyScript = "CPlayerController";
		
		//player speed declaration
		maxspeed = 10f;
		
		//input declaration
		moveXgrabber = "Horizontal1";
		moveYgrabber = "Vertical1";
		LPgrabber = "LP1";
		HPgrabber = "HP1";
		LKgrabber = "LK1";
		HKgrabber = "HK1";
		maxHealth = 1000;
		health = maxHealth;
		
		//super 1 declarations
		super1Aleft = "214A";
		super1Bleft = "214B";
		super1Aright = "236A";
		super1Bright = "236B";
		super1delay = 10;
		
		
		//hyper 1 declarations
		hyper1Aleft = "412364A";
		hyper1Bleft = "412364B";
		hyper1Aright = "632146A";
		hyper1Bright = "632146B";
		hyper1delay = 100;
		hyper1eat = 100;

		//hyper 2 declarations
		hyper2Aleft = "XOXOXOX";
		hyper2Bleft = "XOXOXOX";
		hyper2Aright = "XOXOXOX";
		hyper2Bright = "XOXOXOX";
		hyper2delay = 100;
		
		//hyper 3 declarations
		hyper3Aleft = "214214A";
		hyper3Bleft = "214214B";
		hyper3Aright = "236234A";
		hyper3Bright = "236236B";
		hyper3delay = 100;
		hyper3eat = 300;

		//up force
		jumpForce = 1200f;  

		//how much damage to recieve while blocking
		chipDamage = 2;

		//*****normal data declaration*****
		//normal standing frames values
		//lightPunch					heavyPunch						lightKick						heavyKick
		attackValues[ 0, 0 ] = 6; 		attackValues[ 0, 1 ] = 10;		attackValues[ 0, 2 ] = 6;		attackValues[ 0, 3 ] = 12;   	//totalFrames
		attackValues[ 1, 0 ] = 4; 		attackValues[ 1, 1 ] = 6;		attackValues[ 1, 2 ] = 4;		attackValues[ 1, 3 ] = 9;		//startFrame
		attackValues[ 2, 0 ] = 2; 		attackValues[ 2, 1 ] = 4;		attackValues[ 2, 2 ] = 2;		attackValues[ 2, 3 ] = 6;		//finishFrame
		attackValues[ 3, 0 ] = 500; 	attackValues[ 3, 1 ] = 500;		attackValues[ 3, 2 ] = 500;		attackValues[ 3, 3 ] = 500;		//Xforce
		attackValues[ 4, 0 ] = 200; 	attackValues[ 4, 1 ] = 600;		attackValues[ 4, 2 ] = 300;		attackValues[ 4, 3 ] = 600;		//Yforce
		attackValues[ 5, 0 ] = 10; 		attackValues[ 5, 1 ] = 20;		attackValues[ 5, 2 ] = 10;		attackValues[ 5, 3 ] = 20;		//damageAmount
		attackValues[ 6, 0 ] = 0; 		attackValues[ 6, 1 ] = 0;		attackValues[ 6, 2 ] = 1;		attackValues[ 6, 3 ] = 0;		//damageType
		
		//normal air frame values
		//lightPunch					heavyPunch						lightKick						heavyKick
		attackValues[ 0, 4 ] = 6; 		attackValues[ 0, 5 ] = 10;		attackValues[ 0, 6 ] = 6;		attackValues[ 0, 7 ] = 12;   	//totalFrames
		attackValues[ 1, 4 ] = 4; 		attackValues[ 1, 5 ] = 7;		attackValues[ 1, 6 ] = 4;		attackValues[ 1, 7 ] = 8;		//startFrame
		attackValues[ 2, 4 ] = 2; 		attackValues[ 2, 5 ] = 4;		attackValues[ 2, 6 ] = 2;		attackValues[ 2, 7 ] = 4;		//finishFrame
		attackValues[ 3, 4 ] = 500;		attackValues[ 3, 5 ] = 600;		attackValues[ 3, 6 ] = 500;		attackValues[ 3, 7 ] = 700;		//Xforce
		attackValues[ 4, 4 ] = 300;		attackValues[ 4, 5 ] = 400;		attackValues[ 4, 6 ] = 300;		attackValues[ 4, 7 ] = 500;		//Yforce
		attackValues[ 5, 4 ] = 10; 		attackValues[ 5, 5 ] = 20;		attackValues[ 5, 6 ] = 6;		attackValues[ 5, 7 ] = 25;		//damageAmount
		attackValues[ 6, 4 ] = 3; 		attackValues[ 6, 5 ] = 3;		attackValues[ 6, 6 ] = 3;		attackValues[ 6, 7 ] = 3;		//damageType
		
		//normal crouching frame values
		//lightPunch					heavyPunch						lightKick						heavyKick
		attackValues[ 0, 8 ] = 8; 		attackValues[ 0, 9 ] = 10;		attackValues[ 0, 10 ] = 6;		attackValues[ 0, 11 ] = 12;   	//totalFrames
		attackValues[ 1, 8 ] = 6; 		attackValues[ 1, 9 ] = 8;		attackValues[ 1, 10 ] = 4;		attackValues[ 1, 11 ] = 9;		//startFrame
		attackValues[ 2, 8 ] = 4; 		attackValues[ 2, 9 ] = 4;		attackValues[ 2, 10 ] = 2;		attackValues[ 2, 11 ] = 6;		//finishFrame
		attackValues[ 3, 8 ] = 500;		attackValues[ 3, 9 ] = 600;		attackValues[ 3, 10 ] = 400;	attackValues[ 3, 11 ] = 0;		//Xforce
		attackValues[ 4, 8 ] = 300; 	attackValues[ 4, 9 ] = 600;		attackValues[ 4, 10 ] = 400;	attackValues[ 4, 11 ] = 0;		//Yforce
		attackValues[ 5, 8 ] = 8;	 	attackValues[ 5, 9 ] = 15;		attackValues[ 5, 10 ] = 10;		attackValues[ 5, 11 ] = 25;		//damageAmount
		attackValues[ 6, 8 ] = 2;	 	attackValues[ 6, 9 ] = 2;		attackValues[ 6, 10 ] = 1;		attackValues[ 6, 11 ] = 5;		//damageType

		damageThreshold = 10; //determines if the hit recieved is soft or hard hit
		lightHitFrames = 15;  //stun lasts this long when ground damage is equal or less than damageThreshold
		heavyHitFrames = 25;  //stun lasts this long when ground damage is greater than damageThreshold
		trippedAmount = 40;
		countDownSetter = 20;

		//note: startFrame is when the attack calls actually start happening in the
		//      normal attack animation, finishFrame is when the attack calls stop
		//      happening.  atariDesu is called to stop attack calls once
		//      the individual normal lands

	}
}
