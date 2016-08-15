using UnityEngine;
using System.Collections;

public class TouchCtrl : MonoBehaviour {
	public Transform target;
	[Range (0.1f ,10)]
	public float sensitivity = 1;

	private float rotateSensitivity=4;
	private float pinchSensitivity=0.005f;
	private float rotateMin=0.1f;
	private float pinchMin=0.1f;

	private Quaternion desiredRotation;
	private Vector3 scale;
	private Vector3 position;
	void Start()
	{
		if (target != null)
			Set (target);
	}

	public void Set(Transform t)
	{
		target = t;
		desiredRotation = target.localRotation;
		scale = target.localScale;
		position = target.localPosition;
	}


	void LateUpdate() {
		if (target == null)
			return;
		float pinchAmount = 0;
		DetectTouchMovement.Calculate();

		if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > rotateMin) { // rotate
			Vector3 rotationDeg = Vector3.zero;
			rotationDeg.y = -DetectTouchMovement.turnAngleDelta*rotateSensitivity;
			desiredRotation *= Quaternion.Euler(rotationDeg);
		}
		if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > pinchMin) { // zoom
			pinchAmount = DetectTouchMovement.pinchDistanceDelta;
		}


		scale += Vector3.one * pinchAmount*pinchSensitivity;
		if(scale.x<0.1f)
			scale = Vector3.one * 0.1f;
		position += new Vector3 (DetectTouchMovement.movementDelta.x, 0, DetectTouchMovement.movementDelta.y);

		target.localRotation = Quaternion.Lerp (target.localRotation, desiredRotation, Time.deltaTime * 10);
		target.localScale = Vector3.Lerp (target.localScale, scale, Time.deltaTime * 10);
		target.localPosition = Vector3.Lerp (target.localPosition, position, Time.deltaTime * 10);

	}

}