using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed = 0.6f;
	Vector3 movement;
	public Animator Anim;
	public Rigidbody playerRigidbody;
	public GameObject player;
	int floorMask;
	float camRayLength = 100f;

	void Awake()// new function type for me, similar to start but gets called regardless of whether the script is active
	{
		floorMask = LayerMask.GetMask ("floor"); //assigning variables
		//Anim = GetComponent <Animator> ();
		//playerRigidbody = GetComponent <Rigidbody> ();
	}
	void FixedUpdate() // fires every physics update
	{
		float h = Input.GetAxisRaw ("Horizontal");// raw axis only has value of 1 or -1 meaning that the character
		float v = Input.GetAxisRaw ("Vertical");//will snap to full speed also horizontal and vertical are preprogammed
		// to be the standard movemetn inputs for keyboard and controller 
		move (h,v);
		Turning ();
		animating (h,v);
	}
	void Turning()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotation);
		}
	}
	void move(float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime; // normalizes so that player always goes the same speed
		playerRigidbody.MovePosition (transform.position + movement); // does the actual moving
	}
	void animating(float h, float v)
	{
		bool idle = h == 0f && v == 0f;
		Anim.SetBool ("still", idle);


		if (player.transform.eulerAngles.y > 45 && player.transform.eulerAngles.y < 135) {
			bool forward = v > 0f;
			Anim.SetBool ("left", forward);//find if running forward

			bool back = v < 0f;
			Anim.SetBool ("right", back);

			bool right = h > 0f;
			Anim.SetBool ("forwards", right);

			bool left = h < 0f;
			Anim.SetBool ("backwards", left);
		} else if (player.transform.eulerAngles.y > 135 && player.transform.eulerAngles.y < 225) {
			bool forward = v > 0f;
			Anim.SetBool ("backwards", forward);//find if running forward

			bool back = v < 0f;
			Anim.SetBool ("forwards", back);

			bool right = h > 0f;
			Anim.SetBool ("left", right);

			bool left = h < 0f;
			Anim.SetBool ("right", left);
		} else if (player.transform.eulerAngles.y > 225 && player.transform.eulerAngles.y < 315) {
			bool forward = v > 0f;
			Anim.SetBool ("right", forward);//find if running forward

			bool back = v < 0f;
			Anim.SetBool ("left", back);

			bool right = h > 0f;
			Anim.SetBool ("backwards", right);

			bool left = h < 0f;
			Anim.SetBool ("forwards", left);
		} else {
			//player.transform.eulerAngles.y > 315 && player.transform.eulerAngles.y < 45) 
			bool forward = v > 0f;
			Anim.SetBool ("forwards", forward);//find if running forward

			bool back = v < 0f;
			Anim.SetBool ("backwards", back);

			bool right = h > 0f;
			Anim.SetBool ("right", right);

			bool left = h < 0f;
			Anim.SetBool ("left", left);
		}

	}
}
