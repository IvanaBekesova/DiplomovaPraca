using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisionEffect : MonoBehaviour
{
    public Material visionMaskMaterial;

    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    void OnEndCameraRendering(ScriptableRenderContext context, Camera cam)
    {
        if (cam != GetComponent<Camera>()) return;
        if (visionMaskMaterial == null) return;

        visionMaskMaterial.SetFloat("_Angle", Shader.GetGlobalFloat("_Angle"));
        visionMaskMaterial.SetFloat("_Percent", Shader.GetGlobalFloat("_Percent"));
        visionMaskMaterial.SetFloat("_Softness", Shader.GetGlobalFloat("_Softness"));
    }
}