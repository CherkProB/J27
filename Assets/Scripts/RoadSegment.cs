using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс сегмента дороги
/// Содержит опорные точки сегмента дороги
/// Также содержит компоненты Transform объектов из которых состоит сегмент дороги
/// Также содержит данные о своей длине и угле поворота (глобально)
/// Сегмент дороги представляет собой 3 меша (15% - 70% - 15%)
/// 1 меш - начало сегмента (15% от все длины сегмента)
/// Остается видимым только у первого сегмента дороги
/// 2 меш - середина сегмента (70% от все дины сегмента)
/// Является основным мешом сегмента - всегда видимый
/// 3 меш - конец сегмента (15% от все длины сегмента)
/// Остается видимым только у последнего сегмента дороги
/// </summary>
public class RoadSegment
{
    //Поля класса
    private List<Transform> simpleSegments;
    private List<Vector3> refPoints;
    private float angle;
    private float length;
    //Свойства класса
    public List<Transform> Segments { get { return simpleSegments; } }
    public List<Vector3> RefPoints { get { return refPoints; } }
    public float Angle { get { return angle; } }
    public float Length { get { return length; } }
    /// <summary>
    /// Конструктор сегмента дороги
    /// Создает объект сегмента дороги по двум точкам
    /// Рассчитывает всю информцию о сегменте дороги
    /// </summary>
    /// <param name="firstRoadPoint">Первая точка сегмента дороги</param>
    /// <param name="secondRoadPoint">Полследняя точка сегмента дороги</param>
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
    /// Метод получения Transform первого меша сегмента
    /// </summary>
    /// <returns>Возвращает компонент Transform первого меша сегмента</returns>
    public Transform GetFirstSimpleSegment() { return simpleSegments[0]; }
    /// <summary>
    /// Метод получения Transform последнего меша сегмента
    /// </summary>
    /// <returns>Возвращает компонент Transform последнего меша сегмента</returns>
    public Transform GetLastSimpleSegment() { return simpleSegments[simpleSegments.Count - 1]; }
    /// <summary>
    /// Метод получения координат первой опорной точки
    /// </summary>
    /// <returns>Возвращает координаты первой опроной точки</returns>
    public Vector3 GetFirstRefPoint() { return refPoints[0]; }
    /// <summary>
    /// Метод получения координат последней опорной точки
    /// </summary>
    /// <returns>Возвращает координаты последней опорной точки</returns>
    public Vector3 GetLastRefPoint() { return refPoints[refPoints.Count - 1]; }
    /// <summary>
    /// Метод получения следующей опорной точки по начальной точки и длине
    /// </summary>
    /// <param name="start">Точка относительной которой необходимо отложить следеющую точку</param>
    /// <param name="length">Длина на которую необходимо отложить следующую точку</param>
    /// <returns>Возвращает следующую опорную точку по длине и начальной точке</returns>
    private Vector3 NextPoint(Vector3 start, float length) { return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * length + start; }
}
