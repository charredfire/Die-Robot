using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script handles the enemies health, including initialazation, taking damage, and being destroyed
public class enemyHealth : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
	bool isDead = false;
	
	void Awake () {//when this object is instantiated ensure it has the proper amount of  
		currentHealth = startingHealth;
	}
	

	public void TakeDamage (int amount, Vector3 hitPoint)//right now just subtacts the proper amount of health, the vector is for the future inclusion 
	{//of a particle system activating at the point of impact
		if(isDead)
			return;

		currentHealth -= amount;

		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()//destroy the gameObject
	{
		
		Destroy (gameObject);
		global.money = global.money + 2;//give the player some money to build defenses
	}

}
