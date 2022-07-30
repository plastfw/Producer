using DG.Tweening;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float _punchDuration = 0.1f;
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private Vector3 _desiredSize;
    [SerializeField] private Vector3 _punchPower;

    private Vector3 _startSize = new Vector3(0f, 0f, 0f);
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
