using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {

	public int Damage;
	public float range;
	LineRenderer gunLine;
	Ray shootRay = new Ray();
	RaycastHit shootHit;
	int shootableMask;
	//float effectsDisplayTime = 0.2f;
	AudioSource gunAudio;

	// Use this for initialization
	void Awake() {
		shootableMask = LayerMask.GetMask ("shootable");
		gunLine = GetComponent <LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		//timestamp = Time.time;
	}
		
	
	// Update is called once per frame
	public void active(bool active) {
		if (active == true)
			
			gunAudio.Play ();

			//gunLight.enabled = true;

			//gunParticles.Stop ();
			//gunParticles.Play ();

			gunLine.enabled = true;
			gunLine.SetPosition (0, transform.position);

			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			enemyHealth enemyHealth = shootHit.collider.GetComponent <enemyHealth> ();
			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage (Damage, shootHit.point);
			}
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}

		Invoke ("DisableEffects", 0.2f);
	}

	public void DisableEffects(){
		gunLine.enabled = false;
		//gunLight.enabled = false;
	}
}
