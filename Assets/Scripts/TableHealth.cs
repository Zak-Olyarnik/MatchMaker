using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets tables inactive when shot.
public class TableHealth : MonoBehaviour
{
	public int health = 2;		// controls table health
	public Sprite normal;		// original sprite
	public Sprite damaged;		// new, damaged sprite

	// switches sprite then destroys table when damaged
	void Update()
	{
		if (health == 2)
		{ GetComponent<SpriteRenderer>().sprite = normal; }
		else if (health == 1)
		{ GetComponent<SpriteRenderer>().sprite = damaged; }
		else if (health == 0)
		{ gameObject.SetActive(false); }
	}
}
