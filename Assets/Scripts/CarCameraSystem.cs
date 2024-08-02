using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraSystem : MonoBehaviour
{
    public Transform _target;

    public float _movementSmoothing = 4f;

    public float _tiltSpeed = 2f;

    public float CameraRotationSpeed = 1f;

    public float cameraDistanceModifier = 0.1f;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
    }

    private void MoveCamera()
    {
        //transform.position = Vector3.Lerp(transform.position, ship.transform.position + (ship.plannedPosition * cameraDistanceModifier), _movementSmoothing * Time.smoothDeltaTime);

        transform.position = Vector3.Lerp(transform.position, _target.parent.position, _movementSmoothing * Time.deltaTime);

        Vector3 inputVect = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        /*
        _turnAngle = Vector3.Lerp(_turnAngle, new Vector3(0, 0, inputVect.x * 3f), _tiltSpeed * Time.smoothDeltaTime);
        transform.eulerAngles = _turnAngle;*/

    }

    private void RotateCamera()
    {
        //float mouseX = Input.GetAxis("Horizontal") * ;

        //cameraRotation = Vector3.Lerp(cameraRotation, _target.eulerAngles, CameraRotationSpeed * Time.smoothDeltaTime);

        //transform.eulerAngles = cameraRotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_target.parent.forward, _target.parent.up), CameraRotationSpeed * Time.smoothDeltaTime);
    }
}
