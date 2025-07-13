using UnityEngine;
using TMPro;
using System.Diagnostics;

public class BaseUnitScript : MonoBehaviour
{
    public int baseHealthPoints = 5;
    public int baseAttackDamage = 2;

    public int currentHealthPoints;
    public int currentAttackDamage;

    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private TMP_Text attackText;

    public int linePosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealthPoints = baseHealthPoints;
        currentAttackDamage = baseAttackDamage;

        UpdateHealthValue(0);
        UpdateAttackValue(0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    // Update this unit's HP (taking damage, being increased/healed)
    void UpdateHealthValue(int change)
    {
        currentHealthPoints += change;
        healthText.text = "" + currentHealthPoints;
    }

    // Update this unit's ATK (stat increases/decreases)
    void UpdateAttackValue(int change)
    {
        currentAttackDamage += change;
        attackText.text = "" + currentAttackDamage;
    }

}
