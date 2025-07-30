using UnityEngine;

public class EnemyUnitController : BaseUnitController
{
    private int enemyUnitXOffsetStart = 2;
    private int enemyUnitXOffsetCombat = 2;

    public override void PlaceUnitsStart()
    {
        foreach (Transform child in transform)
        {
            // flipping the sprite horizontally
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.flipX = true;
            }

            child.transform.position = new Vector3(enemyUnitXOffsetStart, transform.position.y, -1);
            enemyUnitXOffsetStart += 2;
        }
        enemyUnitXOffsetStart = 2;
    }

    public override void PlaceUnitsCombat()
    {
        foreach (Transform child in transform)
        {
            int currentLinePos = child.GetComponent<BaseUnitScript>().linePosition;
            child.transform.position = new Vector3(enemyUnitXOffsetCombat * currentLinePos, transform.position.y, -1);
            
        }
        enemyUnitXOffsetCombat = 2;
    }
}
