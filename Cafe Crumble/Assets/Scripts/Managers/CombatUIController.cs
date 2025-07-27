using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatUIController : MonoBehaviour
{
    [SerializeField] private Button returnToCafeButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        returnToCafeButton.onClick.AddListener(ReturnToCafe);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReturnToCafe()
    {
        UnityEngine.Debug.Log("Return to cafe button pressed");
        SceneManager.LoadScene("CafeScene");
    }
}
