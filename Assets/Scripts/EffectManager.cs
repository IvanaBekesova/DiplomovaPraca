using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public GameObject effectModal;
    public Transform effectsList;

    public GameObject fieldOfViewCardPrefab;
    public GameObject centralLossCardPrefab;
    public GameObject colorBlindnessCardPrefab;
    public GameObject tunnelVisionCardPrefab;

    public Button fieldOfViewButton;
    public Button centralLossButton;
    public Button colorBlindnessButton;
    public Button tunnelVisionButton;

    private GameObject fieldOfViewCard;
    private GameObject centralLossCard;
    private GameObject colorBlindnessCard;
    private GameObject tunnelVisionCard;

    public void OpenModal()
    {
        effectModal.SetActive(true);

        if (fieldOfViewButton != null)
            fieldOfViewButton.interactable = fieldOfViewCard == null;
        if (centralLossButton != null)
            centralLossButton.interactable = centralLossCard == null;
        if (colorBlindnessButton != null)
            colorBlindnessButton.interactable = colorBlindnessCard == null;
        if (tunnelVisionButton != null)
            tunnelVisionButton.interactable = tunnelVisionCard == null;
    }

    public void CloseModal()
    {
        effectModal.SetActive(false);
    }

    public void AddFieldOfViewEffect()
    {
        CloseModal();
        if (fieldOfViewCard != null) return;
        fieldOfViewCard = Instantiate(fieldOfViewCardPrefab, effectsList);
    }

    public void AddCentralLossEffect()
    {
        CloseModal();
        if (centralLossCard != null) return;
        centralLossCard = Instantiate(centralLossCardPrefab, effectsList);
    }

    public void AddColorBlindnessEffect()
    {
        CloseModal();
        if (colorBlindnessCard != null) return;
        colorBlindnessCard = Instantiate(colorBlindnessCardPrefab, effectsList);
    }

    public void AddTunnelVisionEffect()
    {
        CloseModal();
        if (tunnelVisionCard != null) return;
        tunnelVisionCard = Instantiate(tunnelVisionCardPrefab, effectsList);
    }
}