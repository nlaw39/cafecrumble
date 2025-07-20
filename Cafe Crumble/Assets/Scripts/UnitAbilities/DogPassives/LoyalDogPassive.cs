using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "LoyalDogPassive", menuName = "Scriptable Objects/LoyalDogPassive")]
public class LoyalDogPassive : PassiveAbility
{
    public override void OnCombatEnd(BaseUnitScript self)
    {
        // Check if the unit is still active in the scene
        if (self.gameObject.activeInHierarchy)
        {
            GameObject myTeamController = self.transform.parent.gameObject;
            if (myTeamController.tag == "AllyUnits")
            {
                GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
                GameManager gameManagerScript = gameManagerObject.GetComponent<GameManager>();
                gameManagerScript.changeMoney(1);
                UnityEngine.Debug.Log("Loyal Dog found $1 in the cafe!");
            }
        }
        else
        {
            UnityEngine.Debug.Log("Loyal Dog is not active (likely dead or disabled).");
        }
    }
}
