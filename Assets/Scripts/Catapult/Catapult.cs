using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Catapult : MonoBehaviour
{
    public static Catapult instance;

    public float maxStretchDistance = 1f;
    public float maxVelocity = 10f;
    public float sensitivity = 1f;

    public AmmoReserve ammoReserve;



    private CatapultHolder holder;
    private LineRenderer line;
    private Vector3 leftLauncherBandPoint;
    private Vector3 rightLauncherBandPoint;

    private Vector3 mouseStartPosition;

    public Projectile ProjectileChambered { get; private set; }

    public Action OnPrepareLaunch;
    public Action OnLaunch;
    void Start()
    {
        instance = this;

        holder = GetComponentInChildren<CatapultHolder>();

        line = GetComponent<LineRenderer>();
        Vector3 localCatapultLeftPoint = transform.TransformPoint(line.GetPosition(1));
        Vector3 localCatapultRightPoint = transform.TransformPoint(line.GetPosition(2));
        leftLauncherBandPoint = holder.transform.InverseTransformPoint(localCatapultLeftPoint);
        rightLauncherBandPoint = holder.transform.InverseTransformPoint(localCatapultRightPoint);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PrepareLaunch();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Launch();
        }

        UpdateLineRenderer();
    }
    private void PrepareLaunch()
    {
        if (ProjectileChambered) return;

        ProjectileChambered = ammoReserve.WithdrawOneAmmo();
        if (ProjectileChambered == null) return;

        ProjectileChambered.AttachToHolder(holder);

        mouseStartPosition = MousePositionInWorld();

        OnPrepareLaunch?.Invoke();
    }
    private void Launch()
    {
        if (!ProjectileChambered) return;

        Vector3 velocity = Velocity();

        ProjectileChambered.Launch(this, velocity);
        ProjectileChambered = null;

        holder.Release(velocity);

        OnLaunch?.Invoke();
    }

    public Vector3 MousePositionInWorld()
    {
        Vector3 value = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        value.z = 0f;
        return value;
    }
    public Vector3 LauncherDelta()
    {
        Vector3 deltaMouse = MousePositionInWorld() - mouseStartPosition;
        Vector3 physicalDistance = deltaMouse * sensitivity;

        if(physicalDistance.magnitude > maxStretchDistance)
        {
            physicalDistance.Normalize();
            physicalDistance *= maxStretchDistance;
        }
        return physicalDistance;
    }
    public Vector3 Velocity()
    {
        float multiplier = 1f;
        if (ProjectileChambered)
        {
            multiplier = ProjectileChambered.VelocityMultiplier;
        }
        Vector3 delta = LauncherDelta();
        Vector3 direction = -delta / maxStretchDistance;
        return direction * multiplier * maxVelocity;
    }

    private void UpdateLineRenderer()
    {
        Vector3 worldLeftPoint = holder.transform.TransformPoint(leftLauncherBandPoint);
        Vector3 worldRightPoint = holder.transform.TransformPoint(rightLauncherBandPoint);
        line.SetPosition(1, transform.InverseTransformPoint(worldLeftPoint));
        line.SetPosition(2, transform.InverseTransformPoint(worldRightPoint));
    }

}
