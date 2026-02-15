using AxGrid;
using AxGrid.Base;
using UnityEngine;

public class ReelView : MonoBehaviourExt
{
    [SerializeField] SlotView slotPrefab;
    [SerializeField] Transform slotParent;
    [SerializeField] float slotSpaceY = 10;

    private SlotView[] slots = new SlotView[SLOT_COUNT];

    private const int SLOT_COUNT = 4;
    private const int SLOT_START_INDEX = -2;

    [OnAwake]
    private void SpawnSlots()
    {
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            slot.Rect.anchoredPosition = new Vector2(0, -(i + SLOT_START_INDEX) * (slot.Rect.sizeDelta.y + slotSpaceY));
            slots[i] = slot;
        }
    }

    [OnUpdate]
    private void UpdateScrolling()
    {
        float reelSpeed = Settings.Model.GetFloat(C.ReelSpeed);
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            var slot = slots[i];
            if (slot != null)
            {
                var pos = slot.Rect.anchoredPosition;
                pos.y -= reelSpeed * Time.deltaTime;
                if (pos.y < -(SLOT_COUNT + SLOT_START_INDEX) * (slot.Rect.sizeDelta.y + slotSpaceY))
                {
                    pos.y += SLOT_COUNT * (slot.Rect.sizeDelta.y + slotSpaceY);
                }
                slot.Rect.anchoredPosition = pos;
            }
        }
    }


}
