using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Pursuit : Steering
{
    public Steering target;      
    public float maxPrediction = 2f;   

    protected override Vector3 GetSteering()
    {
        if (target == null) return -velocity;

        float dist = Vector3.Distance(transform.position, target.transform.position);
        float t = Mathf.Min(dist / maxSpeed, maxPrediction);

        Vector3 futurePos = target.transform.position + target.Velocity * t;
        return Seek(futurePos);
    }
}
