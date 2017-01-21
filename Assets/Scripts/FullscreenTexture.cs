using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenTexture : MonoBehaviour
{
    public Material material;
    public Texture texture;

    void OnEnable() { }

    void OnPostRender()
    {
        material.mainTexture = texture;

        GL.PushMatrix();
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        for (int i = 0; i < material.passCount; i++)
        {
            material.SetPass(i);
            GL.TexCoord2(0, 0);
            GL.Vertex3(0, 0, 0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0, 1, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1, 1, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1, 0, 0);

        }
        GL.End();
        GL.PopMatrix();
    }
}