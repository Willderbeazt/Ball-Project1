using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour {
	public float waterLevel, floatHeight;
	public Vector3 buoyancyCentreOffset;
	public float bounceDamp;
	
	public PhysicMaterial bouncer;
	public PhysicMaterial metal;
	
	BallMove movement;
	
	void Awake()
	{
		movement = gameObject.GetComponent<BallMove>();
	}
	
	void FixedUpdate () 
	{
		if(movement.currentMaterial == BallMove.BallMaterial.Rubber)
		{
			Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
			float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);
			
			if (forceFactor > 0f) {
				Vector3 uplift = -Physics.gravity * (forceFactor - rigidbody.velocity.y * bounceDamp);
				rigidbody.AddForceAtPosition(uplift, actionPoint);
			}
		}
		else if(movement.currentMaterial == BallMove.BallMaterial.Sandpaper)
		{
			print ("Dead?");
		}
	}
}
