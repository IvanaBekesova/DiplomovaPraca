using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldOfViewEffect : MonoBehaviour
{
    public Slider angleSlider;
    public Slider percentSlider;
    public Slider opacitySlider;

    public TextMeshProUGUI angleValue;
    public TextMeshProUGUI percentValue;
    public TextMeshProUGUI opacityValue;

    void Update()
    {
        Shader.SetGlobalFloat("_Angle", angleSlider.value);
        Shader.SetGlobalFloat("_Percent", percentSlider.value / 100f);
        Shader.SetGlobalFloat("_Opacity", opacitySlider.value / 100f);

        angleValue.text = Mathf.Round(angleSlider.value) + "°";
        percentValue.text = Mathf.Round(percentSlider.value) + "%";
        opacityValue.text = Mathf.Round(opacitySlider.value) + "%";
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