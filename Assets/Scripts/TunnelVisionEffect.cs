using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TunnelVisionEffect : MonoBehaviour
{
    public Material tunnelVisionMaterial;

    public Slider sizeSlider;
    public Slider softnessSlider;
    public Slider blurSlider;
    public Slider opacitySlider;

    public TextMeshProUGUI sizeValue;
    public TextMeshProUGUI softnessValue;
    public TextMeshProUGUI blurValue;
    public TextMeshProUGUI opacityValue;

    void Update()
    {
        if (tunnelVisionMaterial != null)
        {
            // slidery 0–100 prevedieme na rozsahy, s ktorými počíta shader
            tunnelVisionMaterial.SetFloat("_TunnelSize", sizeSlider.value / 100f);
            tunnelVisionMaterial.SetFloat("_TunnelSoftness", Mathf.Max(softnessSlider.value / 100f * 0.5f, 0.001f));
            tunnelVisionMaterial.SetFloat("_TunnelBlur", blurSlider.value / 100f);
            tunnelVisionMaterial.SetFloat("_TunnelOpacity", opacitySlider.value / 100f);
        }

        sizeValue.text = Mathf.Round(sizeSlider.value) + "%";
        softnessValue.text = Mathf.Round(softnessSlider.value) + "%";
        blurValue.text = Mathf.Round(blurSlider.value) + "%";
        opacityValue.text = Mathf.Round(opacitySlider.value) + "%";
    }

    public void DeleteEffect()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        // po zmazaní karty efekt vypneme
        if (tunnelVisionMaterial != null)
        {
            tunnelVisionMaterial.SetFloat("_TunnelBlur", 0f);
            tunnelVisionMaterial.SetFloat("_TunnelOpacity", 0f);
        }
    }
}