using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class generators : MonoBehaviour {
	public bool isactive = false;
	private bool initialize = true;
	health playerHealth;
	GameObject player;
	public int regenA = 10;
	public float ROH = 0.25f;
	private float Timestamp;
	bool inrange= false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <health> ();
		Timestamp = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		if (inrange == true && Input.GetKeyDown (KeyCode.Space) && initialize== true) {// activates the generator
			isactive = true;
			activate ();
			initialize = false;
		}

		if (inrange == true && isactive == true && Timestamp <= Time.time) {
			Timestamp = Time.time + ROH;
			playerHealth.Restore (regenA);
		}




	}

		void activate()
	{
		//global.power =global.power +5;
		global.totpower = global.totpower + 5;
		var NavMeshObstacle = gameObject.GetComponent<NavMeshObstacle>();
		NavMeshObstacle.enabled = false;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == player)
			inrange = true;
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == player)
			inrange = false;
	}

}
