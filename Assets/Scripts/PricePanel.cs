using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PricePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private float _price;
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _objecPoint;
    [SerializeField] private GameObject _label;
    [SerializeField] private GameObject _hand;
    [SerializeField] private Image _slider;


    private BoxCollider _collider;
    private float _duration = 1f;


    private void Start()
    {
        _collider = GetComponent<BoxCollider>();

        SetStartPrice();
        MoveHand();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            FillingSlider();
        }
    }

    private void SetStartPrice()
    {
        _priceText.text = "$" + _price;
    }

    private void MoveHand()
    {
        if (_hand == null)
            return;

        _hand.transform.DOMoveZ(_hand.transform.position.z + 1f, _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void SpawnObject()
    {
        if (_object == null)
            return;

        Instantiate(_object, _objecPoint.position, Quaternion.identity, transform);
        
        DeactivateThis();
    }

    private void FillingSlider()
    {
        _slider.DOFillAmount(1, _duration).SetAutoKill(true).SetEase(Ease.Linear);
        StartCoroutine(PriceChange());
    }

    private void DeactivateThis()
    {
        _label.SetActive(false);
        _collider.enabled = false;

    }

    private IEnumerator PriceChange()
    {
        float elepsedTime = 0f;
        float initialValue = _price;
        
        while (elepsedTime < _duration)
        {
            _price = Mathf.Lerp(initialValue, 0, elepsedTime / _duration);
            _priceText.text = $"{(int) _price}";
            elepsedTime += Time.deltaTime;

            yield return null;
        }
        
        SpawnObject();
    }
}