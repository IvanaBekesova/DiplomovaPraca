using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject effectModal;
    public GameObject effectCardPrefab;
    public Transform effectsList;

    public void OpenModal()
    {
        effectModal.SetActive(true);
    }

    public void CloseModal()
    {
        effectModal.SetActive(false);
    }

    public void AddFieldOfViewEffect()
    {
        CloseModal();
        Instantiate(effectCardPrefab, effectsList);
    }
}