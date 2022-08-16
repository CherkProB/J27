using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс "завода" дороги
/// Содержит координаты начала дороги
/// Также содержит коллекцию сегментов дороги
/// Также содержит объект MeshBuilder который создает мешы для дороги
/// </summary>
public class RoadBuilder
{
    //Поля класса
    private Vector3 beginRoad;
    private List<RoadSegment> segments;
    private MeshBuilder meshBuilder;
    /// <summary>
    /// Конструктор создания "завода" дороги
    /// Создает объект RoadBuilder через который можно создавать дорогу
    /// </summary>
    /// <param name="builder">Объект MeshBuilder который будет создавать мешы для дороги</param>
    public RoadBuilder(MeshBuilder builder) 
    {
        beginRoad = new Vector3(0, -10, 0);
        segments = new List<RoadSegment>();
        //points = new List<Vector3>();
        meshBuilder = builder;
    }
    /// <summary>
    /// Метод добавления новой точки дороги
    /// </summary>
    /// <param name="newPoint">Координаты новой точки дороги</param>
    public void SetNewPoint(Vector3 newPoint) 
    {
        if (beginRoad == new Vector3(0, -10, 0)) 
        {
            beginRoad = newPoint;
            return;
        }

        RoadSegment newSegment = null;
        RoadSegment lastSegment = null;

        if (segments.Count == 0)
            newSegment = new RoadSegment(beginRoad, newPoint);
        else 
        {
            lastSegment = segments[segments.Count - 1];
            newSegment = new RoadSegment(lastSegment.GetLastRefPoint(), newPoint);
        }

        Vector3 firstPoint = newSegment.RefPoints[0];
        newSegment.Segments.Add(meshBuilder.BuildQuad(firstPoint, newSegment.Angle, newSegment.Length * 0.15f, Color.grey));
        
        firstPoint = newSegment.RefPoints[1];
        newSegment.Segments.Add(meshBuilder.BuildQuad(firstPoint, newSegment.Angle, newSegment.Length * 0.7f, Color.grey));
        
        firstPoint = newSegment.RefPoints[2];
        newSegment.Segments.Add(meshBuilder.BuildQuad(firstPoint, newSegment.Angle, newSegment.Length * 0.15f, Color.grey));

        segments.Add(newSegment);

        if (lastSegment != null) 
        {
            GameObject.Destroy(lastSegment.GetLastSimpleSegment().gameObject);
            GameObject.Destroy(newSegment.GetFirstSimpleSegment().gameObject);

            meshBuilder.BuildCurve(lastSegment.RefPoints[lastSegment.RefPoints.Count - 2], lastSegment.Angle, newSegment.RefPoints[1], newSegment.Angle, 5, Color.grey);
        }
    }
}
