using UnityEngine;
using UnityEngine.UI;

public class VisionController : MonoBehaviour
{
    public Slider angleSlider;
    public Slider percentSlider;
    public Slider softnessSlider;

    public Material visionMaskMaterial;

    public float Angle { get; private set; }
    public float Percent { get; private set; }
    public float Softness { get; private set; }

    void Update()
{
    Angle = angleSlider.value;
    Percent = percentSlider.value;
    Softness = softnessSlider.value;

    Shader.SetGlobalFloat("_Angle", Angle);
    Shader.SetGlobalFloat("_Percent", Percent / 100f);
    Shader.SetGlobalFloat("_Softness", Softness);
}
}