using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RapperStand : MonoBehaviour
{
    private const float YOffset = 0.15f;

    [SerializeField] private Transform _cassettePoint;
    [SerializeField] private Transform _rapper;
    [SerializeField] private Cash _cash;
    [SerializeField] private List<Cassette> _cassettesOnFloor;
    [SerializeField] private List<Transform> _cashPoints;

    private Vector3 _offset = Vector3.zero;
    private Vector3 _startOffset = new Vector3(0, 0, -0.3f);
    private Vector3 _punch = new Vector3(0.1f, 0.1f, -0.1f);
    private Vector3 _cassetteDirection;
    private float _cassetteSpeed = 0.3f;
    private int _cashIndex = 0;
    private Tween _cashMover;
    private Tween _changeSize;
    private Coroutine _rapping;
    private Coroutine _move;
    
    public Transform CassettePoint => _cassettePoint;

    public event Action<bool> HaveCassettes;
    
    public void GetCassette(Cassette cassette)
    {
        _cassettesOnFloor.Add(cassette);
        
        cassette.SetPosition(_cassettePoint,_offset, _punch, _startOffset, _cassetteSpeed);
        _offset.y += YOffset;
        
        if (_rapping != null)
            StopCoroutine(_rapping);
        
        _rapping = StartCoroutine(Rapping());
    }

    private void GenerateCash()
    {
        Instantiate(_cash, _cashPoints[_cashIndex].position, Quaternion.identity, transform);
        _cashIndex++;
    }
    
    private IEnumerator Rapping()
    {
        var delay =   new WaitForSeconds(1f);
        yield return delay;
        
        HaveCassettes?.Invoke(true);

        for (int i = _cassettesOnFloor.Count - 1; i >= 0; i--)
        {
            _cassettesOnFloor[_cassettesOnFloor.Count - 1].transform.DOMove(_rapper.position, 1f).SetEase(Ease.Linear);
            _changeSize = _cassettesOnFloor[_cassettesOnFloor.Count - 1].transform.DOScale(Vector3.zero, 1f);
            _cassettesOnFloor.RemoveAt(_cassettesOnFloor.Count-1);
            _offset.y -= YOffset;
            yield return delay;
            
            GenerateCash();

            if (i == 0)
            {
                HaveCassettes?.Invoke(false);
                yield break;
            }
        }
    }
}