using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CounterClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ClickEvent clickHandler;
    [SerializeField]
    private GameObject secretObject;
    [SerializeField]
    private int activeCount = 10;
    [SerializeField]
    private float clickInterval = 0.75f;

    private float lastTimeClick;
    private int clickCount = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;
        float currentTimeClick = eventData.clickTime;
        if (Mathf.Abs(currentTimeClick - lastTimeClick) >= clickInterval)
        {
            clickCount = 1;
        }
        lastTimeClick = currentTimeClick;

        // 10回連続クリック
        if (clickCount >= activeCount) secretObject.SetActive(true);
    }

    public void AddClickHandler(UnityAction<GameObject> handler)
    {
        this.clickHandler.AddListener(handler);
    }

    [Serializable]
    public class ClickEvent : UnityEvent<GameObject> { }
}
