using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	//public GUIText p1Health;
	//public GUIText p2Health;
	public GUIText timerDisplay;
	public GUIText timeOver;
	private float timer, nextTime;
	public float timeRate;
	
	//Screen resolution elements for scaling
	int nativeWidth = 1280;
	int nativeHeight = 720;
	
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
	
	// Decrements the time by the time rate
	void DecrementTime ()
	{
		if (timer > 0 && Time.time > nextTime)
		{
			timer -= 1;
			if (timer >= 30 && timer < 60)
				timerDisplay.color = Color.yellow;
			else if (timer < 30)
				timerDisplay.color = Color.red;
			
			timerDisplay.text = timer.ToString ("00");
			nextTime = Time.time + timeRate;
		}
		
		else if (timer == 0)
		{
			timeOver.text = "Time Over!";
		}
	}
	
	void Start ()
	{
		timer = 100.0f;
		nextTime = Time.time + timeRate;
		timerDisplay.text = timer.ToString ("0");
		timeOver.text = "";
	}
	
	
	void Update ()
	{
		DecrementTime ();
	}
	
	// Used for displaying scaled GUI
	void OnGUI()
	{
		AutoResize (nativeWidth, nativeHeight);
	}
}