using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class MiniPointsTextFX : MonoBehaviour
{
    public float lifeTime = 2f;
    public float speed = 20f;

    public int maxScore;
    public Gradient scoreGradient;

    private Text text;
    private RectTransform rectTransform;

    private float spawnTime;
    private Color color;
    private Vector2 randDirection;

    public void Initialize(int score, Vector3 screenPos)
    {
        gameObject.SetActive(true);

        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();

        rectTransform.position = screenPos;
        text.text = score.ToString();
        color = scoreGradient.Evaluate((float)score / maxScore);

        spawnTime = Time.unscaledTime;

        randDirection = Random.insideUnitCircle.normalized;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float lerp = Mathf.InverseLerp(spawnTime + lifeTime, spawnTime, Time.unscaledTime);
        color.a = Mathf.Sqrt(lerp);
        text.color = color;

        rectTransform.Translate(new Vector3(randDirection.x, randDirection.y) * Time.unscaledDeltaTime * speed);
    }
}
