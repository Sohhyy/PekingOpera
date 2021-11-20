using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundMgr : MonoBehaviour
{
    public List<AudioClip> audios;
    public List<AudioClip> dialogues;
    public static SoundMgr Instance = null;
    
    
    AudioSource audioSource;
    public int dialogueIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(int clipIndex)
    {
        audioSource.PlayOneShot(audios[clipIndex]);
        //SoundMgr.Instance. PlaySound(0);  Use this line when you choose the color, or just drag it into the event system.
    }

    public void PlayDialogue()
    {
        if (dialogueIndex == dialogues.Count)
        {
            Debug.LogWarning("dialogue index exceeds range!");
            return;
        }
        audioSource.PlayOneShot(dialogues[dialogueIndex]);
        dialogueIndex++;
    }

    public void PlayBGM()
    {
        //audioSource.clip=audios[clipIndex];
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void PlayDrawSound()
    {
        audioSource.PlayOneShot(audios[Random.Range(1,5)]);
        //SoundMgr.Instance.PlayDrawSound();  Use this line when you change the material of the face,
    }

   
}
