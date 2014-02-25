﻿using UnityEngine;
using System.Collections;

public class BPlayerController : PlayerControl
{


	//initializing for BPlayer1
	void Start () 
	{
		//the other player declaration
		OtherPlayer = "CPlayer2"; //this could probably be another call fuction
									//but this is only a 2 character game
		OtherWhere = GameObject.FindGameObjectWithTag(OtherPlayer).transform;

		//player declaration
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


		//hyper declaration one
		hyper1Aleft = "214214A";
		hyper1Bleft = "214214B";
		hyper1Aright = "236236A";
		hyper1Bright = "236236B";
		hyper1delay = 100;

		//GamePad.GetButtonDown(GamePad.Button.A, 1);
	}


}
