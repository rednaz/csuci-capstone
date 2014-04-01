using UnityEngine;
using System.Collections;

public class FreezeCode : MonoBehaviour 
{
	public int countdown = 0;
	public int startCountdown = 50;
	public bool freezeTrigger = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( countdown > 1 )
		{
			countdown--;
		}
		if( countdown == 1 )
		{
			countdown--;
			Time.timeScale = 1.0F;
		}
	}

	void FixedUpdate () 
	{
		if( freezeTrigger == true )
		{
			freezeTrigger = false;
			countdown = startCountdown;
			Time.timeScale = 0.0F;
		}
	}

	public void tempFreeze()
	{
		freezeTrigger = true;
	}
}
