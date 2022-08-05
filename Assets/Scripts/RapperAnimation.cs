using UnityEngine;

public class RapperAnimation : MonoBehaviour
{
    private const string Rapping = "Rapping";
    
    [SerializeField] private RapperStand _rapperStand;
    [SerializeField] private ParticleSystem _waitingEffect;
    [SerializeField] private ParticleSystem _musicEffect;
    
    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _rapperStand.HaveCassettes += ChangeAnimation;
        ChangeAnimation(false);
    }

    private void OnDisable()
    {
        _rapperStand.HaveCassettes -= ChangeAnimation;
    }

    private void ChangeAnimation(bool isRapping)
    {
        if (isRapping)
        {
            _waitingEffect.Stop();
            _musicEffect.Play();
        }
        else
        {
            _musicEffect.Stop();
            _waitingEffect.Play();
        }
        
        _animator.SetBool(Rapping, isRapping);
    }
}