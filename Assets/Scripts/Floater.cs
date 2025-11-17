using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System.Collections;
using System.Collections.Generic;

public class Floater : MonoBehaviour
{
    // Rigidbody component of the floating object
    public Rigidbody rb;
    // Depth at which objects start to experience buoyant force
    public float waterDepth = 1f;
    // Buoyant force applied to the object
    public float buoyancyForce = 10f;
    // Number of points applying buoyancy
    public int floatPointsCount = 4;

    // Drag coefficients
    public float waterDrag = 1f;
    public float waterAngularDrag = 1f;
    // Reference to water surface management component
    public WaterSurface waterSurface;

    // Hold parameters for searching the water surface
    WaterSearchParameters Search = new WaterSearchParameters();
    // Hold results from water surface search
    WaterSearchResult SearchResult;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Apply a distributed buoyant force
        rb.AddForceAtPosition(Physics.gravity / floatPointsCount, transform.position, ForceMode.Acceleration);

        // Set up search parameters for projecting on water surface
        Search.startPositionWS = transform.position;

        // Project point onto water surface and get result
        waterSurface.ProjectPointOnWaterSurface(Search, out SearchResult);

        // If object is below the water surface, apply buoyant force
        if (transform.position.y < SearchResult.projectedPositionWS.y)
        {
            // Calculate displacement multiplier based on depth
            float displacementMultiplier = Mathf.Clamp01((SearchResult.projectedPositionWS.y - transform.position.y) / waterDepth) * buoyancyForce;
            // Apply buoyant force
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            // Apply water drag force against velocity
            rb.AddForce(displacementMultiplier * -rb.linearVelocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            // Apply water angular drag against angular velocity
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
