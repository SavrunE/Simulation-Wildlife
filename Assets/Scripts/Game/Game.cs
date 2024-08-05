using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] private Vector2Int _boardSize;
	[SerializeField] private int mobsCount;
	[SerializeField] private GameBoard _board;
	[SerializeField] private CameraBorders _cameraBorders;
	[SerializeField] private Mob _mobInstance;
	[SerializeField] private Food _foodInstance;
	[SerializeField] private float _speed = 1f;

	private void Start()
	{
		_board.Initialize(_boardSize);
		_cameraBorders.UpdateCamera();
		Spawner();
	}

	private void Spawner()
	{
		int mobs = _boardSize.x * _boardSize.y / 2;

		for (int i = 0; i < mobs; i++)
		{
			SpawnMob();
		}
	}

	private void SpawnMob()
	{
		Mob mob = Instantiate(_mobInstance);
		Food food = Instantiate(_foodInstance);
		float xPos = Random.Range(-_boardSize.x / 2f, _boardSize.x / 2f);
		float zPos = Random.Range(-_boardSize.y / 2f, _boardSize.y / 2f);
		Vector3 pos = new Vector3(xPos, _mobInstance.transform.localScale.y / 2f, zPos);
		food.transform.position = new Vector3(0f, _foodInstance.transform.localScale.y / 2f, 0f);
		mob.Initialize(food, _speed, pos);
	}
}
