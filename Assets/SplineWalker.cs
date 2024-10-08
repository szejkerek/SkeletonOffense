using UnityEngine;
using UnityEngine.Splines;  // Import the Spline namespace

public class SplineWalker : MonoBehaviour
{
    public SplineContainer splineContainer;   // Reference to the Spline Container component
    public float speed = 5f;                  // Movement speed along the spline
    private float splinePosition = 0f;        // Position on the spline (0.0 to 1.0)

    void Update()
    {
        if (splineContainer != null)
        {
            // Calculate the new position on the spline based on the speed
            splinePosition += (speed * Time.deltaTime) / splineContainer.Spline.GetLength();
            splinePosition = Mathf.Clamp01(splinePosition);  // Ensure the position stays between 0 and 1

            // Get the position along the spline at the current splinePosition
            Vector3 position = splineContainer.Spline.EvaluatePosition(splinePosition);

            // Move the unit to the spline's position
            transform.position = position;

            // Get the tangent (direction of the curve) at the current position and align the unit's forward vector
            Vector3 tangent = splineContainer.Spline.EvaluateTangent(splinePosition);
            transform.forward = tangent.normalized;
        }
    }
}
