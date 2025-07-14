using UnityEngine;

public class HeroSystem : MonoBehaviour
{
    [field: SerializeField]
    public HeroView HeroView {  get; private set; }

    public static HeroSystem Instance;

    public void Setup(HeroDataSO heroData)
    {
        HeroView.Setup(heroData);

    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
