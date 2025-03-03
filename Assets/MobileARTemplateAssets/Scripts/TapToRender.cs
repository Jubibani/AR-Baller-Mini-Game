using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class TapToRender : MonoBehaviour
{
    public GameObject objectToPlace; // Assign your 3D model in the Inspector
    private GameObject spawnedObject; // Store the instantiated object

    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();

        if (arRaycastManager == null)
        {
            Debug.LogError("ARRaycastManager is missing! Attach ARRaycastManager to this GameObject.");
        }
    }

    void Update()
    {
        if (Touchscreen.current == null) return; // Ensure touchscreen input is available

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (touch.press.wasPressedThisFrame)
            {
                Debug.Log("Tap detected!");

                // Prevent multiple instantiations
                if (spawnedObject != null)
                {
                    Debug.Log("An object is already placed. No new instantiation.");
                    return;
                }

                Vector2 touchPosition = touch.position.ReadValue();

                // Perform AR raycast to detect surfaces
                if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if (objectToPlace == null)
                    {
                        Debug.LogError("objectToPlace is NOT assigned in the Inspector!");
                        return;
                    }

                    // Instantiate the object only once
                    spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                    Debug.Log("Model instantiated at " + hitPose.position);
                }
                else
                {
                    Debug.Log("No plane detected at tap position.");
                }
            }
        }
    }
}
