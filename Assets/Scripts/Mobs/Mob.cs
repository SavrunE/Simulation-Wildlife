using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mob : MonoBehaviour
{
	private Rigidbody _rb;
	private Food _food;
	private float _speed;

	public void Initialize(Food food, float speed, Vector3 position)
	{
		_food = food;
		_speed = speed;
		transform.position = position;
		_rb = GetComponent<Rigidbody>();
		_rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
	}

	void FixedUpdate()
	{
		if (_food != null)
		{
			transform.LookAt(_food.transform.position);
			_rb.velocity = transform.forward * _speed * Time.fixedDeltaTime;
		}
	}

	public void SetTarget(Food target)
	{
		_food = target;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform == _food.transform)
		{
			Debug.Log("ASD");
		}
	}
}
