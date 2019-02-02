using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using SuperTiled2Unity;

namespace GatorGame {
    public struct Zone {
        public string name;
        public Rect boundingBox;
    }

    public class CameraController : MonoBehaviour {

        public static GameObject followTarget;

        private Vector3 targetPosition;
        private float fullScreenZoom;

        public float followSpeed;
        public float zoomTime;
        public static bool isZooming; // true while zooming to a zone
        public static bool inZone; // Allow follow targets when in a zone.

        private List<Zone> zones = new List<Zone>();
        private Vector3 mapCellSize;

        private float zoneHeight; 
        private float zoneWidth;

        public static Zone? zoomTarget;

        // Full map of area using Tiled and SuperTiled2Unity
        public GameObject currentMap;
        // Individual layers of map. Also used for individual zones.
        public Tilemap currentTileMap; 

        private Vector2 mapExtents;
        private float cameraAspect; // x
        private float cameraOrtho; // y

        private float currentMapMaxX;
        private float currentMapMinX;
        private float currentMapMaxY;
        private float currentMapMinY;

        void Start() {
            mapExtents = currentTileMap.localBounds.extents;
            
            SetZoneBounds();

            targetPosition = new Vector3((currentMapMaxX + currentMapMinX) / 2, (currentMapMaxY + currentMapMinY) / 2, transform.position.z);
            fullScreenZoom = Camera.main.orthographicSize;

            // Get main map cell sizes
            mapCellSize = currentMap.GetComponent<Grid>().cellSize;

            // Handle adding zones to the list here
            // TODO: Handle in more perfomant way.
            // When we have more objects this could become very slow
            foreach (GameObject layer in GameObject.FindObjectsOfType<GameObject>()) {
                if (layer.name.Contains("_base")) {
                    Rect zoneRect = GetZoneRect(layer.name);
                    zones.Add(new Zone{name=layer.name, boundingBox=zoneRect});
                }
            }
        }

        void Update() {
            // Just using left click
            if (Input.GetMouseButton(0)) {
                // Click and drag implemenation
                if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0 && !isZooming){
                    float mouseMoveX = transform.position.x + Input.GetAxis("Mouse X") * followSpeed * Time.deltaTime * -1;
                    float mouseMoveY = transform.position.y + Input.GetAxis("Mouse Y") * followSpeed * Time.deltaTime * -1;

                    targetPosition = new Vector3(mouseMoveX, mouseMoveY, transform.position.z);

                    // Reset camera if moving around screen manually.
                    followTarget = null;
                    isZooming = false;
                }
                // Find zone to zoom if clicked
                if (!inZone) {
                    zoomTarget = GetZoneToZoom();
                }
            }

            // Whether camera is focused on a zone or not.
            if (zoomTarget != null && !isZooming){
                inZone = true;
            } else {
                inZone = false;
            }

            // Remove zoomTarget when ESC pressed
            if (Input.GetKeyDown(KeyCode.Escape)) {
                // Close a modal before closing the zoomTarget.
                bool wasModal = false;
                foreach(GameObject modal in GameObject.FindGameObjectsWithTag("Modal")) {
                    if (modal.activeSelf) {
                        modal.SetActive(false);
                        wasModal = true;
                    }
                }

                if (!wasModal) zoomTarget = null;
            }

            SetZoneBounds();

            // Where to follow if following a target.
            if (followTarget != null) {
                targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
            }

            // Maintain camera position to be within bounds of a given map.
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

            // Move the camera around the scene.
            if (zoomTarget == null) {
                // Without a zoomTarget the camera should zoom to the whole screen.
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, fullScreenZoom, zoomTime * Time.deltaTime);
            } else if (isZooming) {
                // If zooming into a zone.
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 1, zoomTime * Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, new Vector3(zoomTarget?.boundingBox.center.x ?? 0, zoomTarget?.boundingBox.center.y ?? 0, -10), zoomTime * Time.deltaTime);
                
                // If we're done zooming, say so.
                // TODO: Not fully disallowing mouse input during zoom transition, can lead to weird bugs.
                if (HasZoomed()) {
                    isZooming = false;
                }
            } else {
                // Click and drag and followTarget moves
                transform.position = Vector3.Lerp(transform.position, targetPosition, 5 * Time.deltaTime);
            }
        }

        // Verify that zooming has finished so that users can regain control of camera.
        // TODO: More precise, mathematical way of verifing that the camera has finished zooming.
        private bool HasZoomed() {
            return Camera.main.orthographicSize >= 0.5 && Camera.main.orthographicSize <= 1.001;
        }

        // Set currentMap bounds to current focus (zone/full)
        void SetZoneBounds() {
            if (zoomTarget != null) {
                currentMapMaxX = zoomTarget?.boundingBox.xMax ?? 0;
                currentMapMinX = zoomTarget?.boundingBox.xMin ?? 0;
                currentMapMaxY = zoomTarget?.boundingBox.yMax ?? 0;
                currentMapMinY = zoomTarget?.boundingBox.yMin ?? 0;
            } else {
                currentMapMaxX = currentMap.transform.position.x + (mapExtents.x * 2);
                currentMapMinX = currentMap.transform.position.x;
                currentMapMaxY = currentMap.transform.position.y;
                currentMapMinY = currentMap.transform.position.y - (mapExtents.y * 2);
            }
        }

        // Returns the zone that was clicked on, or null.
        private Zone? GetZoneToZoom() {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (Zone zone in zones) {
                if (zone.boundingBox.Contains(mousePosition)) {
                    isZooming = true;
                    return zone;
                }
            } 
            return null;
        }

        // Returns the calculated Rect for each zone associated with the current map so they can be iterated over.
        private Rect GetZoneRect(string zoneName) {
            // All zones must be named with a _base at the end.
            GameObject zone = GameObject.Find(zoneName);
            Bounds zoneBounds = zone.GetComponent<TilemapRenderer>().bounds;

            Vector2 bottomRightCorner = new Vector2(zoneBounds.center.x + zoneBounds.extents.x, zoneBounds.center.y - zoneBounds.extents.y);
            
            // TODO: This seems to be digging deeper than it really should.
            foreach (SuperTiled2Unity.CustomProperty prop in zone.GetComponent<SuperCustomProperties>().m_Properties) {
                // TODO: Each zone's base object must record these as there's no easy way to verify the height/width in tiles.
                if (prop.m_Name == "height") zoneHeight = float.Parse(prop.m_Value) * mapCellSize.y;
                if (prop.m_Name == "width") zoneWidth = float.Parse(prop.m_Value) * mapCellSize.x;
            }

            Vector2 topLeftCorner = new Vector2(bottomRightCorner.x - zoneWidth, bottomRightCorner.y + zoneHeight);
            return Rect.MinMaxRect(topLeftCorner.x, bottomRightCorner.y, bottomRightCorner.x, topLeftCorner.y);
        }
    }
}