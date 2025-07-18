using System.Collections.Generic;
using UnityEngine;

public class EnemyViewCreator : MonoBehaviour
{
    public static EnemyViewCreator Instance;

    [SerializeField]
    private EnemyView enemyViewPrefab;

    public EnemyView CreateEnemyView(EnemyDataSO enemyDataSO, Vector3 position, Quaternion rotation)
    {
        EnemyView enemyView = Instantiate(enemyViewPrefab, position, rotation);
        enemyView.Setup(enemyDataSO);
        return enemyView;
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
