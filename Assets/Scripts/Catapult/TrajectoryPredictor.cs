using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPredictor : MonoBehaviour
{
    public int pointAmount = 100;
    public float timeDelta = 0.05f;
    public float delay = 0.2f;


    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointAmount;
    }

    private void Update()
    {
        Projectile chambered = Catapult.instance.ProjectileChambered;

        if (chambered == null) 
        {
            lineRenderer.enabled = false;
            return;
        }
        else
        {
            lineRenderer.enabled = true;
            Vector3 vel = Catapult.instance.Velocity();
            Vector3 pos = chambered.transform.position;
            Vector3[] prediction = chambered.PredictTrajectory(vel, pos, pointAmount, timeDelta, delay);
            lineRenderer.SetPositions(prediction);
        }
    }
}
