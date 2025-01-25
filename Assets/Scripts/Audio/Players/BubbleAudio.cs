using UnityEngine;

[RequireComponent(typeof(Bubble))]
public class BubbleAudio : MonoBehaviour
{
    public MultiClip pops;
    public float volume = 1f;

    private Bubble bubble;

    private void Start()
    {
        bubble = GetComponent<Bubble>();
        bubble.OnDeath += PlayPopSound;
    }

    private void PlayPopSound()
    {
        AudioClipPlayer.PlayClip(pops.DrawRandomClip(), volume);
    }
}
