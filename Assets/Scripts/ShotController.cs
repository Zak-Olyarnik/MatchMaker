using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls movement and collision checking for shots.
public class ShotController : MonoBehaviour
{
	public GameController g;

	public float speed;					// speed value multiplier
	public GameObject scoreMinus20;		// images displayed when score increases or decreases,
	public GameObject scoreMinus50;			// based on shots
	public GameObject scorePlus100;
	public GameObject badMatch;
	public GameObject black40;
	public float scoreTime;				// amount of time score image is displayed for

	public AudioClip balloonPop;		// sounds played without AudioSource because their gameobjects
	public AudioClip perfectMatch;			// get destroyed when hit
	public AudioClip tableHit;
	public AudioClip lightBreak;
	public AudioClip speakerBreak;
	public AudioClip miss;

	private GameObject target1 = null;		// first hit target
	private GameObject target2 = null;		// second hit target

	// apply movement direction based on angle of fire
	void Start()
	{
		g = GameController.getInstance();
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.up * speed;
	}

	// check for collisions with obstacles or targets, adjust and display score accordingly
		// generally: play sound, instantiate UI of score/lives effect, time-destory it, 
		// update the underlying score/lives data, and destroy the shot 
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "teacher")
		{
			AudioSource source = coll.gameObject.GetComponent<AudioSource>();
			source.Play();
			GameObject score = Instantiate(badMatch, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
			Destroy(score, scoreTime);
			GameController.lives -= 1;
			g.lifeDisplay();
			Destroy(gameObject);
		}
		else if (coll.gameObject.tag == "table")
		{
            AudioSource.PlayClipAtPoint(tableHit, new Vector3(0, 0, -10), 1f);		// objects that are deactivated/destroyed cannot play sounds so play at point
			coll.gameObject.GetComponent<TableHealth>().health -= 1;				// table takes two hits to destroy 
			GameObject score = Instantiate(scoreMinus50, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
			GameController.score -= 50;
			Destroy(score, scoreTime);
			Destroy(gameObject);
		}
		else if (coll.gameObject.tag == "balloon")
		{
            AudioSource.PlayClipAtPoint(balloonPop, new Vector3(0, 0, -10), 1f);
			GameObject score = Instantiate(scoreMinus20, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
			GameController.score -= 20;
			Destroy(score, scoreTime);
			Destroy(gameObject);
			coll.gameObject.SetActive(false);
		}
		else if (coll.gameObject.tag == "light")
		{
            AudioSource.PlayClipAtPoint(lightBreak, new Vector3(0, 0, -10), 1f);
			GameObject score = Instantiate(scoreMinus20, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
			GameController.score -= 20;
			Destroy(score, scoreTime);
			Destroy(gameObject);
			Instantiate(black40, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
			coll.gameObject.SetActive(false);
		}
		else if (coll.gameObject.tag == "speaker")
		{
            AudioSource.PlayClipAtPoint(speakerBreak, new Vector3(0, 0, -10), 1f);
			GameObject score = Instantiate(scoreMinus20, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
			GameController.score -= 20;
			Destroy(score, scoreTime);
			Destroy(gameObject);
			g.GetComponent<AudioSource>().volume = g.GetComponent<AudioSource>().volume - .4f;
			coll.gameObject.SetActive(false);
		}
        else if (coll.gameObject.tag == "target")
		{
            if (target1 == null)			// first target hit, store it
			{ target1 = coll.gameObject; }
			else							// second target hit, compare match IDs
			{
				target2 = coll.gameObject;
				int ID1 = target1.GetComponent<Target>().matchID;
				int ID2 = target2.GetComponent<Target>().matchID;
				int gID1 = target1.GetComponent<Target>().genderID;
				int gID2 = target2.GetComponent<Target>().genderID;
				if (ID1 == ID2 && gID1 != gID2)		// good match, increase score and clear targets
				{
					AudioSource.PlayClipAtPoint(perfectMatch, new Vector3(0, 0, -10), 1f);
					GameObject score = Instantiate(scorePlus100, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
					GameController.score += 100;
					Destroy(score, scoreTime);
					Destroy(gameObject);
					Destroy(target1);
					Destroy(target2);
					GameController.numPairs -= 1;
				}
				else								// not a match
				{
					AudioSource.PlayClipAtPoint(miss, new Vector3(0, 0, -10), 1f);
					GameObject score = Instantiate(badMatch, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
					Destroy(score, scoreTime);
					GameController.lives -= 1;
					g.lifeDisplay();
					Destroy(gameObject);
				}
			}
		
		}
	}
}
