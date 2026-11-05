using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CentralLossEffect : MonoBehaviour
{
    public Material centralLossMaterial;

    public Slider sizeSlider;
    public Slider softnessSlider;
    public Slider blurSlider;

    public TextMeshProUGUI sizeValue;
    public TextMeshProUGUI softnessValue;
    public TextMeshProUGUI blurValue;

    void Update()
    {
        if (centralLossMaterial != null)
        {
            // slidery 0–100 prevedieme na rozsahy, s ktorými počíta shader
            centralLossMaterial.SetFloat("_ScotomaSize", sizeSlider.value / 100f * 0.7f);
            centralLossMaterial.SetFloat("_ScotomaSoftness", Mathf.Max(softnessSlider.value / 100f * 0.5f, 0.001f));
            centralLossMaterial.SetFloat("_ScotomaBlur", blurSlider.value / 100f);
        }

        sizeValue.text = Mathf.Round(sizeSlider.value) + "%";
        softnessValue.text = Mathf.Round(softnessSlider.value) + "%";
        blurValue.text = Mathf.Round(blurSlider.value) + "%";
    }

    public void DeleteEffect()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        // po zmazaní karty rozmazanie vypneme
        if (centralLossMaterial != null)
            centralLossMaterial.SetFloat("_ScotomaBlur", 0f);
    }
}