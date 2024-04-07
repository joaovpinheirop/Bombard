using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyan : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float buoyancyForce = 10;
    private Rigidbody thisRigidbody;
    private bool hasToucheWater;
    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Check if underWater
        float diffY = transform.position.y;
        bool isUnderWater = diffY < 0;
        if (isUnderWater)
        {
            hasToucheWater = true;
        }
        // Ignore if never touched water
        if (!hasToucheWater)
        {
            return;
        }

        // Bouyancy Logic
        if (isUnderWater)
        {
            Vector3 vector = Vector3.up * buoyancyForce * -diffY;
            thisRigidbody.AddForce(vector, ForceMode.Acceleration);
        }

        thisRigidbody.drag = isUnderWater ? underwaterDrag : airDrag;
        thisRigidbody.angularDrag = isUnderWater ? underwaterAngularDrag : airAngularDrag;
    }
}
