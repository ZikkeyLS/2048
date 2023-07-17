using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] private BannerPosition _position;

    [SerializeField] private string _androidID = "Banner_Android";
    [SerializeField] private string _iosID = "Banner_iOS";

    private string _adID;

    private void Awake()
    {
        _adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosID : _androidID;
    }

    private void Start()
    {
        StartCoroutine(LoadAdBanner());
    }

    private IEnumerator LoadAdBanner()
    {
        yield return new WaitForSeconds(1f);
        LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions()
        {
            loadCallback = () => 
            {
                ShowBannerAd();
            },
            errorCallback = (error) => {
            }
        };

        Advertisement.Banner.SetPosition(_position);
        Advertisement.Banner.Load(_adID, options);
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions()
        {
            clickCallback = () => { },
            hideCallback = () => { },
            showCallback = () => { }
        };

        Advertisement.Banner.Show(_adID, options);
    }
}
