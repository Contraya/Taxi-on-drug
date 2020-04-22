using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class Skid : MonoBehaviour
{
    public Transform SkidTrailPrefab;
    public static Transform skidTrailsDetachedParent;
    public bool skidding { get; private set; }
    public bool PlayingAudio { get; private set; }
    private AudioSource AudioSource;
    private Transform SkidTrail;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.loop = true;
        PlayingAudio = false;

        if (skidTrailsDetachedParent == null) {
            skidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
        }
    }

    public void PlayAudio()
    {
        AudioSource.Play();
        PlayingAudio = true;
    }


    public void StopAudio()
    {
        AudioSource.Stop();
        PlayingAudio = false;
    }


    public IEnumerator StartSkidTrail()
    {
        skidding = true;
        SkidTrail = Instantiate(SkidTrailPrefab);
        while (SkidTrail == null)
        {
            yield return null;
        }
        SkidTrail.parent = transform;
        SkidTrail.localPosition = transform.position;
    }


    public void EndSkidTrail() {
        if (!skidding)
        {
            return;
        }
        skidding = false;
        SkidTrail.parent = skidTrailsDetachedParent;
        Destroy(SkidTrail.gameObject, 10);
        }
}
