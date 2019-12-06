using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class SoundManager : Singleton <SoundManager>
{

    private GameManager gameManager;
    public AudioSource MusicAudioSource;
    public AudioSource SoundEffectAudioSource;

    public AudioClip fave;
    public AudioClip listeningRoom;
    public AudioClip personalValue;
    public AudioClip lightEmpire;
    public AudioClip castle;
    public AudioClip move1;
    public AudioClip move2;
    public AudioClip indoor_move;
    public AudioClip door_open;

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

            case GameManager.SoundState.LIGHT_EMPIRE:
                LESound();
                break;
            case GameManager.SoundState.LIGHT_EMPIRE_MAN:
                LEMSound();
                break;
            case GameManager.SoundState.PERSONAL_VALUE:
                PVSound();
                break;
            case GameManager.SoundState.LISTENING_ROOM:
                LRSound();
                break;
            case GameManager.SoundState.PYRENEE_CATSLE:
                PCSound();
                break;
            case GameManager.SoundState.MOVE1:
                moveSound1();
                break;
            case GameManager.SoundState.MOVE2:
                moveSound2();
                break;
            case GameManager.SoundState.INDOOR_MOVE:
                indoorMoveSound();
                break;
            case GameManager.SoundState.DOOR_OPEN:
                doorSound();
                break;
            case GameManager.SoundState.UNMUTESOUND_EFFECT:
                UnMuteSoundEffect();
                break;
            case GameManager.SoundState.MUTESOUND_EFFECT:
                MuteSoundEffect();
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

    private void LESound()
    {
        this.GetComponent<AudioSource>().clip = fave;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    private void LEMSound()
    {
        this.GetComponent<AudioSource>().clip = lightEmpire;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    private void PVSound()
    {
        this.GetComponent<AudioSource>().clip = personalValue;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    private void LRSound()
    {
        this.GetComponent<AudioSource>().clip = listeningRoom;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    private void PCSound()
    {
        this.GetComponent<AudioSource>().clip = castle;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    private void touchSound()
    {
       // this.GetComponent<AudioSource>().PlayOneShot(touch);
    }

    private void doorSound()
    {
        this.GetComponent<AudioSource>().PlayOneShot(door_open);
    }

    private void moveSound1()
    {
        UnMuteSoundEffect();
        if (!SoundEffectAudioSource.isPlaying)
            SoundEffectAudioSource.PlayOneShot(move1);
    }

    private void moveSound2()
    {
        UnMuteSoundEffect();
        if (!SoundEffectAudioSource.isPlaying)
            SoundEffectAudioSource.PlayOneShot(move2);
    }

    private void indoorMoveSound()
    {
        UnMuteSoundEffect();
        if (!SoundEffectAudioSource.isPlaying)
            SoundEffectAudioSource.PlayOneShot(indoor_move);
    }

    private void None()
    {
        StopAllCoroutines();
        this.GetComponent<AudioSource>().Stop();
    }

    private void MuteSoundEffect()
    {
        SoundEffectAudioSource.GetComponent<AudioSource>().mute = true;
    }

    private void UnMuteSoundEffect()
    {
        SoundEffectAudioSource.GetComponent<AudioSource>().mute = false;
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
