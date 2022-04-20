using System.Runtime.InteropServices;

namespace CrossDK
{
    public class CrossDKConverter
    {
        /* Interface to native implementation */

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void crossDKConfigWithAppId(string appId, string apiKey, string userId);

        [DllImport("__Internal")]
        private static extern void dismissOverlay();

        [DllImport("__Internal")]
        private static extern void displayOverlayWithFormat(int format, int position, bool withCloseButton, bool isRewarded);
#endif

        /* Public interface for use inside C# code */

        public static void CrossDKConfigWithAppId(string appId = "", string apiKey = "", string userId = "")
        {
#if UNITY_IOS && !UNITY_EDITOR
            crossDKConfigWithAppId(appId, apiKey, userId);
#endif
        }

        public static void DismissOverlay()
        {
#if UNITY_IOS && !UNITY_EDITOR
            dismissOverlay();
#endif
        }

        public static void DisplayOverlayWithFormat(OverlayFormat format, OverlayPosition position, bool withCloseButton, bool isRewarded)
        {
#if UNITY_IOS && !UNITY_EDITOR
            displayOverlayWithFormat((int)format, (int)position, withCloseButton, isRewarded);
#endif
        }
    }
}
