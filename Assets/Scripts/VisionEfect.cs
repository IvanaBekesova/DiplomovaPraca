using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisionEffect : MonoBehaviour
{
    public Material visionMaskMaterial;
    public VisionController visionController;

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
        if (visionMaskMaterial == null || visionController == null) return;

        visionMaskMaterial.SetFloat("_Angle", visionController.Angle);
        visionMaskMaterial.SetFloat("_Percent", visionController.Percent / 100f);
        visionMaskMaterial.SetFloat("_Softness", visionController.Softness);

        GL.PushMatrix();
        GL.LoadOrtho();
        visionMaskMaterial.SetPass(0);
        GL.Begin(GL.QUADS);
        GL.TexCoord2(0, 0); GL.Vertex3(0, 0, 0);
        GL.TexCoord2(0, 1); GL.Vertex3(0, 1, 0);
        GL.TexCoord2(1, 1); GL.Vertex3(1, 1, 0);
        GL.TexCoord2(1, 0); GL.Vertex3(1, 0, 0);
        GL.End();
        GL.PopMatrix();
    }
}