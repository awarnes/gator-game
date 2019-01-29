using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class CameraController : MonoBehaviour {

    public static GameObject followTarget;

    private Vector3 targetPosition;
    private float fullScreenZoom;

    public float followSpeed;
    public float zoomTime;
    public static bool isZooming; // true while zooming to a zone
    public static bool inZone; // Allow follow targets when in a zone.

    public GameObject currentMap;
    private Tilemap currentTileMap; 

    private Vector2 mapExtents;
    private float cameraAspect; // x
    private float cameraOrtho; // y

    private float currentMapMaxX;
    private float currentMapMinX;
    private float currentMapMaxY;
    private float currentMapMinY;

    void Start() {
        foreach (Tilemap map in currentMap.GetComponentsInChildren<Tilemap>()) {
            if (map.name == "main_base") currentTileMap = map;
        }

        mapExtents = currentTileMap.localBounds.extents;

        currentMapMaxX = currentMap.transform.position.x + (mapExtents.x * 2);
        currentMapMinX = currentMap.transform.position.x;
        currentMapMaxY = currentMap.transform.position.y;
        currentMapMinY = currentMap.transform.position.y - (mapExtents.y * 2);
        
        targetPosition = new Vector3((currentMapMaxX + currentMapMinX) / 2, (currentMapMaxY + currentMapMinY) / 2, transform.position.z);
        fullScreenZoom = Camera.main.orthographicSize;
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y")!=0){
                targetPosition = new Vector3(transform.position.x + Input.GetAxis("Mouse X") * followSpeed * -1 * Time.deltaTime, transform.position.y + Input.GetAxis("Mouse Y") * followSpeed * Time.deltaTime * -1, transform.position.z);
                followTarget = null;
                isZooming = false;
            }
        }

        // Whether camera is focused on a zone or not.
        if (FarmScreenController.zoomTarget != null && !isZooming){
            inZone = true;
        } else {
            inZone = false;
        }
        // Remove zoomTarget when ESC pressed
        if (Input.GetKeyDown(KeyCode.Escape)) {
            FarmScreenController.zoomTarget = null;
        }
        SetZone();

        if (followTarget != null) {
            targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        }

        // Maintain camera position to be within bounds of map
        if (targetPosition.x + Camera.main.aspect * Camera.main.orthographicSize > currentMapMaxX) {
            targetPosition.x = currentMapMaxX - Camera.main.aspect * Camera.main.orthographicSize;
        }

        if (targetPosition.x - Camera.main.aspect * Camera.main.orthographicSize < currentMapMinX) {
            targetPosition.x = currentMapMinX + Camera.main.aspect * Camera.main.orthographicSize;
        }
        if (targetPosition.y + Camera.main.orthographicSize > currentMapMaxY){
            targetPosition.y = currentMapMaxY - Camera.main.orthographicSize;
        } 
        if (targetPosition.y - Camera.main.orthographicSize < currentMapMinY){
            targetPosition.y = currentMapMinY + Camera.main.orthographicSize;
        }

        // Move camera
        if (FarmScreenController.zoomTarget == null) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, fullScreenZoom, zoomTime * Time.deltaTime);
        } else if (isZooming) {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 1, zoomTime * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(FarmScreenController.zoomTarget?.boundingBox.center.x ?? 0, FarmScreenController.zoomTarget?.boundingBox.center.y ?? 0, -10), zoomTime * Time.deltaTime);
            
            if (HasZoomed()) {
                isZooming = false;
            }
        } else {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 5 * Time.deltaTime);
        }
    }

    // Finished zooming
    private bool HasZoomed() {
        return Camera.main.orthographicSize >= 0.5 && Camera.main.orthographicSize <= 1.001;
    }

    // Set current map to current focus (zone/full)
    void SetZone() {
        if (FarmScreenController.zoomTarget != null) {
            currentMapMaxX = FarmScreenController.zoomTarget?.boundingBox.xMax ?? 0;
            currentMapMinX = FarmScreenController.zoomTarget?.boundingBox.xMin ?? 0;
            currentMapMaxY = FarmScreenController.zoomTarget?.boundingBox.yMax ?? 0;
            currentMapMinY = FarmScreenController.zoomTarget?.boundingBox.yMin ?? 0;
        } else {
            currentMapMaxX = currentMap.transform.position.x + (mapExtents.x * 2);
            currentMapMinX = currentMap.transform.position.x;
            currentMapMaxY = currentMap.transform.position.y;
            currentMapMinY = currentMap.transform.position.y - (mapExtents.y * 2);
        }
    }
}
