using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreController : MonoBehaviour {

    public Font font;
    public Material material;

    [SerializeField]
    private int fontSize = 90;
    [SerializeField]
    private Color colorHeader;
    [SerializeField]
    private Color color1;
    [SerializeField]
    private Color color2;

    private int nbCol = 3;
    private int baseIndex = 0;
    private int maxNbRow = 7;
    [SerializeField]
    private int[] colX = { -250, 0, 250 };
    private string[] colName = { "RANK", "SCORE", "NAME" };
    private int rowHeight = 175;
    [SerializeField]
    private int yOffset = 250;

	// Use this for initialization
	void Start () {
        generateList();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void generateList()
    {
        for (int n = 0; n < nbCol; n++)
        {
            int baseX = colX[n];
            int baseY = yOffset +(rowHeight / 2);

            createText(baseX, baseY, colName[n], colorHeader);
            for (int i = baseIndex; i < baseIndex + maxNbRow; i++)
            {
                if (n == 0) createText(baseX, baseY + ((i - baseIndex) *rowHeight), ""+n, colorHeader);
            }
        }
    }

    GameObject createText(int x, int y, string value, Color color)
    {
        GameObject dummy = new GameObject();
        var transformD = dummy.AddComponent<RectTransform>();
        var rendererD = dummy.AddComponent<CanvasRenderer>();
        var textD = dummy.AddComponent<UnityEngine.UI.Text>();
        dummy.transform.SetParent(gameObject.transform);
        transformD.position = gameObject.transform.position + new Vector3(x, y, 0);
        textD.text = value;
        textD.font = font;
        textD.fontSize = fontSize;
        textD.horizontalOverflow = HorizontalWrapMode.Overflow;
        textD.verticalOverflow = VerticalWrapMode.Overflow;
        textD.alignment = TextAnchor.MiddleCenter;
        textD.color = color;
        textD.material = material;
        return dummy;
    }
}
