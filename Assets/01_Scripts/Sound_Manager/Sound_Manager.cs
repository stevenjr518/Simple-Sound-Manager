using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioListener))]
public class Sound_Manager : MonoBehaviour
{
    [System.Serializable]
    public struct Clip {
        public AudioClip file;
        public string path;
        public float volume;
    }
    
    private AudioSource audioSource;
    public static Sound_Manager instance = null;
    [SerializeField]
    private float volume;
    public Clip[] clips;
    private Clip cache;
    private bool clipSwitching;
    private int currentClip = -1;
    private int nextClip;
    private float fadeScale = 2f;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        audioSource = GetComponent<AudioSource>();
        LoadClips("json_audio");
    }

    public void OnPlayClip(int idx) {
        if (idx == currentClip)
        {
            return;
        }

        if (currentClip == -1) { 
            currentClip = idx;
            volume = clips[idx].volume;
            audioSource.volume = volume;
            audioSource.clip = clips[idx].file;
            audioSource.Play();
            return;
        }

        nextClip = idx;
        cache = clips[idx];
        clipSwitching = true;
        StartCoroutine(ClipSwitch());

    }

    public IEnumerator ClipSwitch() {
        do
        {
            yield return new WaitForEndOfFrame();
            volume -= Time.deltaTime * fadeScale;
            audioSource.volume = volume;
            if (volume <= 0)
            {
                currentClip = nextClip;
                audioSource.clip = cache.file;
                volume = cache.volume;
                audioSource.Stop();
                audioSource.volume = volume;
                clipSwitching = false;
                audioSource.Play();
            }
        } while (clipSwitching);
    }

    public void OnStop()
    {
        audioSource.Stop();
        currentClip = -1;
        audioSource.clip = null;
    }

    public void OnPlayOneShot(int idx) {
        audioSource.PlayOneShot(clips[idx].file, clips[idx].volume);
    }

    public void LoadClips(string fileName) {
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset == null)
        {
            return;
        }
        JsonUtility.FromJsonOverwrite(textAsset.text, this);
        for (int i = 0; i < clips.Length; ++i) {
            clips[i].file = Resources.Load<AudioClip>(clips[i].path);
        }
    }
}
