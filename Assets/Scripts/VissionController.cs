using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisionController : MonoBehaviour
{
    public Material visionMaskMaterial;

    void Update()
    {
        if (visionMaskMaterial != null)
        {
            visionMaskMaterial.SetFloat("_Angle", Shader.GetGlobalFloat("_Angle"));
            visionMaskMaterial.SetFloat("_Percent", Shader.GetGlobalFloat("_Percent"));
            visionMaskMaterial.SetFloat("_Softness", Shader.GetGlobalFloat("_Softness"));
        }
    }
}