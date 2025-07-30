using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] private GameObject orderCanvas;
    [SerializeField] private TMP_Text orderText;

    private void Start()
    {
        orderCanvas.SetActive(false);
    }

    private void OnMouseDown()
    {
        UnitSelectionManager.Instance.ToggleUnit(this);
    }

    public void SetOrderNumber(int number)
    {
        if (number <= 0)
        {
            orderCanvas.SetActive(false);
        }
        else
        {
            orderCanvas.SetActive(true);
            orderText.text = number.ToString();
        }
    }

    public void ClearOrderNumber()
    {
        orderText.text = "";
    }

    public void DeactivateOrderCanvas()
    {
        ClearOrderNumber();
        orderCanvas.SetActive(false);
    }
}
