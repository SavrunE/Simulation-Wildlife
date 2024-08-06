using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Mob _mobPrefab;
	[SerializeField] private Food _foodPrefab;
	[SerializeField] private float _maxDistanceMulty = 5f;
	[SerializeField] private LayerMask _foodLayer;

	private float _maxDistance;
	private float _gridSpacing;

	private List<Vector3> _gridPoints = new List<Vector3>();

	public Dictionary<Mob, MobPack> Spawn(int count, float speed, Vector2Int fieldSize)
	{
		_gridSpacing = Mathf.Max(_foodPrefab.transform.localScale.x, _foodPrefab.transform.localScale.y);
		_maxDistance = _maxDistanceMulty * speed;
		Dictionary<Mob, MobPack> mobs = new Dictionary<Mob, MobPack>();
		GenerateGridPoints(fieldSize);
		for (int i = 0; i < count; i++)
		{
			SpawnMobs(new Vector3(0f, _mobPrefab.transform.localScale.y, 0f), speed, mobs);
		}
		return mobs;
	}

	public void SpawnMobs(Vector3 mobPosition, float speed, Dictionary<Mob, MobPack> mobs)
	{
		Food food = CreateFood(mobPosition);
		var mob = Instantiate(_mobPrefab, this.transform);
		mob.Initialize(food, speed, mobPosition);
		MobPack mobPack = new MobPack();
		mobPack.SetupPack(mob, food);
		mobs.Add(mob, mobPack);
	}

	private Food CreateFood(Vector3 animalPosition)
	{
		List<Vector3> possibleFoodPoints = GetValidPointsWithinDistance(animalPosition);
		List<Vector3> freePoints = GetFreePoints(possibleFoodPoints);
		Food food;
		Vector3 foodPosition;
		if (freePoints.Count > 0)
		{
			foodPosition = freePoints[Random.Range(0, freePoints.Count)];
			food = InitFood(foodPosition);
		}
		else
		{
			Debug.Log("Can't find right point");
			foodPosition = _gridPoints[Random.Range(0, _gridPoints.Count)];
			food = InitFood(foodPosition);
		}
		return food;
	}

	private Food InitFood(Vector3 foodPosition)
	{
		Food food = Instantiate(_foodPrefab, this.transform);
		food.Initialize(foodPosition);
		_gridPoints.Remove(foodPosition);
		return food;
	}

	public void RespawnFood(MobPack mobPack)
	{
		_gridPoints.Add(mobPack.TakeStartFoodPos());
		mobPack.DestroyFood();
		Food food = CreateFood(mobPack.mobPos);
		mobPack.SetFood(food);
	}

	private void GenerateGridPoints(Vector2Int fieldSize)
	{
		for (float x = -fieldSize.x / 2f + _gridSpacing; x <= fieldSize.x / 2f - _gridSpacing; x += _gridSpacing)
		{
			for (float z = -fieldSize.y / 2f + _gridSpacing; z <= fieldSize.y / 2f - _gridSpacing; z += _gridSpacing)
			{
				_gridPoints.Add(new Vector3(x, _foodPrefab.transform.localScale.y, z));
			}
		}
	}

	private List<Vector3> GetValidPointsWithinDistance(Vector3 center)
	{
		List<Vector3> validPoints = new List<Vector3>();

		foreach (var point in _gridPoints)
		{
			if (Vector3.Distance(center, point) >= _maxDistance)
			{
				validPoints.Add(point);
			}
		}

		return validPoints;
	}

	private List<Vector3> GetFreePoints(List<Vector3> points)
	{
		List<Vector3> freePoints = new List<Vector3>();

		foreach (var point in points)
		{
			if (!Physics.CheckSphere(point, _gridSpacing / 2f, _foodLayer))
			{
				freePoints.Add(point);
			}
		}

		return freePoints;
	}
}