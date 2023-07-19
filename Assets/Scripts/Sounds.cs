using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds Instance;

    [SerializeField] private AudioClip _swipeSound;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _soundTrack;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning($"Already have instance of {nameof(Sounds)}");
    }

    public void PauseMusic()
    {
        _soundTrack.Pause();
    }

    public void UnPauseMusic()
    {
        _soundTrack.UnPause();
    }

    public void Play()
    {
        if (GlobalData.EnableSound)
            _source.PlayOneShot(_swipeSound);
    }

    public void UpdateStatus()
    {
        if (GlobalData.EnableSound && _soundTrack.isPlaying == false)
            _soundTrack.Play();
        else if (GlobalData.EnableSound == false && _soundTrack.isPlaying)
            _soundTrack.Stop();
    }

    public void ToggleSoundSetting()
    {
        GlobalData.EnableSound = !GlobalData.EnableSound;

        UpdateStatus();
    }
    
}
