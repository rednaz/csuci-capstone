using UnityEngine;
using System.Collections;

public class CPlayerController : PlayerController
{
	//initializing for BPlayer1
	void Start () 
	{
		anim = GetComponent<Animator> ();
		//string animation declarations
		//this is so the PlayerController can reference to the animator's variables
		//facingLeftstring = "isLeft2";
		//velocityXString = "VelocityX2";
		
		//the other player declaration
		OtherPlayer = "BPlayer1"; //this could probably be another call fuction
		//but this is only a 2 character game
		OtherWhere = GameObject.FindGameObjectWithTag(OtherPlayer).transform;
		
		//player declaration
		maxspeed = 10f;
		health = 1000;
		
		//input declaration
		moveXgrabber = "Horizontal2";
		//moveYgrabber = "Vertical2";
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
		
		//GamePad.GetButtonDown(GamePad.Button.A, 1);
	}
	
	
}