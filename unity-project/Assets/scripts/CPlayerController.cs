using UnityEngine;
using System.Collections;

public class CPlayerController : PlayerControl
{
	Transform Bwhere;

	//initializing for CPlayer2
	void Start () 
	{
		//the other player declaration
		OtherPlayer = "BPlayer1"; //this could probably be another call fuction
									//but this is only a 2 character game
		OtherWhere = GameObject.FindGameObjectWithTag(OtherPlayer).transform;

		//player declaration
		maxspeed = 15f;

		//input declaration
		moveXgrabber = "Horizontal2";
		moveYgrabber = "Vertical2";
		LPgrabber = "LP2";
		HPgrabber = "HP2";
		LKgrabber = "LK2";
		HKgrabber = "HK2";
		health = 1000;

		hyper1Aleft = "214214A";
		hyper1Bleft = "214214B";
		hyper1Aright = "236236A";
		hyper1Bright = "236236B";
		hyper1delay = 10;
	}
}
