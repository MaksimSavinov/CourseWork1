using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Material edgeColor;
    public Material innerColor;
    private GameObject projection;
    private static AbstractMesh meshObject;
    /// <summary>
    /// Создаем тессеракт
    /// </summary>
    void Start()
    {
        meshObject = TesseractMesh.GenerateHypercube();
        projection = new GameObject(meshObject.Name);
        projection.AddComponent<MeshFilter>();
        MeshRenderer meshrenderer = projection.AddComponent<MeshRenderer>();
        meshrenderer.materials = new Material[] { edgeColor, innerColor };
    }
    /// <summary>
    /// вызываем вращение тессеракта каждый кадр
    /// </summary>
    void Update()
    {
        Mesh mesh = projection.GetComponent<MeshFilter>().mesh;
        meshObject.UpdateMesh(mesh);
    }
}
