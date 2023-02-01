using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderBlit : MonoBehaviour
{
    public Material mat;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
