using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    [Header("Links:")]
    [SerializeField]
    private RectTransform viewPortContent;
    [SerializeField]
    private RectTransform elementSpawnPoint;

    [SerializeField]
    private GameObject elementPrefab;

    [SerializeField]
    private List<HistoryElement> historyElementsObjects = new List<HistoryElement>();

    public List<HistoryElementInfo> historyElements = new List<HistoryElementInfo>();

    [Header("Settings:")]
    public float defaultHeight = 0;
    public float margin = 10f;

    public void AddHistoryInfo(HistoryElementInfo info) => historyElements.Add(info);

    public void UpdateElements()
    {
        if (historyElementsObjects.Count > 0)
        {
            foreach (var item in historyElementsObjects)
            {
                Destroy(item.gameObject);
            }

            historyElementsObjects.Clear();
        }

        float summarySizeY = 0;
        foreach (var item in historyElements)
        {
            GameObject obj = Instantiate(elementPrefab, Vector2.zero, Quaternion.identity, viewPortContent);

            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            HistoryElement historyElement = obj.GetComponent<HistoryElement>();

            historyElement.Body.text = item.Body;
            historyElement.Description.text = item.Description;
            historyElement.Tittle.text = item.Tittle;

            rectTransform.anchoredPosition = new Vector2(elementSpawnPoint.anchoredPosition.x,
                                                        elementSpawnPoint.anchoredPosition.y - summarySizeY);

            float sizeY = historyElement.Body.GetPreferredValues().y > defaultHeight ? historyElement.Body.GetPreferredValues().y : defaultHeight;

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeY);

            summarySizeY += sizeY + margin;

            historyElementsObjects.Add(historyElement);
        }

        viewPortContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, summarySizeY + margin);
    }
}