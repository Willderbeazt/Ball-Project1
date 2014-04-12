using UnityEngine;
using System.Collections;

public class CamSeeThrough : MonoBehaviour 
{
	public float Alpha = 0.1f;
 
    void OnTriggerEnter(Collider other)
    {		
        GameObject obj = other.gameObject;
		
		if(obj.renderer != null)
		{
	      	Color c = obj.renderer.material.color;
	      	c.a = Alpha;
	      	obj.renderer.material.color = c;
		}
    }
 
    void OnTriggerExit(Collider other)
    {
       	GameObject obj = other.gameObject;
		if(obj.renderer != null)
		{			
		 	Color c = obj.renderer.material.color;
	     	c.a = 1;
	    	obj.renderer.material.color = c;
		}
    }
	
}
