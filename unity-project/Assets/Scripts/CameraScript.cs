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
	{
		//getting Bplayer position
		Bpos = Bgrab.position.x;

		//getting Cplayer position
		Cpos = Cgrab.position.x;
		//Debug.Log (Bpos + " " + Cpos);
		//doing math to determine new middle between the players
		newX = (Bpos + Cpos) / 2;

		transform.position = new Vector3( newX, 6.26934f, 0 );
	}
}
