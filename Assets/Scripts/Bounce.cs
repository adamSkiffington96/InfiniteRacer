using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour
{
    public float BounceHeight = 0.5f;

    private Vector3 InitialPosition;

    private float _bounceTimer = 2f;

    private float BounceProgress = 0f;
    public float BouncePeriod = 1f;

    private int _bounceDirection = 0;
    private bool _enableBounce = true;

    public Text debugText;

    void Start()
    {
        InitialPosition = transform.localPosition;
    }

    // 360 degrees in a circle
    // 2 pi radians in a circle

    void Update()
    {
        ConstantBouncing();

        if (_enableBounce)
        {
            if (_bounceTimer > 0)
            {
                _bounceTimer -= Time.deltaTime;

                if (_bounceTimer <= 0)
                {
                    CreateBounce();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _enableBounce = true;
            }
        }
    }

    public void ToggleBouncing()
    {
        _enableBounce = false;
    }

    private void ConstantBouncing()
    {
        if(_bounceDirection != 0)
        {
            BounceProgress = (BounceProgress + Time.deltaTime / BouncePeriod) % 1f;

            // calculate the height offset
            float heightOffset = Mathf.Sin(BounceProgress * _bounceDirection * Mathf.PI) * BounceHeight;

            // update the height
            transform.localPosition = InitialPosition + new Vector3(0, heightOffset, 0);

            if (BounceProgress > 0.99f)
            {
                BounceProgress = 0f;

                _bounceTimer = Random.Range(0.5f, 1f);

                _bounceDirection = 0;

                transform.localPosition = Vector3.zero;
            }
        }

        //debugText.text = BounceProgress.ToString() + ", Bounce Direction: " + _bounceDirection;
    }

    private void CreateBounce()
    {
        int randomDir = Random.Range(0, 2);
        if (randomDir == 0)
            randomDir--;
        _bounceDirection = randomDir;

        print("Trying a bounce!");
    }
}