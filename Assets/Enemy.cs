using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Get dot product between current facing direction and direction to player
        Vector2 dirToPlayer = player.transform.position - transform.position;
        float rotationDot = Vector2.Dot(dirToPlayer.normalized, transform.up.normalized);

        // Get which direction to rotate in
        int mult = 1;
        if (Vector3.Cross(dirToPlayer.normalized, transform.up.normalized).z > 0)
            mult = -1;

        // Rotate to face player
        transform.Rotate(0, 0, Mathf.Acos(rotationDot) * Mathf.Rad2Deg * mult);

        // If too close to player, back away
        if (Vector2.Distance(player.transform.position, transform.position) < 2)
            transform.Translate(0, -0.1f, 0);

        // Otherwise, move perpendicular to facing direction (which in combination with rotating will move in a circle)
        else
            transform.Translate(0.05f, 0, 0);
    }
}
