using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreController : MonoBehaviour {


    private int nbCol = 3;
    private int maxNbRow = 7;
    private int[] colX = { -450, 150, 300 };
    private int rowHeight = 175;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void generateList()
    {
        for (int n = 0; n < nbCol; n++)
        {
            int baseX = colX[n];
            GameObject dummy = new GameObject();
            dummy.AddComponent<RectTransform>();
            for (int i = 1; i < )
        }
    }
}
