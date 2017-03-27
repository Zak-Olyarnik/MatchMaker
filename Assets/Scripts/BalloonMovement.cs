using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls movement of balloon obstacles, including "bobbing" up and down.
public class BalloonMovement : MonoBehaviour
{
	public float xSpeed;
	public float ySpeed;
	public float yMin;
	public float yMax;
	
	private Rigidbody2D rb;
	private float pos;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = Vector3.left * xSpeed + Vector3.up * ySpeed;
	}
	
	// makes balloon "bob" by switching direction of y-movement
	void FixedUpdate ()
	{
		pos = GetComponent<Transform>().position.y;
		if (pos > yMax)
		{ rb.velocity = Vector3.left * xSpeed + Vector3.down * ySpeed; }
		if (pos < yMin)
		{ rb.velocity = Vector3.left * xSpeed + Vector3.up * ySpeed; }
		
	}
}
