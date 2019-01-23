using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerScreenController : MonoBehaviour
{
    public float moveSpeed;
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

        currentMapMaxX = currentMap.transform.position.x + (mapExtents.x * 2);
        currentMapMinX = currentMap.transform.position.x;
        currentMapMaxY = currentMap.transform.position.y;
        currentMapMinY = currentMap.transform.position.y - (mapExtents.y * 2);
    }
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5) {
            float movement = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            float newLocation = gameObject.transform.position.x + movement;
            if (newLocation >= currentMapMinX && newLocation <= currentMapMaxX) {
                transform.Translate(new Vector3(movement, 0, 0));
            }
        }

        if (Input.GetAxisRaw("Vertical") > 0.5 || Input.GetAxisRaw("Vertical") < -0.5) {
            float movement = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
            float newLocation = gameObject.transform.position.y + movement;
            if (newLocation >= currentMapMinY && newLocation <= currentMapMaxY) {
                transform.Translate(new Vector3(0, movement, 0));
            }
        }
    }
}
