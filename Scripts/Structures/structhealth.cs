using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class structhealth : MonoBehaviour {
	public int health;
	public int currentHealth;
	//bool damaged;
	//bool destroyed;
	// Use this for initialization
	public bool generator = false;
	void Awake () {
		currentHealth = health;

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void TakeDamage (int amount)
	{
		//damaged = true;

		currentHealth -= amount;

		//healthSlider.value = currentHealth;

		//playerAudio.Play ();

		if (currentHealth <= 0) {
			if (gameObject.CompareTag ("generator")){
					generator = GetComponent<generators> ().isactive;
				if(generator)
				global.totpower = global.totpower - 5;
				//generator = false;
			}
			Destroy (gameObject);
		}
	}
}
