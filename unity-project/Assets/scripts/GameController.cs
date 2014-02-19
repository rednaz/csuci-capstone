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

	void Start ()
	{
		timer = 100.0f;
		nextTime = Time.time + timeRate;
		timerDisplay.text = timer.ToString ("0");
		timeOver.text = "";
	}

	void Update ()
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
}