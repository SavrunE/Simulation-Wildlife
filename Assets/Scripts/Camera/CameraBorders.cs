using UnityEngine;

public class CameraBorders : MonoBehaviour
{
	[SerializeField] private Transform _targetObject;
	[SerializeField] private Camera _mainCamera;       
	public float _padding = 1.1f;
	private float _cameraAspect;

	public void UpdateCamera()
	{
		MoveCamera();
	}

	private void Update()
	{
		if (_cameraAspect != _mainCamera.aspect)
		{
			MoveCamera();
		}
	}

	private void MoveCamera()
	{
		_cameraAspect = _mainCamera.aspect;

		Renderer targetRenderer = _targetObject.GetComponent<Renderer>();
		if (targetRenderer != null)
		{
			Bounds objectBounds = targetRenderer.bounds;
			float objectSize = Mathf.Max(objectBounds.size.x, objectBounds.size.y);
			if (_mainCamera.orthographic)
			{
				_mainCamera.orthographicSize = objectSize * _padding / 2;
			}
			else
			{
				float distance = objectSize / (2.0f * Mathf.Tan(Mathf.Deg2Rad * _mainCamera.fieldOfView / 2.0f));
				_mainCamera.transform.position = _targetObject.position - _mainCamera.transform.forward * distance * _padding;
			}
			_mainCamera.transform.LookAt(_targetObject.position);
		}
	}
}