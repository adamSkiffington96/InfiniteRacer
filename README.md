
# Project Title

A brief description of what this project does and who it's for


## Screenshots

![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)


## Features

- Light/dark mode toggle
- Live previews
- Fullscreen mode
- Cross platform


## Snippets

</details>

<details>
<summary><code>Player.cs</code></summary>

```
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
        // Strafe the car using horizontal (AD or arrow keys)
        //  - Car rotates a bit like its resisting inertia, for effect

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

        _carMovement = Vector3.MoveTowards(_carMovement, _strafeSpeed * inputVect, 10 * Time.deltaTime);

        Vector3 movementDir = transform.position + _carMovement;

        movementDir.x = Mathf.Clamp(movementDir.x, -10, 10);

        transform.position = Vector3.Lerp(transform.position, movementDir, _moveSmoothing * Time.deltaTime);

        _turnAngle = Vector3.MoveTowards(_turnAngle, new Vector3(0, inputVect.x * 15f, inputVect.x * 4f), _turnSpeed * Time.deltaTime * 10f);

        transform.eulerAngles = _turnAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Stop the treadmill and evaluate score if we collide
        if (other.CompareTag("Obstacle"))
        {
            _treadmill.ToggleTreadmill();

            _enableMoving = false;

            _scoreSystem.ChangeHighScore(false);

            print("Collided with obstacle!");
        }
    }
}

```
</details>

</details>

<details>
<summary><code>Treadmill.cs</code></summary>

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treadmill : MonoBehaviour
{
    public Transform _spawnedObjectParent;

    public Transform _prefabObjects;

    public float _speedModifier = 1f;

    public int _populationOnTreadmill = 4;
    private int _currentPopulation = 0;

    private bool _movingTreadmill = true;

    public Text _debugText;

    public float _limitHorizontalSpawn = 10;

    private void Start()
    {
        _prefabObjects.GetChild(0).gameObject.SetActive(false);
        CreateObject();
    }

    private void CreateObject()
    {
        Transform spawned = Instantiate(_prefabObjects.GetChild(0), _spawnedObjectParent);

        spawned.localPosition = new Vector3(Random.Range(-_limitHorizontalSpawn, _limitHorizontalSpawn), 0, 0);
        spawned.gameObject.SetActive(true);
    }

    public void ToggleTreadmill()
    {
        _movingTreadmill = !_movingTreadmill;
    }

    private void HandleAddObjects()
    {
        // Spawn initial obstacles

        float distanceTraveled = _spawnedObjectParent.GetChild(_currentPopulation).localPosition.z;

        if (distanceTraveled <= -10)
        {
            CreateObject();

            _currentPopulation++;
        }
    }

    private void Update()
    {
        if (_movingTreadmill)
        {
            MoveTreadmill();
        }
    }

    private void MoveTreadmill()
    {
        //  Move list off obstacles towards the camera

        Vector3 movement = new Vector3(0, 0f, -1f);

        foreach(Transform movingObject in _spawnedObjectParent)
        {
            movingObject.localPosition += _speedModifier * Time.smoothDeltaTime * movement;

            if(movingObject.localPosition.z <= -transform.position.z + 10) {
                movingObject.localPosition = new Vector3(Random.Range(-_limitHorizontalSpawn, _limitHorizontalSpawn), 0, 0);
            }
        }

        // Reposition the obstacle when it passes

        if(_currentPopulation < _populationOnTreadmill - 1) {
            HandleAddObjects();
        }
    }
}

```
</details>

</details>

<details>
<summary><code>CarCameraSystem.cs</code></summary>

```
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


    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
    }

    private void MoveCamera()
    {
        // Interpolate camera towards our input vector

        transform.position = Vector3.Lerp(transform.position, _target.parent.position, _movementSmoothing * Time.deltaTime);

        Vector3 inputVect = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }

    private void RotateCamera()
    {
        // Effect: slightly rotating the camera in the direction of travel

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_target.parent.forward, _target.parent.up), CameraRotationSpeed * Time.smoothDeltaTime);
    }
}

```
</details>


## Optimizations

What optimizations did you make in your code? E.g. refactors, performance improvements, accessibility


## Demo

Insert gif or link to demo

