using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls player aiming and shooting.
public class PlayerController : MonoBehaviour
{
	public GameObject shot;			// prefab to instantiate
	public float shotSpeed;			// speed shots travel
	public float fireRate;          // delay between firing shots
	private float nextFire;			// time next shot can be fired
	
	// Update is called once per frame
	void Update ()
	{
		// aim
		Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 aim = Input.mousePosition - playerPosition;

		// clamp angle - for some reason the angles seem to range from -270 to 90, with 0 = straight up
		float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 90; 
		if (angle < -180 || angle > 60)
		{ angle = 60; }
		if (angle < -60)
		{ angle = -60; }
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		Vector3 shotOffsetPos = transform.position + transform.up;		// spawn offsets to look good with sprites

		// fire
		if (Input.GetButton("Fire1") && Time.time > nextFire)   // checks for fire button and if time delay has passed
        {
            nextFire = Time.time + fireRate;
			Instantiate(shot, shotOffsetPos, transform.rotation);
			AudioSource source = GetComponent<AudioSource>();
			source.Play();
		}

	}
}
