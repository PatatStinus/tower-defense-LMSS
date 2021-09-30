using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Author>
/// Lisa Werner
/// </Author>
public class CameraMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float cameraPanSpeed = 20f;
    [SerializeField] private float cameraScrollSpeed = 4f;
    [SerializeField] private float scrollSpeedMultiplier = 2f;
    [SerializeField] private float minZoomHeightY = 4f; //this value is currently just for testing, will set them as constants once we have a map
    [SerializeField] private float maxZoomHeightY = 54f; //this value is currently just for testing, will set them as constants once we have a map
    [SerializeField] private float scroll;
    [SerializeField] private bool canScroll;

    private float scrollClampMin = -0.2f;
    private float scrollClampMax = 0.2f;

    private void Update()
    {
        MoveCamera(transform.position);
        ScrollCamera(transform.position);
    }

    private void MoveCamera(Vector3 cameraPosition)
    {
        if (Input.GetMouseButton(2))
        {
            cameraPosition.x -= Input.GetAxis("Mouse X") * cameraPanSpeed * Time.deltaTime;
            cameraPosition.z -= Input.GetAxis("Mouse Y") * cameraPanSpeed * Time.deltaTime;
        }

        cameraPosition.x = Mathf.Clamp(
            cameraPosition.x,
            MapBorder.VerticesPositions[(int)MapBorder.Corners.bottomLeft].x,
            MapBorder.VerticesPositions[(int)MapBorder.Corners.bottomRight].x);

        cameraPosition.z = Mathf.Clamp(
            cameraPosition.z,
            MapBorder.VerticesPositions[(int)MapBorder.Corners.bottomLeft].z,
            MapBorder.VerticesPositions[(int)MapBorder.Corners.topLeft].z);

        transform.position = cameraPosition;
    }

    private void ScrollCamera(Vector3 cameraPosition)
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (cameraPosition.y < maxZoomHeightY && scroll < 0 && scroll > scrollClampMin ||
            cameraPosition.y > minZoomHeightY && scroll > 0 && scroll < scrollClampMax)
        {
            cameraPosition += transform.forward * scroll * cameraScrollSpeed * scrollSpeedMultiplier;
        }

        transform.position = cameraPosition;
    }

}