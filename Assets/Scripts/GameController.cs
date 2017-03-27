using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Main game logic, including spawning and round separation
public class GameController : MonoBehaviour
{
	static public int numPairs = -1;		// number of matching pairs of targets to spawn
	static public int score;				// player's score
	static public int lives;				// player's lives
	
	public GameObject[] livesUI;			// lives displayed onscreen
	public Sprite heart;					// images for lives UI
	public Sprite half;
	public Text scoreText;					// score displayed onscreen
	public Text highScoreText;				// high score displayed onscreen

	public GameObject[] rounds;				// list of images to display at the start of each round
	public GameObject[] allGuys;			// total pool of male targets
	public GameObject[] allGirls;			// total pool of female targets
	public GameObject[] tables;				// obstacles to spawn
	public GameObject[] balloons;
	public GameObject[] lights;
	public GameObject[] speakers;
	public GameObject teacher;
	public GameObject black;				// used for background switching
	public GameObject end, win, lose;		// used for game end

	private int round = 0;					// player's current round
	private int numTypes = 0;				// number of different types of targets to spawn
	private float speed = 0;				// speed targets move

	private Vector3 balloonSpawn = new Vector3(10f, .36f, 0);
	private Quaternion rotZero = new Quaternion(0, 0, 0, 0);
	private GameObject[] guysToSpawn;		// list of possible male targets depending on current round
	private GameObject[] girlsToSpawn;		// list of possible female targets depending on current round
	private static float guysSpawnY = 3.08f;	// location of top row of targets
	private static float girlsSpawnY = 1.71f;	// location of second row of targets
	private Vector3[] guysSpawnLocs1 = new[]	// pre-defined spawn locations based on round and movement speed
		{											// created so all targets start onscreen and player doesn't
			new Vector3(-9.6f, guysSpawnY, 0),		// have to wait to start shooting
			new Vector3(-7.68f, guysSpawnY, 0),
			new Vector3(-5.76f, guysSpawnY, 0),
			new Vector3(-3.84f, guysSpawnY, 0),
			new Vector3(-1.92f, guysSpawnY, 0),
			new Vector3(0, guysSpawnY, 0),
			new Vector3(1.92f, guysSpawnY, 0),
			new Vector3(3.84f, guysSpawnY, 0),
			new Vector3(5.76f, guysSpawnY, 0),
			new Vector3(7.68f, guysSpawnY, 0),
			new Vector3(9.6f, guysSpawnY, 0)
		};
	private Vector3[] girlsSpawnLocs1 = new[]
		{
			new Vector3(-9.6f, girlsSpawnY, 0),
			new Vector3(-7.68f, girlsSpawnY, 0),
			new Vector3(-5.76f, girlsSpawnY, 0),
			new Vector3(-3.84f, girlsSpawnY, 0),
			new Vector3(-1.92f, girlsSpawnY, 0),
			new Vector3(0, girlsSpawnY, 0),
			new Vector3(1.92f, girlsSpawnY, 0),
			new Vector3(3.84f, girlsSpawnY, 0),
			new Vector3(5.76f, girlsSpawnY, 0),
			new Vector3(7.68f, girlsSpawnY, 0),
			new Vector3(9.6f, girlsSpawnY, 0)
		};
	private Vector3[] guysSpawnLocs2 = new[]
		{
			new Vector3(-9.63f, guysSpawnY, 0),
			new Vector3(-7.87f, guysSpawnY, 0),
			new Vector3(-6.13f, guysSpawnY, 0),
			new Vector3(-4.37f, guysSpawnY, 0),
			new Vector3(-2.61f, guysSpawnY, 0),
			new Vector3(-.87f, guysSpawnY, 0),
			new Vector3(.87f, guysSpawnY, 0),
			new Vector3(2.61f, guysSpawnY, 0),
			new Vector3(4.37f, guysSpawnY, 0),
			new Vector3(6.13f, guysSpawnY, 0),
			new Vector3(7.87f, guysSpawnY, 0),
			new Vector3(9.63f, guysSpawnY, 0)
		};
	private Vector3[] girlsSpawnLocs2 = new[]
		{
			new Vector3(-9.63f, girlsSpawnY, 0),
			new Vector3(-7.87f, girlsSpawnY, 0),
			new Vector3(-6.13f, girlsSpawnY, 0),
			new Vector3(-4.37f, girlsSpawnY, 0),
			new Vector3(-2.61f, girlsSpawnY, 0),
			new Vector3(-.87f, girlsSpawnY, 0),
			new Vector3(.87f, girlsSpawnY, 0),
			new Vector3(2.61f, girlsSpawnY, 0),
			new Vector3(4.37f, girlsSpawnY, 0),
			new Vector3(6.13f, girlsSpawnY, 0),
			new Vector3(7.87f, girlsSpawnY, 0),
			new Vector3(9.63f, girlsSpawnY, 0)
		};
	private Vector3[] guysSpawnLocs3 = new[]
		{
			new Vector3(-9.6f, guysSpawnY, 0),
			new Vector3(-8.2f, guysSpawnY, 0),
			new Vector3(-6.8f, guysSpawnY, 0),
			new Vector3(-5.4f, guysSpawnY, 0),
			new Vector3(-4f, guysSpawnY, 0),
			new Vector3(-2.6f, guysSpawnY, 0),
			new Vector3(-1.2f, guysSpawnY, 0),
			new Vector3(.2f, guysSpawnY, 0),
			new Vector3(1.6f, guysSpawnY, 0),
			new Vector3(3f, guysSpawnY, 0),
			new Vector3(4.4f, guysSpawnY, 0),
			new Vector3(5.8f, guysSpawnY, 0),
			new Vector3(7.2f, guysSpawnY, 0),
			new Vector3(8.6f, guysSpawnY, 0),
			new Vector3(10f, guysSpawnY, 0)
		};
	private Vector3[] girlsSpawnLocs3 = new[]
		{
			new Vector3(-9.6f, girlsSpawnY, 0),
			new Vector3(-8.2f, girlsSpawnY, 0),
			new Vector3(-6.8f, girlsSpawnY, 0),
			new Vector3(-5.4f, girlsSpawnY, 0),
			new Vector3(-4f, girlsSpawnY, 0),
			new Vector3(-2.6f, girlsSpawnY, 0),
			new Vector3(-1.2f, girlsSpawnY, 0),
			new Vector3(.2f, girlsSpawnY, 0),
			new Vector3(1.6f, girlsSpawnY, 0),
			new Vector3(3f, girlsSpawnY, 0),
			new Vector3(4.4f, girlsSpawnY, 0),
			new Vector3(5.8f, girlsSpawnY, 0),
			new Vector3(7.2f, girlsSpawnY, 0),
			new Vector3(8.6f, girlsSpawnY, 0),
			new Vector3(10f, girlsSpawnY, 0)
		};

