using UnityEngine;
using System.Collections;

public class PathLighting : MonoBehaviour 
{
	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.name == "Player")
			StartCoroutine("FadeInColour");
	}
	
	void OnTriggerExit(Collider c)
	{
		if(c.gameObject.name == "Player")
		{
			StopCoroutine("FadeInColour");
			StartCoroutine("FadeOutColour");
		}
	}
	
	IEnumerator FadeInColour()
	{
		
		Color current = transform.parent.renderer.material.color;
		Color dest = new Color(1,0,0,1);
		float elapsedTime = 0;
		float transitionTime = 0.5f;
		
		while(elapsedTime < transitionTime)
		{
			Color lerpedColour = Color.Lerp(current, dest, elapsedTime / transitionTime);
			transform.parent.renderer.material.color = lerpedColour;			
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.parent.renderer.material.color = dest;
	}
	
	IEnumerator FadeOutColour()
	{
		Color c = transform.parent.renderer.material.color;
		Color d = new Color(1,1,1,1);
		float elapsedTime = 0;
		float transitionTime = 0.5f;
		
		while(elapsedTime < transitionTime)
		{
			Color lerpedColour = Color.Lerp(c, d, elapsedTime / transitionTime);
			transform.parent.renderer.material.color = lerpedColour;			
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.parent.renderer.material.color = d;
	}
}