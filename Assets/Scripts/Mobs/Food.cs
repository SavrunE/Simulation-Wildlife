using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Food : MonoBehaviour
{
	[SerializeField] private ParticleSystem _particleSystem;
	[SerializeField] private float _timeToDie = 1f;
	private Vector3 _spawnPointPosition;
	public Vector3 spawnPointPosition => _spawnPointPosition;

	public void Initialize(Vector3 pos)
	{
		transform.position = pos;
	}

	public void ToDestroy()
	{
		Instantiate(_particleSystem, this.transform.position, Quaternion.identity, this.transform);
		GetComponent<MeshRenderer>().enabled = false;
		StartCoroutine(DieDelay());
	}

	private IEnumerator DieDelay()
	{
		float time = 0f;
		while (time <= _timeToDie)
		{
			time += Time.deltaTime;
			yield return null;
		}
		Destroy(this.gameObject);
	}
}
