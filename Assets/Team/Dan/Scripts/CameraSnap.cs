using UnityEngine;

public class CameraSnap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject cameraObject;
    string currentFacingDirection = "straight";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentFacingDirection == "straight")
            {
                currentFacingDirection = "left";
                SetCameraRotation(-90f);
            }
            else if (currentFacingDirection == "right")
            {
                currentFacingDirection = "straight";
                SetCameraRotation(0f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentFacingDirection == "straight")
            {
                currentFacingDirection = "right";
                SetCameraRotation(90f);
            }
            else if (currentFacingDirection == "left")
            {
                currentFacingDirection = "straight";
                SetCameraRotation(0f);
            }
        }
    }

    private void SetCameraRotation(float targetYAngle)
    {
        cameraObject.transform.rotation = Quaternion.Euler(10f, targetYAngle, 0f);
    }
}