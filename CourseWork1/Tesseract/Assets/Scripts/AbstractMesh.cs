using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMesh
{
    protected string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }
    public AbstractMesh(string name)
    {
        _name = name;
    }

    public abstract void UpdateMesh(Mesh mesh);

    /// <summary>
    /// считаем нормали для каждой грани, записываем их как сумму для каждой вершины
    /// </summary>
    /// <param name="vertices">список вершин-векторов</param>
    /// <param name="quads">грани</param>
    /// <returns></returns>
    protected static Vector3[] CalculateNormals(Vector3[] vertices, int[] quads)
    {
        Vector3[] normals = new Vector3[vertices.Length];
        for (int i = 0; i < quads.Length; i += 4)
        {
            int v1 = quads[i];
            int v2 = quads[i + 1];
            int v3 = quads[i + 2];
            int v4 = quads[i + 3];
            Vector3 normal = CalculateNormal(vertices[v1], vertices[v2], vertices[v3]);

            normals[v1] = normals[v1] + normal;
            normals[v2] = normals[v2] + normal;
            normals[v3] = normals[v3] + normal;
            normals[v4] = normals[v4] + normal;
        }
        //считаем нормаль к каждой вершине
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = (normals[i]).normalized;
        }
        return normals;
    }
    /// <summary>
    /// находим нормаль к заданной через 3 точки плоскости по формуле через векторное произведение
    /// </summary>
    /// <param name="v0">первая точка</param>
    /// <param name="v1">вторая точка</param>
    /// <param name="v2">третья точка</param>
    /// <returns></returns>
    private static Vector3 CalculateNormal(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        Vector3 d0 = v1 - v0;
        Vector3 d1 = v2 - v0;
        return Vector3.Cross(d0, d1);
    }
}
