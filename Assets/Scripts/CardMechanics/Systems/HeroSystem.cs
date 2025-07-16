using System.Collections;
using UnityEngine;

public class HeroSystem : MonoBehaviour
{
    [field: SerializeField]
    public HeroView HeroView {  get; private set; }

    public static HeroSystem Instance;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddMoraleGA>(AddMoralePerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddMoraleGA>();
    }

    public void Setup(HeroDataSO heroData)
    {
        HeroView.Setup(heroData);

    }

    private IEnumerator AddMoralePerformer(AddMoraleGA addMoraleGA)
    {
        HeroView.AddMorale(addMoraleGA.Amount);
        yield return null;
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
