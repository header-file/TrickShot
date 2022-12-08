using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayScaleCamera : MonoBehaviour
{
    public Material CamMaterial;
    public float GrayScale = 0.0f;

    void Awake()
    {
        CamMaterial = new Material(Shader.Find("Hidden/Gray"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(GrayScale == 0.0f)
            Graphics.Blit(source, destination);
        else
        {
            CamMaterial.SetFloat("_Grayscale", GrayScale);
            Graphics.Blit(source, destination, CamMaterial);
        }        
    }

    public void ToGray()
    {
        StartCoroutine(MakeGrayscale());
    }

    IEnumerator MakeGrayscale()
    {
        float time = 0.0f;

        while(time < 0.125f)
        {
            time += Time.deltaTime;
            GrayScale = time / 0.125f;

            yield return null;
        }
    }

    public void ReturnGray()
    {
        StartCoroutine(ReturnGrayscale());
    }

    IEnumerator ReturnGrayscale()
    {
        float time = 0.0f;

        while(time < 0.5f)
        {
            time += Time.deltaTime;
            GrayScale = 1.0f - time / 0.5f;

            yield return null;
        }
    }
}
