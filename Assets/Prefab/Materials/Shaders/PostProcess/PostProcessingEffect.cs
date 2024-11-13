using UnityEngine;

[ExecuteInEditMode]
public class PostProcessingEffect : MonoBehaviour
{
    public Shader postProcessingShader;
    private Material postProcessingMaterial;

    [ColorUsage(true, true)]
    public Color tintColor = new Color(0, 1, 1, 0.3f); // Cyan tint with transparency

    [Range(0, 2)]
    public float saturation = 1.5f; // Boost for vibrant colors

    [Range(0, 2)]
    public float brightness = 1.2f; // Brightness boost 

    [Range(0, 1)]
    public float glowIntensity = 0.3f; // Glow effect intensity

    void Start()
    {
        if (postProcessingShader == null)
        {
            Debug.LogError("Post-processing shader is missing.");
            enabled = false;
            return;
        }

        postProcessingMaterial = new Material(postProcessingShader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (postProcessingMaterial != null)
        {
            // Set shader properties
            postProcessingMaterial.SetColor("_TintColor", tintColor);
            postProcessingMaterial.SetFloat("_Saturation", saturation);
            postProcessingMaterial.SetFloat("_Brightness", brightness);
            postProcessingMaterial.SetFloat("_GlowIntensity", glowIntensity);

            // Apply the post-processing effect
            Graphics.Blit(src, dest, postProcessingMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

    void OnDestroy()
    {
        if (postProcessingMaterial != null)
        {
            DestroyImmediate(postProcessingMaterial);
        }
    }
}
