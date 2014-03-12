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
		
		
		//hyper declaration one
		hyper1Aleft = "214214A";
		hyper1Bleft = "214214B";
		hyper1Aright = "236236A";
		hyper1Bright = "236236B";
		hyper1delay = 100;
		
		//*****normal data declaration*****
		//normal standing frames values
		SLPtotalFrames = 4;		//SHPtotalFrames;		SLKtotalFrames;		SHKtotalFrame;
		SLPstartFrame = 2;		//SHPstartFrame;		SLKstartFrame;		SHKstartFrame;
		SLPfinishFrame = 0;		//SHPfinishFrame;		SLKfinishFrame;		SHKfinishFrame;
		
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
		
		//GamePad.GetButtonDown(GamePad.Button.A, 1);
	}
}