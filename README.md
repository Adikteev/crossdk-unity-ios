# CrossDK for Unity

## Overview

This repo contains the CrossDK interface for Unity. It allows you to cross-promote your application catalog in your Unity project.

## Requirements

**Unity** version **>= 2018.4.36**

**iOS** version **>= 11.0**

CrossDK for Unity is available with iOS 11 minimal target version, but the `CrossDKOverlay` is only available since iOS 14. CrossDK provides support in order to handle cases where the `CrossDKOverlay` is not available (see [Overlay Delegate](#overlay-delegate)).

## Installation

### CocoaPods

To install CocoaPods on MacOS, add the following entry:

```rb
$ sudo gem install cocoapods
```

The CrossDK pod is automatically installed in the Xcode project when building with Unity, thanks to [External Dependency Manager for Unity](https://github.com/googlesamples/unity-jar-resolver). 

If you already use CocoaPods in your Unity project, you should consider adding your pods with [EDM4U](https://github.com/googlesamples/unity-jar-resolver) as well. 

## Configuration

To use CrossDK in your Unity project, you must download the `CrossDK.unitypackage` on the [releases page](https://github.com/Adikteev/crossdk-unity-ios/releases), then import it into your project. Once it's finished, drag the **CrossDK prefab** (located in `Assets\CrossDK\CrossDK`) into your scene. 

All the methods you'll need to call are in the `CrossDKSingleton` script on this prefab, and they all are public and static. Thus, you can call them from anywhere at anytime just by adding `import CrossDK;` at the top of any script.

In order to display an overlay properly, CrossDK requires some information. Since CrossDK won't work without these, you should set them up as soon as possible. In the following example, we use the setup function inside a `Start` event, but it's up to you to set it up wherever you like:

```csharp
CrossDKSingleton.CrossDKConfigWithAppId(string appId, string apiKey, string userId)
```

```csharp
using UnityEngine;
using CrossDK;

public class CrossDKSample : MonoBehaviour
{
    private void Start()
    {
        CrossDKSingleton.Config(
            <YOUR APP ID>,
            <YOUR API KEY>,
            <USER ID (optional)>);
    }
}
```

You can also enter this information on the CrossDK prefab and check autoCallConfig to let it call the config method automatically during the `Awake` event.

Note: The CrossDK prefab is not destroyed during scenes changes, so you only need to drag it into your first scene.

## Usage

Here are the configurations for each overlay format :  
- `OverlayFormat.Banner`: settle its position between `OverlayPosition.Bottom` or `OverlayPosition.BottomRaised`.
- `OverlayFormat.MidSize`: settle its position between `OverlayPosition.Bottom` or `OverlayPosition.BottomRaised`, with or without a close button .
- `OverlayFormat.Interstitial`: settle it with or without a close button, with or without a rewarded.

```csharp
CrossDKSingleton.DisplayOverlayWithFormat(OverlayFormat format, OverlayPosition position, bool withCloseButton, bool isRewarded)
```

```csharp
using UnityEngine;
using CrossDK;

public class CrossDKSample : MonoBehaviour
{
    public void DisplayMidSizeOverlayExample()
    {
        CrossDKSingleton.DisplayOverlay(
            OverlayFormat.MidSize, 
            OverlayPosition.Bottom, 
            true, 
            false);
    }
}
```

A `DismissOverlay()` method is available in order to prevent screen changes :

```csharp
CrossDKSingleton.DismissOverlay()
```

```csharp
using UnityEngine;
using CrossDK;

public class CrossDKSample : MonoBehaviour
{
    public void DismissExample()
    {
        CrossDKSingleton.DismissOverlay();
    }
}
```

## Overlay Delegate

Additionally, many delegates are available if you want to monitor what is happening with the `CrossDKOverlay`:

For instance, you can track when the user is rewarded with the delegate:
```csharp
CrossDKSingleton.overlayDidRewardUserWithRewardDelegate
```

```csharp
using UnityEngine;
using CrossDK;

public class CrossDKDelegatesSample : MonoBehaviour
{
    private void Start()
    {
        CrossDKSingleton.overlayDidRewardUserWithRewardDelegate += OverlayDidRewardUserWithRewardExample;
    }

    private void OnDestroy()
    {
        CrossDKSingleton.overlayDidRewardUserWithRewardDelegate -= OverlayDidRewardUserWithRewardExample;
    }

    private void OverlayDidRewardUserWithRewardExample(string message)
    {
        Debug.Log("User was rewarded");
    }
}
```
You can check the [crossdk-ios](https://github.com/Adikteev/crossdk-ios) repository to know more about the available delegate.

That’s all you need to know !
