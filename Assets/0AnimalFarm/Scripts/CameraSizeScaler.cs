using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CameraSizeScaler : MonoBehaviour
{

    public CanvasScaler canvasScaler;

        public enum ORIENT
    {
        PORTRAIT,
        LANDSCAPE
    }
    public ORIENT orientation = ORIENT.PORTRAIT;

    //public float ratio3_2 = 1;
    [Range(.1f, 3f)]
    public float ratio3_2_scale = 1;

    //public float ratio2_1 = 1;
    [Range(.1f, 3f)]
    public float ratio2_1_scale = 1;

    //public float ratio4_3 = 1;
    [Range(.1f, 3f)]
    public float ratio4_3_scale = 1;

    //public float ratio5_3 = 1;
    [Range(.1f, 3f)]
    public float ratio5_3_scale = 1;

    //public float ratio128_75 = 1;
    [Range(.1f, 3f)]
    public float ratio128_75_scale = 1;

    //public float ratio71_40 = 1;
    [Range(.1f, 3f)]
    public float ratio71_40_scale = 1;

    //public float ratio16_9 = 1;
    [Range(.1f, 3f)]
    public float ratio16_9_scale = 1;

    //public float ratio8_5 = 1;
    [Range(.1f, 3f)]
    public float ratio8_5_scale = 1;

    //public float ratio37_18 = 1;
    [Range(.1f, 3f)]
    public float ratio37_18_scale = 1;

    [Range(.1f, 3f)]
    public float ratio281_135_scale = 1;

    //public float ratio812_375 = 1;
    [Range(.1f, 3f)]
    public float ratio812_375_scale = 1;

    [Range(.1f, 3f)]
    public float ratio683_512_scale = 1;

    [Range(.1f, 3f)]
    public float ratio19_9_scale = 1;

    //Resolution res;
    Vector2 scaler = new Vector2(1920f, 1080f);
    float curScale = -1f;
    bool deviceIsIphoneX = false;
    public bool isScale;
    private void Awake() {
        if (orientation == ORIENT.PORTRAIT) scaler = new Vector2(1080f, 1920f);
        else scaler = new Vector2(1920f, 1080f);
        curScale = ratio16_9_scale;
        canvasScaler.referenceResolution = scaler * curScale;
    }

    private void Start() {
#if UNITY_IOS
        deviceIsIphoneX = (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneX) || (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone11);
#endif
        checkRatio();
    }

#if UNITY_EDITOR
    void Update() {

        if (Application.isPlaying) return;
        //if (res.width != Screen.width || res.height != Screen.height) {

        //res = Screen.currentResolution;

        checkRatio();

        //}

    }
#endif

    void checkRatio() {

        if (canvasScaler == null) return;

        if (orientation == ORIENT.PORTRAIT) scaler = new Vector2(1080f, 1920f);
        else scaler = new Vector2(1920f, 1080f);

        float ratio = (float)Screen.width / (float)Screen.height;// Camera.main.aspect;
       // print(ratio);

        if (Mathf.Approximately(ratio, 1.5f) || Mathf.Approximately(ratio, 0.6666667f) || Mathf.Approximately(ratio, 0.6668399f) || Mathf.Approximately(ratio, 0.6984925f)) {
             // print("ratio : 3:2");
             Camera.main.orthographicSize = 9.6f;
         
            if (!isScale) return;
            if (curScale != ratio3_2_scale) {
                curScale = ratio3_2_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }
            canvasScaler.matchWidthOrHeight = 1;

        } else if (Mathf.Approximately(ratio, 2f) || Mathf.Approximately(ratio, 0.5f)) {
            // print("ratio : 2:1");
            Camera.main.orthographicSize = 10.85f;
            if (!isScale) return;
            if (curScale != ratio2_1_scale) {
                curScale = ratio2_1_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 1.333333f) || Mathf.Approximately(ratio, 0.75f)) {
            // print("ratio : 4:3");
            Camera.main.orthographicSize = 9.6f;
            if (!isScale) return;
            if (curScale != ratio4_3_scale) {
                curScale = ratio4_3_scale;
                canvasScaler.referenceResolution = scaler * curScale;
                
            }
            canvasScaler.matchWidthOrHeight = 0;
        } else if (Mathf.Approximately(ratio, 1.666667f) || Mathf.Approximately(ratio, 0.6f)) {
             // print("ratio : 5:3");
            Camera.main.orthographicSize = 9.6f;
            if (!isScale) return;
            if (curScale != ratio5_3_scale) {
                curScale = ratio5_3_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 1.706667f) || Mathf.Approximately(ratio, 0.5859375f)) {
           // print("ratio : 128:75");
            Camera.main.orthographicSize = 9.6f;
            if (!isScale) return;
            if (curScale != ratio128_75_scale) {
                curScale = ratio128_75_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 1.775f) || Mathf.Approximately(ratio, 0.5633803f)) {
            //print("ratio : 71:40");
            Camera.main.orthographicSize = 9.6f;
           
            if (!isScale) return;
            if (curScale != ratio71_40_scale) {
                curScale = ratio71_40_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 1.779167f) || Mathf.Approximately(ratio, 0.5620609f) || Mathf.Approximately(ratio, 1.777778f) ||
                   Mathf.Approximately(ratio, 0.5625f) || Mathf.Approximately(ratio, 1.778667f) || Mathf.Approximately(ratio, 0.5622189f) ||
                   Mathf.Approximately(ratio, 0.5623701f) || Mathf.Approximately(ratio, 0.5626374f) || Mathf.Approximately(ratio, 0.5627376f)) {

           // print("ratio : 16:9");
            Camera.main.orthographicSize = 9.6f;
            if (!isScale) return;
            if (curScale != ratio16_9_scale) {
                curScale = ratio16_9_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 1.6f) || Mathf.Approximately(ratio, 0.625f)) {
           //print("ratio : 8:5");
         
            Camera.main.orthographicSize = 9.6f;
            if (!isScale) return;
            if (curScale != ratio8_5_scale) {
                curScale = ratio8_5_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 2.055556f) || Mathf.Approximately(ratio, 0.4864865f)) {
            //print("ratio : 37:18");
            Camera.main.orthographicSize = 11.12f;
            if (!isScale) return;
            if (curScale != ratio37_18_scale) {
                curScale = ratio37_18_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 0.4804271f) || Mathf.Approximately(ratio, 0.4812834f)) {
            // print("ratio : 281:135");
            Camera.main.orthographicSize = 11.26f;
            if (!isScale) return;
            if (curScale != ratio281_135_scale) {
                curScale = ratio281_135_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 2.165333f) || Mathf.Approximately(ratio, 0.4618227f) || Mathf.Approximately(ratio, 0.4620536f) ||
            Mathf.Approximately(ratio, 0.4615385f) || deviceIsIphoneX) {
            // print("ratio : 812:375, iPhoneX");
            Camera.main.orthographicSize = 11.7f;
            if (!isScale) return;
            if (curScale != ratio812_375_scale) { // iphoneX
                curScale = ratio812_375_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 1.333984f) || Mathf.Approximately(ratio, 0.749634f)) {
            //print("ratio : 683:512, ipad pro"); // ipad pro
           
            Camera.main.orthographicSize = 9.6f;
            if (!isScale) return;
            if (curScale != ratio683_512_scale) { // ipad pro
                curScale = ratio683_512_scale;
                canvasScaler.referenceResolution = scaler * curScale;
                canvasScaler.matchWidthOrHeight = 0;
            }

        } else if (Mathf.Approximately(ratio, 2.111437f) || Mathf.Approximately(ratio, 0.4741474f) || Mathf.Approximately(ratio, 0.4945055f) ||
                   Mathf.Approximately(ratio, 2.022222f) || Mathf.Approximately(ratio, 0.4734927f) || Mathf.Approximately(ratio, 0.4736264f) ||
                   Mathf.Approximately(ratio, 0.4740331f) || Mathf.Approximately(ratio, 0.4736842f)) {
           // print("ratio : 19:9");
            Camera.main.orthographicSize = 11.42f;
            if (!isScale) return;
            if (curScale != ratio19_9_scale) {
                curScale = ratio19_9_scale;
                canvasScaler.referenceResolution = scaler * curScale;
            }

        } else if (Mathf.Approximately(ratio, 0.462203f) || Mathf.Approximately(ratio, 0.4620853f) || Mathf.Approximately(ratio, 0.4619333f) ||
            Mathf.Approximately(ratio, 0.4620853f))
        {

            //  print("ratio : 19:9");
           // print("iphone 12-13");
              Camera.main.orthographicSize = 11.42f;
            canvasScaler.matchWidthOrHeight = 0.15f;
        }
    }

}
