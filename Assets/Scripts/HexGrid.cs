using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int width = 7;
    public int height = 7;

    public Text cellLabelPrefab;

    Canvas gridCanvas;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
	}

    private void Start()
    {
        for (int z = -height / 2; z < height / 2 + 1; z++)
        {
            for (int x = -width / 2; x < width / 2 + 1; x++)
            {
                CreateCell(x, z);
            }
        }
    }

    private void CreateCell(int x, int z)
    {
        Vector3 position;

        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        GameObject cell = Pooler.Instance.SpawnPoolObject("HexCells", position, Quaternion.identity);
        cell.GetComponent<HexBehavior>().coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        Text label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = cell.GetComponent<HexBehavior>().coordinates.ToStringOnSeparateLines();

    }


}
