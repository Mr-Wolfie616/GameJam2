using UnityEngine;

public class CameraSnap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject cameraObject;
    [SerializeField] MainMenuManager menuManager;
    string currentFacingDirection = "straight";

    float CameraRot = 0;

    void Update()
    {
        if (menuManager.isPlayMode)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentFacingDirection == "straight")
                {
                    currentFacingDirection = "left";
                    CameraRot = -90f;
                }
                else if (currentFacingDirection == "right")
                {
                    currentFacingDirection = "straight";
                    CameraRot = 0f;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentFacingDirection == "straight")
                {
                    currentFacingDirection = "right";
                    CameraRot = 90f;
                }
                else if (currentFacingDirection == "left")
                {
                    currentFacingDirection = "straight";
                    CameraRot = 0f;
                }
            }
        }

        Debug.Log(CameraRot);

        cameraObject.transform.rotation = Quaternion.Lerp
        (
            cameraObject.transform.rotation, 
            Quaternion.Euler(10f, CameraRot, 0f), 
            0.1f
        );
    }

    private void SetCameraRotation(float targetYAngle)
    {
        cameraObject.transform.rotation = Quaternion.Euler(10f, targetYAngle, 0f);
    }
}