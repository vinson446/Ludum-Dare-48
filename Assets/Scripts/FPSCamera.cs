using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class FPSCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool playerInput;
    [SerializeField] float mouseSensitivity = 100f;

    [Header("Camera Shake")]
    [SerializeField] float swayIntensity;
    [SerializeField] float swayRoughness;

    [SerializeField] float onHitShakeIntensity;
    [SerializeField] float onHitShakeRoughness;
    [SerializeField] float onHitFadeOutTime;

    [Header("IR")]
    [SerializeField] Transform playerBody;

    float xRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        CameraShaker.Instance.StartShake(swayIntensity, swayRoughness, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput)
            LookAndRotateWithMouse();
    }

    void LookAndRotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90, 90);

        // camera vertical rotation
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        // player horizontal rotation
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void CameraShake()
    {
        CameraShaker.Instance.ShakeOnce(onHitShakeIntensity, onHitShakeRoughness, 0.1f, onHitFadeOutTime);
    }
}
