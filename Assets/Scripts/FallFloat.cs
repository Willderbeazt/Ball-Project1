using UnityEngine;
using System.Collections;

public class FallFloat : MonoBehaviour 
{
	public float bounceDamp;
	public float airBoyancy;
	
	void OnTriggerStay(Collider c)
	{
		if(c.attachedRigidbody != null)
		{
			if(c.gameObject.GetComponent("BallMove") as BallMove != null)
			{
				BallMove.BallMaterial mat = c.gameObject.GetComponent<BallMove>().currentMaterial;
				if(mat == BallMove.BallMaterial.Metal)
					airBoyancy = 0;
				else if(mat == BallMove.BallMaterial.Rubber)
					airBoyancy = 1;
				else
					airBoyancy = 2;
				Vector3 uplift = -Physics.gravity * (airBoyancy - c.rigidbody.velocity.y * bounceDamp);
				c.rigidbody.AddForceAtPosition(uplift, c.transform.position);
			}
		}
	}
}
