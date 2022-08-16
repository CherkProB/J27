using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����� "������" �����
/// ������� ��������� ���� �� ��������� ���������
/// �������� ���������� � ��������� �� ��������� �������� ��������� �������� ��� �����
/// </summary>
public class MeshBuilder
{
    //���� ������
    private Material defaultMaterial;
    //�������� ������
    public Material DefaultMaterial { set { defaultMaterial = value; } get { return defaultMaterial; } }
    /// <summary>
    /// ����������� 
    /// </summary>
    /// <param name="mat">�������� �� ��������� �������� ����� ����������� �������� ��� �����</param>
    public MeshBuilder(Material mat) { defaultMaterial = mat; }

    /// <summary>
    /// ���������� ����������� ���� ������ �� ���� �� ������
    /// </summary>
    /// <param name="startPoint">��������� ����� ������</param>
    /// <param name="endPoint">�������� ����� ������</param>
    /// <returns>���������� ���������� ���� �������� ������ � ���� �� 0 �� 360</returns>
    public static float Angle(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 vectorA = new Vector3(endPoint.x - startPoint.x, 0, endPoint.z - startPoint.z);
        Vector3 vectorB = new Vector3(endPoint.x - startPoint.x, 0, 0);

        float angle = Mathf.Abs(vectorA.x * vectorB.x + vectorA.z * vectorB.z) /
            (Mathf.Sqrt(vectorA.x * vectorA.x + vectorA.z * vectorA.z) * Mathf.Sqrt(vectorB.x * vectorB.x + vectorB.z * vectorB.z));

        if (startPoint.z <= endPoint.z)
        {
            if (startPoint.x <= endPoint.x)
                return Mathf.Acos(angle) * Mathf.Rad2Deg;        //part 1
            else
                return 180 - Mathf.Acos(angle) * Mathf.Rad2Deg;  //part 2
        }
        else
        {
            if (startPoint.x <= endPoint.x)
                return 360 - Mathf.Acos(angle) * Mathf.Rad2Deg; //part 4
            else
                return Mathf.Acos(angle) * Mathf.Rad2Deg + 180; //part 3
        }
    }

    /// <summary>
    /// �������� �������� � ������� ��������
    /// </summary>
    /// <param name="materialColor">���� ����</param>
    /// <param name="sideLength">������� ��������</param>
    /// <returns>���������� Transform ���������� �������</returns>
    public Transform BuildQuad(Color materialColor, float sideLength = 1f)
    {
        GameObject newQuad = new GameObject("NewGameObj");

        MeshRenderer meshRenderer = newQuad.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = newQuad.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mesh.name = "NewGameObj";

        Vector3[] vert = new Vector3[] {
        new Vector3(0, 0, -0.5f) * sideLength,
        new Vector3(0, 0, 0.5f) * sideLength,
        new Vector3(1, 0, -0.5f) * sideLength,
        new Vector3(1, 0, 0.5f) * sideLength};

        int[] tri = new int[] { 0, 1, 2, 1, 3, 2 };

        Vector2[] uv = new Vector2[]{
        new Vector2(0, 0),
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(1, 1)};

        mesh.vertices = vert;
        mesh.triangles = tri;
        mesh.uv = uv;

        Material newMaterial = new Material(defaultMaterial.shader);
        newMaterial.color = materialColor;
        meshRenderer.material = newMaterial;
        meshFilter.mesh = mesh;

        return newQuad.transform;
    }

    /// <summary>
    /// ������� ������������� �� ��������� ����� � ������� ����� ������ � �������
    /// </summary>
    /// <param name="startPoint">��������� ����� ��������������</param>
    /// <param name="angle">���������� ���� ��������� �������������� � ����</param>
    /// <param name="length">����� ��������������</param>
    /// <param name="materialColor">���� ����</param>
    /// <param name="sideLength">������ ��������������</param>
    /// <returns>���������� Transform ���������� �������</returns>
    public Transform BuildQuad(Vector3 startPoint, float angle, float length, Color materialColor, float sideLength = 0.5f)
    {
        Vector3 endPoint = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * length + startPoint;

        GameObject newQuad = new GameObject("NewGameObj");

        MeshRenderer meshRenderer = newQuad.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = newQuad.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mesh.name = "NewGameObj";

        Vector3[] vert = new Vector3[] {
        new Vector3(Mathf.Cos((-90 + angle) * Mathf.Deg2Rad), 0, Mathf.Sin((-90 + angle) * Mathf.Deg2Rad)) * sideLength + startPoint,
        new Vector3(Mathf.Cos((90 + angle) * Mathf.Deg2Rad), 0, Mathf.Sin((90 + angle) * Mathf.Deg2Rad)) * sideLength + startPoint,
        new Vector3(Mathf.Cos((-90 + angle) * Mathf.Deg2Rad), 0, Mathf.Sin((-90 + angle) * Mathf.Deg2Rad)) * sideLength + endPoint,
        new Vector3(Mathf.Cos((90 + angle) * Mathf.Deg2Rad), 0, Mathf.Sin((90 + angle) * Mathf.Deg2Rad)) * sideLength + endPoint};

        int[] tri = new int[] { 0, 1, 2, 1, 3, 2 };

        Vector2[] uv = new Vector2[]{
        new Vector2(0, 0),
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(1, 1)};

        mesh.vertices = vert;
        mesh.triangles = tri;
        mesh.uv = uv;

        Material newMaterial = new Material(defaultMaterial.shader);
        newMaterial.color = materialColor;
        meshRenderer.material = newMaterial;
        meshFilter.mesh = mesh;

        return newQuad.transform;
    }

