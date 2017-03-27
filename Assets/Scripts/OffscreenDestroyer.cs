using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Automatically destroys anything that leaves the screen.  Note that the
	// "screen" includes a buffer zone to implement the wrap-around effect.
public class OffscreenDestroyer : MonoBehaviour
{
	void OnTriggerExit2D(Collider2D other)
	{ Destroy(other.gameObject); }
}
