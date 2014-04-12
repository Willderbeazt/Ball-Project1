using UnityEngine;
using System;
using System.Collections;

public class BallMove : MonoBehaviour 
{
	public enum PlayerSizes
	{
		Small,
		Medium,
		Large
	}
	
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
	public Camera secondCam;
	public CameraPosition currentCamPosition;
	public float speed = 10;
	Vector3 sidePosition;
	Vector3 abovePosition;
	Vector3 behindPosition;
	
	public PhysicMaterial Bouncer;
	public PhysicMaterial Metal;
	public PhysicMaterial Paper;
	
	public PlayerSizes Size;
	
	KeyCode currentKey;
	KeyCode previousKey;
	
	void Awake () 
	{
		sidePosition = new Vector3(10, 7, 0);
		abovePosition = new Vector3(0, 10, 0);
		behindPosition = new Vector3(0, 0, -10);	
		currentCamPosition = CameraPosition.Side;
		currentMaterial = BallMaterial.Rubber;	
		Size = PlayerSizes.Small;
	}
	
	void Update()
	{
		switch(currentCamPosition)
		{
			case CameraPosition.Above:
				cam.transform.position = transform.position + abovePosition;
				cam.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
				cam.gameObject.SetActive (true);			
				secondCam.gameObject.SetActive (false);
				break;
			case CameraPosition.Behind:
				secondCam.transform.position = transform.position + behindPosition;
				cam.gameObject.SetActive (false);			
				secondCam.gameObject.SetActive (true);
				cam.transform.LookAt(transform);
				break;
			case CameraPosition.Side:	
				cam.transform.position = transform.position + sidePosition;
				cam.transform.rotation = Quaternion.Euler(new Vector3(0,-90,0));
				cam.gameObject.SetActive (true);			
				secondCam.gameObject.SetActive (false);
				break;			
		}
		
		
		
		RaycastHit[] hits;
       
        hits = Physics.RaycastAll(transform.position, transform.forward, Vector3.Distance(transform.position, cam.transform.position));
        foreach(RaycastHit hit in hits)
        {
			if(hit.collider)
				if(hit.collider.gameObject.GetComponent("SeeThrough") as SeeThrough != null)
					hit.collider.gameObject.GetComponent<SeeThrough>().lastInterupt = 0;
		}
	}
			
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		if(!Input.anyKey)
			currentKey = KeyCode.None;
		
		if(Input.GetKey(KeyCode.PageUp))
		{
			currentKey = KeyCode.PageUp;
			
			if(previousKey != currentKey)
				GrowPlayer();
		}
		
		if(Input.GetKey(KeyCode.PageDown))
		{
			currentKey = KeyCode.PageDown;
			
			if(previousKey != currentKey)			
				ShrinkPlayer();
		}
		/*
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
		*/
		
		HandleMovement();
		
		if(Input.GetKey(KeyCode.Q))
		{
			currentKey = KeyCode.Space;
			if(previousKey != currentKey)
			{
				if(currentMaterial == BallMaterial.Rubber)
				{
					currentMaterial = BallMaterial.Metal;
					collider.material = Metal;
					renderer.material.color = new Color32(81,81,81,255);
				}
				else if(currentMaterial == BallMaterial.Sandpaper)
				{
					currentMaterial = BallMaterial.Rubber;
					collider.material = Bouncer;
					renderer.material.color = new Color32(255,156,255,255);
				}
				else
				{
					currentMaterial = BallMaterial.Sandpaper;
					collider.material = Paper;
					renderer.material.color = new Color32(241,255,112,255);
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
	
	void ShrinkPlayer()
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
	
	void GrowPlayer()
	{
		if(isGrounded())
			rigidbody.AddForce(Vector3.up); //Add a bit of force to avoid sinking into floor
		
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
	
	void HandleMovement()
	{
		switch(	currentCamPosition)
		{
			case CameraPosition.Above:
				if(Input.GetKey(KeyCode.UpArrow))
					rigidbody.AddForce (Vector3.forward*speed);
				else if(Input.GetKey(KeyCode.DownArrow))
					rigidbody.AddForce (-Vector3.forward*(speed/2));
				else if(Input.GetKey(KeyCode.RightArrow))
					rigidbody.AddForce (Vector3.right*speed);
				else if(Input.GetKey(KeyCode.LeftArrow))
					rigidbody.AddForce (Vector3.left*speed);
			break;
			case CameraPosition.Behind:
				if(Input.GetKey(KeyCode.UpArrow))
					rigidbody.AddForce (Vector3.forward*speed);
				else if(Input.GetKey(KeyCode.DownArrow))
					rigidbody.AddForce (-Vector3.forward*(speed/2));
				else if(Input.GetKey(KeyCode.RightArrow))
					rigidbody.AddForce (Vector3.right*speed);
				else if(Input.GetKey(KeyCode.LeftArrow))
					rigidbody.AddForce (Vector3.left*speed);
			break;
			case CameraPosition.Side:	
				if(Input.GetKey(KeyCode.UpArrow))
					rigidbody.AddForce (Vector3.left*speed);
				else if(Input.GetKey(KeyCode.DownArrow))
					rigidbody.AddForce (Vector3.right*speed);
				else if(Input.GetKey(KeyCode.RightArrow))
					rigidbody.AddForce(Vector3.forward*speed);
				else if(Input.GetKey(KeyCode.LeftArrow))
					rigidbody.AddForce(-Vector3.forward*(speed/2));
				break;			
		}
	}
	
	bool isGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1f);	
	}
	
	void CheckDrag()
	{
		if(currentMaterial == BallMaterial.Rubber)
		{
			if(Size == PlayerSizes.Large)
				rigidbody.drag = 0.3f;
			else if(Size == PlayerSizes.Medium)
				rigidbody.drag = 0.18f;
			else if(Size == PlayerSizes.Small)
				rigidbody.drag = 0.05f;
		}
		else if(currentMaterial == BallMaterial.Sandpaper)
		{
			rigidbody.drag = 0.05f;	
		}
		else
		{
			rigidbody.drag = 0.5f;	
		}
	}
}
