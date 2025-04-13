using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform throwOrigin;
    public int points = 30;
    public float timeStep = 0.1f;
    public float forceMultiplier = 10f;

    public void DrawTrajectory(float power)
    {
        Vector3 velocity = throwOrigin.forward * power * forceMultiplier;

        Vector3[] positions = new Vector3[points];
        Vector3 currentPos = throwOrigin.position;
        Vector3 currentVel = velocity;

        for (int i = 0; i < points; i++)
        {
            positions[i] = currentPos;
            currentVel += Physics.gravity * timeStep;
            currentPos += currentVel * timeStep;
        }

        lineRenderer.positionCount = points;
        lineRenderer.SetPositions(positions);
    }
}
