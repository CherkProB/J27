using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��������� �������
/// </summary>
public class Startup : MonoBehaviour
{
    [Header("CameraController")]
    [SerializeField] private UserController userController;
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float cameraSpeed = 6f;
    [SerializeField] private float yLockRotation = 80f;

    [Header("Ground")]
    [SerializeField] private float fieldSize = 50;
    [SerializeField] private Transform groundParent;

    [Header("Material")]
    [SerializeField] private Material defaultMaterial;


    private void Awake()
    {
        userController.MouseSensitivity = mouseSensitivity;
        userController.CameraSpeed = cameraSpeed;
        userController.YLockRotation = yLockRotation;
    }

    private void Start()
    {
        //�������� "������" �����
        MeshBuilder builder = new MeshBuilder(defaultMaterial);

        //�������� �����
        Transform ground = builder.BuildQuad(Color.green, fieldSize);
        ground.position -= new Vector3(fieldSize / 2, -0.5f, 0);
        ground.parent = groundParent;

        //�������� ���������� �����
        BoxCollider groundCollider = ground.gameObject.AddComponent<BoxCollider>();
        groundCollider.enabled = true;
        groundCollider.size = new Vector3(fieldSize, 0.1f, fieldSize);

        //�������� "������" ������ � �������� ��� ������ ������������
        userController.RoadBuilder = new RoadBuilder(builder);
    }
}
