using UnityEngine;
using System.Collections;

public class PlayNear : MonoBehaviour 
{

	public Transform player;
	public float range;
	public AudioClip sound;
	
	bool played;
	public float timing;
	//delay is time until the sound can be replayed after starting
	public float delay = 10;
 
	void Awake()
	{
		played = false;
		timing = Time.time;
	}
 
	void Update()
	{
		if (Vector3.Distance(player.position, transform.position)<range)
		{
			if(timing<Time.time)
			{
		           played = true;
		           timing = Time.time + delay;
			}
		}
		
		if(played)
		{
			AudioSource.PlayClipAtPoint(sound, transform.position);
			played=false;
		}
		
		if(played)
			audio.loop = false;
	}
}
