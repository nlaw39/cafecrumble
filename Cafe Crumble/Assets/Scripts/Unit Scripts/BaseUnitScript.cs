using UnityEngine;
using TMPro;
using System.Diagnostics;

public class BaseUnitScript : MonoBehaviour
{
    public int baseHealthPoints = 5;
    public int baseAttackDamage = 2;

    public int currentHealthPoints;
    public int currentAttackDamage;

    public TMP_Text healthText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.Debug.Log("I'm starting");
        currentHealthPoints = baseHealthPoints;
        currentAttackDamage = baseAttackDamage;

        UnityEngine.Debug.Log("Trying to get Health Text Box");
        healthText = GetComponent<TMP_Text>();
        if (healthText != null)
        {
            UnityEngine.Debug.Log("Successfully got the text box");
        }
        UpdateHealth();
    }

    void Initialize()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealth()
    {
        healthText.text = "" + currentHealthPoints;
    }
}
