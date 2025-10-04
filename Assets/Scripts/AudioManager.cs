using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    [Header("Music Clips")]
    public AudioClip backgroundMusic;
    public AudioClip menuMusic;
    public AudioClip winMusic;
    
    [Header("SFX Clips")]
    public AudioClip diceRollSound;
    public AudioClip playerMoveSound;
    public AudioClip snakeBiteSound;
    public AudioClip ladderClimbSound;
    public AudioClip winSound;
    public AudioClip buttonClickSound;
    
    [Header("Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 0.7f;
    [Range(0f, 1f)]
    public float sfxVolume = 0.8f;
    
    private static AudioManager instance;
    
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<AudioManager>();
            return instance;
        }
    }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        InitializeAudio();
        PlayMenuMusic();
    }
    
    void InitializeAudio()
    {
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
            
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();
        
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }
    
    public void PlayMenuMusic()
    {
        if (menuMusic != null)
            PlayMusic(menuMusic);
    }
    
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
            PlayMusic(backgroundMusic);
    }
    
    public void PlayWinMusic()
    {
        if (winMusic != null)
            PlayMusic(winMusic);
    }
    
    void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }
    
    public void PlayDiceRoll()
    {
        PlaySFX(diceRollSound);
    }
    
    public void PlayPlayerMove()
    {
        PlaySFX(playerMoveSound);
    }
    
    public void PlaySnakeBite()
    {
        PlaySFX(snakeBiteSound);
    }
    
    public void PlayLadderClimb()
    {
        PlaySFX(ladderClimbSound);
    }
    
    public void PlayWinSound()
    {
        PlaySFX(winSound);
    }
    
    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSound);
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
            musicSource.volume = musicVolume;
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }
    
    public void MuteMusic(bool mute)
    {
        if (musicSource != null)
            musicSource.mute = mute;
    }
    
    public void MuteSFX(bool mute)
    {
        if (sfxSource != null)
            sfxSource.mute = mute;
    }
    
    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }
    
    public void PauseMusic()
    {
        if (musicSource != null)
            musicSource.Pause();
    }
    
    public void ResumeMusic()
    {
        if (musicSource != null)
            musicSource.UnPause();
    }
}