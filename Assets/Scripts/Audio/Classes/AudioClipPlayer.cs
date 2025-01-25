using UnityEngine;

[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
public class AudioClipPlayer : MonoBehaviour
{
    public static AudioClipPlayer instance;
    public static AudioSource audioSource;

    private void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayClip(AudioClip clip)
    {
        PlayClip(clip, 1f);
    }
    public static void PlayClip(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
