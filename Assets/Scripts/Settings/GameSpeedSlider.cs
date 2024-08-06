using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSlider : MonoBehaviour
{
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Text _text;

    private void OnEnable()
    {
        _speedSlider.onValueChanged.AddListener(ChangeGameSpeed);
        _speedSlider.onValueChanged.AddListener(ChangeText);
	}

    private void ChangeGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    private void ChangeText(float value)
    {
        _text.text = value.ToString();
	}
}
