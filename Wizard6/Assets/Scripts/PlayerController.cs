using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool lockCursor = true;
    [SerializeField] private float walkSpeed;
    
    [SerializeField] private float upSensitivity;
    [SerializeField] private float sideSensitivity;
    private float cameraRotationLimit = 90.0f;
    private float currentCameraRotationX = 0;

    [SerializeField] private Camera theCamera;
    private Rigidbody myRigid;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    void Update()
    {
        PlayerMove();
        CameraRotate();
        PlayerRotate();
    }

    void PlayerMove()
    {
        float _mouseDirX = Input.GetAxisRaw("Horizontal");
        float _mouseDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _mouseDirX;
        Vector3 _moveVertical = transform.forward * _mouseDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    void CameraRotate()
    {
        float _XRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _XRotation * upSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    void PlayerRotate()
    {
        float _YRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _playerRotationY = new Vector3(0f, _YRotation, 0f) * sideSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_playerRotationY));
    }
}
