using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� �������� ������
/// �������� ������� ����� �������� ������
/// ����� �������� ���������� Transform �������� �� ������� ������� ������� ������
/// ����� �������� ������ � ����� ����� � ���� �������� (���������)
/// ������� ������ ������������ ����� 3 ���� (15% - 70% - 15%)
/// 1 ��� - ������ �������� (15% �� ��� ����� ��������)
/// �������� ������� ������ � ������� �������� ������
/// 2 ��� - �������� �������� (70% �� ��� ���� ��������)
/// �������� �������� ����� �������� - ������ �������
/// 3 ��� - ����� �������� (15% �� ��� ����� ��������)
/// �������� ������� ������ � ���������� �������� ������
/// </summary>
public class RoadSegment
{
    //���� ������
    private List<Transform> simpleSegments;
    private List<Vector3> refPoints;
    private float angle;
    private float length;
    //�������� ������
    public List<Transform> Segments { get { return simpleSegments; } }
    public List<Vector3> RefPoints { get { return refPoints; } }
    public float Angle { get { return angle; } }
    public float Length { get { return length; } }
    /// <summary>
    /// ����������� �������� ������
    /// ������� ������ �������� ������ �� ���� ������
    /// ������������ ��� ��������� � �������� ������
    /// </summary>
    /// <param name="firstRoadPoint">������ ����� �������� ������</param>
    /// <param name="secondRoadPoint">���������� ����� �������� ������</param>
    public RoadSegment(Vector3 firstRoadPoint, Vector3 secondRoadPoint)
    {
        angle = MeshBuilder.Angle(firstRoadPoint, secondRoadPoint);
        length = Vector3.Distance(firstRoadPoint, secondRoadPoint);
        simpleSegments = new List<Transform>();
        refPoints = new List<Vector3>();

        refPoints.Add(firstRoadPoint);

        float segmentLength = length * 0.15f;
        Vector3 temp = NextPoint(firstRoadPoint, segmentLength);
        refPoints.Add(temp);

        segmentLength = length * 0.7f;
        temp = NextPoint(refPoints[1], segmentLength);
        refPoints.Add(temp);

        refPoints.Add(secondRoadPoint);
    }
    /// <summary>
    /// ����� ��������� Transform ������� ���� ��������
    /// </summary>
    /// <returns>���������� ��������� Transform ������� ���� ��������</returns>
    public Transform GetFirstSimpleSegment() { return simpleSegments[0]; }
    /// <summary>
    /// ����� ��������� Transform ���������� ���� ��������
    /// </summary>
    /// <returns>���������� ��������� Transform ���������� ���� ��������</returns>
    public Transform GetLastSimpleSegment() { return simpleSegments[simpleSegments.Count - 1]; }
    /// <summary>
    /// ����� ��������� ��������� ������ ������� �����
    /// </summary>
    /// <returns>���������� ���������� ������ ������� �����</returns>
    public Vector3 GetFirstRefPoint() { return refPoints[0]; }
    /// <summary>
    /// ����� ��������� ��������� ��������� ������� �����
    /// </summary>
    /// <returns>���������� ���������� ��������� ������� �����</returns>
    public Vector3 GetLastRefPoint() { return refPoints[refPoints.Count - 1]; }
    /// <summary>
    /// ����� ��������� ��������� ������� ����� �� ��������� ����� � �����
    /// </summary>
    /// <param name="start">����� ������������� ������� ���������� �������� ��������� �����</param>
    /// <param name="length">����� �� ������� ���������� �������� ��������� �����</param>
    /// <returns>���������� ��������� ������� ����� �� ����� � ��������� �����</returns>
    private Vector3 NextPoint(Vector3 start, float length) { return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * length + start; }
}
