using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CleanerMesh : MonoBehaviour
{
    [Range(0, 360)]
    public float angle = 90f; // 부채꼴의 각도
    public float radius = 1f; // 부채꼴의 반지름
    public int segments = 30; // 부채꼴을 구성하는 삼각형의 개수

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

        // 중심점 추가
        vertices[0] = Vector3.zero;

        // 부채꼴의 각도에 따라 정점 생성
        float angleStep = angle / segments;
        for (int i = 1; i <= segments + 1; i++)
        {
            float angleRadians = Mathf.Deg2Rad * (angleStep * (i - 1));
            vertices[i] = new Vector3(Mathf.Cos(angleRadians) * radius, Mathf.Sin(angleRadians) * radius, 0);
        }

        // 삼각형 인덱스 생성
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0; // 중심점
            triangles[i * 3 + 1] = i + 1; // 현재 정점
            triangles[i * 3 + 2] = i + 2; // 다음 정점
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard")); // 기본적인 재질 설정
    }

    void OnValidate()
    {
        GenerateMesh();
    }
}
