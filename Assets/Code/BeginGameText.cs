using TMPro;
using UnityEngine;

public class BeginGameText : MonoBehaviour
{
    public TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string  text)
    {
        Text.SetText(text);
        if (Text != null && text.Equals(string.Empty))
        {
            Text.enabled = false;
        }
    }
}
