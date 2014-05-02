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
	
	// Stellar Drive Bar UI
	private Vector2 stellarSize;
	
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
	private float bDeltaHealth, bDeltaStellar, cDeltaHealth, cDeltaStellar;
	private float bHealthFillSpeed, cHealthFillSpeed, bStellarFillSpeed, cStellarFillSpeed;
	private bool initialFill, isFillSpeedSetProperly;
	
	// Temporary or testing variables
	private float timer, nextTime;
	public float timeRate;
	public int bPlayerMaxStellar, cPlayerMaxStellar;
	private int bPlayerCurrentStellar, cPlayerCurrentStellar;
	
	
	void Start ()
	{
		// Save resolution to variables
		currentResX = Screen.width;
		currentResY = Screen.height;
		
		
		// Set position and size of health bars, stellar bars, and timer
		setFillBars (currentResX, currentResY);
		setTimerGUI (currentResX, currentResY);
		
		
		// Attempt to get B Player object
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
		
		
		
		// Attempt to get C Player object
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
		bDeltaHealth = 0;
		cDeltaHealth = 0;
		bDeltaStellar = 0;
		cDeltaStellar = 0;
		
		// Set player health fill speed
		initialFill = true;
		isFillSpeedSetProperly = false;
		bHealthFillSpeed = calculateFillSpeed (bPlayer.maxHealth);
		cHealthFillSpeed = calculateFillSpeed (cPlayer.maxHealth);
		bStellarFillSpeed = calculateFillSpeed (bPlayerMaxStellar);
		cStellarFillSpeed = calculateFillSpeed (cPlayerMaxStellar);

		if (bHealthFillSpeed > 0 && bStellarFillSpeed > 0 && cHealthFillSpeed > 0 && cStellarFillSpeed > 0)
			isFillSpeedSetProperly = true;
		
		
		
		// Temporary actions to be implemented properly later (THIS SHOULD BE EMPTY UPON COMPLETION OF THE PROJECT)
		timer = 100.0f;
		nextTime = Time.time + timeRate;
		bPlayerCurrentStellar = bPlayerMaxStellar;
		cPlayerCurrentStellar = cPlayerMaxStellar;
	}
	
	
	void OnGUI ()
	{
		// Adjust the health/stellar bar size and positions if the screen resolution changes
		if (screenResChange (currentResX, currentResY))
		{
			currentResX = Screen.width;
			currentResY = Screen.height;
			setFillBars (currentResX, currentResY);
			setTimerGUI (currentResX, currentResY);
		}
		
		// Draw health and stellar bar for B player
		drawFillBar (bDeltaHealth, bPlayer.maxHealth, bHealthBarPos.x, bHealthBarPos.y, healthBarSize.x, healthBarSize.y, false);
		drawFillBar (bDeltaStellar, bPlayerMaxStellar, bHealthBarPos.x, bHealthBarPos.y + healthBarSize.y, stellarSize.x, stellarSize.y, false);
		
		// Draw health and stellar bar for C player
		drawFillBar (cDeltaHealth, cPlayer.maxHealth, cHealthBarPos.x, cHealthBarPos.y, healthBarSize.x, healthBarSize.y, true);
		drawFillBar (cDeltaStellar, cPlayerMaxStellar, cHealthBarPos.x + stellarSize.x, cHealthBarPos.y + healthBarSize.y, stellarSize.x, stellarSize.y, true);
		
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
		Debug.Log ("UI:\nB Player: " + bPlayer.health + " / " + bPlayer.maxHealth + ", C Player: " + cPlayer.health + " / " + cPlayer.maxHealth);
		// Temporary actions go here
		
		if (bPlayer.health < 1 || cPlayer.health < 1)
		{
			koOver = true;
		}
		
		if (!timeOver && !koOver)
			DecrementTime ();
		
		int fillScale = 5;
		// Update delta health and stellar values
		if (!initialFill)
			fillScale = 1;
		
		else if (bDeltaHealth >= (float) bPlayer.health || cDeltaHealth >= (float) cPlayer.health)
		{
			initialFill = false;
		}

		if (!isFillSpeedSetProperly)
		{
			bHealthFillSpeed = calculateFillSpeed (bPlayer.maxHealth);
			cHealthFillSpeed = calculateFillSpeed (cPlayer.maxHealth);
			bStellarFillSpeed = calculateFillSpeed (bPlayerMaxStellar);
			cStellarFillSpeed = calculateFillSpeed (cPlayerMaxStellar);

			if (bHealthFillSpeed > 0 && bStellarFillSpeed > 0 && cHealthFillSpeed > 0 && cStellarFillSpeed > 0)
				isFillSpeedSetProperly = true;
		}

		bDeltaHealth = updateDeltaVals (bPlayer.health, bDeltaHealth, bHealthFillSpeed * fillScale);
		bDeltaStellar = updateDeltaVals (bPlayerCurrentStellar, bDeltaStellar, bStellarFillSpeed * fillScale);
		cDeltaHealth = updateDeltaVals (cPlayer.health, cDeltaHealth, cHealthFillSpeed * fillScale);
		cDeltaStellar = updateDeltaVals (cPlayerCurrentStellar, cDeltaStellar, cStellarFillSpeed * fillScale);
		Debug.Log ("C Player delta - " + cDeltaHealth);
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
	void setFillBars(float newWidth, float newHeight)
	{
		healthBarSize = new Vector2((float) Math.Round (newWidth / 3.0), (float) Math.Round (newHeight / 20.0));
		bHealthBarPos = new Vector2((float) Math.Round (newWidth / 10.0), (float) Math.Round (newHeight / 20.0));
		cHealthBarPos = new Vector2(newWidth - healthBarSize.x - bHealthBarPos.x, bHealthBarPos.y);
		
		// Set stellar bar size
		stellarSize = new Vector2((float) Math.Round (healthBarSize.x / 2.0), (float) Math.Round (healthBarSize.y * (2.0/3.0)));
	}
	
	// Draws health bar, either for player B or C depending on the value of the boolean variable 'anchorRight'
	void drawFillBar(float curHealth, float maxHealth, float posX, float posY, float sizeX, float sizeY, bool anchorRight)
	{
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		GUI.Box (new Rect (0,0, sizeX, sizeY),progressBarEmpty);
		
		
		// draw the filled-in part of the bar: if anchorRight is true, then the health bar gets "anchored" from the right side
		// this is accomplished by adjusting the X position as the size of the health bar changes
		float offsetX = 0;
		if (anchorRight == true)
			offsetX = sizeX * (1 - (curHealth / maxHealth));
		
		GUI.BeginGroup (new Rect (offsetX, 0, sizeX * (curHealth / maxHealth), sizeY));
		GUI.Box (new Rect (0, 0, sizeX, sizeY),progressBarFull);
		
		GUI.EndGroup ();  // end "fill" part of the health bar
		GUI.EndGroup ();  // end empty part of health bar
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
		finishStyle.fontSize = (int) sizeY / 3;
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		GUI.Label (new Rect (0, 0, sizeX, sizeY), message, finishStyle);
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

	// Sets the fill speed based on the player's maximum health/stellar value
	float calculateFillSpeed(float maxVal)
	{
		return maxVal / 500;
	}

	// Updates delta fillbar values
	float updateDeltaVals(int currentVal, float deltaVal, float fillSpeed)
	{
		if (deltaVal < (float) currentVal)
		{
			// Used to approximate so the delta value doesn't constantly fluctuate around the actual value
			if (Math.Abs (deltaVal - (float) currentVal) <= fillSpeed)
				deltaVal = (float) currentVal;
			
			else
				deltaVal += fillSpeed;
		}
		
		else if (deltaVal > (float) currentVal)
		{
			// Used to approximate so the delta value doesn't constantly fluctuate around the actual value
			if (Math.Abs (deltaVal - (float) currentVal) <= fillSpeed)
				deltaVal = (float) currentVal;
			
			else
				deltaVal -= fillSpeed;
		}
		
		return deltaVal;
	}
	
	
	/*
	 * 	END	(Hopefully) awesome UI Methods
	 */
}