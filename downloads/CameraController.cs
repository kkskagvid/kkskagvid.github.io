using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Camera Follow")]
	public Transform target; // The target to orbit around
	public float distance = 5.0f; // Distance from the target
	public float xSpeed = 1.0f; // Rotation speed around Y-axis
	public float ySpeed = 1.0f; // Rotation speed around X-axis

	[Header("Angle Limit")]
	public float yMinLimit = -20f; // Minimum vertical angle
	public float yMaxLimit = 80f; // Maximum vertical angle

	[Header("Zoom")]
	public float zoomSpeed = 5.0f; // Zoom speed
	public float zoomMinDistance = 1.0f; // Minimum distance
	public float zoomMaxDistance = 10.0f; // Maximum distance

	[Header("Obstacle Detection")]
	public float raycastDistance = 1.0f; // Distance for raycasting
	public LayerMask obstacleLayer; // Layer mask for obstacles

	private float rotationX = 0.0f;
	private float rotationY = 0.0f;


	void Start()
	{
		// Set initial rotation angles
		rotationX = transform.localEulerAngles.x;
		rotationY = transform.localEulerAngles.y;

		// Lock the cursor and hide it
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (Cursor.lockState == CursorLockMode.Locked)
		{
			MouseInput();
			Orbit();
		}

		CursorControl();
	}

	private void MouseInput()
	{
		// Get mouse delta
		rotationX -= Input.GetAxis("Mouse Y") * (ySpeed * 100) * Time.deltaTime;
		rotationY += Input.GetAxis("Mouse X") * (xSpeed * 100) * Time.deltaTime;

		// Clamp the rotation angles
		rotationX = Mathf.Clamp(rotationX, yMinLimit, yMaxLimit);
		rotationY %= 360f;

		// Zoom functionality
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		distance = Mathf.Clamp(distance - scroll * zoomSpeed, zoomMinDistance, zoomMaxDistance);
	}

	private void Orbit()
	{
		// Calculate the camera position based on the rotation angles
		Vector3 pos = target.position + Quaternion.Euler(rotationX, rotationY, 0.0f) * Vector3.forward * distance;
		transform.position = pos;

		// Adjust the look-at point to be slightly higher
		Vector3 adjustedTargetPosition = target.position + Vector3.up * 1f;
		transform.LookAt(adjustedTargetPosition);

		// Check for obstacles in front of the camera
		//RaycastHit hit;
		//if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, obstacleLayer))
		//{
		//	// If an obstacle is found, move the camera back
		//	transform.position -= transform.forward * (hit.distance - 0.1f); // Adjust the offset as needed
		//}
	}

	private void CursorControl()
	{
		if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
		{
			Cursor.lockState = CursorLockMode.Confined;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}


