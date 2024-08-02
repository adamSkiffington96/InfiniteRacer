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
        //_populationOnTreadmill = 

        _prefabObjects.GetChild(0).gameObject.SetActive(false);
        CreateObject();
    }

    private void CreateObject()
    {
        Transform spawned = Instantiate(_prefabObjects.GetChild(0), _spawnedObjectParent);

        //spawned.localPosition = Vector3.zero;
        spawned.localPosition = new Vector3(Random.Range(-_limitHorizontalSpawn, _limitHorizontalSpawn), 0, 0);
        spawned.gameObject.SetActive(true);
    }

    public void ToggleTreadmill()
    {
        _movingTreadmill = !_movingTreadmill;
    }

    private void HandleAddObjects()
    {
        float distanceTraveled = _spawnedObjectParent.GetChild(_currentPopulation).localPosition.z;

        //_debugText.text = "Travel distance = " + distanceTraveled;

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
        Vector3 movement = new Vector3(0, 0f, -1f);

        foreach(Transform movingObject in _spawnedObjectParent)
        {
            movingObject.localPosition += _speedModifier * Time.smoothDeltaTime * movement;

            if(movingObject.localPosition.z <= -transform.position.z + 10) {
                //movingObject.localPosition = Vector3.zero;
                movingObject.localPosition = new Vector3(Random.Range(-_limitHorizontalSpawn, _limitHorizontalSpawn), 0, 0);
            }
        }

        if(_currentPopulation < _populationOnTreadmill - 1) {
            HandleAddObjects();
        }
    }
}
