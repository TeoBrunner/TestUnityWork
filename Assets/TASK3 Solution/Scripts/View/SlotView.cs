using UnityEngine;
using UnityEngine.UI;
public class SlotView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    private RectTransform rect;
    public RectTransform Rect => rect;
    private void Awake()
    {
        iconImage = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }
    public void SetSprite(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }
}
