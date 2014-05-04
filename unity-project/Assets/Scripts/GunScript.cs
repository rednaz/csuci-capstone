using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour 
{
	public float bulletSpeed = 70f;
	public Rigidbody2D projectile;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () 
	{
		if ( Input.GetKeyDown ("space") )
		{
			Rigidbody2D instantiateProjectile = Instantiate ( projectile, transform.position, transform.rotation ) as Rigidbody2D;
			
			instantiateProjectile.velocity = transform.TransformDirection (new Vector3 ( bulletSpeed, 0, 0 ) );
		}
		
		
	}
}
