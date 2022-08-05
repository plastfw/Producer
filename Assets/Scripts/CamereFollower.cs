using UnityEngine;

public class CamereFollower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = _player.transform.position + _offset;
    }
}
