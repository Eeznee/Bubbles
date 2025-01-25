using UnityEngine;

public class BouncingProjectile : Projectile
{
    public float velocityMultiplier = 1.5f;
    public override float VelocityMultiplier => velocityMultiplier;

    private float xMin;
    private float xMax;
    protected override void Awake()
    {
        base.Awake();

        xMin = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        xMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f)).x;

    }
    protected override void Update()
    {
        base.Update();

        if (transform.position.x < xMin) rb.linearVelocityX = Mathf.Abs(rb.linearVelocityX);
        if (transform.position.x > xMax) rb.linearVelocityX = -Mathf.Abs(rb.linearVelocityX);
    }
}
