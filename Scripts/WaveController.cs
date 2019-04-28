using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour {

	public int WaveNumber =0;
	public GameObject[] ship;
	private int x;
	private int y;
	private int z;
	private int w;
	public int enemynum = 5;
	private int save;
	public int increment = 2;
	public GameObject isEnemy;
	public float TimeBetweenWave = 30f;
	private float timestamp;
	public Text Wave;
	public Text prompt;
	// Use this for initialization
	void Start(){
		timestamp = Time.time;
		save = enemynum;
	}
	// Update is called once per frame
	void Update () {
		Wave.text = "Wave " + WaveNumber;
		isEnemy = GameObject.FindWithTag ("Enemy");
		if (isEnemy == null && timestamp <= Time.time) {
			prompt.enabled = true;
		}
		if (Input.GetButtonDown ("Submit") && isEnemy== null && timestamp <= Time.time ) {
			prompt.enabled = false;
			WaveNumber++;
			timestamp = Time.time + TimeBetweenWave;
			x = Random.Range (0, 4);
			enemynum = enemynum + (increment * WaveNumber);
			if (WaveNumber >= 3) {
				Wavethree();

				if(WaveNumber == 3) 
				{
					enemynum = save;
				}
			}
			if (WaveNumber >= 5) {
				Wavefive();

				if(WaveNumber == 5) 
				{
					enemynum = save;
				}
			}
			if (WaveNumber >= 8) {
				Waveten ();

				if(WaveNumber == 8)
				{
					enemynum = save;
				}
			}

				UFO UFO = ship [x].GetComponent<UFO> ();
				UFO.control (enemynum);
			}
		}


	void Wavethree()
	{
		w = Random.Range (0, 4);
		while( x== w){
			w = Random.Range (0, 4);
		}
	UFO UFO = ship [w].GetComponent<UFO> ();
	UFO.control (enemynum);

	}

	void Wavefive()
	{
		y = Random.Range (0, 4);
		while(( y== w) || (y == x)){
			y = Random.Range (0, 4);
		}
		UFO UFO = ship [y].GetComponent<UFO> ();
		UFO.control (enemynum);
	}

	void Waveten()
	{
		z = Random.Range (0, 4);
		while(( z== w) || (z == x) || (z == y)){
			z = Random.Range (0, 4);
		}
		UFO UFO = ship [z].GetComponent<UFO> ();
		UFO.control (enemynum);
	}
}
