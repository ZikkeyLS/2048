using UnityEngine;
using UnityEngine.Advertisements;

public class Monetization : MonoBehaviour, IUnityAdsInitializationListener
{
    public static Monetization Instance { get; private set; }

    [SerializeField] private string _androidID = "5075821";
    [SerializeField] private string _iosID = "5075820";
    [SerializeField] private bool _testMode = true;

    private string _gameID;

    public string GameID => _gameID;

    private void Awake()
    {
        Instance = this;
        InitializeAds();
    }

    private void InitializeAds()
    {
        _gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosID : _androidID;
        Advertisement.Initialize(_gameID, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        print("ADS init successfully!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print("ADS init failed: " + message);
    }

}
