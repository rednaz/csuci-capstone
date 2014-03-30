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
	private float bHealth, bMaxHealth, cHealth, cMaxHealth;
	private Vector2 healthBarSize, bHealthBarPos, cHealthBarPos;

	// Super/Hyper UI


	// Timer UI
	private float timeLeft;
	private Vector2 timerSize, timerPos;


	// Temporary or testing variables
	private bool timeOver;
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
		GameObject bPlayerObject = GameObject.FindWithTag ("BPlayer1");
		GameObject cPlayerObject = GameObject.FindWithTag ("CPlayer2");
		Debug.Log ("bPlayerObject value is '" + bPlayerObject + "'");
		Debug.Log ("cPlayerObject value is '" + cPlayerObject + "'");


		// Attempt to get B Player
		if (bPlayerObject != null)
		{
			bPlayer = bPlayerObject.GetComponent <BPlayerController>();
		}
		if (bPlayer == null)
		{
			Debug.Log ("Cannot find 'BPlayerController' script");
		}

		// Attempt to get C Player
		if (cPlayerObject != null)
		{
			cPlayer = cPlayerObject.GetComponent <CPlayerController>();
		}
		if (cPlayer == null)
		{
			Debug.Log ("Cannot find 'CPlayerController' script");
		}


		// Set b and c player max health
		bMaxHealth = 1000;
		cMaxHealth = 1000;

		Debug.Log ("B Player - Max Health set to " + bMaxHealth );
		Debug.Log ("C Player - Max Health set to " + cMaxHealth );


		// Temporary actions to be implemented properly later (THIS SHOULD BE EMPTY UPON COMPLETION OF THE PROJECT)
		timer = 100.0f;
		nextTime = Time.time + timeRate;
		timeOver = false;
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
		drawBHealthBar (500f, bMaxHealth, bHealthBarPos.x, bHealthBarPos.y, healthBarSize.x, healthBarSize.y);
		drawCHealthBar (500f, cMaxHealth, cHealthBarPos.x, cHealthBarPos.y, healthBarSize.x, healthBarSize.y);

		// Draw the super/hyper bars

		// Draw the timer
		drawTimerGUI (timer, timerPos.x, timerPos.y, timerSize.x, timerSize.y);
	} 


	void Update ()
	{
		// Temporary actions go here
		if (!timeOver)
			DecrementTime ();
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
	void drawBHealthBar(float curHealth, float maxHealth, float posX, float posY, float sizeX, float sizeY)
	{
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		GUI.Box (new Rect (0,0, sizeX, sizeY),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, sizeX * (curHealth / maxHealth), sizeY));
		GUI.Box (new Rect (0,0, sizeX, sizeY),progressBarFull);
		
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	
	// Draws player C health bar, using offsets so the health gets "anchored" from the right
	void drawCHealthBar(float curHealth, float maxHealth, float posX, float posY, float sizeX, float sizeY)
	{
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		GUI.Box (new Rect (0,0, sizeX, sizeY),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, sizeX * (curHealth / maxHealth), sizeY));
		GUI.Box (new Rect (0,0, sizeX, sizeY),progressBarFull);
		
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
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		GUI.Box (new Rect (0, 0, sizeX, sizeY), timeLeft.ToString("00"));
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
