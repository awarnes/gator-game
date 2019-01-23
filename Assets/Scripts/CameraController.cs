using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class CameraController : MonoBehaviour {

    public static GameObject followTarget;

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

    void Start() {
        foreach (Tilemap map in currentMap.GetComponentsInChildren<Tilemap>()) {
            if (map.name == "base") currentTileMap = map;
        }

        mapExtents = currentTileMap.localBounds.extents;
        cameraOrtho = Camera.main.orthographicSize;
        cameraAspect = Camera.main.aspect;

        currentMapMaxX = currentMap.transform.position.x + (mapExtents.x * 2);
        currentMapMinX = currentMap.transform.position.x;
        currentMapMaxY = currentMap.transform.position.y;
        currentMapMinY = currentMap.transform.position.y - (mapExtents.y * 2);
    }

    void Update() {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            followTarget = null;
        }
        if (followTarget == null) {
            targetPosition = new Vector3((currentMapMaxX + currentMapMinX) / 2, (currentMapMaxY + currentMapMinY) / 2, transform.position.z);

            if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5) {
                targetPosition = new Vector3(Input.GetAxisRaw("Horizontal") * followSpeed * Time.deltaTime, 0, transform.position.z);
            }
            if (Input.GetAxisRaw("Vertical") > 0.5 || Input.GetAxisRaw("Vertical") < -0.5) {
                targetPosition = new Vector3(0, Input.GetAxisRaw("Vertical") * followSpeed * Time.deltaTime, transform.position.z);
            }
        } else {
            targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        }

        if (targetPosition.x + cameraAspect > currentMapMaxX) targetPosition.x = currentMapMaxX - cameraAspect;
        if (targetPosition.x - cameraAspect < currentMapMinX) targetPosition.x = currentMapMinX + cameraAspect;
        if (targetPosition.y + cameraOrtho > currentMapMaxY) targetPosition.y = currentMapMaxY - cameraOrtho;
        if (targetPosition.y - cameraOrtho < currentMapMinY) targetPosition.y = currentMapMinY + cameraOrtho;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
