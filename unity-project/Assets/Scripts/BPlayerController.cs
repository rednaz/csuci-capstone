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

		//*****normal data declaration*****
		//normal standing frames values
		SLPtotalFrames = 6;		SHPtotalFrames = 10;		SLKtotalFrames = 6;		SHKtotalFrame = 12;
		SLPstartFrame = 4;		SHPstartFrame = 7;			SLKstartFrame = 4;		SHKstartFrame = 9;
		SLPfinishFrame = 2;		SHPfinishFrame = 4;			SLKfinishFrame = 2;		SHKfinishFrame = 6;
		SLPXforce = 5;			SHPXforce = 10;				SLKXforce = 5;			SHKXforce = 10;
		SLPYforce = 200;		SHPYforce = 300;			SLKYforce = 200;		SHKYforce = 300;
		
		//normal air frames values
		//ALPtotalFrames;		AHPtotalFrames;		ALKtotalFrames;		AHKtotalFrame;
		//ALPstartFrame;		AHPstartFrame;		ALKstartFrame;		AHKstartFrame;
		//ALPfinishFrame;		AHPfinishFrame;		ALKfinishFrame;		AHKfinishFrame;
		
		//normal crouching frames values
		//CLPtotalFrames;		CHPtotalFrames;		CLKtotalFrames;		CHKtotalFrame;
		//CLPstartFrame;		CHPstartFrame;		CLKstartFrame;		CHKstartFrame;
		//CLPfinishFrame;		CHPfinishFrame;		CLKfinishFrame;		CHKfinishFrame;

		//note: startFrame is when the attack calls actually start happening in the
		//      normal attack animation, finishFrame is when the attack calls stop
		//      happening.  atariDesu is called to stop attack calls once
		//      the individual normal lands

	}
}
