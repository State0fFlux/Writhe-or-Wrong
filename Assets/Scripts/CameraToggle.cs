using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Camera regCam; // Reference to the first camera
    public Camera zoomCam; // Reference to the second camera

    private void Start()
    {
        // Ensure only cam1 is active at the start
        regCam.gameObject.SetActive(true);
        zoomCam.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Listen for the spacebar key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle between the two cameras
            bool isRegActive = regCam.gameObject.activeSelf;
            regCam.gameObject.SetActive(!isRegActive);
            zoomCam.gameObject.SetActive(isRegActive);
        }
    }
}
