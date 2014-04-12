using UnityEngine;
using System.Collections;

public class SeeThrough : MonoBehaviour 
{
	public float lastInterupt = 0.5f;
	float interuptTime = 0.5f;
	
	public Texture visibleMat;
	public Texture invisibleMat;
	
	void Update () 
	{		
		if(lastInterupt < interuptTime || this.renderer.material.mainTexture != visibleMat)
		{
			if(lastInterupt < interuptTime)
			{
				lastInterupt += Time.deltaTime;
				this.renderer.material.mainTexture = invisibleMat;
			}
			else
				this.renderer.material.mainTexture = visibleMat;			
		}
	}
}
