using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoReserve : MonoBehaviour
{
    public static AmmoReserve instance;
    public static bool infiniteAmmoActive;

    public int maxCapacity = 5;
    public float reloadTime = 1f;
    public Projectile projectile;

    public Vector2 spawnVelocity = Vector2.left;
    public Vector3 spawnPosition;

    private List<Projectile> projectiles;

    private Projectile specialProjectile;
    private int specialProjectileCount;
    void Start()
    {
        instance = this;
        infiniteAmmoActive = false;

        gameObject.layer = LayerMask.NameToLayer("AmmoReserve");
        projectile.gameObject.SetActive(false);

        projectiles = new List<Projectile>();
        InvokeRepeating("ReplenishOneAmmo", 0f, reloadTime);

        specialProjectileCount = 0;
    }
    private void ReplenishOneAmmo()
    {
        if (projectiles.Count == maxCapacity) return;

        Vector3 position = transform.position + spawnPosition;
        Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        Projectile projectileToSpawn = projectile;

        if(specialProjectileCount > 0)
        {
            projectileToSpawn = specialProjectile;
            specialProjectileCount--;
        }

        Projectile newProjectile = Instantiate(projectileToSpawn, position, rotation, transform);
        newProjectile.gameObject.SetActive(true);
        projectiles.Add(newProjectile);
        newProjectile.AttachToAmmoReserve(this);

        if(projectiles.Count == maxCapacity) CancelInvoke();
    }
    public Projectile WithdrawOneAmmo()
    {
        if (projectiles.Count == 0) return null;
        if (projectiles.Count == maxCapacity) InvokeRepeating("ReplenishOneAmmo", reloadTime, reloadTime);

        Projectile projectileToWithdraw = projectiles.ElementAt(0);
        projectiles.RemoveAt(0);
        projectileToWithdraw.transform.parent = null;

        if (infiniteAmmoActive && projectiles.Count < maxCapacity) ReplenishOneAmmo();

        return projectileToWithdraw;
    }

    public static void AddSpecialAmmo(Projectile special, int count)
    {
        instance.specialProjectileCount = count;
        instance.specialProjectile = special;
    }
}
