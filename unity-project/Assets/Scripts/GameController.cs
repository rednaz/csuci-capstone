using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	// Instance Variables
	public BPlayerController bPlayer;
	public CPlayerController cPlayer;
	public ReadySetGo readySetGo;
	
	private float nextTime;
	public float timer, timeRate;
	public bool timeOver, koOver;
	
	// Use this for initialization
	void Start ()
	{
		nextTime = Time.time + timeRate;
		timeOver = false;
		koOver = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!timeOver && (bPlayer.health < 1 || cPlayer.health < 1))
		{
			koOver = true;
		}
		
		if (!timeOver && !koOver && readySetGo.counter <= 1)
			DecrementTime ();
	}
	
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
}