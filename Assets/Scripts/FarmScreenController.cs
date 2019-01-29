using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using SuperTiled2Unity;


public struct Zone {
    public string name;
    public Rect boundingBox;
}

public class FarmScreenController : MonoBehaviour
{

    private Rect growOutBounds;
    private List<Zone> zones = new List<Zone>();
    private Vector3 mapCellSize;

    private float zoneHeight; 
    private float zoneWidth;

    public static Zone? zoomTarget;

    void Start() {
        mapCellSize = gameObject.GetComponent<Grid>().cellSize;
        // Handle adding zones to the list here?
        growOutBounds = GetZoneRect("growOut");
        zones.Add(new Zone{name="growOut", boundingBox=growOutBounds});
    }

    void Update() {
        if (CameraController.inZone && CameraController.followTarget != null) {
            CameraController.followTarget = null;
        }
        if (Input.GetMouseButtonDown(0) && !CameraController.inZone) {  
            zoomTarget = GetZoneToZoom();     
        }
    }

    private Zone? GetZoneToZoom() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CameraController.isZooming = true;
        foreach (Zone zone in zones) {
            if (zone.boundingBox.Contains(mousePosition)) {
                return zone;
            }
        } 
        return null;
    }

    private Rect GetZoneRect(string zoneName) {
        GameObject zone = GameObject.Find(zoneName + "_base");
        Bounds zoneBounds = zone.GetComponent<TilemapRenderer>().bounds;

        Vector2 bottomRightCorner = new Vector2(zoneBounds.center.x + zoneBounds.extents.x, zoneBounds.center.y - zoneBounds.extents.y);
        
        foreach (SuperTiled2Unity.CustomProperty prop in zone.GetComponent<SuperCustomProperties>().m_Properties) {
            if (prop.m_Name == "height") zoneHeight = float.Parse(prop.m_Value) * mapCellSize.y;
            if (prop.m_Name == "width") zoneWidth = float.Parse(prop.m_Value) * mapCellSize.x;
        }

        Vector2 topLeftCorner = new Vector2(bottomRightCorner.x - zoneWidth, bottomRightCorner.y + zoneHeight);
        return Rect.MinMaxRect(topLeftCorner.x, bottomRightCorner.y, bottomRightCorner.x, topLeftCorner.y);
    }
}
