using UnityEngine;
using System;
using System.Collections;

public class BallMove : MonoBehaviour 
{
	public enum CameraPosition
	{
		Side,
		Above,
		Behind
	}
	
	public enum BallMaterial
	{
		Rubber,
		Metal,
		Sandpaper
	}
	
	public BallMaterial currentMaterial;
	public Camera cam;
	public CameraPosition currentCamPosition;
	public float speed = 10;
	Vector3 sidePosition;
	Vector3 abovePosition;
	Vector3 behindPosition;
	
	public PhysicMaterial Bouncer;
	public PhysicMaterial Metal;
	
	KeyCode currentKey;
	KeyCode previousKey;
	
	void Awake () 
	{
		sidePosition = new Vector3(0, 15, -20);
		abovePosition = new Vector3(0, 15, 0);
		behindPosition = new Vector3(-10, 15, 0);	
		currentCamPosition = CameraPosition.Side;
		currentMaterial = BallMaterial.Rubber;
	}
	
	void Update()
	{
		switch(currentCamPosition)
		{
			case CameraPosition.Above:
				cam.transform.position = abovePosition;
				break;
			case CameraPosition.Behind:
				cam.transform.position = behindPosition;
				break;
			case CameraPosition.Side:	
				cam.transform.position = sidePosition;
				break;			
		}
		
		cam.transform.LookAt(transform);
	}
			
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(!Input.anyKey)
			currentKey = KeyCode.None;
		
		if(Input.GetKey(KeyCode.DownArrow))
		{
			rigidbody.AddForce (-Vector3.forward*(speed/2));
			currentKey = KeyCode.DownArrow;	
		}
		
		if(Input.GetKey(KeyCode.UpArrow))
		{
			rigidbody.AddForce (Vector3.forward*speed);
			currentKey = KeyCode.UpArrow;	
		}
		
		if(Input.GetKey(KeyCode.RightArrow))
		{
			rigidbody.AddForce (Vector3.right*speed);
			currentKey = KeyCode.RightArrow;	
		}
		
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			rigidbody.AddForce (Vector3.left*speed);
			currentKey = KeyCode.LeftArrow;	
		}
	
		if(Input.GetKey(KeyCode.Q))
		{
			currentKey = KeyCode.Space;
			if(previousKey != currentKey)
			{
				if(currentMaterial == BallMaterial.Rubber)
				{
					currentMaterial = BallMaterial.Metal;
					collider.material = Metal;
				}
				else
				{
					currentMaterial = BallMaterial.Rubber;
					collider.material = Bouncer;
				}
			}
		}
			
		
		if(Input.GetKey(KeyCode.Space))
		{
			currentKey = KeyCode.Space;
			if(previousKey != currentKey)
			{
				if((int)currentCamPosition == Enum.GetValues(typeof(CameraPosition)).Length-1)
					currentCamPosition = (CameraPosition)0;
				else
					currentCamPosition = (CameraPosition)((int)currentCamPosition+1);
			}
		}
		
		previousKey = currentKey;
	}
}