	// Singleton
	public static GameController instance;

	public static GameController getInstance()
	{ return instance; }

	// resets score and starts first round
	void Start ()
	{
		instance = this;
		score = 0;
		highScoreText.text = "High Score: " + Menu.highScore;
		nextRound();
	}

	// updates score and checks for end of round
	void Update()
	{
		if (lives == 0)		// game lost, invoke ending
		{ Invoke("Lose", 1); }

		scoreText.text = "Score: " + score;
		if (numPairs == 0)		// all pairs have been matched
		{
			if (round == 5)		// round 5 complete, game won
			{ Invoke("Win", 1); }
			else				// start next round
			{
				numPairs = -1;
				nextRound();
			}
		}
	}

	// sets values and spawns obstacles specific to each round
	void nextRound()
	{
		lives = 6;
		lifeDisplay();
		GetComponent<AudioSource>().volume = 1f;	// resets bgm volume
		Brighten();		// resets screen lightness
		round += 1;
		rounds[round - 1].SetActive(true);	// displays start of round text
		Invoke("HideRound", 3);

		switch (round)
		{
			case 1:
				numPairs = 11;
				numTypes = 4;
				speed = .6f;
				break;
			case 2:
				numPairs = 11;
				numTypes = 4;
				speed = .6f;
				InvokeRepeating("SpawnBalloons", 0, 8f);	// spawn balloons
				SpawnLightsAndSpeakers();
				break;
			case 3:
				numPairs = 12;
				numTypes = 5;
				speed = 1f;
				SpawnLightsAndSpeakers();	// reset any destroyed lights and speakers
				teacher.SetActive(true);		// spawn teacher
				SpawnTables();
				break;
			case 4:
				numPairs = 12;
				numTypes = 6;
				speed = 1.2f;
				SpawnLightsAndSpeakers();
				SpawnTables();
				break;
			case 5:
				numPairs = 15;
				numTypes = 6;
				speed = 1.3f;
				SpawnLightsAndSpeakers();
				SpawnTables();
				break;
		}

		guysToSpawn = new GameObject[numPairs];
		girlsToSpawn = new GameObject[numPairs];

		// selects a round-specific number of matching pairs from the round-specific target pool
		for (int i = 0; i < numPairs; i++)
		{
			int toSpawn = Random.Range(0, numTypes);
			guysToSpawn[i] = allGuys[toSpawn];
			girlsToSpawn[i] = allGirls[toSpawn];
		}

		// randomizes spawn order and starts spawning
		ShuffleArray(girlsToSpawn);
		Spawn(guysToSpawn.Length);
	}

