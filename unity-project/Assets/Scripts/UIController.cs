using UnityEngine;
using System.Collections;
using System;

public class UIController : MonoBehaviour
{	
	// Instance Variables

	// Players
	private BPlayerController bPlayer;
	private CPlayerController cPlayer;

	// Screen resolution
	private float currentResX, currentResY;

	// Progress Bar textures
	private Texture2D progressBarEmpty, progressBarFull; // These are placeholders for now

	// Health Bar UI
	private Vector2 healthBarSize, bHealthBarPos, cHealthBarPos;

	// Super/Hyper UI
	// IMPLEMENT THIS

	// Timer UI
	private float timeLeft;
	private Vector2 timerSize, timerPos;
	private GUIStyle timerStyle;


	// Fight Finish UI (knockout or time over)
	private Vector2 finishSize, finishPos;
	private GUIStyle finishStyle;

	// Miscellaneous UI
	private bool koOver;
	private bool timeOver;

	// Temporary or testing variables
	private float timer, nextTime;
	public float timeRate;


	void Start ()
	{
		// Save resolution to variables
		currentResX = Screen.width;
		currentResY = Screen.height;

		// Set position and size of health bars, super bars (to be implemented), and timer
		setHealthBars (currentResX, currentResY);
		setTimerGUI (currentResX, currentResY);

		// Initialize progress bar textures
		//progressBarEmpty = new Texture2D(1,1);
		//progressBarFull = new Texture2D(1,1);


		// Initialize player controller objects

		// Attempt to get B Player
		GameObject bPlayerObject = GameObject.FindWithTag ("BPlayer1");
		//Debug.Log ("bPlayerObject value is '" + bPlayerObject + "'");

		if (bPlayerObject != null)
		{
			bPlayer = bPlayerObject.GetComponent <BPlayerController>();
		}
		if (bPlayer == null)
		{
			Debug.Log ("Cannot find 'BPlayerController' script");
		}
		Debug.Log ("B Player initialized with " + bPlayer.health + " / " + bPlayer.maxHealth + " health" );



		// Attempt to get C Player
		GameObject cPlayerObject = GameObject.FindWithTag ("CPlayer2");
		//Debug.Log ("cPlayerObject value is '" + cPlayerObject + "'");

		if (cPlayerObject != null)
		{
			cPlayer = cPlayerObject.GetComponent <CPlayerController>();
		}
		if (cPlayer == null)
		{
			Debug.Log ("Cannot find 'CPlayerController' script");
		}
		Debug.Log ("C Player initialized with " + cPlayer.health + " / " + cPlayer.maxHealth + " health" );


		// Miscellaneous actions taken here (if not dependent on above actions)
		timeOver = false;
		koOver = false;

		// Temporary actions to be implemented properly later (THIS SHOULD BE EMPTY UPON COMPLETION OF THE PROJECT)
		timer = 100.0f;
		nextTime = Time.time + timeRate;
	}


	void OnGUI ()
	{
		// Adjust the health bar size and positions if the screen resolution changes
		if (screenResChange (currentResX, currentResY))
		{
			currentResX = Screen.width;
			currentResY = Screen.height;
			setHealthBars (currentResX, currentResY);
			setTimerGUI (currentResX, currentResY);
		}
	
		// Draw the health bars

		// B Player health bar
		drawHealthBar (bPlayer.health, bPlayer.maxHealth, bHealthBarPos.x, bHealthBarPos.y, healthBarSize.x, healthBarSize.y, false);
		// C Player health bar
		drawHealthBar (cPlayer.health, cPlayer.maxHealth, cHealthBarPos.x, cHealthBarPos.y, healthBarSize.x, healthBarSize.y, true);

		// Draw the super/hyper bars

		// Draw the timer
		timerStyle = GUI.skin.GetStyle ("Box");
		drawTimerGUI (timer, timerPos.x, timerPos.y, timerSize.x, timerSize.y);

		// Draw KO/time over UI depending on which flag gets set
		if (koOver || timeOver)
		{
			finishStyle = GUI.skin.GetStyle ("Label");
			setFinishGUI (currentResX, currentResY);

			if (koOver)
				drawFinishGUI ("K.O.", finishPos.x, finishPos.y, finishSize.x, finishSize.y);

			else
				drawFinishGUI ("Time Over", finishPos.x, finishPos.y, finishSize.x, finishSize.y);
		}
	} 


