﻿using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Positioning")]
    public Vector2 cameraOffset = new Vector2(10f, 14f);
    public float lookAtOffset = 20f;

    [Header("Move Controls")]
    public float inOutSpeed = 75f;
    public float lateralSpeed = 75f;

    [Header("Move Bounds")]
    public Vector2 minBounds, maxBounds;

    [Header("Zoom Controls")]
    public float zoomSpeed = 30f;
    public float nearZoomLimit = 50f;
    public float farZoomLimit = 1000f;
    public float startingZoom = 50f;

    IZoomStrategy zoomStrategy;
    Vector3 frameMove;
    float frameZoom;
    Camera cam;

    private void Awake()
    {
        maxBounds.x = 190;
        maxBounds.y = 190;
        cam = GetComponentInChildren<Camera>();
        cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x));
        zoomStrategy = cam.orthographic ? ((IZoomStrategy)new OrthographicZoomStrategy(cam, startingZoom)) : new PerspectiveZoomStrategy(cam, cameraOffset, startingZoom);
        cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);
    }

    private void OnEnable()
    {
        KeyboardInputManager.OnMoveInput += UpdateFrameMove;
        KeyboardInputManager.OnZoomInput += UpdateFrameZoom;
        MouseInputManager.OnMoveInput += UpdateFrameMove;
        MouseInputManager.OnZoomInput += UpdateFrameZoom;
    }

    private void OnDisable()
    {
        KeyboardInputManager.OnMoveInput -= UpdateFrameMove;
        KeyboardInputManager.OnZoomInput -= UpdateFrameZoom;
        MouseInputManager.OnMoveInput -= UpdateFrameMove;
        MouseInputManager.OnZoomInput -= UpdateFrameZoom;
    }

    private void UpdateFrameZoom(float zoomAmount)
    {
        frameZoom += zoomAmount;
    }

    private void UpdateFrameMove(Vector3 moveVector)
    {
        frameMove += moveVector;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            cam = GetComponentInChildren<Camera>();
            cameraOffset.x = 15f;
            cameraOffset.y = 20f;
            lookAtOffset = 20f;
            transform.localPosition = new Vector3(40f, 0, 40f);
            cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x));
            zoomStrategy = cam.orthographic ? ((IZoomStrategy)new OrthographicZoomStrategy(cam, startingZoom)) : new PerspectiveZoomStrategy(cam, cameraOffset, startingZoom);
            cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);
        }
        if(frameMove != Vector3.zero)
        {
            Vector3 speedModFrameMove = new Vector3(frameMove.x * lateralSpeed, frameMove.y, frameMove.z * inOutSpeed);
            transform.position += transform.TransformDirection(speedModFrameMove) * Time.deltaTime;
            LockPositionInBounds();
            frameMove = Vector3.zero;
        }

        if(frameZoom < 0f)
        {
            zoomStrategy.ZoomIn(cam, Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed, nearZoomLimit);
            frameZoom = 0f;
        }
        
        if(frameZoom > 0f)
        {
            zoomStrategy.ZoomOut(cam, Time.deltaTime * frameZoom * zoomSpeed, farZoomLimit);
            frameZoom = 0f;
        }
    }

    private void LockPositionInBounds()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y)
            );
    }
}