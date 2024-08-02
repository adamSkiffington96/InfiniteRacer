using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpace : MonoBehaviour
{
    public Transform prefabList;
    public Transform spawnedObjectsList;

    public int localAsteroidPopulation = 20;

    public Transform spaceship;

    public float asteroidRelocateRadius = 20;

    void Start()
    {
        SpawnInitialAsteroids();
    }

    private void SpawnInitialAsteroids()
    {
        for(int i = 0; i < localAsteroidPopulation; i++)
        {
            GameObject body = Instantiate(prefabList.GetChild(0).gameObject);

            body.transform.SetParent(spawnedObjectsList);

            Vector3 randomPosition = Random.insideUnitSphere * asteroidRelocateRadius;

            body.GetComponent<Rigidbody>().angularDrag = 0f;
            body.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            body.transform.GetChild(0).localScale *= Random.Range(0.5f, 3f);
            body.transform.rotation = Random.rotation;
            body.transform.position = randomPosition;
        }

        prefabList.GetChild(0).gameObject.SetActive(false);
    }

    public void RelocateAsteroid(int listIndex)
    {
        print("Relocating asteroid with index " + listIndex);

        Vector3 randomPosition = Random.insideUnitSphere * asteroidRelocateRadius;

        spawnedObjectsList.GetChild(listIndex).position = (spaceship.position) + randomPosition;
    }

    void Update()
    {
        
    }
}
