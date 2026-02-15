using UnityEngine;
using UnityEngine.UI;
public class SlotView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private RectTransform rect;
    public RectTransform Rect => rect;
    public Sprite IconSprite => iconImage.sprite;
    public void SetIconSprite(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}
