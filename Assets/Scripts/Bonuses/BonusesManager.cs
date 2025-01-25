using System;
using UnityEngine;

[Serializable]
public class BonusAndRate
{
    public Bonus bonus;
    public float rate = 1f;
}

public class BonusesManager : MonoBehaviour
{
    public BonusAndRate[] allBonuses;

    private void Start()
    {
        foreach (BonusAndRate bonus in allBonuses) bonus.bonus.Initialize();
    }
    private void Update()
    {
        foreach (BonusAndRate bonus in allBonuses) bonus.bonus.Update();

        if (Input.GetKeyDown(KeyCode.Alpha1)) allBonuses[0].bonus.TriggerBonus();
        if (Input.GetKeyDown(KeyCode.Alpha2)) allBonuses[1].bonus.TriggerBonus();
        if (Input.GetKeyDown(KeyCode.Alpha3)) allBonuses[2].bonus.TriggerBonus();
        if (Input.GetKeyDown(KeyCode.Alpha4)) allBonuses[3].bonus.TriggerBonus();
        if (Input.GetKeyDown(KeyCode.Alpha5)) allBonuses[4].bonus.TriggerBonus();
        if (Input.GetKeyDown(KeyCode.Alpha6)) allBonuses[5].bonus.TriggerBonus();
        if (Input.GetKeyDown(KeyCode.Alpha7)) allBonuses[6].bonus.TriggerBonus();
        if (Input.GetKeyDown(KeyCode.Alpha8)) allBonuses[7].bonus.TriggerBonus();
        //if (Input.GetKeyDown(KeyCode.Alpha9)) allBonuses[8].bonus.TriggerBonus();
        //if (Input.GetKeyDown(KeyCode.Alpha0)) allBonuses[9].bonus.TriggerBonus();
    }
}



