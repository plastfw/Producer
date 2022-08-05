using DG.Tweening;
using UnityEngine;

public class Cash : MonoBehaviour
{
    private Vector3 _targetScale = new Vector3(1.3f, 1.3f, 1.3f);
    private Vector3 _startRotation = new Vector3(0, 90, 0);
    private float _duration = 0.5f;
    
    private void Start()
    {
        transform.rotation = Quaternion.Euler(_startRotation);
        ChangeScale();
    }

    private void ChangeScale()
    {
        transform.DOScale(_targetScale, _duration);
    }
}
