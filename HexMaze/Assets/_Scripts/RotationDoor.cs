﻿using UnityEngine;
using System.Collections;

public class RotationDoor : MonoBehaviour {
    [SerializeField] float speed = 2f;
    [SerializeField] bool isPlayerTouching;
    LTDescr currentRotation;
    float deltaAngle = 3f;
    float deltaAngleReverse = 90f;
    bool isMoved;
    bool enableMoving = true;
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        if (enableMoving) {
            if (transform.eulerAngles.z < 360f - deltaAngle && transform.eulerAngles.z > 240f && GetComponent<Rigidbody2D>().angularVelocity != 0f) {
                if (currentRotation == null) {
                    RotateToAngle(240f);
                    if (transform.eulerAngles.z > 240f+deltaAngleReverse) {
                        isMoved = true;
                    }
                }
            }
            if (transform.eulerAngles.z < 240f - deltaAngle && transform.eulerAngles.z > 120f && GetComponent<Rigidbody2D>().angularVelocity != 0f) {
                if (currentRotation == null) {
                    RotateToAngle(120f);
                    if (transform.eulerAngles.z > 1200f+deltaAngleReverse) {
                        isMoved = true;
                    }
                }
            }
            if (transform.eulerAngles.z < 120f - deltaAngle && transform.eulerAngles.z > 0f && GetComponent<Rigidbody2D>().angularVelocity != 0f) {
                if (currentRotation == null) {
                    RotateToAngle(0f);
                    if (transform.eulerAngles.z > 0f+deltaAngleReverse) {
                        isMoved = true;
                    }
                }
            }
        } else {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            NextLevelCheck();
        }
    }
    void NextLevelCheck () {
        if (isMoved) {
            SpriteRenderer[] rendererList = new SpriteRenderer[99];
            rendererList = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer r in rendererList) {
                r.color = Color.red;
            }
            enableMoving = false;

        }

    }
    void RotateToAngle (float angle) {
        
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        currentRotation = LeanTween.rotateZ(gameObject, angle, 2f).setOnComplete(() => {
                Invoke("AllowDoorMovement", 0.5f);
            });
    }

    void AllowDoorMovement () {
        GetComponent<Rigidbody2D>().isKinematic = false;
        currentRotation = null;
    }
    
    public void InCollider (bool isColliding) {
        isPlayerTouching = isColliding;
    }
    
}
