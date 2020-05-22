using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Bullet : MonoBehaviour 
{
	private TextMeshProUGUI MainText = default;
	private bool canThrowBall = false;

	private Vector3 direction = default;
	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	private LineRenderer lineRenderer = default;

	private Rigidbody rb;

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = false;
	}

	bool TryGetTouchPosition()
	{
#if UNITY_EDITOR
		if (Input.GetMouseButton(0))
		{
			var mousePosition = Input.mousePosition;
			return true;
		}
#else
        if (Input.touchCount > 0)
        {
            return true;
        }
#endif
		return false;
	}

	private void Update()
	{
		direction = transform.forward * 10.0f;
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, direction);
	}

	private void FixedUpdate()
	{
		if (canThrowBall)
		{
			rb.AddForce(direction);
		}
	}

	public void Fire(GameObject target)
	{
		direction = transform.forward * 10.0f;
		SetLineRenderer();

		rb.isKinematic = false;
		canThrowBall = true;
		MainText.text = "Fire !";
	}

	public void SetLineRenderer()
	{
		lineRenderer.positionCount = 2;
		float alpha = 1.0f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);
		lineRenderer.colorGradient = gradient;
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, transform.forward * 10f);
	}

	public void SetText(TextMeshProUGUI text)
	{
		MainText = text;
	}

	public void CanTrowBall(bool status)
	{
		canThrowBall = status;
	}
}
