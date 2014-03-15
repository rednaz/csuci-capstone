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
		
		//note: startFrame is when the attack calls actually start happening in the
		//      normal attack animation, finishFrame is when the attack calls stop
		//      happening.  atariDesu is called to stop attack calls once
		//      the individual normal lands
		
		//GamePad.GetButtonDown(GamePad.Button.A, 1);
	}
}