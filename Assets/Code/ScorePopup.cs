using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    public Transform ScorePopupTransform;

    private TextMeshPro textMesh;
    private float disappearTime;
    private Color textColor;

    public ScorePopup Create(Vector3 position, int amount, bool isBad)
    {
        Transform scorePopupTransform = Instantiate(ScorePopupTransform, position, Quaternion.identity);
        ScorePopup scorePopup = scorePopupTransform.GetComponent<ScorePopup>();
        scorePopup.Setup(amount, isBad);
        return scorePopup;
    }
    
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int amount, bool isBad)
    {
        textMesh.SetText(amount.ToString());

        if (isBad)
        {
            textColor = new Color32(145, 34, 8, 255);

        }
        else
        {
            textColor = new Color32(86, 144, 255, 255);
        }
        textMesh.fontSize = 5;
        textMesh.faceColor = textColor;

        disappearTime = 1f;
    }

    private void Update()
    {
        float moveSpeed = 0.5f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        disappearTime -= Time.deltaTime;
        if (disappearTime < 0)
        {
            // Start disappearing
            float disappearSpeed = 2f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.faceColor = textColor;

            Color outlineColor = textMesh.outlineColor;
            outlineColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.outlineColor = outlineColor;
        }
        if (textColor.a < 0)
        {
            // Text invisible
            Destroy(gameObject);
        }
     }
}
