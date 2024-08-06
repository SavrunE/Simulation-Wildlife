using UnityEngine;
using UnityEngine.UI;

public class StartMenuParameters : MonoBehaviour
{
	private int _scale, _mobsCount, _speed;
	[SerializeField] Slider _sliderN, _sliderM, _sliderV;
	[SerializeField] private Text _textN, _textM, _textV;
	[SerializeField] private Game _game;

	private void OnEnable()
	{
		_sliderN.onValueChanged.AddListener(ChangeN);
		_sliderM.onValueChanged.AddListener(ChangeM);
		_sliderV.onValueChanged.AddListener(ChangeV);
	}

	private void Start()
	{
		ChangeN(_sliderN.value);
		ChangeM(_sliderM.value);
		ChangeV(_sliderV.value);
	}

	private void ChangeN(float value)
	{
		_scale = (int)value;
		_mobsCount = (_scale * _scale / 2);
		_sliderM.maxValue = _mobsCount;
		_sliderM.value = _mobsCount;
		_textN.text = value.ToString();
		_textM.text = _mobsCount.ToString();
	}

	private void ChangeM(float value)
	{
		_mobsCount = (int)value;
		_textM.text = _mobsCount.ToString();
	}

	private void ChangeV(float value)
	{
		_speed = (int)value;
		_textV.text = value.ToString();
	}

	public void StartGame()
	{
		_game.StartGame(_scale, _mobsCount, _speed);
	}
}
