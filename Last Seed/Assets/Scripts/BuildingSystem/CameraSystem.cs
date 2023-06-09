using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Camera main;
    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;
    private float targetOrtographicSize = 14;
    private float orthographicSizeMin = 5;
    private float orthographicSizeMax = 25;
        
    void Start()
    {
        
    }

    void Update()
    {
        HandleCameraMovement();
        HandleCameraZoom();
    }

    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0,0,0);

        if(Input.GetKey(KeyCode.W)) inputDir.y = +1f;
        if(Input.GetKey(KeyCode.S)) inputDir.y = -1f;
        if(Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if(Input.GetKey(KeyCode.D)) inputDir.x = +1f;
        
        int edgeScrollSize = 10;
        
        if(Input.mousePosition.x < edgeScrollSize)
        {
            inputDir.x = -1f;
        }
        if(Input.mousePosition.y < edgeScrollSize) 
        {
            inputDir.y = -1f;
        }
        if(Input.mousePosition.x > Screen.width - edgeScrollSize) 
        {
            inputDir.x = +1;
        }
        if(Input.mousePosition.y > Screen.height - edgeScrollSize) 
        {
            inputDir.y = +1;
        }

        if(Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        
        if(Input.GetMouseButtonUp(1))
        {
            dragPanMoveActive = false;
        }

        if(dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;
            float dragPanSpeed = 1.5f;
            inputDir.x = -(mouseMovementDelta.x * dragPanSpeed);
            inputDir.y = -(mouseMovementDelta.y * dragPanSpeed);
            lastMousePosition = Input.mousePosition;
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 20f;
        transform.position += inputDir * moveSpeed * Time.deltaTime;

    }

    private void HandleCameraZoom()
    {

        if(Input.mouseScrollDelta.y > 0)
        {
            targetOrtographicSize -= 5;
        }

        if(Input.mouseScrollDelta.y < 0)
        {
            targetOrtographicSize += 5;
        }

        float zoomSpeed = 10f;
        targetOrtographicSize = Mathf.Clamp(targetOrtographicSize, orthographicSizeMin, orthographicSizeMax);
        main.orthographicSize = Mathf.Lerp(main.orthographicSize, targetOrtographicSize, Time.deltaTime * zoomSpeed);
    }

}
