
using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	float Bpos;
	float Cpos;
	float newX;
	private Transform Bgrab;
	private Transform Cgrab;

	// Use this for initialization
	void Awake()
	{
		Bgrab = GameObject.FindGameObjectWithTag("BPlayer1").transform;
		Cgrab = GameObject.FindGameObjectWithTag("CPlayer2").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{//farleft -14.81057f			farright 23.75795f
		//getting Bplayer position
		Bpos = Bgrab.position.x;

		//getting Cplayer position
		Cpos = Cgrab.position.x;
		//Debug.Log (Bpos + " " + Cpos);
		//doing math to determine new middle between the players
		newX = (Bpos + Cpos) / 2;
		if( newX < -14.81057f)
		{
			newX = -14.81057f;
		}
		else if( newX > 23.75795f )
		{
			newX = 23.75795f;
		}
		else
		{
			newX = newX;
		}
		transform.position = new Vector3( newX, 6.26934f, 0 );
	}
}
