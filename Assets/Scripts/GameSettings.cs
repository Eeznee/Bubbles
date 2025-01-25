using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;


    public int chainingBonus = 1000;
    public int fallingNutMultiplier = 1;
    public int maxHealth;

    public static int currentHealth;
    public static int multiplierBonus = 1;

    private void Start()
    {
        instance = this;
        multiplierBonus = 1;

        currentHealth = maxHealth;
    }

    public static void IncrementHealth(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, instance.maxHealth);
    }
}
