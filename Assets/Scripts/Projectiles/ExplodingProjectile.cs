using UnityEngine;

public class ExplodingProjectile : Projectile
{
    public float explosionRadius = 2.5f;
    public GameObject explosionEffect;
    public float explosionFXLifetime = 5f;


    private bool exploded;


    public override void Launch(Catapult catapult, Vector3 velocity)
    {
        base.Launch(catapult, velocity);
        exploded = false;
    }
    protected override void Update()
    {
        base.Update();

        bool reachedMaxHeight = Mathf.Sign(rb.linearVelocityY * initialVelocity.y) == -1f;

        if (reachedMaxHeight) Explodes();
    }

    private void Explodes()
    {
        if (exploded) return;

        GameObject fx = Instantiate(explosionEffect, transform.position, transform.rotation);

        circleCollider.radius = explosionRadius;

        Destroy(fx, explosionFXLifetime);
        Destroy(gameObject, 0.02f);

        exploded = true;
    }
}
