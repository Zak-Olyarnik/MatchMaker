using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAndSpeakerAnimation : MonoBehaviour
{
	public float spriteTime;		// time to switch sprites for animation
	public Sprite[] sprites;		// sprites that are switched to fake animation

	private int selectedSprite = 0;		// currently displayed sprite index

	void Start()
	{ InvokeRepeating("SwitchSprite", spriteTime, spriteTime); }

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
