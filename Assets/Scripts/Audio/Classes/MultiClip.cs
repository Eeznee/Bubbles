using UnityEngine;

[CreateAssetMenu(fileName = "New Multi-Clip", menuName = "Multi-Clip")]
public class MultiClip : ScriptableObject
{
    public AudioClip[] clips;


    private int lastClip = -1;

    public AudioClip DrawRandomClip()
    {
        int clipId = Random.Range(0, clips.Length);

        if (clipId == lastClip && clips.Length > 1) return DrawRandomClip();

        lastClip = clipId;
        return clips[clipId];
    }
}
