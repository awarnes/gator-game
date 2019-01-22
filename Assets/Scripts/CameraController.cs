using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    public GameObject followTarget;
    private Vector3 targetPosition;

    public float followSpeed;

    public GameObject currentMap;
    private Tilemap currentTileMap; 

    private Vector2 mapExtents;
    private float cameraAspect; // x
    private float cameraOrtho; // y

    private float currentMapMaxX;
    private float currentMapMinX;
    private float currentMapMaxY;
    private float currentMapMinY;

    void Start()
    {
        foreach (Tilemap map in currentMap.GetComponentsInChildren<Tilemap>()) {
            if (map.name == "base") currentTileMap = map;
        }

        mapExtents = currentTileMap.localBounds.extents;
        cameraOrtho = Camera.main.orthographicSize;
        cameraAspect = Camera.main.aspect;
    }

    void Update()
    {   
        currentMapMaxX = currentMap.transform.position.x + (mapExtents.x * 2);
        currentMapMinX = currentMap.transform.position.x;
        currentMapMaxY = currentMap.transform.position.y;
        currentMapMinY = currentMap.transform.position.y - (mapExtents.y * 2);

        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);

        if (targetPosition.x + cameraAspect > currentMapMaxX) targetPosition.x = currentMapMaxX - cameraAspect;
        if (targetPosition.x - cameraAspect < currentMapMinX) targetPosition.x = currentMapMinX + cameraAspect;
        if (targetPosition.y + cameraOrtho > currentMapMaxY) targetPosition.y = currentMapMaxY - cameraOrtho;
        if (targetPosition.y - cameraOrtho < currentMapMinY) targetPosition.y = currentMapMinY + cameraOrtho;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
