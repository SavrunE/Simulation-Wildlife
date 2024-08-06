using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
	[SerializeField] private GameBoard _board;
	[SerializeField] private CameraBorders _cameraBorders;
	[SerializeField] private Spawner _spawner;
	Dictionary<Mob, MobPack> _mobs = new Dictionary<Mob, MobPack>();

	public void StartGame(int size, int mobsCount, int mobsSpeed)
	{
		if (_mobs.Count > 0)
		{
			foreach (var mob in _mobs)
			{
				mob.Value.DeleteAll();
			}
		}
		Vector2Int boardSize = new Vector2Int(size, size);
		_board.Initialize(boardSize);
		_cameraBorders.UpdateCamera();
		_mobs = _spawner.Spawn(mobsCount, mobsSpeed, boardSize);
		foreach (var mob in _mobs)
		{
			mob.Key.onEat += MobEat;
		}
	}

	private void MobEat(Mob mob)
	{
		if (_mobs.ContainsKey(mob))
		{
			_spawner.RespawnFood(_mobs[mob]);
		}
		else
		{
			throw new NotImplementedException();
		}
	}
}
