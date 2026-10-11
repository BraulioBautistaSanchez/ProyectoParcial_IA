using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Wander : Steering
{
    public float circleDistance = 2f;   // qué tan lejos está el círculo
    public float circleRadius = 1f;     // tamaño del círculo (más grande = giros más fuertes)
    public float angleChange = 4f;      // cuánto puede variar el ángulo por segundo (radianes)

    float wanderAngle;

    void Start()
    {
        wanderAngle = Random.Range(0f, Mathf.PI * 2f);
    }

    protected override Vector3 GetSteering()
    {
        // 1) Centro del círculo: delante del agente
        Vector3 forward = velocity.sqrMagnitude > 0.01f ? velocity.normalized : transform.up;
        Vector3 circleCenter = transform.position + forward * circleDistance;

        // 2) Mover un poco el ángulo al azar (así el merodeo es suave, no errático)
        wanderAngle += Random.Range(-1f, 1f) * angleChange * Time.deltaTime;

        // 3) Punto sobre el borde del círculo
        Vector3 displacement = new Vector3(Mathf.Cos(wanderAngle), Mathf.Sin(wanderAngle), 0f) * circleRadius;

        // 4) Ir hacia ese punto
        return Seek(circleCenter + displacement);
    }
}