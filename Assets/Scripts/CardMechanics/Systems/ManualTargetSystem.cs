using UnityEngine;

public class ManualTargetSystem : MonoBehaviour
{
    public static ManualTargetSystem Instance;

    [SerializeField]
    private ArrowView arrowView;
    [SerializeField]
    private LayerMask targetLayerMask;

    public void StartTargeting(Vector3 startPosition)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.SetupArrow(startPosition);
    }

    public EnemyView EndTargeting(Vector3 endPosition)
    {
        arrowView.gameObject.SetActive(false);

        RaycastHit[] hits = Physics.RaycastAll(endPosition, Vector3.forward, 100f, targetLayerMask);
        foreach (RaycastHit hit in hits)
        {
            Debug.Log("I hit" + hit.collider);
            if (hit.collider.gameObject != this.gameObject &&
                hit.collider != null &&
                hit.transform.TryGetComponent<EnemyView>(out EnemyView enemyView))
            {
                return enemyView;
            }
        }
        return null;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
