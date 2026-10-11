using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Pursuit : Steering
{
    public Steering target;      // arrastrar aquí al Agente 5
    public float maxPrediction = 2f;    // límite de segundos hacia el futuro

    protected override Vector3 GetSteering()
    {
        if (target == null) return -velocity;

        float dist = Vector3.Distance(transform.position, target.transform.position);
        float t = Mathf.Min(dist / maxSpeed, maxPrediction);

        Vector3 futurePos = target.transform.position + target.Velocity * t;
        return Seek(futurePos);
    }
}
