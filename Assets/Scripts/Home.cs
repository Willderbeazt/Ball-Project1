using UnityEngine;
using System.Collections;

public class Home : MonoBehaviour 
{
	public Transform HomeLocation;
	
	void OnTriggerEnter(Collider c)
	{
		c.rigidbody.velocity = Vector3.zero;
		c.gameObject.transform.position = HomeLocation.position;
	}
}