    /// <summary>
    /// ������� ������������� ����� ����� �������
    /// </summary>
    /// <param name="startPoint">��������� ����� ��������������</param>
    /// <param name="endPoint">�������� ����� ��������������</param>
    /// <param name="materialColor">���� ����</param>
    /// <param name="sideLength">������ ��������������</param>
    /// <returns>���������� Transform ���������� �������</returns>
    public Transform BuildQuad(Vector3 startPoint, Vector3 endPoint, Color materialColor, float sideLength = 1f)
    {
        Transform newQuad = BuildQuad(materialColor);

        newQuad.position = startPoint;
        newQuad.localScale = new Vector3(Vector3.Distance(startPoint, endPoint), 1, sideLength);
        newQuad.rotation = Quaternion.Euler(0, -Angle(startPoint, endPoint), 0);

        return newQuad;
    }

    /// <summary>
    /// ������� ������������� �� ���� ����� � �� ��������
    /// </summary>
    /// <param name="startPoint">��������� ����� ����������������</param>
    /// <param name="firstAngle">���� �������� ��������� ���������������</param>
    /// <param name="endPoint">�������� ����� ������� ���������</param>
    /// <param name="endAngle">���� �������� ������� ����� ����������������</param>
    /// <param name="materialColor">���� ����</param>
    /// <param name="sideLength">������ ����������������</param>
    /// <returns>���������� Transform ���������� �������</returns>
    public Transform BuildCurveQuad(Vector3 startPoint, float firstAngle, Vector3 endPoint, float endAngle, Color materialColor, float sideLength = 0.5f)
    {
        GameObject newQuad = new GameObject("NewGameObj");

        MeshRenderer meshRenderer = newQuad.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = newQuad.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mesh.name = "NewGameObj";

        Vector3[] vert = new Vector3[] {
        new Vector3(Mathf.Cos((-90 + firstAngle) * Mathf.Deg2Rad), 0, Mathf.Sin((-90 + firstAngle) * Mathf.Deg2Rad)) * sideLength + startPoint,
        new Vector3(Mathf.Cos((90 + firstAngle) * Mathf.Deg2Rad), 0, Mathf.Sin((90 + firstAngle) * Mathf.Deg2Rad)) * sideLength + startPoint,
        new Vector3(Mathf.Cos((-90 + endAngle) * Mathf.Deg2Rad), 0, Mathf.Sin((-90 + endAngle) * Mathf.Deg2Rad)) * sideLength + endPoint,
        new Vector3(Mathf.Cos((90 + endAngle) * Mathf.Deg2Rad), 0, Mathf.Sin((90 + endAngle) * Mathf.Deg2Rad)) * sideLength + endPoint};

        int[] tri = new int[] { 0, 1, 2, 1, 3, 2 };

        Vector2[] uv = new Vector2[]{
        new Vector2(0, 0),
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(1, 1)};

        mesh.vertices = vert;
        mesh.triangles = tri;
        mesh.uv = uv;

        Material newMaterial = new Material(defaultMaterial.shader);
        newMaterial.color = materialColor;
        meshRenderer.material = newMaterial;
        meshFilter.mesh = mesh;

        return newQuad.transform;
    }

