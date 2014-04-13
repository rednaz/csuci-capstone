using UnityEngine;
using System.Collections;

public class CPlayerController : PlayerController 
{
	//initializing for BPlayer1
	void Start () 
	{
		//debug string
		debugString = "Olivia";

		anim = GetComponent<Animator> ();
		//string animation declarations
		//this is so the PlayerController can reference to the animator's variables
		facingLeftstring = "isLeft2";
		velocityXString = "VelocityX2";
		blockingString = "blocking2";
		crouchingString = "crouching2";
		normalFramesString = "normals2";
		LPStringTrigger = "LPtrigger2";
		HPStringTrigger = "HPtrigger2";
		LKStringTrigger = "LKtrigger2";
		HKStringTrigger = "HKtrigger2";
		groundString = "isGrounded2";
		velocityYString = "VelocityY2";
		hurtLockStringLight = "hurtLockLight2";
		hurtLockStringHeavy = "hurtLockHeavy2";
		hitFramesString = "hitFrames2";
		blockLockStandingString = "blockLockStanding2";
		blockLockCrouchingString = "blockLockCrouching2";
		
		//the other player declaration
		OtherPlayer = "BPlayer1"; //this could probably be another call fuction
		//but this is only a 2 character game
		OtherWhere = GameObject.FindGameObjectWithTag(OtherPlayer).transform;
		enemyScript = "BPlayerController";
		
		//player declaration
		maxspeed = 10f;
		
		//input declaration
		moveXgrabber = "Horizontal2";
		moveYgrabber = "Vertical2";
		LPgrabber = "LP2";
		HPgrabber = "HP2";
		LKgrabber = "LK2";
		HKgrabber = "HK2";
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
		SLPtotalFrames = 4;		SHPtotalFrames = 7;		SLKtotalFrames = 4;		SHKtotalFrame = 7;
		SLPstartFrame = 2;		SHPstartFrame = 5;		SLKstartFrame = 2;		SHKstartFrame = 5;
		SLPfinishFrame = 0;		SHPfinishFrame = 3;		SLKfinishFrame = 0;		SHKfinishFrame = 3;
		SLPXforce = 500;		SHPXforce = 500;		SLKXforce = 500;		SHKXforce = 500;
		SLPYforce = 300;		SHPYforce = 600;		SLKYforce = 300;		SHKYforce = 600;
		SLPdamage = 10;			SHPdamage = 20;			SLKdamage = 10;			SHKdamage = 20;
		SLPlow = false;			SHPlow = false;			SLKlow = true;			SHKlow = false;
		
		//normal crouching frames values
		CLPtotalFrames = 8;		CHPtotalFrames = 10;		CLKtotalFrames = 6;		CHKtotalFrame = 12;
		CLPstartFrame = 6;		CHPstartFrame = 8;			CLKstartFrame = 4;		CHKstartFrame = 9;
		CLPfinishFrame = 4;		CHPfinishFrame = 4;			CLKfinishFrame = 2;		CHKfinishFrame = 6;
		CLPXforce = 200;		CHPXforce = 300;			CLKXforce = 100;		CHKXforce = 0;
		CLPYforce = 100;		CHPYforce = 300;			CLKYforce = 0;			CHKYforce = 0;
		CLPdamage = 4;			CHPdamage = 20;				CLKdamage = 8;			CHKdamage = 20;
		CLPlow = false;			CHPlow = false;				CLKlow = true;			CHKlow = true;

		//normal standing frames values
		//lightPunch					heavyPunch						lightKick						heavyKick
		attackValues[ 0, 0 ] = 4; 		attackValues[ 0, 1 ] = 7;		attackValues[ 0, 2 ] = 4;		attackValues[ 0, 3 ] = 7;   	//totalFrames
		attackValues[ 1, 0 ] = 2; 		attackValues[ 1, 1 ] = 5;		attackValues[ 1, 2 ] = 2;		attackValues[ 1, 3 ] = 5;		//startFrame
		attackValues[ 2, 0 ] = 0; 		attackValues[ 2, 1 ] = 3;		attackValues[ 2, 2 ] = 0;		attackValues[ 2, 3 ] = 3;		//finishFrame
		attackValues[ 3, 0 ] = 500; 	attackValues[ 3, 1 ] = 500;		attackValues[ 3, 2 ] = 500;		attackValues[ 3, 3 ] = 500;		//Xforce
		attackValues[ 4, 0 ] = 300; 	attackValues[ 4, 1 ] = 600;		attackValues[ 4, 2 ] = 300;		attackValues[ 4, 3 ] = 600;		//Yforce
		attackValues[ 5, 0 ] = 10; 		attackValues[ 5, 1 ] = 20;		attackValues[ 5, 2 ] = 10;		attackValues[ 5, 3 ] = 20;		//damageAmount
		attackValues[ 6, 0 ] = 0; 		attackValues[ 6, 1 ] = 0;		attackValues[ 6, 2 ] = 1;		attackValues[ 6, 3 ] = 0;		//damageType
		
		//normal air frame values
		//lightPunch				heavyPunch					lightKick					heavyKick
		attackValues[ 0, 4 ] = 6; 	attackValues[ 0, 5 ] = 6;	attackValues[ 0, 6 ] = 6;	attackValues[ 0, 7 ] = 6;   	//totalFrames
		attackValues[ 1, 4 ] = 6; 	attackValues[ 1, 5 ] = 6;	attackValues[ 1, 6 ] = 6;	attackValues[ 1, 7 ] = 6;		//startFrame
		attackValues[ 2, 4 ] = 6; 	attackValues[ 2, 5 ] = 6;	attackValues[ 2, 6 ] = 6;	attackValues[ 2, 7 ] = 6;		//finishFrame
		attackValues[ 3, 4 ] = 6; 	attackValues[ 3, 5 ] = 6;	attackValues[ 3, 6 ] = 6;	attackValues[ 3, 7 ] = 6;		//Xforce
		attackValues[ 4, 4 ] = 6; 	attackValues[ 4, 5 ] = 6;	attackValues[ 4, 6 ] = 6;	attackValues[ 4, 7 ] = 6;		//Yforce
		attackValues[ 5, 4 ] = 6; 	attackValues[ 5, 5 ] = 6;	attackValues[ 5, 6 ] = 6;	attackValues[ 5, 7 ] = 6;		//damageAmount
		attackValues[ 6, 4 ] = 3; 		attackValues[ 6, 5 ] = 3;		attackValues[ 6, 6 ] = 3;		attackValues[ 6, 7 ] = 3;		//damageType
		
		//normal crouching frame values
		//lightPunch					heavyPunch						lightKick						heavyKick
		attackValues[ 0, 8 ] = 8; 		attackValues[ 0, 9 ] = 10;		attackValues[ 0, 10 ] = 6;		attackValues[ 0, 11 ] = 7;   	//totalFrames
		attackValues[ 1, 8 ] = 6; 		attackValues[ 1, 9 ] = 8;		attackValues[ 1, 10 ] = 4;		attackValues[ 1, 11 ] = 5;		//startFrame
		attackValues[ 2, 8 ] = 4; 		attackValues[ 2, 9 ] = 4;		attackValues[ 2, 10 ] = 2;		attackValues[ 2, 11 ] = 3;		//finishFrame
		attackValues[ 3, 8 ] = 200; 	attackValues[ 3, 9 ] = 300;		attackValues[ 3, 10 ] = 100;	attackValues[ 3, 11 ] = 0;		//Xforce
		attackValues[ 4, 8 ] = 100; 	attackValues[ 4, 9 ] = 300;		attackValues[ 4, 10 ] = 0;		attackValues[ 4, 11 ] = 0;		//Yforce
		attackValues[ 5, 8 ] = 6; 		attackValues[ 5, 9 ] = 6;		attackValues[ 5, 10 ] = 8;		attackValues[ 5, 11 ] = 20;		//damageAmount
		attackValues[ 6, 8 ] = 2;	 	attackValues[ 6, 9 ] = 2;		attackValues[ 6, 10 ] = 1;		attackValues[ 6, 11 ] = 5;		//damageType

		damageThreshold = 10; //determines if the hit recieved is soft or hard hit
		lightHitFrames = 15;  //stun lasts this long when ground damage is equal or less than damageThreshold
		heavyHitFrames = 25;  //stun lasts this long when ground damage is greater than damageThreshold
		
		//note: startFrame is when the attack calls actually start happening in the
		//      normal attack animation, finishFrame is when the attack calls stop
		//      happening.  atariDesu is called to stop attack calls once
		//      the individual normal lands
		
		//GamePad.GetButtonDown(GamePad.Button.A, 1);
	}
}