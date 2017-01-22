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
    [SerializeField]
    private Sprite backgroundSprite;
    [SerializeField]
    private float backgroundYScale;

    private int nbCol = 3;
    private int baseIndex = 1;
    [SerializeField]
    private int maxNbRow = 7;
    [SerializeField]
    private int[] colX = { -250, 0, 250 };
    private string[] colName = { "RANK", "SCORE", "NAME" };
    [SerializeField]
    private int rowHeight = 175;
    [SerializeField]
    private int yOffset = 250;

	// Use this for initialization
	void Start () {
        generateList();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("space") /*|| Input.GetButton("XboxA")*/) UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    void generateList()
    {
        foreach (var pair in GameManager.Instance.scoreList) Debug.Log("HS: " + pair.Key + " " + pair.Value);
        for (int n = 0; n < nbCol; n++)
        {
            int baseX = colX[n];
            int baseY = yOffset +(rowHeight / 2);

            createText(baseX, baseY, colName[n], colorHeader);
            for (int i = baseIndex; i < baseIndex + maxNbRow; i++)
            {
                Color color = color1;
                string text = "---";
                string score = "---";
                if (i % 2 == 0)
                {
                    color = color2;
                    createBackground(0, baseY - ((i - baseIndex + 1) * rowHeight));
                }
                if (i < GameManager.Instance.scoreList.Count + 1)
                {
                    Debug.Log("OK");
                    text = GameManager.Instance.scoreList[i - 1].Key;
                    score = "" + GameManager.Instance.scoreList[i - 1].Value;
                }
                if (n == 0) createText(baseX, baseY - ((i - baseIndex + 1) *rowHeight), ""+i, color);
                else if (n == 1) createText(baseX, baseY - ((i - baseIndex + 1) * rowHeight), score, color);
                else if (n == 2) createText(baseX, baseY - ((i - baseIndex + 1) * rowHeight), text, color);

            }
        }
    }

    GameObject createText(int x, int y, string value, Color color)
    {
        GameObject dummy = new GameObject();
        dummy.SetActive(true);
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

    GameObject createBackground(int x, int y)
    {
        GameObject dummy = new GameObject();
        dummy.name = "background";
        dummy.SetActive(true);
        var rendererD = dummy.AddComponent<SpriteRenderer>();
        rendererD.sprite = backgroundSprite;
        rendererD.color = Color.black;
        rendererD.material = material;
        rendererD.material.color = new Color(rendererD.material.color.r, rendererD.material.color.g, rendererD.material.color.b, (float)0.5);
        rendererD.enabled = true;
        dummy.transform.SetParent(gameObject.transform);
        dummy.transform.position = Camera.main.transform.position + Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
        dummy.transform.position += Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
        var pos = dummy.transform.position;
        pos.z = 0;
        dummy.transform.position = pos;
        dummy.transform.localScale = new Vector3(1, backgroundYScale, 1);
        return dummy;
    }
}
