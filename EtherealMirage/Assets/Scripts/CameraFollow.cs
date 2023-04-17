using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    
    public float smoothSpeed = 3;
    public Vector2 offset;
    private Transform MinX_MaxY;
    private Transform MaxX_MinY;
    private float limitMinX, limitMaxX, limitMinY, limitMaxY;
    float cameraHalfWidth, cameraHalfHeight;

    static public CameraFollow instance; // 변수 추가
    
    private void Start()
    {
        MinX_MaxY = GameObject.FindWithTag("MinX_MaxY").GetComponent<Transform>();
        MaxX_MinY = GameObject.FindWithTag("MaxX_MinY").GetComponent<Transform>();
        
        limitMinX = MinX_MaxY.position.x;
        limitMaxY = MinX_MaxY.position.y;
        limitMaxX = MaxX_MinY.position.x;
        limitMinY = MaxX_MinY.position.y;
        
        target = GameObject.FindWithTag("User").GetComponent<Transform>();

        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }
    
    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
    
}
