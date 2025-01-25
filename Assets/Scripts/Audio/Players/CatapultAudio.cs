using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(Catapult))]
public class CatapultAudio : MonoBehaviour
{
    public AudioClip launchClip;
    public float launchClipVolume = 1f;
    public AudioClip prepareLaunchClip;
    public float prepareLaunchClipVolume = 1f;


    private Catapult catapult;
    void Start()
    {
        catapult = GetComponent<Catapult>();

        catapult.OnLaunch += PlayLaunchSound;
        catapult.OnPrepareLaunch += PlayPreparedLaunchSound;
    }

    void PlayPreparedLaunchSound()
    {
        AudioClipPlayer.PlayClip(prepareLaunchClip, prepareLaunchClipVolume);
    }

    void PlayLaunchSound()
    {
        AudioClipPlayer.PlayClip(launchClip,launchClipVolume);
    }
}
