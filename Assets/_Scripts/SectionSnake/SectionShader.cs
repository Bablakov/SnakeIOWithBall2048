using UnityEngine;

public class SectionShader : MonoBehaviour {
    private const float MAX_VALUE_ALPHA = 1.0f;
    private const float MIN_VALUE_ALPHA = 0f;

    private Material material;

    public void Initialize() {
        var meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    public float Alpha {
        get { return GetAlpha(); }
        set { SetAlphaValue(value); }
    }

    public void SetAlphaValue (float alpha) {
        if (alpha > MAX_VALUE_ALPHA) {
            SetAlpha(MAX_VALUE_ALPHA);
        }
        else if (alpha < MIN_VALUE_ALPHA) {
            SetAlpha(MIN_VALUE_ALPHA);
        }
        else {
            SetAlpha(alpha);
        }
    }

    public void SetFullVisual() {
        SetAlpha(MAX_VALUE_ALPHA);
    }

    public void SetFullHide() {
        SetAlpha(MIN_VALUE_ALPHA);
    }

    private void SetAlpha(float alpha) {
        material.SetFloat("_Alpha", alpha);
    }

    private float GetAlpha() {
        return material.GetFloat("_Alpha");
    }
}