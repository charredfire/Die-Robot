using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

	public Transform hand;
	//public GameObject Gun;
	public float fixedRotation= 0f;
	Transform t;
	public GameObject gunttip;
	public GameObject player;
	public float adjust = 1;
	void Awake(){
		transform.SetParent (hand);
	
	}

		
	// Use this for initialization
	void Start () {
		t = transform;
	}
	
	// Update is called once per frame
	void Update () {
		float p = player.transform.eulerAngles.y + adjust;
		t.eulerAngles = new Vector3 (t.eulerAngles.x,p, fixedRotation);
		transform.SetParent (hand);
		gunttip.transform.rotation = player.transform.rotation;
		//hand.transform.rotation= player.transform.rotation;
		//transform.rotation = player.transform.rotation;
		//t.eulerAngles.y = t.eulerAngles.y - adjust;
	}
		
}
