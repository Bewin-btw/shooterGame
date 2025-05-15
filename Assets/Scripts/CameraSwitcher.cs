using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;
    public KeyCode toggleKey = KeyCode.C;

    void Start()
    {
        if (mainCamera != null) mainCamera.enabled = true;
        if (secondCamera != null) secondCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (secondCamera != null)
            {
                secondCamera.enabled = !secondCamera.enabled;
                mainCamera.enabled = !mainCamera.enabled;
                if (secondCamera.enabled == mainCamera.enabled) 
                {
                    mainCamera.enabled = !secondCamera.enabled;
                }
            }
        }
    }
}
