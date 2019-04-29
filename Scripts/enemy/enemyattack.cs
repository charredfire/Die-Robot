using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script handles the targeting and activation of enemy attacks via colliders and handles the associated special effects
public class enemyattack : MonoBehaviour {
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	public GameObject PointofOrigin;//point from which the laser fires
	LineRenderer laser;
	public float range = 10f;
	public float rotationSpeed = 10f;
	AudioSource lasersound;

	Animator anim;
	GameObject player;
	health playerHealth;
	enemyHealth enemyHealth;
	bool playerInRange;
	float timer;
	bool structInRange;
	GameObject Target;
	private bool generatoractive;//is this generator turned on?
	
	void Start () {//when this enemy is instantiate this function runs and populates all of the necessary variables
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <health> ();
		enemyHealth = GetComponent<enemyHealth>();
		anim = GetComponent <Animator> ();
		laser = PointofOrigin.GetComponent<LineRenderer> ();
		lasersound = laser.GetComponent<AudioSource> ();
	}

	void OnTriggerEnter (Collider other)//collider determines if the player in range to be fired at by the laser
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
		if (other.CompareTag ("struct") || other.CompareTag("Wall")) {//determines of there is a player structure in range
			other.GetComponent<Collider> ();
			if(!(other.isTrigger)){
			structInRange = true;
			//structhealth structhealth = Target.GetComponent<structhealth>();//this is not necessary as destroying the object triggers the onTriggerExit function
			Target = other.gameObject;
			}

		}
		if (other.CompareTag ("generator")) {//if a generator is in range check to see if it is active if it is shoot it
			generatoractive = other.GetComponent<generators> ().isactive;
			if (generatoractive == true) {
				other.GetComponent<Collider> ();
				if(!(other.isTrigger)){
					structInRange = true;
					//structhealth structhealth = Target.GetComponent<structhealth>();
					Target = other.gameObject;
				}
			}
		}
	}


	void OnTriggerExit (Collider other)//when an targeted object leaves the collider clean up
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
		if (other.CompareTag ("struct") || other.CompareTag("Wall") || other.CompareTag("generator")) {
			structInRange = false;
			//structhealth structhealth = null;
			Target = null;
		}
	}


	void Update ()//this function primarily checks the timing and sees if an attack is valid
	{
		timer += Time.deltaTime;

		//structhealth structhealth = Target.GetComponent<structhealth>();
		if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) {
			Attack ();
		}
		if (Target != null) {
			structhealth structhealth = Target.GetComponent<structhealth>();
			//RotateTowards (Target);//this will taken care of in the movement script
		}
		if (timer >= timeBetweenAttacks && structInRange && enemyHealth.currentHealth > 0 && Target != null) {
			AttackStruct ();
			//RotateTowards (Target);
		}

	}

	void Attack ()//executes the firing of the laser at the player with all of the sound effects and actually deals the damage
	{
		timer = 0f;

		if(playerHealth.currentHealth > 0)
		{
			lasersound.Play ();
			laser.enabled = true;
			laser.SetPosition (0, PointofOrigin.transform.position);
			laser.SetPosition (1, PointofOrigin.transform.position + (transform.forward) * range);
			playerHealth.TakeDamage (attackDamage);//damages the player
			Invoke ("Disable", 0.14f);
		}
	}

	void AttackStruct(){// same as the Attack() function but this targets structures
		timer = 0f;
		structhealth structhealth = Target.GetComponent<structhealth> ();
		if (structhealth.currentHealth > 0) {
			lasersound.Play ();
			laser.enabled = true;
			laser.SetPosition (0, PointofOrigin.transform.position);
			laser.SetPosition (1, PointofOrigin.transform.position + (transform.forward) * range);
			structhealth.TakeDamage (attackDamage);//damages strucutres
			Invoke ("Disable", 0.14f);
		}
		if (structhealth.currentHealth <= 0) {//if structure is destroyed move on
			Target = null;
		}
	}

		void Disable()//turns of the laser
	{
		laser.enabled = false;
	}

	void RotateTowards(GameObject Target){//this function is actually unnecessary as it is taken care of by enemy movement script
		if (Target != null) {
			Vector3 direction = (Target.transform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation (direction);
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
		}


	}
	
}
