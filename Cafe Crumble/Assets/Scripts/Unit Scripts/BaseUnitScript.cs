using UnityEngine;
using TMPro;
using System.Diagnostics;

public class BaseUnitScript : MonoBehaviour
{
    public int baseHealthPoints = 5;
    public int baseAttackDamage = 5;

    public int currentHealthPoints;
    public int currentAttackDamage;

    private int healthGrowth = 1;
    private int attackGrowth = 2;

    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private TMP_Text attackText;

    public int linePosition;


    public UnitLevel level;

    public enum UnitLevel
    { 
        Level1 = 1,
        Level2 = 2,
        Level3 = 3
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentLevel = (int)level;
        currentHealthPoints = baseHealthPoints + ((currentLevel - 1) * healthGrowth);
        currentAttackDamage = baseAttackDamage + ((currentLevel - 1) * attackGrowth);

        UpdateHealthValue(0);
        UpdateAttackValue(0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    // Update this unit's HP (taking damage, being increased/healed)
    public void UpdateHealthValue(int change)
    {
        currentHealthPoints += change;
        healthText.text = "" + currentHealthPoints;
    }

    // Update this unit's ATK (stat increases/decreases)
    public void UpdateAttackValue(int change)
    {
        currentAttackDamage += change;
        attackText.text = "" + currentAttackDamage;
    }

}
