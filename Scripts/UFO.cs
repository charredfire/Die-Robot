using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour {
	public GameObject[] place;//chooses spawn location
	private int i = 0;
	private GameObject goal;// both are involved int getting UFO to said location
	private Transform goalpos;
	public float speed;
	public GameObject enemy; // enemy type dropped
	private bool active;// is dropping enemies
	private Transform enemypos;
	public bool called = false; // starts the process of moving towards a spawn
	private Vector3 reset;
	private bool ret= false;// returning to original spot
	private bool one = true;
	public int enemynum = 10;
	public bool ready= true;

	// Use this for initialization
	void Start () {
		reset = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		if (called == true) {
			ready = false;
			i = Random.Range (0, 7);
			goal = place [i];
			goalpos = goal.GetComponent<Transform> ();
			goalpos.position = new Vector3 (goalpos.position.x, 4.2f, goalpos.position.z);
			enemypos = goal.GetComponent<Transform> (); 
			called = false;
		}
		if (goal != null) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, goalpos.position, step);
		}
		if (active == true) {
			if (active == true) {
				for (int x = 0; x < enemynum; x++) {
					spawnEnemy ();
				}
				active = false;
				ret = true;
				goal = null;
			}
			}
			if (ret == true) {
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, reset, step);
				if (transform.position == reset) {
					ret = false;
					ready = true;
				}
			}

	}

	void OnTriggerEnter (Collider other){

		if(other.CompareTag("spawnpoint")){
			if (one==true) {
				active = true;
				one = false;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("spawnpoint"))
			one = true;
	}

	void spawnEnemy()
	{
		enemypos.position = new Vector3 (goalpos.position.x, 2, goalpos.position.z);
			Instantiate(enemy, enemypos.position, enemypos.rotation);

	}

	public void control(int num){
		called = true;
		enemynum = num;
	}
}
