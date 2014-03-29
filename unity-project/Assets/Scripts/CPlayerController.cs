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
		health = 1000;
		
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
		SLPXforce = 5;			SHPXforce = 10;			SLKXforce = 5;			SHKXforce = 10;
		SLPYforce = 200;		SHPYforce = 300;		SLKYforce = 200;		SHKYforce = 300;
		SLPdamage = 10;			SHPdamage = 20;			SLKdamage = 10;			SHKdamage = 20;
		SLPlow = false;			SHPlow = false;			SLKlow = true;			SHKlow = false;
		
		//normal air frames values
		//ALPtotalFrames;		AHPtotalFrames;		ALKtotalFrames;		AHKtotalFrame;
		//ALPstartFrame;		AHPstartFrame;		ALKstartFrame;		AHKstartFrame;
		//ALPfinishFrame;		AHPfinishFrame;		ALKfinishFrame;		AHKfinishFrame;
		//ALPXforce = 5;		AHPXforce = 10;		ALKXforce = 5;		AHKXforce = 10;
		//ALPYforce = 200;		AHPYforce = 300;	ALKYforce = 200;	AHKYforce = 300;
		//ALPdamage = 10;		AHPdamage = 20;		ALKdamage = 10;		AHKdamage = 20;
		//ALPlow = true;		AHPlow = true;		ALKlow = true;		AHKlow = true;
		
		//normal crouching frames values
		//CLPtotalFrames;		CHPtotalFrames;		CLKtotalFrames;		CHKtotalFrame;
		//CLPstartFrame;		CHPstartFrame;		CLKstartFrame;		CHKstartFrame;
		//CLPfinishFrame;		CHPfinishFrame;		CLKfinishFrame;		CHKfinishFrame;
		//CLPXforce = 5;		CHPXforce = 10;		CLKXforce = 5;		CHKXforce = 10;
		//CLPYforce = 200;		CHPYforce = 300;	CLKYforce = 200;	CHKYforce = 300;
		//CLPdamage = 10;		CHPdamage = 20;		CLKdamage = 10;		CHKdamage = 20;
		//CLPlow = false;		CHPlow = false;		CLKlow = true;		CHKlow = true;

		//normal standing frames values
		//lightPunch				heavyPunch					lightKick					heavyKick
		attackValues[ 0, 0 ] = 6; 	attackValues[ 0, 1 ] = 6;	attackValues[ 0, 2 ] = 6;	attackValues[ 0, 3 ] = 6;   
		attackValues[ 1, 0 ] = 6; 	attackValues[ 1, 1 ] = 6;	attackValues[ 1, 2 ] = 6;	attackValues[ 1, 3 ] = 6;
		attackValues[ 2, 0 ] = 6; 	attackValues[ 2, 1 ] = 6;	attackValues[ 2, 2 ] = 6;	attackValues[ 2, 3 ] = 6;
		attackValues[ 3, 0 ] = 6; 	attackValues[ 3, 1 ] = 6;	attackValues[ 3, 2 ] = 6;	attackValues[ 3, 3 ] = 6;
		attackValues[ 4, 0 ] = 6; 	attackValues[ 4, 1 ] = 6;	attackValues[ 4, 2 ] = 6;	attackValues[ 4, 3 ] = 6;
		attackValues[ 5, 0 ] = 6; 	attackValues[ 5, 1 ] = 6;	attackValues[ 5, 2 ] = 6;	attackValues[ 5, 3 ] = 6;
		
		//normal air frame values
		//lightPunch				heavyPunch					lightKick					heavyKick
		attackValues[ 0, 4 ] = 6; 	attackValues[ 0, 5 ] = 6;	attackValues[ 0, 6 ] = 6;	attackValues[ 0, 7 ] = 6;   
		attackValues[ 1, 4 ] = 6; 	attackValues[ 1, 5 ] = 6;	attackValues[ 1, 6 ] = 6;	attackValues[ 1, 7 ] = 6;
		attackValues[ 2, 4 ] = 6; 	attackValues[ 2, 5 ] = 6;	attackValues[ 2, 6 ] = 6;	attackValues[ 2, 7 ] = 6;
		attackValues[ 3, 4 ] = 6; 	attackValues[ 3, 5 ] = 6;	attackValues[ 3, 6 ] = 6;	attackValues[ 3, 7 ] = 6;
		attackValues[ 4, 4 ] = 6; 	attackValues[ 4, 5 ] = 6;	attackValues[ 4, 6 ] = 6;	attackValues[ 4, 7 ] = 6;
		attackValues[ 5, 4 ] = 6; 	attackValues[ 5, 5 ] = 6;	attackValues[ 5, 6 ] = 6;	attackValues[ 5, 7 ] = 6;
		
		//normal crouching frame values
		//lightPunch				heavyPunch					lightKick					heavyKick
		attackValues[ 0, 8 ] = 6; 	attackValues[ 0, 9 ] = 6;	attackValues[ 0, 10 ] = 6;	attackValues[ 0, 11 ] = 6;   
		attackValues[ 1, 8 ] = 6; 	attackValues[ 1, 9 ] = 6;	attackValues[ 1, 10 ] = 6;	attackValues[ 1, 11 ] = 6;
		attackValues[ 2, 8 ] = 6; 	attackValues[ 2, 9 ] = 6;	attackValues[ 2, 10 ] = 6;	attackValues[ 2, 11 ] = 6;
		attackValues[ 3, 8 ] = 6; 	attackValues[ 3, 9 ] = 6;	attackValues[ 3, 10 ] = 6;	attackValues[ 3, 11 ] = 6;
		attackValues[ 4, 8 ] = 6; 	attackValues[ 4, 9 ] = 6;	attackValues[ 4, 10 ] = 6;	attackValues[ 4, 11 ] = 6;
		attackValues[ 5, 8 ] = 6; 	attackValues[ 5, 9 ] = 6;	attackValues[ 5, 10 ] = 6;	attackValues[ 5, 11 ] = 6;

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