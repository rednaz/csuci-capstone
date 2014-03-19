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
		SLPtotalFrames = 6;		SHPtotalFrames = 10;		SLKtotalFrames = 6;		SHKtotalFrame = 12;
		SLPstartFrame = 4;		SHPstartFrame = 7;			SLKstartFrame = 4;		SHKstartFrame = 9;
		SLPfinishFrame = 2;		SHPfinishFrame = 4;			SLKfinishFrame = 2;		SHKfinishFrame = 6;
		SLPXforce = 5;			SHPXforce = 10;				SLKXforce = 5;			SHKXforce = 10;
		SLPYforce = 200;		SHPYforce = 300;			SLKYforce = 200;		SHKYforce = 300;
		SLPdamage = 10;			SHPdamage = 20;				SLKdamage = 10;			SHKdamage = 20;
		SLPlow = false;			SHPlow = false;				SLKlow = true;			SHKlow = false;
		
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

		damageThreshold = 10; //determines if the hit recieved is soft or hard hit
		lightHitFrames = 15;  //stun lasts this long when ground damage is equal or less than damageThreshold
		heavyHitFrames = 25;  //stun lasts this long when ground damage is greater than damageThreshold

		//note: startFrame is when the attack calls actually start happening in the
		//      normal attack animation, finishFrame is when the attack calls stop
		//      happening.  atariDesu is called to stop attack calls once
		//      the individual normal lands

	}
}
