using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Singleton
    public static AudioManager Instance { get; private set; }

    //Exposed private fields
    [Header("References")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip sfx0;
    [SerializeField] private AudioClip sfx1, sfx2;

    //private fields
    private bool playSFX;

    //---MonoBehaviour methods---
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //---Class methods---
    //Applies music settings
    public void SetMusic(bool playMusic)
    {
        if(playMusic)
        {
            if(musicSource.isPlaying) { return; }

            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }

    //Sets music volume
    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    //Applies sfx settings
    public void SetSFX(bool playSFX)
    {
        this.playSFX = playSFX;
    }

    //Plays SFX
    public void PlaySFX(SFXCode code)
    {
        if (!playSFX) { return; }
        if (sfxSource.isPlaying) { sfxSource.Stop(); }

        switch(code)
        {
            case SFXCode.Code0:
                sfxSource.PlayOneShot(sfx0);
                break;

            case SFXCode.Code1:
                sfxSource.PlayOneShot(sfx1);
                break;

            case SFXCode.Code2:
                sfxSource.PlayOneShot(sfx2);
                break;
        }
    }

    //Overloaded play sfx
    public void PlaySFX(AudioClip sfx)
    {
        if (!playSFX) { return ; }
        if (sfxSource.isPlaying) { sfxSource.Stop(); }

        sfxSource.PlayOneShot(sfx);
    }
}
