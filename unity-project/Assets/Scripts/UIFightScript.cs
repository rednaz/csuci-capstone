using UnityEngine;
using System.Collections;
using System;

public class UIFightScript : MonoBehaviour
{	
	private float currentResX, currentResY;
	private float bHealth, bMaxHealth, cHealth, cMaxHealth;
	private BPlayerController bPlayer;
	private CPlayerController cPlayer;
	
	private float barDisplay;
	private Vector2 healthBarSize, bHealthBarPos, cHealthBarPos;
	private Texture2D progressBarEmpty, progressBarFull;


	void Start ()
	{
		// Save resolution to variables
		currentResX = Screen.width;
		currentResY = Screen.height;

		// Set position and size of health bars
		setHealthBars (currentResX, currentResY);

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
	}


	void OnGUI ()
	{
		// Adjust the health bar size and positions if the screen resolution changes
		if (screenResChange (currentResX, currentResY))
		{
			currentResX = Screen.width;
			currentResY = Screen.height;
			setHealthBars (currentResX, currentResY);
		}
	
		// Draw the health bars
		drawBHealthBar (500f, bMaxHealth, bHealthBarPos.x, bHealthBarPos.y, healthBarSize.x, healthBarSize.y);
		drawCHealthBar (500f, cMaxHealth, cHealthBarPos.x, cHealthBarPos.y, healthBarSize.x, healthBarSize.y);
	} 

	/*
	void Update ()
	{
		// for this example, the bar display is linked to the current time,
		// however you would set this value based on your desired display
		// eg, the loading progress, the player's health, or whatever.
		barDisplay = Time.time * 0.15f;
	}
	*/





	/*
	 *	Evan's (hopefully) Awesome UI Methods!
	*/
	
	// Returns true or false if the screen resolution has changed or not, respectively
	bool screenResChange(float width, float height)
	{
		return (width != Screen.width || height != Screen.height);
	}

	// Reset health bar size and position information
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

	/*
	 * 	END	(Hopefully) awesome UI Methods
	 */
}
