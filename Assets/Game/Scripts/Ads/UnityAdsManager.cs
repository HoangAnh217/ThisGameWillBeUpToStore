using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string androidGameId = "5877519";
    [SerializeField] string iosGameId = "   ";
    [SerializeField] string adUnitId = "Rewarded_Android"; // hoặc Rewarded_iOS
    [SerializeField] bool testMode = true;

    void Start()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(androidGameId, testMode);
#elif UNITY_IOS
        Advertisement.Initialize(iosGameId, testMode);
#endif
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd()
    {
        /*if (Advertisement.IsReady(adUnitId))  // ⚠️ KHÔNG còn dùng được ở v4.x!
        {
            Advertisement.Show(adUnitId, this);
        }
        else
        {
            Debug.Log("Ad not ready yet, retrying load...");
            Advertisement.Load(adUnitId, this);
        }*/
    }

    // CALLBACKS
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad loaded: " + adUnitId);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning($"Failed to load Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(this.adUnitId) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("User watched the full ad and should be rewarded.");
            // TODO: Add reward logic here
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning($"Failed to show ad {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
