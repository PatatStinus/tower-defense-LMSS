using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Author>
/// Lisa Werner
/// </Author>
public class CameraMovement : MonoBehaviour
{
    [Header("Camera Settings")]

    [SerializeField] float cameraPanSpeed = 20f;
    [SerializeField] float cameraScrollSpeed = 20f;
    [SerializeField] float scrollSpeedMultiplier = 100f;
    //[SerializeField] Vector2 minpanLimit;   // Once we have a map I will set this
    //[SerializeField] Vector2 maxpanLimit;   // Once we have a map I will set this
    [SerializeField] float minZoomHeightY = 1f; //this value is currently just for testing, will set them as constants once we have a map
    [SerializeField] float maxZoomHeightY = 10f; //this value is currently just for testing, will set them as constants once we have a map
    [SerializeField] float scroll;


    void Update()
    {
        Vector3 cameraPosition = this.gameObject.transform.position;

        if (Input.GetMouseButton(2))
        {
            cameraPosition.x -= Input.GetAxis("Mouse X") * cameraPanSpeed * Time.deltaTime;
            cameraPosition.z -= Input.GetAxis("Mouse Y") * cameraPanSpeed * Time.deltaTime;
        }
        
        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (cameraPosition.y < maxZoomHeightY && scroll < 0 || cameraPosition.y > minZoomHeightY && scroll > 0)
        {
            cameraPosition += transform.forward * scroll * cameraScrollSpeed * scrollSpeedMultiplier * Time.deltaTime;
        }
        else
        {
            scroll = 0f;
        }

        // UNCOMMENT ONCE THE MAP SIZE IS KNOWN
        //cameraPosition.x = Mathf.Clamp(cameraPosition.x, minpanLimit.x, maxpanLimit.x);
        //cameraPosition.y = Mathf.Clamp(cameraPosition.y, minZoom, maxZoom);
        //cameraPosition.z = Mathf.Clamp(cameraPosition.z, minpanLimit.y, maxpanLimit.y);

        transform.position = cameraPosition;

    }
}
