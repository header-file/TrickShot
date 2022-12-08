using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTextMesh : MonoBehaviour
{
    public MeshRenderer Mesh;
    public TextMesh Text;

    void Awake()
    {
        Mesh = GetComponent<MeshRenderer>();
        Text = GetComponent<TextMesh>();
    }

    void Start()
    {
        Mesh.sortingLayerName = "Default";
        Mesh.sortingOrder = 100;
    }

    public void SetNumber(int num) { Text.text = num.ToString(); }
}
