using UnityEngine;

public class ObstacleAvoidance2D : MonoBehaviour
{
    public LayerMask obstacleLayer;    // asignar la capa Obstacle
    public float lookAhead = 2f;       // qué tan lejos "ve" el agente
    public float agentRadius = 0.4f;   // grosor del rayo (≈ tamaño del agente)
    public float avoidForce = 20f;     // debe ser >= maxForce para que gane al seek/flee

    public Vector3 GetAvoidance(Vector3 velocity)
    {
        // Dirección de movimiento (si está quieto, usa hacia donde mira)
        Vector2 dir = velocity.sqrMagnitude > 0.01f ? (Vector2)velocity.normalized : (Vector2)transform.up;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, agentRadius, dir, lookAhead, obstacleLayer);
        if (hit.collider == null) return Vector3.zero;

        // ¿El obstáculo queda a la izquierda o a la derecha? Esquivamos hacia el lado contrario.
        Vector2 toObstacle = (Vector2)hit.collider.bounds.center - (Vector2)transform.position;
        float cross = dir.x * toObstacle.y - dir.y * toObstacle.x;
        Vector2 perp = new Vector2(-dir.y, dir.x);          // 90° a la izquierda
        Vector2 side = cross > 0f ? -perp : perp;

        // Un poco de empuje contra la normal para alejarse de la superficie
        Vector2 avoid = (side + hit.normal * 0.5f).normalized;
        return (Vector3)(avoid * avoidForce);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * lookAhead);
        Gizmos.DrawWireSphere(transform.position + transform.up * lookAhead, agentRadius);
    }
}