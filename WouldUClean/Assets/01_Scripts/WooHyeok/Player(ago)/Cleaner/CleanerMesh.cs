using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CleanerMesh : MonoBehaviour
{
    [Range(0, 360)]
    public float angle = 90f; // ��ä���� ����
    public float radius = 1f; // ��ä���� ������
    public int segments = 30; // ��ä���� �����ϴ� �ﰢ���� ����

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        // �߽��� �߰�
        vertices[0] = Vector3.zero;

        // ��ä���� ������ ���� ���� ����
        float angleStep = angle / segments;
        for (int i = 1; i <= segments + 1; i++)
        {
            float angleRadians = Mathf.Deg2Rad * (angleStep * (i - 1));
            vertices[i] = new Vector3(Mathf.Cos(angleRadians) * radius, Mathf.Sin(angleRadians) * radius, 0);
        }

        // �ﰢ�� �ε��� ����
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0; // �߽���
            triangles[i * 3 + 1] = i + 1; // ���� ����
            triangles[i * 3 + 2] = i + 2; // ���� ����
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard")); // �⺻���� ���� ����
    }

    void OnValidate()
    {
        GenerateMesh();
    }
}
