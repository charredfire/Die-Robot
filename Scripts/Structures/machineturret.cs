using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineturret : MonoBehaviour {
	//public float ROF;
	//public float damage;
	private Transform target;
	private Quaternion targetpos;
	public GameObject guntip;
	public float ROF;
	private float timestamp;


	void OnTriggerEnter (Collider other){

		 if(other.CompareTag ("Enemy")) {
			target = other.transform;
			//fire fire = guntip.GetComponent<fire>();
			//fire.active(true);

		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Enemy")) {
			target = null;
		}
	}

	void Awake()
	{
		timestamp = Time.time;
	}
	void Update()
	{
		if (target != null) {
			Vector3 targetdir = transform.position - target.position;
			targetdir.y = 0;
			transform.forward = targetdir * -1;
			fire fire = guntip.GetComponent<fire> ();
			if (timestamp <= Time.time) {
				timestamp = Time.time + ROF;
				fire.active (true);
			}
		} 
	}

	

}
