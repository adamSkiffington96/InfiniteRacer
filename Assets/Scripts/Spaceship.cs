using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float pitchSpeed = 2f;
    public float rollSpeed = 2f;

    private Vector3 lastRotation = Vector3.zero;

    public float turnAcceleration = 4f;
    public float moveAcceleration = 4f;

    public float shipThrust = 8f;

    public Vector3 plannedPosition;


    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        plannedPosition = shipThrust * transform.forward.normalized;

        transform.position = Vector3.Lerp(transform.position, transform.position + plannedPosition, moveAcceleration * Time.smoothDeltaTime);

        TurnShip();
    }

    private void TurnShip()
    {
        //Vector3 inputVect = new Vector3(Input.GetAxis("Horizontal") * rollSpeed, Input.GetAxis("Vertical") * pitchSpeed, 0) * Time.smoothDeltaTime;
        Vector3 inputVect = new Vector3(Input.GetAxis("Vertical") * pitchSpeed, 0, -Input.GetAxis("Horizontal") * rollSpeed) * Time.smoothDeltaTime;
        lastRotation = Vector3.Lerp(lastRotation, inputVect, turnAcceleration * Time.smoothDeltaTime);

        transform.Rotate(lastRotation);
    }
}
