using UnityEngine;

public abstract class Steering : MonoBehaviour
{
   
    public float maxSpeed = 4f;    // velocidad máxima
    public float maxForce = 10f;   // qué tan rápido puede cambiar de dirección

    protected Vector3 velocity;
    public Vector3 Velocity => velocity;   

    ObstacleAvoidance2D avoidance;

    void Awake()
    {
        avoidance = GetComponent<ObstacleAvoidance2D>();   // opcional
    }

    // Cada comportamiento devuelve su fuerza de steering.
    protected abstract Vector3 GetSteering();

    void Update()
    {
        Vector3 steering = GetSteering();

        // Se suma la fuerza de evasión de obstáculos (si el agente tiene el componente)
        if (avoidance != null)
            steering += avoidance.GetAvoidance(velocity);

        steering.z = 0f;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        velocity = Vector3.ClampMagnitude(velocity + steering * Time.deltaTime, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        // Rotar el sprite hacia donde se mueve (el sprite debe mirar hacia ARRIBA)
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    // ---- Helpers ----
    protected Vector3 Seek(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        toTarget.z = 0f;
        Vector3 desired = toTarget.normalized * maxSpeed;
        return desired - velocity;
    }

    protected Vector3 Flee(Vector3 threat)
    {
        Vector3 away = transform.position - threat;
        away.z = 0f;
        Vector3 desired = away.normalized * maxSpeed;
        return desired - velocity;
    }
}