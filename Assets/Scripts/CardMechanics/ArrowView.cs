using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowHead;

    [SerializeField]
    private SpriteRenderer arrow;

    private Vector3 startPosition;

    private void Update()
    {
        Vector3 endPosition = MouseUtils.GetMousePositionInWorldSpace();
        Vector3 direction = (endPosition - startPosition).normalized;

        arrowHead.transform.right = direction;
        float distance = Vector3.Distance(startPosition, endPosition);
        float quantizedDistance = Mathf.Floor(distance / 2f) * 2f;
        arrow.size = new Vector2(quantizedDistance, arrow.size.y);

        arrowHead.transform.position = endPosition;
    }

    public void SetupArrow(Vector3 startPosition)
    {
        this.startPosition = startPosition;

    }
}
