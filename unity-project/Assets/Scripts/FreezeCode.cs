using UnityEngine;
using System.Collections;

public class FreezeCode : MonoBehaviour 
{
	public int countdown = 0;
	public int startCountdown = 50;
	public bool freezeTrigger = false;

	float BposX;
	float BposY;
	float CposX;
	float CposY;
	private Transform Bgrab;
	private Transform Cgrab;
	
	// Use this for initialization
	void Awake()
	{
		renderer.enabled = false;
		Bgrab = GameObject.FindGameObjectWithTag("BPlayer1").transform;
		Cgrab = GameObject.FindGameObjectWithTag("CPlayer2").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//getting Barrett's position
		BposX = Bgrab.position.x;
		BposY = Bgrab.position.y;

		//getting Olivia's position
		CposX = Cgrab.position.x;
		CposY = Cgrab.position.y;

		if( countdown > 1 )
		{
			countdown--;
		}
		if( countdown == 1 )
		{
			renderer.enabled = false;
			countdown--;
			Time.timeScale = 1.0F;
		}
		transform.position = new Vector3( BposX, BposY, 0 );
		transform.Rotate(Vector3.forward * 1000000000);
	}

	void FixedUpdate () 
	{
		if( freezeTrigger == true )
		{
			freezeTrigger = false;
			countdown = startCountdown;
			Time.timeScale = 0.0F;
			renderer.enabled = true;
		}
	}

	public void tempFreeze()
	{
		freezeTrigger = true;
	}
}
