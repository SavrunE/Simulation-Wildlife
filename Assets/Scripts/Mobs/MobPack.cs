using UnityEngine;

public class MobPack
{
	private Mob _mob;
	public Vector3 mobPos => _mob.transform.position;
	private Food _food;
	private Vector3 _startFoodPosition;

	public void SetupPack(Mob mob, Food food)
	{
		_mob = mob;
		SetFood(food);
	}

	public void SetFood(Food food)
	{
		_mob.SetNewTarget(food);
		_food = food;
		_startFoodPosition = food.spawnPointPosition;
	}

	public void DestroyFood()
	{
		_food.ToDestroy();
	}

	public Vector3 TakeStartFoodPos()
	{
		return _startFoodPosition;
	}

	public void DeleteAll()
	{
		_mob.DestroyMob();
		DestroyFood();
	}
}
