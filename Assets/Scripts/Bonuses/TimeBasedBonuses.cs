using UnityEngine;

[CreateAssetMenu(fileName = "New Infinite Nuts Bonus", menuName = "Bonus/Infinite Nuts")]
public class InfiniteNuts : TimeLimitedBonus
{
    public override void OnBonusStarts()
    {
        base.OnBonusStarts();
        AmmoReserve.infiniteAmmoActive = true;
    }
    public override void OnBonusEnds()
    {
        base.OnBonusEnds();
        AmmoReserve.infiniteAmmoActive = false;
    }
}
[CreateAssetMenu(fileName = "New Slow Motion Bonus", menuName = "Bonus/Slow Motion")]
public class SlowMotion : TimeLimitedBonus
{
    public float slowMoFactor = 0.5f;

    public override void OnBonusStarts()
    {
        base.OnBonusStarts();
        Time.timeScale = slowMoFactor;
    }
    public override void OnBonusEnds()
    {
        base.OnBonusEnds();
        Time.timeScale = 1f;
    }
}

[CreateAssetMenu(fileName = "New Aiming Assist Bonus", menuName = "Bonus/Aiming Assist")]
public class AimingAssist : TimeLimitedBonus
{
    public override void OnBonusStarts()
    {
        base.OnBonusStarts();
        TrajectoryPredictor.instance.gameObject.SetActive(true);
    }
    public override void OnBonusEnds()
    {
        base.OnBonusEnds();
        TrajectoryPredictor.instance.gameObject.SetActive(false);
    }
}

[CreateAssetMenu(fileName = "New Points Multiplier Bonus", menuName = "Bonus/Points Multiplier")]
public class PointsMultiplier : TimeLimitedBonus
{
    public int multiplier = 2;

    public override void OnBonusStarts()
    {
        base.OnBonusStarts();
        GameSettings.multiplierBonus = multiplier;

    }
    public override void OnBonusEnds()
    {
        base.OnBonusEnds();
        GameSettings.multiplierBonus = 1;
    }
}


