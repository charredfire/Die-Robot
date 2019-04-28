using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class health : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	//public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


	Animator anim;
	AudioSource playerAudio;
	PlayerMovement playerMovement;
	shooting playerShooting;
	bool isDead;
	bool damaged;

	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		playerShooting = GetComponentInChildren <shooting> ();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame

	void Update () {
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

		//playerAudio.Play ();

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	public void Restore(int amount)
	{
		if (currentHealth < startingHealth) {
			currentHealth = currentHealth + amount;
			healthSlider.value = currentHealth;
		}
	}


	void Death ()
	{
		isDead = true;

		playerShooting.DisableEffects ();

		anim.SetTrigger ("Die");

		//playerAudio.clip = deathClip;
		//playerAudio.Play ();

		playerMovement.enabled = false;
		playerShooting.enabled = false;
		Invoke ("RestartLevel", 2);
	}


	public void RestartLevel ()
	{
		SceneManager.LoadScene (0);
		global.money = 100;
		global.power = 0;
		global.totpower = 0;
	}
}
