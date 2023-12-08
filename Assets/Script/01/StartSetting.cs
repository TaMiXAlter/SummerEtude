using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity.Tutorial
{
public class StartSetting : MonoBehaviour
{
    private ResourceManager _resourceManager;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        _resourceManager = new LocalResourceManager();
        yield return _resourceManager.PrepareAssetAsync("pose_detection.bytes");
        yield return _resourceManager.PrepareAssetAsync("pose_landmark_heavy.bytes");
    }

}
}
