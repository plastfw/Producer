using DG.Tweening;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float _punchDuration;
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _desiredSize;
    [SerializeField] private Vector3 _punchPower;

    private Vector3 _startSize = Vector3.zero;
    private Sequence _spawnAnimation;
    
    private void Start()
    {
        _spawnAnimation = DOTween.Sequence();
        
        SetStartSize();
        SpawnAnimation();
    }

    private void SetStartSize()
    {
        transform.localScale = _startSize;
    }

    private void SpawnAnimation()
    {
        _spawnAnimation.Append(transform.DOScale(_desiredSize, _duration))
            .Append(transform.DOPunchScale(_punchPower, _punchDuration, 1, 1));
    }
}
