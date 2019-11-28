using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class SoundManager : Singleton <SoundManager>
{

    private GameManager gameManager;
    public AudioSource MusicAudioSource;

    public AudioClip title;
    public AudioClip move;
    public AudioClip door;

    public override void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.OnSoundStateChange += HandleOnSoundStateChange;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        this.GetComponent<AudioSource>().mute = false;
        MusicAudioSource.GetComponent<AudioSource>().mute = false;
    }

    public void HandleOnSoundStateChange()
    {
        switch (gameManager.GetCurrentSoundState())
        {

            case GameManager.SoundState.TITLE:
                titleSound();
                break;
            case GameManager.SoundState.MOVE:
                moveSound();
                break;
            case GameManager.SoundState.DOOR:
                doorSound();
                break;
            case GameManager.SoundState.UNMUTESOUND:
                UnMuteSound();
                break;
            case GameManager.SoundState.MUTESOUND:
                MuteSound();
                break;

            case GameManager.SoundState.NONE:
                None();
                break;

            default:
                break;

        }

    }

    private void Update()
    {
       //if (Application.platform == RuntimePlatform.Android)
       //{
       //    if (Input.touchCount > 0)
       //    {
       //        if (Input.GetTouch(0).phase == TouchPhase.Ended)
       //            touchSound();
       //    }
       //}
       //else
       //{
       //    if (Input.GetMouseButtonUp(0))
       //    {
       //        touchSound();
       //    }
       //}
    }


    public IEnumerator delaySound(AudioClip audio, float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        this.GetComponent<AudioSource>().PlayOneShot(audio);
    }

    public IEnumerator delayLoopSound(AudioClip audio, float delay = 0.5f)
    {
        yield return new WaitForSeconds(delay);
        this.GetComponent<AudioSource>().clip = audio;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    private void titleSound()
    {
        this.GetComponent<AudioSource>().clip = title;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    private void touchSound()
    {
       // this.GetComponent<AudioSource>().PlayOneShot(touch);
    }

    private void doorSound()
    {
        this.GetComponent<AudioSource>().PlayOneShot(door);
    }

    private void moveSound()
    {
        this.GetComponent<AudioSource>().PlayOneShot(move);
    }

    private void None()
    {
        StopAllCoroutines();
        this.GetComponent<AudioSource>().Stop();
    }

    private void MuteSound()
    {
        this.GetComponent<AudioSource>().mute = true;
        MusicAudioSource.GetComponent<AudioSource>().mute = true;
    }

    private void UnMuteSound()
    {
        this.GetComponent<AudioSource>().mute = false;
        MusicAudioSource.GetComponent<AudioSource>().mute = false;
    }

}
