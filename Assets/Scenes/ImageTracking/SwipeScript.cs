using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwipeScript : MonoBehaviour {

	Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
	float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

	[SerializeField]
	float throwForceInXandY = 1f; // to control throw force in X and Y directions

	[SerializeField]
	float throwForceInZ = 50f; // to control throw force in Z direction

	private Camera mainCamera = default;
	private Vector3 cameraForward = default;

	private TextMeshProUGUI MainText = default;
	private bool canThrowBall = false;
	private Vector3 startBallPos;

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (canThrowBall)
		{
			// if you touch the screen
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{

				// getting touch position and marking time when you touch the screen
				touchTimeStart = Time.time;
				startPos = Input.GetTouch(0).position;
			}

			// if you release your finger
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				MainText.text = "Throw Ball";

				if (cameraForward != null)
				{
					cameraForward = mainCamera.transform.forward;
				}
				// marking time when you release it
				touchTimeFinish = Time.time;

				// calculate swipe time interval 
				timeInterval = touchTimeFinish - touchTimeStart;

				// getting release finger position
				endPos = Input.GetTouch(0).position;

				// calculating swipe direction in 2D space
				direction = startPos - endPos;

				// add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
				rb.isKinematic = false;
				rb.AddForce((-direction.x * throwForceInXandY) + cameraForward.x, 
					(-direction.y * throwForceInXandY) + cameraForward.y, 
					(throwForceInZ / timeInterval) + cameraForward.z);

				/*rb.AddForce((-direction.x * throwForceInXandY),
						(-direction.y * throwForceInXandY),
						(throwForceInZ / timeInterval));*/

				//Destroy(gameObject, 3f);

			}
		}
			
	}

	public void SetCamera(Camera newCamera)
	{
		mainCamera = newCamera;
		startBallPos = transform.position;
	}

	public void SetText(TextMeshProUGUI text)
	{
		MainText = text;
	}

	public void ThrowBall(bool status)
	{
		canThrowBall = status;
	}
}
