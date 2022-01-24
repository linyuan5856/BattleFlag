using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    [SerializeField] private int width = 6;
    [SerializeField] private int height = 6;
    [SerializeField] private HexCell cellPrefab;
    [SerializeField] private Text cellText;

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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleInput();
    }

    private void HandleInput()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
            TouchCell(hit.point);
    }

    private void TouchCell(Vector3 pos)
    {
        var localPos = transform.InverseTransformPoint(pos);
        var coordinates = HexCoordinates.FromPosition(localPos);
        Debug.Log(coordinates.ToString());
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 pos;
        pos.x = (x + z * 0.5f - z/2) * (HexMetrics.InnerRadius * 2f);
        pos.y = 0f;
        pos.z = z * (HexMetrics.OuterRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = pos;
        cell.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        Text txt = Instantiate(cellText);
        txt.transform.SetParent(hexCanvas.transform, false);
        txt.rectTransform.anchoredPosition = new Vector2(pos.x, pos.z);
        txt.text = cell.Coordinates.ToStringOnSeparateLines();
    }
}