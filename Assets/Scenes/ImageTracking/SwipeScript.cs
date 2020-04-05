using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SwipeScript : MonoBehaviour {

	Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
	float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

	[SerializeField]
	float throwForceInXandY = 1f; // to control throw force in X and Y directions

	[SerializeField]
	float throwForceInZ = 50f; // to control throw force in Z direction

	private Camera mainCamera = default;
	private Vector3 cameraForward = default;

	private ARRaycastManager ARRaycastManagerScript = default;
	private TextMeshProUGUI MainText = default;
	private bool canThrowBall = false;
	private Vector3 startBallPos;

	private Vector3 startRayPos = default;
	private Vector3 endRayPos = default;
	private Vector3 velocity = default;
	private Vector2 touchPosition = default;

	static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();



	Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	bool TryGetTouchPosition()
	{
#if UNITY_EDITOR
		if (Input.GetMouseButton(0))
		{
			var mousePosition = Input.mousePosition;
			touchPosition = new Vector2(mousePosition.x, mousePosition.y);
			return true;
		}
#else
        if (Input.touchCount > 0)
        {
            startRayPos = Input.GetTouch(0).position;
            return true;
        }
#endif

		touchPosition = default;
		return false;
	}

	void Update () 
	{
		if (!TryGetTouchPosition())
			return;

		if (ARRaycastManagerScript.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
		{
			// Raycast hits are sorted by distance, so the first one
			// will be the closest hit.
			var hitPose = s_Hits[0].pose;

			endRayPos = hitPose.position;
			MainText.text = "Throw Ball " + endRayPos.ToString();
		}

		if (canThrowBall)
		{
			// if you touch the screen
			/*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{

				// getting touch position and marking time when you touch the screen
				//touchTimeStart = Time.time;
				//startPos = Input.GetTouch(0).position;

				if (!TryGetTouchPosition())
					return;

				if (ARRaycastManagerScript.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
				{
					// Raycast hits are sorted by distance, so the first one
					// will be the closest hit.
					var hitPose = s_Hits[0].pose;

					startRayPos = hitPose.position;
					MainText.text = "Throw Ball " + startRayPos;
				}
			}*/

			// if you release your finger
			if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended))
			{
				MainText.text = "Throw Ball";

				/*if (cameraForward != null)
				{
					cameraForward = mainCamera.transform.forward;
				}*/
				// marking time when you release it
				//touchTimeFinish = Time.time;

				// calculate swipe time interval 
				//timeInterval = touchTimeFinish - touchTimeStart;

				// getting release finger position
				//endPos = Input.GetTouch(0).position;

				// calculating swipe direction in 2D space
				//direction = startPos - endPos;

				velocity = startRayPos - endRayPos;
				//rb.velocity = velocity;
				endRayPos = new Vector3(endRayPos.x, endRayPos.y + 2.0f, endRayPos.z);
				//rb.AddForce(endRayPos * -2.5f);

				// add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
				rb.isKinematic = false;
				/*rb.AddForce((-direction.x * throwForceInXandY) + cameraForward.x, 
					(-direction.y * throwForceInXandY) + cameraForward.y, 
					(throwForceInZ / timeInterval) + cameraForward.z);*/

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

	public void SetARRaycastManager(ARRaycastManager manager)
	{
		ARRaycastManagerScript = manager;
	}

	public void ThrowBall(bool status)
	{
		canThrowBall = status;
	}
}
