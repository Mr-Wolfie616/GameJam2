using UnityEngine;
using System.Collections.Generic;
public class RibSmash : MonoBehaviour
{
    private AudioSource audioSource;
    public List<AudioClip> boneAudioSources = new List<AudioClip>();
    public void smashs()
    {
        Destroy(gameObject, 0.5f);
        GameObject temp = new GameObject("TempAudio");
        audioSource = temp.AddComponent<AudioSource>();
        //audioSource.PlayOneShot(boneAudioSources[Random.Range(0, boneAudioSources.Count)]);
    }
}
