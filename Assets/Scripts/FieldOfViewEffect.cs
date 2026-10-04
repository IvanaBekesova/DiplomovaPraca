using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldOfViewEffect : MonoBehaviour
{
    public Slider angleSlider;
    public Slider percentSlider;
    public Slider softnessSlider;

    public TextMeshProUGUI angleValue;
    public TextMeshProUGUI percentValue;
    public TextMeshProUGUI softnessValue;

    void Update()
    {
        Shader.SetGlobalFloat("_Angle", angleSlider.value);
        Shader.SetGlobalFloat("_Percent", percentSlider.value / 100f);
        Shader.SetGlobalFloat("_Softness", softnessSlider.value);

        angleValue.text = Mathf.Round(angleSlider.value) + "°";
        percentValue.text = Mathf.Round(percentSlider.value) + "%";
        softnessValue.text = Mathf.Round(softnessSlider.value * 100) + "%";
    }

    public void DeleteEffect()
{
    Destroy(gameObject);
}
void OnDestroy()
    {
        Shader.SetGlobalFloat("_Percent", 0f);
    }
}