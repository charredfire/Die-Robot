using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// this script requires the unity navmesh to work
public class enemyMove : MonoBehaviour {

	public GameObject goal;//player to navigate too
	enemyHealth enemyHealth;
	UnityEngine.AI.NavMeshAgent nav;//unity navigational tool
	private Transform player;//player position
	private Transform wall;//wall position (walls are obstacles in the navmesh but can be targeted and destroyed
	public GameObject[] altgoal;//find active generators to attack
	public Vector3 derp;//variable for testing does not impact functionality
	public bool hasgoal = false;//are we targeting a generator?
	public  float dist =100; //how far away are we from the target
	private float refreshrate;//time stamp to recalculate path
	public GameObject target= null;//this will be filled with an active generator
	bool isactive = false;//is the generator active

	void Awake () {
		enemyHealth = GetComponent <enemyHealth> ();
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		goal = GameObject.FindWithTag ("Player"); 
		player = goal.GetComponent<Transform> ();
		refreshrate = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth.currentHealth > 0) {//&& playerHealth.currentHealth > 0)//if enemy is still alive
			
			if(refreshrate <= Time.time){
				refreshrate = Time.time + 0.5f;
				altgoal = GameObject.FindGameObjectsWithTag("generator");
				}
			foreach (GameObject gen in altgoal) {//if an active generator is found target it
				isactive = gen.GetComponent<generators> ().isactive;
				if (isactive)
					target = gen;
			}
			if (target != null)//if there is a target see how far away it is
				dist = Vector3.Distance (target.transform.position, transform.position);
			else//if now target you are infinity far away from a target
				dist = Mathf.Infinity;
			if (dist <= 15f) {//if you are closer tha 15 ft navigate to the generator
				nav.SetDestination (target.transform.position);
				hasgoal = true;
			} else//if not you do not have a generator to target
				hasgoal = false;

			if (hasgoal == false) {//if no generator is availabke to target navigate towards the player
				nav.SetDestination (player.position);
			}
			if (nav.remainingDistance <= 3) {//if we are 3 or closer to a target stop and rotate towards it
				Vector3 direction = (nav.destination - transform.position).normalized;
				direction.y = 0f;
				Quaternion newRotation = Quaternion.LookRotation (direction);
				transform.rotation = newRotation;
			}
			derp = nav.destination;
		}

		else
		{
			nav.enabled = false;
		}
	}


	}

