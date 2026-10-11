using UnityEngine;

public class Seguir : MonoBehaviour
{
    public Transform target;

    public float maxSpeed = 5f;
    public float maxForce = 0.1f;
    public float mass = 1f;
    public float slowingR = 3f;

    private Vector3 velocity;

    void Update()
    {
        if (target == null) return;

        // 1. Calcular la direccion hacia el objetivo
        Vector3 direction = target.position - transform.position;

        float distance = direction.magnitude;

        // 2. Normalizar la direccion
        direction.Normalize();

        // 3. Calcular la velocidad deseada
        float targetSpeed = maxSpeed;

        if (distance < slowingR)
        {
            targetSpeed = maxSpeed * (distance / slowingR);
        }

        Vector3 desiredVelocity = direction * targetSpeed;

        // 4. Calcular el steering
        Vector3 steering = desiredVelocity - velocity;

        // 5. Limitar la fuerza del steering
        steering = Vector3.ClampMagnitude(steering, maxForce);

        // 6. Aplicar la masa
        steering /= mass;

        // 7. Actualizar y limitar la velocidad
        velocity += steering;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // 8. Moverse
        transform.position += velocity * Time.deltaTime;
    }
}