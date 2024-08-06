using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mob : MonoBehaviour
{
	private Rigidbody _rb;
	private Food _food;
	private float _speed;

	public Action<Mob> onEat;

	public void Initialize(Food food, float speed, Vector3 position)
	{
		SetNewTarget(food);
		_speed = speed;
		transform.position = position;
		_rb = GetComponent<Rigidbody>();
		_rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
	}

	private void FixedUpdate()
	{
		if (_food != null)
		{
			transform.LookAt(_food.transform.position);
			_rb.velocity = transform.forward * _speed;
		}
	}

	public void SetNewTarget(Food target)
	{
		_food = target;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform == _food.transform)
		{
			onEat?.Invoke(this);
		}
	}

	public void DestroyMob()
	{
		Destroy(this.gameObject);
	}
}
