using UnityEngine;

public class RollbackCardMove
{
    private float elapsed;

    public void ApplyRollbackMove(Card card, Vector3 targetPos)
    {
        elapsed += Time.deltaTime;
        var t = elapsed / DURATION;

        if (t < 1f)
        {
            // TODO: use AnimationCurve
            card.transform.position = Vector3.Lerp(card.transform.position, targetPos, t);
        }
        else
        {
            card.transform.position = targetPos;
            card.EndAnimation();
        }
    }

    private const float DURATION = 0.2f;
}
