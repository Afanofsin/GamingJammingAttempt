using UnityEngine;

public class PileCardView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _image;
    [SerializeField]
    private Animator _animator;

    public void TurnOffAnimation()
    {
        _animator.enabled = false;
    }

    public void TurnOnAnimation()
    {
        _animator.enabled = true;
    }

    public void MakeSpriteOnTop()
    {
        _image.sortingOrder = 10;
    }

    public void MakeSpriteOnBottom() 
    {
        _image.sortingOrder = 5;
    }
}
