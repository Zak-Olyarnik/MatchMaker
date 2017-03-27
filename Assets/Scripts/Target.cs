using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds match IDs for matching targets.
public class Target : MonoBehaviour
{
	public int matchID;				// 0-5, based on student type
	public int genderID;			// 0 or 1, based on gender
	public float speed;				// positive moves right, negative moves left
	public float rightBound;		// absolute right edge (for OffscreenDestroyer) 
	public float leftBound;			// absolute left edge (for OffscreenDestroyer) 
	public float buffer;			// used to create a safe spawning buffer away from the absolute edges
	public float spriteTime;		// time to switch sprites for animation
	public Sprite[] sprites;		// sprites that are switched to fake animation

	private int selectedSprite = 0;		// currently displayed sprite index
	private int numPairs = 0;			// local copy of GameController.numPairs
	private Rigidbody2D rb;				// the target's rigidbody for movement physics


	// apply movement speed based on round or obstacle type
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = Vector3.right * speed;
		InvokeRepeating("SwitchSprite", spriteTime, spriteTime);
	}

	void Update()
	{
		// speeds up as pairs are cleared
		if (numPairs != GameController.numPairs)
		{
			numPairs = GameController.numPairs;
			speed = speed * 1.1f;
			rb.velocity = Vector3.right * speed;
		}

		// implements screen wrap-around
		if (transform.position.x > rightBound)
		{ transform.position = new Vector3(leftBound + buffer, transform.position.y, transform.position.z); }
		if (transform.position.x < leftBound)
		{ transform.position = new Vector3(rightBound - buffer, transform.position.y, transform.position.z); }
	}

	// switches sprite, called every few seconds to fake animation
	void SwitchSprite()
	{
		if (selectedSprite == 0)
		{ selectedSprite = 1; }
		else
		{ selectedSprite = 0; }
		GetComponent<SpriteRenderer>().sprite = sprites[selectedSprite]; 
	}
}
