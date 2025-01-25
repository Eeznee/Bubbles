using UnityEngine;
using UnityEngine.UI;
public class ScoreTracker : MonoBehaviour
{
    private static ScoreTracker instance;
    public static int currentScore;
    public static int highestScore;



    public int chainBonus = 1000;
    public Text scoreText;
    public MiniPointsTextFX miniPointsFx;

    void Start()
    {
        instance = this;

        currentScore = 0;
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);

        UpdateText();
    }
    void UpdateText()
    {
        scoreText.text = currentScore.ToString();
    }

    void CompareToHighestScoreAndSave()
    {
        if(currentScore > highestScore)
        {
            PlayerPrefs.SetInt("HighestScore", currentScore);
        }
    }

    public static void IncreaseScore(int points, Vector3 worldPosition)
    {
        points *= GameSettings.multiplierBonus;

        currentScore += points;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        MiniPointsTextFX miniPoints =  Instantiate(instance.miniPointsFx, instance.transform.root);
        miniPoints.Initialize(points, screenPos);

        instance.UpdateText();
    }
}
