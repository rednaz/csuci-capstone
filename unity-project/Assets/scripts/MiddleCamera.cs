using UnityEngine;
using System.Collections;


public class MiddleCamera : MonoBehaviour 
{
	float Bpos;
	float Cpos;
	float newX;
	private Transform Bgrab;
	private Transform Cgrab;


	// Use this for initialization
	//void Start () 
	//{

	//}

	void Awake()
	{
		Bgrab = GameObject.FindGameObjectWithTag("BPlayer1").transform;
		Cgrab = GameObject.FindGameObjectWithTag("CPlayer2").transform;
	}

	void FixedUpdate()
	{
		//getting Bplayer position

		Bpos = Bgrab.position.x;
		Cpos = Cgrab.position.x;
		//getting Cplayer position
		newX = (Bpos + Cpos) / 2;

		//double temp = BplayerControllerScript.position.x;
		transform.position = new Vector3( newX, 9.272855f, 0 );
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
