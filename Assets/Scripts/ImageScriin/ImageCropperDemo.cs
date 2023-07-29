using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ImageCropperDemo : MonoBehaviour
{
    public GameObject panelActive;
    private RawImage croppedImageHolderCircle;
    private RawImage croppedImageHolder;
    public Text croppedImageSize;
    public float rectangleHeight = 1;

    public Toggle ovalSelectionInput, autoZoomInput;
    public InputField minAspectRatioInput, maxAspectRatioInput;
    public static ImageCropperDemo imageCropeD;
    private bool DoubleImage;
    [SerializeField] private GridGallery gridGallery;
    private Texture2D circleTexture;//временное хранилище
    private void Start()
    {
        imageCropeD = this;
    }
    public void CropCircle(RawImage rawImage1, RawImage rawImage2)
    {
        circleTexture = null;
        croppedImageHolderCircle = rawImage1;
        croppedImageHolder = rawImage2;

        // If image cropper is already open, do nothing
        if (ImageCropper.Instance.IsOpen)
            return;
        DoubleImage = true;
        StartCoroutine(TakeScreenshotAndCrop(true));
    }
    public void CropSquare()
    {
        if (DoubleImage)
        {
            // If image cropper is already open, do nothing
            if (ImageCropper.Instance.IsOpen)
                return;
            DoubleImage = false;
            StartCoroutine(TakeScreenshotAndCrop(false));
        }
    }

    private IEnumerator TakeScreenshotAndCrop(bool isFigure)
    {
        yield return new WaitForEndOfFrame();

        //bool ovalSelection = ovalSelectionInput.isOn;
        bool ovalSelection = isFigure;
        //bool autoZoom = autoZoomInput.isOn;
        bool autoZoom = true;

        float minAspectRatio, maxAspectRatio;
        //if( !float.TryParse( minAspectRatioInput.text, out minAspectRatio ) )
        //	minAspectRatio = 0f;
        //if( !float.TryParse( maxAspectRatioInput.text, out maxAspectRatio ) )
        //	maxAspectRatio = 0f;
        if (!float.TryParse("1", out minAspectRatio))
            minAspectRatio = 0f;
        if (!float.TryParse("1", out maxAspectRatio))
            maxAspectRatio = 0f;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        ImageCropper.Instance.Show(screenshot, (bool result, Texture originalImage, Texture2D croppedImage) =>
        {
            // Destroy previously cropped texture (if any) to free memory
            //Destroy( croppedImageHolder.texture, 5f );

            // If screenshot was cropped successfully
            if (result)
            {
                // Assign cropped texture to the RawImage

                if (DoubleImage == true)
                {
                    circleTexture = croppedImage;
                }
                else
                {
                    croppedImageHolderCircle.texture = circleTexture;
                    croppedImageHolder.texture = croppedImage;
                    ImageLoad.imageLoad.UploadCirclePhoto(circleTexture, croppedImage);
                }

                //размер
                /*
                Vector2 size = croppedImageHolder.rectTransform.sizeDelta;
                if( croppedImage.height <= croppedImage.width )
                    size = new Vector2( 400f, 400f * ( croppedImage.height / (float) croppedImage.width ) );
                else
                    size = new Vector2( 400f * ( croppedImage.width / (float) croppedImage.height ), 400f );
                croppedImageHolder.rectTransform.sizeDelta = size;
                */


                //croppedImageSize.enabled = true;
                //croppedImageSize.text = "Image size: " + croppedImage.width + ", " + croppedImage.height;
                panelActive.SetActive(isFigure);
                print("izi");
            }
            else
                if(DoubleImage == false || circleTexture == null)
                gridGallery.RemoveImage();

            // Destroy the screenshot as we no longer need it in this case
            Destroy(screenshot);
        },

        settings: new ImageCropper.Settings()
        {
            ovalSelection = ovalSelection,
            autoZoomEnabled = autoZoom,
            imageBackground = Color.clear, // transparent background
            selectionMinAspectRatio = minAspectRatio,
            selectionMaxAspectRatio = maxAspectRatio,
            rectangleHeightFactor = rectangleHeight,
            PanelActive = panelActive

        },
        croppedImageResizePolicy: (ref int width, ref int height) =>
        {
            // uncomment lines below to save cropped image at half resolution
            //width /= 2;
            //height /= 2;
        });
    }
}