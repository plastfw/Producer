using DG.Tweening;
using UnityEngine;

public class Cassette : MonoBehaviour
{
    private Sequence _moveAnimation;
    private float _rotationDuration = 0.01f;
    private float _duration = 0.05f;
    private float _punchDuration = 0.5f;
    private Vector3 _targetRotation = Vector3.zero;

    public void SetPosition(Transform target,Vector3 targetPosition, Vector3 punch, Vector3 _startOffset, float moveDuration)
    {
        _moveAnimation = DOTween.Sequence();

        _moveAnimation
            .Append(transform.DOLookAt(target.position/2,_rotationDuration,up:Vector3.back))
            .Append(transform.DOLocalMove(_startOffset + targetPosition, moveDuration).SetEase(Ease.Flash))
            .Append(transform.DOLocalRotate(_targetRotation, _rotationDuration))
            .Append(transform.DOLocalMove(targetPosition + punch, _duration))
            .Append(transform.DOLocalMove(targetPosition, _punchDuration));
    }
}