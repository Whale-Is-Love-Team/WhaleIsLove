using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHeart : MonoBehaviour {

    [SerializeField]
    protected Sprite defaultSprite;
    [SerializeField]
    protected Sprite emptySprite;

    public bool isEmpty = false;

    protected Image _uiImage;

	void Start () {
        _uiImage = GetComponent<Image>();
	}
	
	void Update () {
        if (isEmpty && _uiImage.sprite == defaultSprite)
            _uiImage.sprite = emptySprite;
        else if (!isEmpty && _uiImage.sprite == emptySprite)
            _uiImage.sprite = defaultSprite;
	}
}
