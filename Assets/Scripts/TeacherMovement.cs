using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Randomly changes speed and direction of movement, or pauses entirely.
public class TeacherMovement : MonoBehaviour
{
	public float rightBound;	// absolute right edge (for OffscreenDestroyer) 
	public float leftBound;		// absolute left edge (for OffscreenDestroyer) 
	public float buffer;		// used to create a safe spawning buffer away from the absolute edges
	public float change;		// time between movement changes

	private Rigidbody2D rb;		// controls movement
	private float speed;		// speed of movement, randomly assigned
	private int dir;			// direction of movement, left, right, or zero

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		InvokeRepeating("RandomizeMovement", 0, change);
	}
	
	void Update ()
	{
		// implements screen wrap-around
		if (transform.position.x > rightBound)
		{ transform.position = new Vector3(leftBound + buffer, transform.position.y, transform.position.z); }
		if (transform.position.x < leftBound)
		{ transform.position = new Vector3(rightBound - buffer, transform.position.y, transform.position.z); }
	}

	// randomly changes speed and direction of movement, or pauses entirely
	void RandomizeMovement()
	{
		dir = Random.Range(-1, 2);				// Random.Range int is max-exclusive
		speed = Random.Range(0, 3.5f);			// Random.Range float is max-inclusive
		rb.velocity = Vector3.left * dir * speed;
	}
}
