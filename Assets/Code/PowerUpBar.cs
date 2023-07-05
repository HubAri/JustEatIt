using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour
{
    [SerializeField]
    GameObject bar;

    private Image barBGImage;
    private Image barImage;

    private void Start()
    {
        SetSizeBar();
        PositionBar();

        // make whole bar invisible
        barBGImage = transform.parent.GetComponent<Image>();
        barImage = GetComponent<Image>();

        if (barBGImage != null)
            barBGImage.enabled = false;
        barImage.enabled = false;
    }


    public IEnumerator DecreaseScaleOverTime(float duration)
    {
        // set scale to 1
        bar.transform.localScale = new Vector3(1, 1, 1);

        // make visible
        barBGImage.enabled = true;
        barImage.enabled = true;

        float elapsedTime = 0f;
        Vector3 initialScale = bar.transform.localScale;
        Vector3 targetScale = new Vector3(0f, initialScale.y, initialScale.z);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            bar.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure scale is 0 at end
        bar.transform.localScale = targetScale;
        barBGImage.enabled = false;
        barImage.enabled = false;

    }


    private void PositionBar()
    {
        RectTransform rectTransform = transform.parent.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0f, 1f);
        rectTransform.anchorMax = new Vector2(0f, 1f);
        rectTransform.pivot = new Vector2(0f, 1f);

        float offsetXPercentage = 0.02f;  // 2% offset
        float offsetX = Screen.height * offsetXPercentage;
        float offsetYPercentage = 0.02f;  // 2% offset
        float offsetY = Screen.height * offsetYPercentage;

        rectTransform.anchoredPosition = new Vector2(offsetX, -offsetY);
    }

    private void SetSizeBar()
    {
        RectTransform rectTransformBG = transform.parent.GetComponent<RectTransform>();
        RectTransform rectTransform = GetComponent<RectTransform>();

        float offsetX = 0.98f;
        float offsetY = 0.85f;

        float heightPercentage = 0.05f;  // 5% height
        float barHeightBG = Screen.height * heightPercentage;
        float barHeight = barHeightBG * offsetY;

        float widthPercentage = 0.15f;  // 15% width
        float barWidthtBG = Screen.width * widthPercentage;
        float barWidth = barWidthtBG * offsetX;

        rectTransformBG.sizeDelta = new Vector2(rectTransformBG.sizeDelta.x, barHeightBG);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, barHeight);

        rectTransformBG.sizeDelta = new Vector2(barWidthtBG, rectTransformBG.sizeDelta.y);
        rectTransform.sizeDelta = new Vector2(barWidth, rectTransform.sizeDelta.y);
    }
}
