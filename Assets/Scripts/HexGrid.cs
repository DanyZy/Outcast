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
        float abscissa;
        Vector3 position;

        if (z > 0)
        {
            abscissa = (x - z * 0.5f + z / 2);
        }
        else
        {
            abscissa = (x + z * 0.5f - z / 2);
        }

        position.x = abscissa * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        Pooler.Instance.SpawnPoolObject("HexCells", position, Quaternion.identity);

        Text label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = x.ToString() + "\n" + z.ToString();
    }


}
