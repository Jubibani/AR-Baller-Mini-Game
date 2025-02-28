using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class TapToPlaceModel : MonoBehaviour
{
    public GameObject testCubePrefab; // Assign a simple Cube prefab here
    private GameObject spawnedTestCube;

    public GameObject hoopPrefab; // Assign your basketball hoop prefab in the Inspector
    private GameObject spawnedHoop;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            Debug.Log(" Touch detected at: " + touchPosition);

            if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Debug.Log($" Surface detected at: {hitPose.position}");

                if (spawnedTestCube == null)
                {
                    spawnedTestCube = Instantiate(testCubePrefab, hitPose.position, Quaternion.identity);
                    Debug.Log(" Cube instantiated!");
                }
                else
                {
                    spawnedTestCube.transform.position = hitPose.position;
                    Debug.Log(" Cube moved!");
                }
            }
            else
            {
                Debug.LogError(" No AR plane detected!");
            }
        }
    }

}
