using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GatorGame {
    public class GatorController : MonoBehaviour, ITargetable {
        public float moveSpeed;

        private Rigidbody2D myRigidbody;

        private bool moving;

        public float timeBetweenMove;
        private float timeBetweenMoveCounter;
        public float timeToMove;
        private float timeToMoveCounter;

        private Vector3 moveDirection;

        public AlligatorData gatorData;

        void Start() {
            myRigidbody = GetComponent<Rigidbody2D>();
            gatorData = new AlligatorData();

            timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        }

        void Update() {
            if (moving) {
                timeToMoveCounter -= Time.deltaTime;
                myRigidbody.velocity = moveDirection;

                if (timeToMoveCounter < 0) {
                    moving = false;
                    timeBetweenMoveCounter = timeBetweenMove;
                }
            } else {
                timeBetweenMoveCounter -= Time.deltaTime;
                myRigidbody.velocity = Vector2.zero;

                if (timeBetweenMoveCounter < 0) {
                    moving = true;
                    timeToMoveCounter = timeToMove;

                    moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0);
                }
            }

            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        void OnMouseDown() {
            SetThisAsFollowTarget();
            PrintNameAndMeasurements();
        }

        public void SetThisAsFollowTarget() {
            CameraController.followTarget = gameObject;
        }

        public void PrintNameAndMeasurements() {
            Debug.Log("NAME: " + gatorData.GatorName);
            Debug.Log("Height: " + gatorData.measurements.height);
            Debug.Log("Weight: " + gatorData.measurements.weight);
            Debug.Log("Girth: " + gatorData.measurements.girth);
        }
    }
}