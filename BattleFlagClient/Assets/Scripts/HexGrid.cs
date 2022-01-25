using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    [SerializeField] private int width = 6;
    [SerializeField] private int height = 6;
    [SerializeField] private HexCell cellPrefab;
    [SerializeField] private Text cellText;
    [SerializeField] private Color defaultColor = Color.white;

    private HexCell[] cells;
    private Canvas hexCanvas;
    private HexMesh hexMesh;

    void Awake()
    {
        Debug.Assert(cellPrefab != null, "cellPrefab!=null");
        if (cellPrefab == null) return;

        hexCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        cells = new HexCell[width * height];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
                CreateCell(x, z, i++);
        }
    }

    void Start()
    {
        hexMesh.Triangulate(cells);
    }


    public void ColorCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.CellColor = color;
        hexMesh.Triangulate(cells);
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 pos;
        pos.x = (x + z * 0.5f - z / 2) * (HexMetrics.InnerRadius * 2f);
        pos.y = 0f;
        pos.z = z * (HexMetrics.OuterRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = pos;
        cell.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.CellColor = defaultColor;

        if (x > 0)
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        if (z > 0)
        {
            if ((z & 1) == 0) //偶数
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - width]);
                if (x > 0) cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
            }
            else
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - width]);
                if (x < width - 1) cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
            }
        }

        Text txt = Instantiate(cellText);
        txt.rectTransform.SetParent(hexCanvas.transform, false);
        txt.rectTransform.anchoredPosition = new Vector2(pos.x, pos.z);
        txt.text = cell.Coordinates.ToStringOnSeparateLines();
    }
}