using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float fallSpeed = 0.2f;
    public float swayFactor = 0.1f;
    public float swayRate = 1f;

    public int hp = 1;
    public int points = 1000;

    float perlinRd;

    Vector3 pos;

    public Action OnDeath;


    void Start()
    {
        pos = transform.position;
        perlinRd = UnityEngine.Random.Range(0f, 100000f);

        gameObject.layer = LayerMask.NameToLayer("Bubble");
    }
    void Update()
    {
        pos += Vector3.down * fallSpeed * Time.deltaTime;


        Vector3 noise = Vector3.zero;
        noise.x = Mathf.PerlinNoise(perlinRd, Time.time * swayRate);
        noise.y = Mathf.PerlinNoise(perlinRd + 1000f, Time.time * swayRate + 1000f);
        noise.x = noise.x * 2f - 1f;
        noise.y = noise.y * 2f - 1f;
        noise *= swayFactor;

        transform.position = pos + noise;
    }

    public virtual void HitByProjectile(Projectile projectile)
    {
        hp--;
        if (hp <= 0) Death();
    }

    public int BaseScore()
    {
        return points;
    }
    public void Death()
    {
        OnDeath?.Invoke();

        Destroy(gameObject);
    }
}
