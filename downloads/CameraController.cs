using Sprint.Util;
using UnityEngine;

namespace Sprint.Player.CameraController
{
	public class CameraController : MonoBehaviour
	{
		[Header("Camera Follow")]
		public Transform target;
		public float distance = 3f;
		public float followSmoothTime = 0.3f;
		public float mouseSensitivity = 5f;
		public float joystickSensitivity = 80f;
		public float rotationSpeed = 90f;

		[Header("Angle Limit")]
		public float minPitchAngle = -90f;
		public float maxPitchAngle = 90f;

		[Header("Zoom")]
		public float zoomSpeed = 2f;
		public float zoomMinDistance = 2f;
		public float zoomMaxDistance = 5f;

		[Header("Obstacle Detection")]
		public LayerMask obstructionMask;

		private Camera regularCamera;
		private Vector2 orbitAngles = new Vector2(45f, 0f);

		private bool cameraAsisXInvert = false;
		private bool cameraAsisYInvert = true;

		void Start()
		{
			regularCamera = GetComponent<Camera>();
			transform.localRotation = Quaternion.Euler(orbitAngles);
			Cursor.lockState = CursorLockMode.Locked;

			GameSettings.Load();
			cameraAsisXInvert = GameSettings.CameraAsisXInvert;
			cameraAsisYInvert = GameSettings.CameraAsisYInvert;
		}

		void LateUpdate()
		{
			if (CursorControl())
			{
				Quaternion lookRotation;
				if (IsRotation())
				{
					ConstrainAngles();
					lookRotation = Quaternion.Euler(orbitAngles);
				}
				else
				{
					lookRotation = transform.localRotation;
				}

				Zoom();

				Vector3 lookDirection = lookRotation * Vector3.forward;
				Vector3 lookPosition = target.position - lookDirection * distance;


				Vector3 rectOffset = lookDirection * regularCamera.nearClipPlane;
				Vector3 rectPosition = lookPosition + rectOffset;
				Vector3 castFrom = target.position;
				Vector3 castLine = rectPosition - castFrom;
				float castDistance = castLine.magnitude;
				Vector3 castDirection = castLine / castDistance;

				if (Physics.BoxCast(
				castFrom, CameraHalfExtends(), castDirection, out RaycastHit hit,
				lookRotation, castDistance, obstructionMask
				))
				{
					rectPosition = castFrom + castDirection * hit.distance;
					lookPosition = rectPosition - rectOffset;
				}

				transform.SetPositionAndRotation(lookPosition, lookRotation);
			}
		}

		private bool IsRotation(float deadband = 0.001f)
		{
			float yaw = 
				Input.GetAxis("Mouse X") +
				Input.GetAxis("Camera Joystick X") * joystickSensitivity * Time.deltaTime;
			
			float pitch = 
				Input.GetAxis("Mouse Y") +
				Input.GetAxis("Camera Joystick Y") * joystickSensitivity * Time.deltaTime;

			if (cameraAsisXInvert) { pitch = -pitch; }
			if (cameraAsisYInvert) { yaw = -yaw; }

			Vector2 input = new Vector2(pitch, yaw);

			if (input.x < -deadband || input.x > deadband || input.y < -deadband || input.y > deadband)
			{
				orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
				return true;
			}

			return false;
		}

		private void ConstrainAngles()
		{
			orbitAngles.x = Mathf.Clamp(orbitAngles.x, minPitchAngle, maxPitchAngle);

			if (orbitAngles.y < 0f)
			{
				orbitAngles.y += 360f;
			}

			else if (orbitAngles.y >= 360f)
			{
				orbitAngles.y -= 360f;
			}
		}

		private Vector3 CameraHalfExtends()
		{
			Vector3 halfExtends;
			halfExtends.y =
				regularCamera.nearClipPlane *
				Mathf.Tan(0.5f * Mathf.Deg2Rad * regularCamera.fieldOfView);
			halfExtends.x = halfExtends.y * regularCamera.aspect;
			halfExtends.z = 0f;

			return halfExtends;
		}

		private bool CursorControl()
		{
			if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
			{
				Cursor.lockState = CursorLockMode.Confined;

				return false;
			}
			else
			{
				int width = Screen.width;
				int height = Screen.height;

				width = width / 2;
				height = height / 2;



				Cursor.lockState = CursorLockMode.Locked;

				return true;
			}
		}

		private void Zoom()
		{
			float scroll = Input.GetAxis("Mouse ScrollWheel");
			distance = Mathf.Clamp(distance - scroll * zoomSpeed, zoomMinDistance, zoomMaxDistance);
		}
	}
}
