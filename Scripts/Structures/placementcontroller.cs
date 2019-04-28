using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class global
{
	public static int money = 100;
	public static int power = 0;//amount used
	public static int totpower =0;//total amount of power
}
public class placementcontroller : MonoBehaviour {
	//spawnable objects
	[SerializeField]
	private GameObject Wall;
	[SerializeField]
	private GameObject GunTurret;

	public Text money;
	public Text powert;

	public GameObject[] structsel;//decalare the array
	public float[] height;
	public int[] cost;
	public int[] power;
	//load it
	//structsel[0] = Wall;
	//structsel[1] = GunTurret;
	public Image WallHUD;
	public Image MachineHUD;


	float camRayLength = 100f;
	private GameObject CurrentStructure;
	int floorMask;
	private int i =0;
	void Awake()// new function type for me, similar to start but gets called regardless of whether the script is active
	{
		floorMask = LayerMask.GetMask ("floor"); //assigning variables
		//Anim = GetComponent <Animator> ();
		//playerRigidbody = GetComponent <Rigidbody> ();
	}
	// Update is called once per frame
	void Update () {

		money.text = "Money" + global.money;
		powert.text = global.power +"/"+ global.totpower;

		//selects what to spawn
		if(Input.GetKey(KeyCode.Alpha1)){ //select the first spot in the array or wall
			i=0;
		}
		if(Input.GetKey(KeyCode.Alpha2)){
			i=1;
		}
		HUD ();//expand the appropiate option in the HUD

		//does the spawning
		if(Input.GetMouseButtonDown(1))
		{

			//1 place a new object
			if(CurrentStructure == null && cost[i] <= global.money && power[i] <= (global.totpower -global.power)){
				CurrentStructure = Instantiate(structsel[i]);
				global.money = global.money - cost [i];
				global.power = global.power + power [i];
			}
			//or 2 move the new object to the approiate position
			/*if(CurrentStructure !=null){
				MoveToPosition();
				ReleaseIfClicked();*/

		}
		if (CurrentStructure != null) {
			MoveToPosition ();
			ReleaseIfClicked ();
			RotateObject ();
		}
}
	void MoveToPosition()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 position  = floorHit.point;
			position.y =  height[i];
			CurrentStructure.transform.position = position;

		}
	}

	private void ReleaseIfClicked()
	{
		if (!(Input.GetMouseButton(1)))
		{
			//CurrentStructure = null;
			var col = CurrentStructure.GetComponent<Collider> ();
			col.enabled = true;
			CurrentStructure = null;
		}
	}

	private void RotateObject(){

		if (Input.GetKeyDown(KeyCode.Space)) {
			float angle = CurrentStructure.transform.eulerAngles.y + 45;
			CurrentStructure.transform.eulerAngles = new Vector3 (transform.eulerAngles.x, angle, transform.eulerAngles.z);
		}
	}

	void HUD(){
		if (i == 0) {
			WallHUD.rectTransform.sizeDelta = new Vector2 (40, 40);
		} else {

			WallHUD.rectTransform.sizeDelta = new Vector2 (25, 25);
		}
		if(i==1){
			MachineHUD.rectTransform.sizeDelta = new Vector2 (40, 40);
		} else {

			MachineHUD.rectTransform.sizeDelta = new Vector2 (25, 25);
		}
	}

	}
		
