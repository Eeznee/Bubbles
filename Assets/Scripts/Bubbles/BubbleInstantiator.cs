using UnityEngine;

public class BubbleInstantiator : MonoBehaviour
{
    public float sideBorder = 0.5f;
    public float spawnRate = 1f;
    public Bubble bubble;


    private float xSpawnRange;

    void Start()
    {
        InvokeRepeating("SpawnABubble", 0f, spawnRate);

        xSpawnRange = Camera.main.orthographicSize * Camera.main.aspect - sideBorder;

    }

    private void SpawnABubble()
    {
        Vector3 pos = transform.position;
        pos.x = Random.Range(-xSpawnRange, xSpawnRange);
        Bubble newBubble = Instantiate(bubble, pos, Quaternion.identity);
        newBubble.gameObject.SetActive(true);
    }
}
