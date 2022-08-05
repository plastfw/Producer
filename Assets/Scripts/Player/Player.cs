using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float YOffset = 8;
    
    [SerializeField] private Transform _cassettePoint;
    [SerializeField] private List<Cassette> _cassettesInHands;
    
    public Transform CassettePoint => _cassettePoint;
    private Vector3 _offset = new Vector3(0,0,0);
    private Vector3 _startOffset = new Vector3(0, 0, -30f);
    private Vector3 _punch = new Vector3(0, 5, 5);
    private float _cassetteSpeed = 0.07f;
    private Coroutine _give;

    public event Action<bool> IsCarry;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out RapperStand rapperStand))
        {
            if (_cassettesInHands.Count == 0) 
                return;
            
            if(_give!=null)
                StopCoroutine(_give);

            _give = StartCoroutine(GiveСassettes(rapperStand));
        }
    }

    public void GetCassette(Cassette cassette)
    {
        IsCarry?.Invoke(true);

        _cassettesInHands.Add(cassette);

        cassette.SetPosition(_cassettePoint,_offset,_punch,_startOffset,_cassetteSpeed);
        _offset.y += YOffset;
    }

    private IEnumerator GiveСassettes(RapperStand rapperStand)
    {
        var delay = new WaitForSeconds(0.25f);

        for (int i = _cassettesInHands.Count-1; i >= 0; i--)
        {
            _cassettesInHands[i].transform.SetParent(rapperStand.CassettePoint.transform);
            rapperStand.GetCassette(_cassettesInHands[i]);
            yield return delay;
        }

        _cassettesInHands.Clear();
        IsCarry?.Invoke(false);
        _offset.y = 0;
    }
}