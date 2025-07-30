using UnityEngine;

public class UnitHoverUI : MonoBehaviour
{
    public Canvas infoCanvas;

    void Start()
    {
        if (infoCanvas != null)
            infoCanvas.gameObject.SetActive(false);
    }

    void OnMouseEnter()
    {
        if (infoCanvas != null)
            infoCanvas.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        if (infoCanvas != null)
            infoCanvas.gameObject.SetActive(false);
    }
}
