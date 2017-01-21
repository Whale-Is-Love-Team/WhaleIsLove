using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private float scrollSlow;
    public float tileSizeZ;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (scrollSlow != 0)
        {
            float newPosition = Mathf.Repeat(Time.time / scrollSlow, tileSizeZ);
            transform.position = startPosition + Vector3.left * newPosition;
        }
    }
}