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
	
	// Fill Bar textures
	//private Texture2D barEmpty, healthBarFull, stellarBarFull;

	// Main Bar UI
	private Vector2 mainBarSize, mainBarPos;

	// Health Bar UI
	private Vector2 healthBarSize, bHealthBarPos, cHealthBarPos;
	
	// Stellar Drive Bar UI
	private Vector2 stellarSize;
	
	// Timer UI
	private float timeLeft;
	private Vector2 timerSize, timerPos;
	
	// Fight Finish UI (knockout or time over)
	private Vector2 finishSize, finishPos;

	// GUI Styles
	public GUIStyle mainBarStyle, barEmpty, healthFill, stellarFill, timerStyle, finishStyle, whiteText;
	public GUIStyle barrettPortrait, oliviaPortrait; // Player portraits on left and right sides of mainbar

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
		Debug.Log (mainBarStyle.normal.background.width + ", " + mainBarStyle.normal.background.height);
		// Save resolution to variables
		currentResX = Screen.width;
		currentResY = Screen.height;

		// Set position and size of health bars, stellar bars, and timer
		setMainBar ();

		
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
		bPlayerMaxStellar = 300;
		cPlayerMaxStellar = 300;
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
			setMainBar ();
		}
		
		// Draw main bar UI
		// Includes both health bars, all six stellar drive meters, the timer, and remaining revolver shots
		drawMainUI ();
		
		// Draw KO/time over UI depending on which flag gets set
		if (koOver || timeOver)
		{
			//finishStyle = new GUIStyle(GUI.skin.box);
			setFinishGUI ();
			
			if (koOver)
				drawFinishGUI ("Beatdown!", finishPos.x, finishPos.y, finishSize.x, finishSize.y);
			
			else
				drawFinishGUI ("Time Over", finishPos.x, finishPos.y, finishSize.x, finishSize.y);
		}
	} 
	
	
	void Update ()
	{
		//Debug.Log ("UI:\nB Player: " + bPlayer.health + " / " + bPlayer.maxHealth + ", C Player: " + cPlayer.health + " / " + cPlayer.maxHealth);
		// Temporary actions go here
		
		if (!timeOver && (bPlayer.health < 1 || cPlayer.health < 1))
		{
			koOver = true;
		}
		
		if (!timeOver && !koOver)
			DecrementTime ();


		// Fillscale is used to quickly fill up the health and hyper/stellar meters at the beginning of the match.
		int fillScale = 5;

		// Update delta health and stellar values
		if (!initialFill)
			fillScale = 1;
		
		else if (bDeltaHealth >= (float) bPlayer.health || cDeltaHealth >= (float) cPlayer.health)
			initialFill = false;

		// Used to counteract possibly getting zero health values at the program start
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
	}
	
	
	
	
	
	/*
	 *	Evan's (hopefully) Awesome UI Methods!
	*/
	
	// Returns true or false if the screen resolution has changed or not, respectively
	bool screenResChange(float width, float height)
	{
		return (width != Screen.width || height != Screen.height);
	}

	// Used to scale some part of the UI to fit the current screen dimensions
	Vector2 scaleSize (float width, float height)
	{
		float scaledWidth = (float) Math.Round((width / 1920) * currentResX);
		float scaledHeight = (float) Math.Round((height/ 1080) * currentResY);

		return new Vector2 (scaledWidth, scaledHeight);
	}

	// Set main bar size and position
	void setMainBar()
	{
		// Main bar dimensions in pixels: 1726x211 using our current asset


		float mainBarAbsWidth = mainBarStyle.normal.background.width;
		float mainBarAbsHeight = mainBarStyle.normal.background.height;

		// Scaled main bar dimensions
		float mainBarWidth = (float)Math.Round ((mainBarAbsWidth / 1920) * currentResX);
		float mainBarHeight = (float)Math.Round ((mainBarAbsHeight / 1080) * currentResY);

		// Finally, set final size and dimensions
		mainBarSize = new Vector2 (mainBarWidth, mainBarHeight);
		mainBarPos = new Vector2 ((currentResX / 2) - (mainBarSize.x / 2), (float)Math.Round (currentResY / 20.0));
	}

	// Draw main bar GUI
	void drawMainUI()
	{
		// Set main bar size and position
		mainBarSize = scaleSize (mainBarStyle.normal.background.width, mainBarStyle.normal.background.height);
		mainBarPos = new Vector2 ((currentResX / 2) - (mainBarSize.x / 2), (float)Math.Round (currentResY / 20.0));

		// Begin the mainbar group to set relative pixel position values for the rest of the main bar GUI
		// Draw the player portraits, then the mainbar
		GUI.BeginGroup (new Rect (mainBarPos.x, mainBarPos.y, mainBarSize.x, mainBarSize.y));

		// Draw Barrett and Olivia portraits
		Vector2 portraitSize = scaleSize (142, 151);
		Vector2 barrettPos = scaleSize (22, 38);
		Vector2 oliviaPos = scaleSize (1563, 38);

		GUI.Box (new Rect (barrettPos.x, barrettPos.y, portraitSize.x, portraitSize.y), new GUIContent (""), barrettPortrait);
		GUI.Box (new Rect (oliviaPos.x, oliviaPos.y, portraitSize.x, portraitSize.y), new GUIContent (""), oliviaPortrait);

		// Draw main bar UI
		GUI.Box (new Rect (0, 0, mainBarSize.x, mainBarSize.y), new GUIContent (""), mainBarStyle);

		// Draw player name text
		// White text GUI style
		Vector2 whiteFont = scaleSize (48, 48);
		whiteText.normal.textColor = Color.white;
		whiteText.fontSize = (int) whiteFont.x;
		Vector2 barrettTextPos = scaleSize (188, 160);
		Vector2 oliviaTextPos = scaleSize (1539/* - 188*/, 160);
		whiteText.alignment = TextAnchor.UpperLeft;
		GUI.Box (new Rect (barrettTextPos.x, barrettTextPos.y, 0, 0), "Barrett", whiteText);
		whiteText.alignment = TextAnchor.UpperRight;
		GUI.Box (new Rect (oliviaTextPos.x, oliviaTextPos.y, 0, 0), "Olivia", whiteText);

		// Get health bar size
		healthBarSize = scaleSize (331, 32);

		// Draw B player health bar (X must be set 5 more, Y must be set 14 more than what photoshop says!)
		bHealthBarPos = scaleSize (234, 40);
		drawFillBar (bDeltaHealth, bPlayer.maxHealth, bHealthBarPos.x, bHealthBarPos.y, healthBarSize.x, healthBarSize.y, healthFill, false);

		// Size and positions of all six stellar meters (based on 1920x1080 pixels scaled to different resolutions)
		Vector2 bPos1, bPos2, bPos3, cPos1, cPos2, cPos3;
		stellarSize = scaleSize (114, 27);
		bPos1 = scaleSize (202, 95);
		bPos2 = scaleSize (326, 95);
		bPos3 = scaleSize (449, 95);

		// Remember - these bars go from right to left, which is why Pos1 is further right than the other two!
		cPos1 = scaleSize (1409, 95);
		cPos2 = scaleSize (1286, 95);
		cPos3 = scaleSize (1162, 95);

		
		// Draw B player stellar bars
		if (bDeltaStellar > 200)
		{
			drawFillBar (100, 100, bPos1.x, bPos1.y, stellarSize.x, stellarSize.y, stellarFill, false);
			drawFillBar (100, 100, bPos2.x, bPos2.y, stellarSize.x, stellarSize.y, stellarFill, false);
			drawFillBar (bDeltaStellar - 200, 100, bPos3.x, bPos3.y, stellarSize.x, stellarSize.y, stellarFill, false);
		}

		else if (bDeltaStellar > 100)
		{
			drawFillBar (100, 100, bPos1.x, bPos1.y, stellarSize.x, stellarSize.y, stellarFill, false);
			drawFillBar (bDeltaStellar - 100, 100, bPos2.x, bPos2.y, stellarSize.x, stellarSize.y, stellarFill, false);
		}

		else
			drawFillBar (bDeltaStellar, 100, bPos1.x, bPos1.y, stellarSize.x, stellarSize.y, stellarFill, false);
		

		// Draw C player health bar
		cHealthBarPos = scaleSize (1161, 40);
		drawFillBar (cDeltaHealth, cPlayer.maxHealth, cHealthBarPos.x, cHealthBarPos.y, healthBarSize.x, healthBarSize.y, healthFill, true);


		// Draw C player stellar bars
		if (cDeltaStellar > 200)
		{
			drawFillBar (100, 100, cPos1.x, cPos1.y, stellarSize.x, stellarSize.y, stellarFill, true);
			drawFillBar (100, 100, cPos2.x, cPos2.y, stellarSize.x, stellarSize.y, stellarFill, true);
			drawFillBar (bDeltaStellar - 200, 100, cPos3.x, cPos3.y, stellarSize.x, stellarSize.y, stellarFill, true);
		}
		
		else if (cDeltaStellar > 100)
		{
			drawFillBar (100, 100, cPos1.x, cPos1.y, stellarSize.x, stellarSize.y, stellarFill, true);
			drawFillBar (cDeltaStellar - 100, 100, cPos2.x, cPos2.y, stellarSize.x, stellarSize.y, stellarFill, true);
		}
		
		else
			drawFillBar (cDeltaStellar, 100, cPos1.x, cPos1.y, stellarSize.x, stellarSize.y, stellarFill, true);


		// Draw the timer
		timerSize = new Vector2 ((float)Math.Round (mainBarSize.x / 7.5), (float) Math.Round (mainBarSize.y / 7.5));
		timerPos = new Vector2 ((mainBarSize.x / 2) - (timerSize.x / 2), mainBarSize.y / 3);
		timerStyle.alignment = TextAnchor.MiddleCenter;
		timerStyle.fontSize = (int) timerSize.x / 2;
		timerStyle.normal.textColor = new Color (0.25f, 0.85f, 1f);
		GUI.Box (new Rect (timerPos.x, timerPos.y, timerSize.x, timerSize.y), timer.ToString("00"), timerStyle);

		GUI.EndGroup ();

	}
	
	// Draws health or stellar meter, either for player B or C depending on the value of the boolean variable 'anchorRight'
	void drawFillBar(float curHealth, float maxHealth, float posX, float posY, float sizeX, float sizeY, GUIStyle style, bool anchorRight)
	{
		GUI.BeginGroup (new Rect (posX, posY, sizeX, sizeY));
		
		// draw the filled-in part of the bar: if anchorRight is true, then the health bar gets "anchored" from the right side
		// this is accomplished by adjusting the X position as the size of the health bar changes
		float offsetX = 0;
		if (anchorRight == true)
			offsetX = sizeX * (1 - (curHealth / maxHealth));

		GUI.Box (new Rect (offsetX, 0, sizeX * (curHealth / maxHealth), sizeY), new GUIContent(""), style);
		GUI.EndGroup ();  // end empty part of health bar
	}
	
	void setFinishGUI()
	{
		finishSize = new Vector2 ((float)Math.Round (currentResX / 4.0 * 3.0), (float)Math.Round (currentResY / 4.0 * 3.0));
		finishPos = new Vector2 ((currentResX / 2) - (finishSize.x / 2), (currentResY / 2) - (finishSize.y / 2));
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
	
	// Sets the fill speed based on the player's maximum health/stellar drive value
	float calculateFillSpeed(float maxVal)
	{
		return maxVal / 500;
	}

	// Updates delta values for gradually changing the health and stellar meters
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