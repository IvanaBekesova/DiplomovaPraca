using UnityEngine;
using TMPro;

public class ColorBlindnessEffect : MonoBehaviour
{
    public Material colorBlindnessMaterial;
    public TMP_Dropdown typeDropdown;

    void Update()
    {
        if (colorBlindnessMaterial != null)
        {
            // dropdown: 0 = protanopia, 1 = deuteranopia, 2 = tritanopia
            // shader:   1 = protanopia, 2 = deuteranopia, 3 = tritanopia (0 = vypnuté)
            colorBlindnessMaterial.SetFloat("_ColorBlindType", typeDropdown.value + 1);
        }
    }

    public void DeleteEffect()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        // po zmazaní karty efekt vypneme
        if (colorBlindnessMaterial != null)
            colorBlindnessMaterial.SetFloat("_ColorBlindType", 0f);
    }
}