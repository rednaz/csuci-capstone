using UnityEngine;
using System.Collections;

public class ReadySetGo : MonoBehaviour 
{
	public BPlayerController unlock1;
	public CPlayerController unlock2;
	public MusicPlay1 chime;
	public int counter;

	// Use this for initialization
	void Start () 
	{
		counter = 160;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//ready...
		if ( counter == 160 )
		{
			audio.Play();
			counter--;
		}
		//set....		
		else if ( counter > 1 )
		{
			counter--;
		}
		//GO!  (fight begins)
		else if ( counter == 1 )
		{
			unlock1.fightOn1();
			unlock2.fightOn2();
			chime.RockIt1();
			counter--;
		}
	}
}
