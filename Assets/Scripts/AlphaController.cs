using UnityEngine;
using UnityEngine.UI;

public class AlphaController : MonoBehaviour
{
    [SerializeField]
    private GameObject alphaObj;
    private Image alphaImage;
    private bool pause = false;
    void Start()
    {
        alphaImage = alphaObj.GetComponent<Image>();
    }
    public void Pause()
    {
        pause = true;
    }
    public void Continue()
    {
        alphaImage = alphaObj.GetComponent<Image>();
        alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.g, 0);
        pause = false;
    }
    void Update()
    {
        if (pause)
        {
            alphaImage.color = new Color(alphaImage.color.r, alphaImage.color.g, alphaImage.color.g, alphaImage.color.a + 0.5f * Time.deltaTime);
        }
       
    }
}
