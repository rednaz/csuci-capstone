using UnityEngine;
using System.Collections;

public class MusicPlay1 : MonoBehaviour 
{
	public bool letsRock = false;
	public bool alreadyRockin = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( letsRock == true && alreadyRockin == false )
		{
			audio.Play();
			alreadyRockin = true;
		}
	}

	public void RockIt1()
	{
		letsRock = true;
	}
}
