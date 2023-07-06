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
}
