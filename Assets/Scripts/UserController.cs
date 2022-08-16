using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс управления камерой (пользователем)
/// mb разделить на 2 класса
/// 1 класс - перемещение
/// 2 класс - строительство дороги
/// </summary>
public class UserController : MonoBehaviour
{
    //"Завод" дороги
    private RoadBuilder roadBuilder;
    //Настройки камеры
    private float mouseSensitivity;
    private float cameraSpeed;
    private float yLockRotation;
    private float xRotation;
    private float yRotation;
    private Camera cam;

    //Режимы пользователя
    private CameraMode mode;
    private enum CameraMode
    {
        Spectator,  //Перемещение по карте
        Creator     //Создание дороги
    }

    //Свойства класса
    public RoadBuilder RoadBuilder { set { roadBuilder = value; } }
    public float MouseSensitivity { set { mouseSensitivity = value; } }
    public float CameraSpeed { set { cameraSpeed = value; } }
    public float YLockRotation { set { yLockRotation = value; } }

    private void Start()
    {
        cam = GetComponent<Camera>();

        mode = CameraMode.Creator;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) SwitchMode();

        if (mode == CameraMode.Spectator)
        {
            xRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
            yRotation += Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation = ClampXRotation(xRotation);
            yRotation = Mathf.Clamp(yRotation, -yLockRotation, yLockRotation);

            transform.rotation = Quaternion.Euler(-yRotation, xRotation, 0);

            float xAxisInput = Input.GetAxis("Horizontal");
            float yAxisInput = Input.GetAxis("Vertical");

            Vector3 moveDir = (xAxisInput * transform.right + yAxisInput * transform.forward).normalized;

            if (Input.GetKey(KeyCode.LeftShift))
                transform.position += moveDir * 2 * cameraSpeed * Time.deltaTime;
            else
                transform.position += moveDir * cameraSpeed * Time.deltaTime;
        }
        else
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //lmb
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
                roadBuilder.SetNewPoint(hit.point);
        }
    }

    private float ClampXRotation(float xRot)
    {
        float newXRot = xRot;

        if (newXRot < -360f)
            while (newXRot < -360f)
                newXRot += 360f;

        if (newXRot > 360f)
            while (newXRot > 360f)
                newXRot -= 360f;

        return newXRot;
    }

    /// <summary>
    /// Переключение режима пользователя
    /// </summary>
    private void SwitchMode()
    {
        if (mode == CameraMode.Spectator)
        {
            mode = CameraMode.Creator;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            mode = CameraMode.Spectator;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}