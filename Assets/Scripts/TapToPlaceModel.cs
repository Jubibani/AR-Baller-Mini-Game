using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlace : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign your 3D model in Inspector

    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // If tap detected
            if (touch.phase == TouchPhase.Began)
            {
                // Perform a Raycast to detect planes
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose; // Get the hit position

                    // Instantiate the object at the tap location
                    Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
