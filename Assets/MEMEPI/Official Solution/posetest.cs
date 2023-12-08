using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Stopwatch = System.Diagnostics.Stopwatch;

namespace Mediapipe.Unity.Tutorial
{
  public class posetest : MonoBehaviour
  {
    [SerializeField] private TextAsset _configAsset;
    [SerializeField] private RawImage _screen;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _fps;
    [SerializeField] private PoseLandmarkListAnnotationController _poseLandmarkListAnnotationController;

    private CalculatorGraph _graph;
    // private ResourceManager _resourceManager;

    private WebCamTexture _webCamTexture;
    private Texture2D _inputTexture;
    private Color32[] _inputPixelData;

    static public PosePoints[] posepoint = new PosePoints[33];
    
    SidePacket _sidePacketpass;

    private IEnumerator Start()
    {
      if (WebCamTexture.devices.Length == 0)
      {
        throw new System.Exception("Web Camera devices are not found");
      }
      var webCamDevice = WebCamTexture.devices[1];
      _webCamTexture = new WebCamTexture(webCamDevice.name, _width, _height, _fps);
      _webCamTexture.Play();

      yield return new WaitUntil(() => _webCamTexture.width > 16);

      _screen.rectTransform.sizeDelta = new Vector2(_width, _height);

      _inputTexture = new Texture2D(_width, _height, TextureFormat.RGBA32, false);
      _inputPixelData = new Color32[_width * _height];

      _screen.texture = _webCamTexture;

      
      // _resourceManager = new LocalResourceManager();
      //       yield return _resourceManager.PrepareAssetAsync("pose_detection.bytes");
      //       yield return _resourceManager.PrepareAssetAsync("pose_landmark_heavy.bytes");

      _sidePacketpass = new SidePacket();

      _sidePacketpass.Emplace("input_rotation",new IntPacket(0));
      _sidePacketpass.Emplace("input_horizontally_flipped", new BoolPacket(false));
      _sidePacketpass.Emplace("input_vertically_flipped", new BoolPacket(true));


      _sidePacketpass.Emplace("output_rotation",new IntPacket(0));
      _sidePacketpass.Emplace("output_horizontally_flipped", new BoolPacket(false));
      _sidePacketpass.Emplace("output_vertically_flipped", new BoolPacket(false));
      _sidePacketpass.Emplace("model_complexity", new IntPacket(1));
      _sidePacketpass.Emplace("smooth_landmarks", new BoolPacket(true));
      _sidePacketpass.Emplace("enable_segmentation", new BoolPacket(true));
      _sidePacketpass.Emplace("smooth_segmentation", new BoolPacket(true));


      var stopwatch = new Stopwatch();

      _graph = new CalculatorGraph(_configAsset.text);
      var multiFaceLandmarksStream = new OutputStream<NormalizedLandmarkListPacket, NormalizedLandmarkList>(_graph, "pose_landmarks");
      multiFaceLandmarksStream.StartPolling().AssertOk();
      _graph.StartRun(_sidePacketpass).AssertOk();
      stopwatch.Start();

      var screenRect = _screen.GetComponent<RectTransform>().rect;
    

      while (true)
      {
        _inputTexture.SetPixels32(_webCamTexture.GetPixels32(_inputPixelData));
        var imageFrame = new ImageFrame(ImageFormat.Types.Format.Srgba, _width, _height, _width * 4, _inputTexture.GetRawTextureData<byte>());
        var currentTimestamp = stopwatch.ElapsedTicks / (System.TimeSpan.TicksPerMillisecond / 1000);
        _graph.AddPacketToInputStream("input_video", new ImageFramePacket(imageFrame, new Timestamp(currentTimestamp))).AssertOk();

        yield return new WaitForEndOfFrame();

        if (multiFaceLandmarksStream.TryGetNext(out var multiFaceLandmarks))
        {
          _poseLandmarkListAnnotationController.DrawNow(multiFaceLandmarks);

          for(int i = 0 ;i<posepoint.Length;i++){
            posepoint[i].x = 1-multiFaceLandmarks.Landmark[i].X;
            posepoint[i].y = 1-multiFaceLandmarks.Landmark[i].Y;
            posepoint[i].z = 1-multiFaceLandmarks.Landmark[i].Z;
          }
        }
       
      }
    }

    private void OnDestroy()
    {
      if (_webCamTexture != null)
      {
        _webCamTexture.Stop();
      }

      if (_graph != null)
      {
        try
        {
          _graph.CloseInputStream("input_video").AssertOk();
          _graph.WaitUntilDone().AssertOk();
        }
        finally
        {

          _graph.Dispose();
        }
      }
    }

    public struct PosePoints{
       public float x ; 
       public float y ; 
       public float z ; 
    }
  }
}