using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

namespace unityad {

    public class ADManager: MonoBehaviour
    {

        public static ADManager Ins = null;

        public delegate void ADReady();
        public static event ADReady OnADReady;

        UnityAction _watched, _closed;

       // string gameId = "";

        void Awake() {
            Ins = this;
        }

        private void Start() {

#if UNITY_ANDROID
           // gameId = "3571923";
#elif UNITY_IOS
           // gameId = "3571922";
#endif
          //  Advertisement.AddListener(this);
            //Advertisement.Initialize(gameId, true);

        }

        //public bool IsRewardADLoaded() {
        //    return Advertisement.IsReady("rewardedVideo");
        //}

        //public void ShowRewardAD(UnityAction onWatched, UnityAction onClosed = null) {
        //    if (Advertisement.IsReady("rewardedVideo")) {
        //        _closed = onClosed;
        //        _watched = onWatched;
        //        //var options = new ShowOptions { resultCallback = HandleShowResult };
        //        //Advertisement.Show("rewardedVideo", options);
        //        Advertisement.Show("rewardedVideo");
        //    }
        //}

        public void OnUnityAdsReady(string placementId) {
            // If the ready Placement is rewarded, activate the button: 
            if (placementId == "rewardedVideo"/* && GameManager.ins != null && GameManager.ins.giftManager != null && GameManager.ins.giftManager.gameObject.activeInHierarchy*/) {
                //GameManager.ins.giftManager.ShowRewardButton();
                if(OnADReady != null) OnADReady();
            } else if(placementId == "bannerPlacement") {

            }
        }

        //public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        //    // Define conditional logic for each ad completion status:
        //    if (showResult == ShowResult.Finished) {
        //        // Reward the user for watching the ad to completion.
        //        if (_watched != null) _watched.Invoke();
        //    } else if (showResult == ShowResult.Skipped) {
        //        // Do not reward the user for skipping the ad.
        //        if (_watched != null) _watched.Invoke();
        //    } else if (showResult == ShowResult.Failed) {
        //        Debug.LogWarning("The ad did not finish due to an error.");
        //    }
        //}

        public void OnUnityAdsDidError(string message) {
            // Log the error.
        }

        public void OnUnityAdsDidStart(string placementId) {
            // Optional actions to take when the end-users triggers an ad.
        }

        public void ShowBannerAD() {
          //  Advertisement.Banner.Show("bannerPlacement");
          //  Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        }

        public void HideBannerAD() {
          //  Advertisement.Banner.Hide();
        }

        void ShowADFreeCandle() {
            //ShowRewardAD(() => DOVirtual.DelayedCall(.5f, FreeCandle, false));
        }


    }
}
