using UnityEngine;

public class Projectile : MonoBehaviour
{
    public virtual float VelocityMultiplier => 1f;

    protected Rigidbody2D rb;
    protected CircleCollider2D circleCollider;


    protected int chainBonus;
    protected Vector3 initialVelocity;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        chainBonus = 0;
    }
    protected virtual void Update()
    {

    }

    public void AttachToAmmoReserve(AmmoReserve ammoReserve)
    {
        gameObject.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("AmmoReserve");

        rb.linearVelocity = ammoReserve.spawnVelocity;
        rb.gravityScale = 3f;
        circleCollider.isTrigger = false;

        transform.parent = ammoReserve.transform;
    }

    public void AttachToHolder(CatapultHolder holder)
    {
        gameObject.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 1f;

        circleCollider.isTrigger = true;

        transform.parent = holder.transform;
        transform.position = holder.transform.position;
    }

    public virtual void Launch(Catapult catapult, Vector3 velocity)
    {
        gameObject.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("Projectile");

        transform.parent = null;

        circleCollider.isTrigger = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;

        initialVelocity = velocity;
        rb.linearVelocity = velocity;
        rb.angularVelocity = velocity.x * 100f + Random.Range(-50f,50f);


        Destroy(gameObject, 50f);
    }

    public virtual void BubbleHit(Bubble bubble)
    {
        bubble.HitByProjectile(this);

        int points = bubble.points + chainBonus;
        ScoreTracker.IncreaseScore(points, bubble.transform.position);

        chainBonus += GameSettings.instance.chainingBonus;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bubble"))
        {
            Bubble bubble = collision.GetComponent<Bubble>();
            if (!bubble) return;
            BubbleHit(bubble);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bubble"))
        {
            Bubble bubble = collision.collider.GetComponent<Bubble>();
            if (!bubble) return;
            BubbleHit(bubble);
        }
    }

    public Vector3[] PredictTrajectory(Vector3 initialVelocity, Vector3 initialPosition, int count, float delta, float delay)
    {
        Vector3[] v3 = new Vector3[count];

        for(int i = 0; i < count; i++)
        {
            float time = i * delta + delay;
            v3[i] = PredictPosition(initialVelocity, initialPosition, time);
        }

        return v3;
    }
    public virtual Vector3 PredictPosition(Vector3 initialVelocity, Vector3 initialPosition, float time)
    {
        Vector3 gravity = Vector3.up * Physics2D.gravity.y * time * time * 0.5f;
        return initialPosition + initialVelocity * time + gravity;
    }
}
