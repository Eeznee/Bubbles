using UnityEngine;

public class ClusterProjectile : Projectile
{
    public Projectile subMunition;
    public float spread = 10f;
    public int count = 5;


    public override void Launch(Catapult catapult, Vector3 velocity)
    {
        base.Launch(catapult, velocity);

        float minAngle = -spread;
        float maxAngle = spread;

        for(int i = 0; i < count; i++)
        {
            float angle = Mathf.Lerp(minAngle, maxAngle, (float) i / (count-1));
            Vector3 subVelocity = Quaternion.Euler(0f, 0f, angle) * velocity;

            Projectile sub = Instantiate(subMunition);
            sub.transform.position = transform.position;
            sub.transform.rotation = Quaternion.Euler(0f, 0f, angle) * transform.rotation;
            sub.Launch(catapult, subVelocity);
        }
    }
}
