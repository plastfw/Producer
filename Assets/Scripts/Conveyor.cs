using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private List<Transform> _cassetePoints;
    [SerializeField] private Cassette _cassette;
    [SerializeField] private Transform _cassetteSpawnPoint;

    private float _generationSpeed = 0.4f;
    private Tween _mover;
    private Coroutine _generator;
    private Coroutine _give;
    private List<Cassette> _spawnedCassette = new List<Cassette>();
    
    private void Start()
    {
        _generator = StartCoroutine(GenerateСassettes());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            if (_give!=null)
                StopCoroutine(_give);
            
            StopCoroutine(_generator);
            _give = StartCoroutine(GiveСassettes(player));
        }
    }

    private void RestartConveyor()
    {
        if (_generator != null)
            StopCoroutine(_generator);

        _generator = StartCoroutine(GenerateСassettes());
    }

    private IEnumerator GiveСassettes(Player player)
    {
        var delay = new WaitForSeconds(0.2f);
        
        foreach (Cassette cassette in _spawnedCassette)
        {
            cassette.transform.SetParent(player.CassettePoint.transform);
            player.GetCassette(cassette);
            yield return delay;
        }
        _spawnedCassette.Clear();
        RestartConveyor();
    }

    private IEnumerator GenerateСassettes()
    {
        for (int i = 0; i < _cassetePoints.Count; i++)
        {
            Cassette cassette = Instantiate(_cassette, _cassetteSpawnPoint.position, Quaternion.identity, transform);
            _mover = cassette.transform.DOMove(_cassetePoints[i].transform.position, _generationSpeed);
            _spawnedCassette.Add(cassette);
            yield return _mover.WaitForCompletion();
        }
    }
}
