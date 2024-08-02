using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _strafeSpeed = 1f;

    private Vector3 _turnAngle = Vector3.zero;

    public float _turnSpeed = 2f;

    private Vector3 _carMovement = Vector3.zero;

    public float _moveSmoothing = 4f;

    public ScoreSystem _scoreSystem;

    private Vector3 _currentTurningVel = Vector3.zero;

    public Treadmill _treadmill;

    private bool _enableMoving = true;


    private void Update()
    {
        if (_enableMoving)
        {
            MoveCar();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _enableMoving = true;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _enableMoving = true;
            }

            if (_enableMoving)
            {
                _scoreSystem.ChangeHighScore(true);
                _treadmill.ToggleTreadmill();
            }
                
        }
    }

    private void MoveCar()
    {
        Vector3 inputVect = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (inputVect.x > 0)
        {
            if (transform.position.x >= 10.2f)
            {
                inputVect.x = 0;
            }
        }
        if (inputVect.x < 0)
        {
            if (transform.position.x <= -10.2f)
            {
                inputVect.x = 0;
            }
        }

        //_carMovement = Vector3.Lerp(_carMovement, (_strafeSpeed * inputVect), _moveSmoothing * Time.smoothDeltaTime);
        _carMovement = Vector3.MoveTowards(_carMovement, _strafeSpeed * inputVect, 10 * Time.deltaTime);

        Vector3 movementDir = transform.position + _carMovement;

        movementDir.x = Mathf.Clamp(movementDir.x, -10, 10);

        //transform.position = movementDir;
        transform.position = Vector3.Lerp(transform.position, movementDir, _moveSmoothing * Time.deltaTime);


        _turnAngle = Vector3.MoveTowards(_turnAngle, new Vector3(0, inputVect.x * 15f, inputVect.x * 4f), _turnSpeed * Time.deltaTime * 10f);
        //_turnAngle = Vector3.SmoothDamp(_turnAngle, new Vector3(0, inputVect.x * 14f, inputVect.x * 5f), ref _currentTurningVel, Time.smoothDeltaTime * 15, _turnSpeed);

        transform.eulerAngles = _turnAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _treadmill.ToggleTreadmill();

            _enableMoving = false;

            //Vector3 myPosition = new Vector3(0, transform.position.y, 20);

            //transform.position = myPosition;

            _scoreSystem.ChangeHighScore(false);

            print("Collided with obstacle!");
        }
    }
}
