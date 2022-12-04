using UnityEngine;

public class SwipeSound : MonoBehaviour
{
    [SerializeField] private AudioClip _swipeSound;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _soundTrack;

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
