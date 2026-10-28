using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource songHead;
    public AudioSource songBody;

    double musicDuration;

    void Start()
    {
        songHead.Play();

        musicDuration = (double) songHead.clip.samples / songHead.clip.frequency;
        songBody.PlayScheduled(musicDuration);
    }
}
