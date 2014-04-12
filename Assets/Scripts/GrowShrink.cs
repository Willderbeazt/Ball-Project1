using UnityEngine;
using System.Collections;

public class GrowShrink : MonoBehaviour 
{
	public enum PlayerSizes
	{
		Small,
		Medium,
		Large
	}
	
	KeyCode currentKey;
	KeyCode previousKey;
	
	public PlayerSizes Size;
	// Update is called once per frame
	
	void Awake()
	{
		Size = PlayerSizes.Small;	
	}
	
	void FixedUpdate () 
	{	
		print(currentKey);
		
		if(!Input.anyKey)
			currentKey = KeyCode.None;
		
		if(Input.GetKey(KeyCode.PageUp))
		{
			currentKey = KeyCode.PageUp;
			
			if(previousKey != currentKey)
			{
				if(Size == PlayerSizes.Small)
				{
					Size = PlayerSizes.Medium;
					animation["SmallMed"].speed = 2;
					animation.Play("SmallMed");					
				}
				else if(Size == PlayerSizes.Medium)
				{
					Size = PlayerSizes.Large;
					animation["MedLrg"].speed = 2;
					animation.Play("MedLrg");					
				}
			}
		}
		
		if(Input.GetKey(KeyCode.PageDown))
		{
			currentKey = KeyCode.PageDown;
			
			if(previousKey != currentKey)
			{				
				if(Size == PlayerSizes.Medium)
				{
					animation["SmallMed"].speed = -2;
					animation["SmallMed"].time = animation["SmallMed"].length;
					animation.Play("SmallMed");
					Size = PlayerSizes.Small;
				}
				else if(Size == PlayerSizes.Large)
				{
					animation["MedLrg"].speed = -2;
					animation["MedLrg"].time = animation["MedLrg"].length;
					animation.Play("MedLrg");
					Size = PlayerSizes.Medium;
				}
			}
		}
		
		previousKey = currentKey;
	}
}
