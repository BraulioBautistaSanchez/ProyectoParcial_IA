using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Flee : Steering
{
    public float panicRadius = 6f;  

    protected override Vector3 GetSteering()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;

        float dist = Vector3.Distance(transform.position, mouse);
        if (dist > panicRadius)
            return -velocity;       

        return Flee(mouse);
    }
}
