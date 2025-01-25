using UnityEngine;

public class CatapultHolder : MonoBehaviour
{
    public float springStrength = 1f;
    public float springDamper = 1f;

    private Catapult catapult;
    private Vector3 startPosition;

    private Vector3 velocity;


    private void Start()
    {
        catapult = GetComponentInParent<Catapult>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (catapult.ProjectileChambered)
        {
            UpdateDragged();
        }
        else
        {
            UpdateReleased();
        }
    }

    public void Release(Vector3 launchVelocity)
    {

    }

    private void UpdateDragged()
    {
        if (!catapult.ProjectileChambered) return;

        velocity = Vector3.zero;

        Vector3 launchDelta = catapult.LauncherDelta();

        Vector3 pos = startPosition + launchDelta;
        transform.position = pos + Vector3.back;

        float angle = Mathf.Atan2(launchDelta.y, launchDelta.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    const float rotationResetFactor = 5f;
    private void UpdateReleased()
    {
        Vector3 delta = startPosition - transform.position;
        Vector3 springForce = delta * springStrength;
        Vector3 damper = -velocity * springDamper;

        velocity += (springForce + damper) * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationResetFactor * Time.deltaTime);
    }
}
