using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    protected Dictionary<string,AudioClip> clips = new Dictionary<string, AudioClip>();
    protected AudioSource audioSrc;
    [SerializeField]
    protected string [] audioNames;
    protected virtual void Start()
    {
        audioNames = new string[]{"roll","bingo","puzzleComplete","Title","fail","bgm-Rain"};
        foreach(string audioName in audioNames)
            clips.Add(audioName,Resources.Load<AudioClip>(audioName));
        audioSrc = GetComponent<AudioSource>();
    }
    public void PlaySound(string clipName) {
        audioSrc.PlayOneShot(clips[clipName]);
    }
}
