using UnityEngine;

public abstract class Bonus : ScriptableObject
{
    public Sprite sprite;
    public AudioClip clip;

    public virtual bool CanBeUsed => true;

    public abstract void Initialize();
    public virtual void Update() { }
    public abstract void TriggerBonus();
}

public abstract class TimeLimitedBonus : Bonus
{
    public float duration = 10f;

    private float finalTime;
    private bool bonusActive;

    public override void Initialize()
    {
        finalTime = -1f;
        bonusActive = false;
    }
    public override void Update()
    {
        if (Time.time > finalTime && bonusActive) OnBonusEnds();
    }
    public override void TriggerBonus()
    {
        if (!bonusActive)
        {
            OnBonusStarts();
            finalTime = Time.time + duration;
        }
        else finalTime += duration;
    }
    public virtual void OnBonusStarts()
    {
        bonusActive = true;
    }
    public virtual void OnBonusEnds()
    {
        bonusActive = false;
    }
    public enum TimeLimitedBonusType
    {
        Aim,
        Multiplier,
        Infinite,
        SlowMo
    }
}
[CreateAssetMenu(fileName = "New Special Nuts Bonus", menuName = "Bonus/Special Nuts")]
public class SpecialNutsBonus : Bonus
{
    public Projectile projectile;
    public int count = 3;

    public override void Initialize()
    {
        
    }
    public override void TriggerBonus()
    {
        AmmoReserve.AddSpecialAmmo(projectile, count);
    }
}

[CreateAssetMenu(fileName = "New Restore Health Bonus", menuName = "Bonus/Restore Health")]
public class RestoreHealthBonus : Bonus
{
    public int healthRestored;

    public override bool CanBeUsed => GameSettings.instance.maxHealth != GameSettings.currentHealth;

    public override void Initialize()
    {
        
    }
    public override void TriggerBonus()
    {
        GameSettings.IncrementHealth(healthRestored);
    }
}