    /// <summary>
    /// ������� ������ �� ���� ������ � �� ��������
    /// </summary>
    /// <param name="startPoint">��������� ����� ������</param>
    /// <param name="startAngle">��������� ���� ������</param>
    /// <param name="endPoint">�������� ����� ������</param>
    /// <param name="endAngle">�������� ���� ������</param>
    /// <param name="smoothness">��������� ������ (���������� ���������� �� �������� ����� �������� ������)</param>
    /// <param name="materialColor">���� ����</param>
    /// <param name="sideLength">������ ������</param>
    /// <returns>���������� ������ ����������</returns>
    public Transform[] BuildCurve(Vector3 startPoint, float startAngle, Vector3 endPoint, float endAngle, int smoothness, Color materialColor, float sideLength = 0.5f)
    {
        List<Transform> newCurve = new List<Transform>();

        Vector3[] curveCenters = GetCurveCenter(startPoint, startAngle, endPoint, endAngle);
        List<Vector3> curvePoints = new List<Vector3>();
        float deltaAngle = endAngle - startAngle;
        deltaAngle = deltaAngle > 180 ? -(360 - deltaAngle) : deltaAngle < -180 ? 360 + deltaAngle : deltaAngle;

        curvePoints.Add(startPoint);
        for (int i = 1; i < smoothness; i++)
        {
            Vector3 newCurvePoint = Vector3.Lerp(startPoint, endPoint, (float)i / smoothness);
            Vector3 interCurveCenter = Vector3.Lerp(curveCenters[0], curveCenters[1], (float)i / smoothness);
            Vector3 direction = new Vector3(newCurvePoint.x - interCurveCenter.x, 0, newCurvePoint.z - interCurveCenter.z);
            float directionAngle = Angle(interCurveCenter, newCurvePoint + direction);
            float radius = Mathf.Lerp(Vector3.Distance(curveCenters[0], startPoint), Vector3.Distance(curveCenters[1], endPoint), (float)i / smoothness);
            float delta = radius - Vector3.Distance(newCurvePoint, interCurveCenter);
            
            newCurvePoint = newCurvePoint + (Vector3.right * Mathf.Cos(directionAngle * Mathf.Deg2Rad) + Vector3.forward * Mathf.Sin(directionAngle * Mathf.Deg2Rad)) * delta;
            
            curvePoints.Add(newCurvePoint);
        }
        curvePoints.Add(endPoint);

        for (int i = 0; i < smoothness; i++) 
            newCurve.Add(BuildCurveQuad(curvePoints[i], startAngle + deltaAngle / smoothness * i, curvePoints[i + 1], startAngle + deltaAngle / smoothness * (i + 1), materialColor, sideLength));

        return newCurve.ToArray();
    }

    /// <summary>
    /// ���������� ��������� � �������� ����� ������
    /// </summary>
    /// <param name="start">��������� ����� ������</param>
    /// <param name="startAngle">��������� ���� ������</param>
    /// <param name="end">�������� ����� ������</param>
    /// <param name="endAngle">�������� ���� ������</param>
    /// <returns>���������� ��������� � �������� ����� ������</returns>
    private Vector3[] GetCurveCenter(Vector3 start, float startAngle, Vector3 end, float endAngle)
    {
        float accuracy = 0.01f;
        int side = Vector3.Distance(NextNormalPoint(start, startAngle, 1, 1), NextNormalPoint(end, endAngle, 1, 1)) < Vector3.Distance(NextNormalPoint(start, startAngle, -1, 1), NextNormalPoint(end, endAngle, -1, 1)) ? 1 : -1;

        Vector3 normalStart = start;
        Vector3 normalEnd = end;

        for (float i = accuracy; Vector3.Distance(normalStart, normalEnd) > accuracy; i += accuracy)
        {
            Vector3 newNormalStart = NextNormalPoint(start, startAngle, side, i);
            Vector3 newNormalEnd = NextNormalPoint(end, endAngle, side, i);

            if (Vector3.Distance(newNormalStart, newNormalEnd) > Vector3.Distance(normalStart, normalEnd))
                break;

            normalStart = newNormalStart;
            normalEnd = newNormalEnd;
        }

        return new[] { normalStart, normalEnd };
    }

    /// <summary>
    /// ��������������� ������� ���������� ����� ������ (����������)
    /// </summary>
    /// <param name="point">����� ������������� ������� ���������� ����� �������</param>
    /// <param name="pointAngle">���������� ���� ��������</param>
    /// <param name="side">������� �������</param>
    /// <param name="step">�������� �� ��������� �����</param>
    /// <returns>���������� ������� ������� ����� �� ������� ������� � ��������</returns>
    private Vector3 NextNormalPoint(Vector3 point, float pointAngle, int side, float step)
    { return point + step * (Vector3.right * Mathf.Cos((pointAngle + 90 * side) * Mathf.Deg2Rad) + Vector3.forward * Mathf.Sin((pointAngle + 90 * side) * Mathf.Deg2Rad)); }
}
