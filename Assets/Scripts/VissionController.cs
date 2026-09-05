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

    if (visionMaskMaterial != null)
    {
        visionMaskMaterial.SetFloat("_Angle", Angle);
        visionMaskMaterial.SetFloat("_Percent", Percent / 100f);
        visionMaskMaterial.SetFloat("_Softness", Softness);
    }
}
}