	void Update ()
	{
		//Debug.Log ("UI:\nB Player: " + bPlayer.health + " / " + bPlayer.maxHealth + ", C Player: " + cPlayer.health + " / " + cPlayer.maxHealth);
		// Temporary actions go here
		if (!timeOver && !koOver)
			DecrementTime ();

		if ((bPlayer.health < 1 || cPlayer.health < 1) && !timeOver)
			koOver = true;
	}






	/*
	 *	Evan's (hopefully) Awesome UI Methods!
	*/
	
	// Returns true or false if the screen resolution has changed or not, respectively
	bool screenResChange(float width, float height)
	{
		return (width != Screen.width || height != Screen.height);
	}

	// Reset health bar size and position values
	void setHealthBars(float newWidth, float newHeight)
	{
		healthBarSize = new Vector2((float) Math.Round (newWidth / 3.0), (float) Math.Round (newHeight / 20.0));
		bHealthBarPos = new Vector2((float) Math.Round (newWidth / 10.0), (float) Math.Round (newHeight / 20.0));
		cHealthBarPos = new Vector2(newWidth - healthBarSize.x - bHealthBarPos.x, bHealthBarPos.y);
	}

	// Draws player B health bar
	void drawHealthBar(float curHealth, float maxHealth, float posX, float posY, float sizeX, float sizeY, bool anchorRight)
	{
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		GUI.Box (new Rect (0,0, sizeX, sizeY),progressBarEmpty);
		float offsetX = 0;
		// draw the filled-in part:
		// if anchorRight is true, then the health bar gets "anchored" from the right side
		if (anchorRight == true)
			offsetX = sizeX * (1 - (curHealth / maxHealth));

		GUI.BeginGroup (new Rect (offsetX, 0, sizeX * (curHealth / maxHealth), sizeY));
		GUI.Box (new Rect (0, 0, sizeX, sizeY),progressBarFull);
		
		GUI.EndGroup ();
		GUI.EndGroup ();
	}



	// Sets timer GUI size and position values
	void setTimerGUI(float newWidth, float newHeight)
	{
		timerSize = new Vector2 ((float)Math.Round (newWidth / 10.0), (float)Math.Round (newHeight / 10.0));
		timerPos = new Vector2 ((newWidth / 2) - (timerSize.x / 2), (float)Math.Round (newHeight / 20.0));
	}

	void drawTimerGUI(float timeLeft, float posX, float posY, float sizeX, float sizeY)
	{
		timerStyle.alignment = TextAnchor.MiddleCenter;
		timerStyle.fontSize = (int) sizeX / 2;
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		GUI.Box (new Rect (0, 0, sizeX, sizeY), timeLeft.ToString("00"), timerStyle);
		GUI.EndGroup ();
	}



	void setFinishGUI(float newWidth, float newHeight)
	{
		finishSize = new Vector2 ((float)Math.Round (newWidth / 4.0 * 3.0), (float)Math.Round (newHeight / 4.0 * 3.0));
		finishPos = new Vector2 ((newWidth / 2) - (finishSize.x / 2), (newHeight / 2) - (finishSize.y / 2));
	}


	void drawFinishGUI(string message, float posX, float posY, float sizeX, float sizeY)
	{
		finishStyle.alignment = TextAnchor.MiddleCenter;
		finishStyle.fontSize = (int) sizeY;
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		GUI.Label (new Rect (0, 0, sizeX, sizeY), message, timerStyle);
		GUI.EndGroup ();
	}

	// Temporary functions go here, SHOULD BE EMPTY UPON PROJECT COMPLETION
	void DecrementTime ()
	{
		if (timer > 0 && Time.time > nextTime)
		{
			timer -= 1;
			nextTime = Time.time + timeRate;
		}
		
		else if (timer == 0)
		{
			timeOver = true;
		}
	}


	/*
	 * 	END	(Hopefully) awesome UI Methods
	 */
}
