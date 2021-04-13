using System;
using System.Collections.Generic;
using UnityEngine;

public class TesseractMesh : AbstractMesh
{
    private static int[] lines;
    private static int[] quads;
    private static Vector4[] vertices;

    public TesseractMesh(string name) : base(name)
    {
    }
    public override void UpdateMesh(Mesh mesh)
    {
        //вычисляем угол поворота с помощью времени работы программы
        float angle = (Time.time * 0.5f) % 360.0f;
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        Vector3[] transformedVertices = new Vector3[vertices.Length];
        for (int i = 0; i < transformedVertices.Length; i++)
        {
            Vector4 oldVert = vertices[i];
            //для каждой вершины считаем ее поворот
            float newX = (oldVert.x * cos) + (oldVert.y * sin);
            float newY = (oldVert.y * cos) - (oldVert.x * sin);
            float newz = (oldVert.z * cos) + (oldVert.w * sin);
            float newW = (oldVert.w * cos) - (oldVert.z * sin);
            //считаем коэффициент матрицы проекции
            float wProj = 5.0f / (4.0f - newW);
            //проектируем на трехмерное пространство
            transformedVertices[i] = wProj * (new Vector3(newX, newY, newz));
        }
        //очищаем старую сетку
        mesh.Clear();
        //задаем  вершины проекции
        mesh.vertices = transformedVertices;
        //задаем нормали проекции
        mesh.normals = CalculateNormals(transformedVertices, quads);
        mesh.subMeshCount = 2;
        //задаем ребра проекции
        mesh.SetIndices(lines, MeshTopology.Lines, 0);
        //задаем грани проекции
        mesh.SetIndices(quads, MeshTopology.Quads, 1);
    }
    /// <summary>
    /// создание тессеракта
    /// </summary>
    /// <returns></returns>
    public static TesseractMesh GenerateHypercube()
    {
        //задаем вершины
        vertices = new Vector4[] {
            new Vector4(-1.0f, -1.0f, -1.0f,  1.0f),
            new Vector4( 1.0f, -1.0f, -1.0f,  1.0f),
            new Vector4( 1.0f, -1.0f,  1.0f,  1.0f),
            new Vector4(-1.0f, -1.0f,  1.0f,  1.0f),

            new Vector4(-1.0f,  1.0f,  1.0f,  1.0f),
            new Vector4( 1.0f,  1.0f,  1.0f,  1.0f),
            new Vector4( 1.0f,  1.0f, -1.0f,  1.0f),
            new Vector4(-1.0f,  1.0f, -1.0f,  1.0f),

            new Vector4(-1.0f, -1.0f, -1.0f, -1.0f),
            new Vector4( 1.0f, -1.0f, -1.0f, -1.0f),
            new Vector4( 1.0f, -1.0f,  1.0f, -1.0f),
            new Vector4(-1.0f, -1.0f,  1.0f, -1.0f),

            new Vector4(-1.0f,  1.0f,  1.0f, -1.0f),
            new Vector4( 1.0f,  1.0f,  1.0f, -1.0f),
            new Vector4( 1.0f,  1.0f, -1.0f, -1.0f),
            new Vector4(-1.0f,  1.0f, -1.0f, -1.0f),
        };
        //задаем грани проекции
        quads = new int[] {
             0,  1,  2,  3,
             4,  5,  6,  7,
             7,  6,  1,  0,
             6,  5,  2,  1,
             5,  4,  3,  2,
             4,  7,  0,  3,

             8,  9, 10, 11,
            12, 13, 14, 15,
            15, 14,  9,  8,
            14, 13, 10,  9,
            13, 12, 11, 10,
            12, 15,  8, 11,
        };
        //задаем ребра проекции
        lines = new int[] {
             0,  1,  1,  2,  2,  3,  3,  0,
             4,  5,  5,  6,  6,  7,  7,  4,
             0,  7,  1,  6,  2,  5,  3,  4,

             8,  9,  9, 10, 10, 11, 11,  8,
            12, 13, 13, 14, 14, 15, 15, 12,
             8, 15,  9, 14, 10, 13, 11, 12,

             0,  8,  1,  9,  2, 10,  3, 11,
             4, 12,  5, 13,  6, 14,  7, 15,
        };
        //создаем новый объект класса TesseractMesh
        TesseractMesh tesseract = new TesseractMesh("Tesseract");
        return tesseract;
    }
}
