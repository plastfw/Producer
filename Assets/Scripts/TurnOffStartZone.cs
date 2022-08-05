using UnityEngine;

public class TurnOffStartZone : MonoBehaviour
{

    [SerializeField] private PricePanel _pricePanel;

    private void OnEnable()
    {
        _pricePanel.ConveyorReady += TurnOffThis;
    }

    private void OnDisable()
    {
        _pricePanel.ConveyorReady -= TurnOffThis;
    }

    private void TurnOffThis()
    {
        gameObject.SetActive(false);
    }
}
