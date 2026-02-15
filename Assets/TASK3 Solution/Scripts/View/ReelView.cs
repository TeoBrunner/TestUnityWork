using AxGrid;
using AxGrid.Base;
using System.Collections;
using UnityEngine;

public class ReelView : MonoBehaviourExt
{
    [Header("Slot Spawn")]
    [SerializeField] private SlotView slotPrefab;
    [SerializeField] Transform slotParent;
    [SerializeField] float slotSpaceY = 10;
    [Header("Reel Settings")]
    [SerializeField] private float MaxReelSpeed = 300f;
    [SerializeField] private float ReelAccelerationTime = 2f;
    [SerializeField] private float ReelDecelerationTime = 3f;
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
    [OnStart]
    private void Init()
    {
        Settings.Model.Set(C.MaxReelSpeed, MaxReelSpeed);
        Settings.Model.Set(C.ReelAccelerationTime, ReelAccelerationTime);
        Settings.Model.Set(C.ReelDecelerationTime, ReelDecelerationTime);

        Settings.Model.EventManager.AddAction(C.OnReelStarting, () =>
        {
            float time = Settings.Model.Get<float>(C.ReelAccelerationTime);
            float startSpeed = 0f;
            float targetSpeed = Settings.Model.Get<float>(C.MaxReelSpeed);
            StartCoroutine(LerpReelSpeed(time, startSpeed, targetSpeed,C.FSMStartedSig));
        });
        Settings.Model.EventManager.AddAction(C.OnReelStopping, () =>
        {
            StartCoroutine(SnapStop());
        });
    }

    [OnUpdate]
    private void UpdateScrolling()
    {

        float reelSpeed = Settings.Model.GetFloat(C.ReelSpeed);
        MoveSlots(reelSpeed * Time.deltaTime);
    }
    private void MoveSlots(float deltaY)
    {
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            var slot = slots[i];
            if (slot != null)
            {
                var pos = slot.Rect.anchoredPosition;
                pos.y -= deltaY;
                if (pos.y < -(SLOT_COUNT + SLOT_START_INDEX) * (slot.Rect.sizeDelta.y + slotSpaceY))
                {
                    pos.y += SLOT_COUNT * (slot.Rect.sizeDelta.y + slotSpaceY);
                }
                slot.Rect.anchoredPosition = pos;
            }
        }
    }
    private IEnumerator LerpReelSpeed(float time, float from, float to, string resultEvent)
    {
        float elapsedTime = 0f;;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = Mathf.Lerp(from, to, elapsedTime / time);
            Settings.Model.Set(C.ReelSpeed, currentSpeed);
            yield return null;
        }
        Settings.Fsm.Invoke(resultEvent);
    }
    private IEnumerator SnapStop()
    {
        Settings.Model.Set(C.ReelSpeed, 0f);

        float currentY = slots[0].Rect.anchoredPosition.y;
        float stepY = slotPrefab.Rect.sizeDelta.y + slotSpaceY;
        float offset = currentY % stepY;
        int slotsToPass = Random.Range(3,6);
        float totalDistance = offset + slotsToPass * stepY;
        float timeToStop = Settings.Model.Get<float>(C.ReelDecelerationTime);
        float elapsedTime = 0f;
        float lastDistance = 0f;
        while (elapsedTime < timeToStop)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / timeToStop;

            float progress = 1f - Mathf.Pow(1f - t, 3f);

            float currentDistance = totalDistance * progress;
            float delta = currentDistance - lastDistance;
            lastDistance = currentDistance;

            MoveSlots(delta);
            yield return null;
        }

        Settings.Fsm.Invoke(C.FSMStoppedSig);
    }
}