	// spawns targets in their pre-defined locations (depending on round and movement speed)
	void Spawn(int numToSpawn)
	{
		for (int i = 0; i < numToSpawn; i++)
		{
			GameObject guy;
			GameObject girl;

			if (round < 3)
			{
				guy = Instantiate(guysToSpawn[i], guysSpawnLocs1[i], rotZero);
				girl = Instantiate(girlsToSpawn[i], girlsSpawnLocs1[i], rotZero);
			}
			else if (round < 5)
			{
				guy = Instantiate(guysToSpawn[i], guysSpawnLocs2[i], rotZero);
				girl = Instantiate(girlsToSpawn[i], girlsSpawnLocs2[i], rotZero);
			}
			else
			{
				guy = Instantiate(guysToSpawn[i], guysSpawnLocs3[i], rotZero);
				girl = Instantiate(girlsToSpawn[i], girlsSpawnLocs3[i], rotZero);
			}
			guy.GetComponent<Target>().speed = speed;
			girl.GetComponent<Target>().speed = -speed;
		}
	}

	// updates life UI
	public void lifeDisplay()
	{
		switch (lives)
		{
			case 6:
				livesUI[0].GetComponent<Image>().sprite = heart;
				livesUI[1].GetComponent<Image>().sprite = heart;
				livesUI[2].GetComponent<Image>().sprite = heart;
				livesUI[0].SetActive(true);
				livesUI[1].SetActive(true);
				livesUI[2].SetActive(true);
				break;
			case 5:
				livesUI[0].GetComponent<Image>().sprite = half;
				break;
			case 4:
				livesUI[0].SetActive(false);
				break;
			case 3:
				livesUI[1].GetComponent<Image>().sprite = half;
				break;
			case 2:
				livesUI[1].SetActive(false);
				break;
			case 1:
				livesUI[2].GetComponent<Image>().sprite = half;
				break;
			default:
				livesUI[0].SetActive(false);
				livesUI[1].SetActive(false);
				livesUI[2].SetActive(false);
				break;
		}
	}

	// hides the round display text after set time
	void HideRound()
	{ rounds[round - 1].SetActive(false); }

	// spawns a random balloon, called via InvokeRepeating
	void SpawnBalloons()
	{ Instantiate(balloons[Random.Range(0, balloons.Length)], balloonSpawn, rotZero); }

	// resets tables between rounds
	void SpawnTables()
	{
		foreach (GameObject table in tables)
		{
			table.GetComponent<TableHealth>().health = 2;
			table.SetActive(true);
		}
	}

	// resets lights and speakers between rounds
	void SpawnLightsAndSpeakers()
	{
		foreach (GameObject light in lights)
		{ light.SetActive(true); }
		foreach (GameObject speaker in speakers)
		{ speaker.SetActive(true); }
	}

	// resets any darkness screens created by shooting lights
	void Brighten()
	{
		GameObject[] screens = GameObject.FindGameObjectsWithTag("black");
		foreach (GameObject screen in screens)
		{ Destroy(screen); }
	}

	// shuffles array, courtesy of http://answers.unity3d.com/questions/16531/randomizing-arrays.html
	void ShuffleArray<T>(T[] array)
	{
		for (int i = array.Length - 1; i > 0; i--)
		{
			int r = Random.Range(0, i);
			T tmp = array[i];
			array[i] = array[r];
			array[r] = tmp;
		}
	}

	// displays game end (loss)
	void Lose()
	{
		AudioSource source = GetComponent<AudioSource>();
		source.Stop();
		lose.SetActive(true);
		Invoke("End", 2.5f);
	}

	// displays game end (win)
	void Win()
	{
		livesUI[0].SetActive(false);
		livesUI[1].SetActive(false);
		livesUI[2].SetActive(false);
		AudioSource source = GetComponent<AudioSource>();
		source.Stop();
		win.SetActive(true);
		Invoke("End", 4f);
	}

	// displays final game end
	void End()
	{
		end.SetActive(true);
		Invoke("LoadMenu", 5);
	}

	// loads the menu, called with delay via Invoke
	void LoadMenu()
	{ SceneManager.LoadScene("menu"); }
}
