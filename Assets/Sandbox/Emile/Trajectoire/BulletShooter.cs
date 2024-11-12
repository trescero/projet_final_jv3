using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;  // Bullet prefab to shoot
    public Transform target;         // Target the bullet will shoot at
    public float shootingSpeed = 20f; // Speed at which the bullet travels
    public float gravity = -9.81f;   // Gravity affecting the projectile
    public float fireRate = 1f;      // Time in seconds between each shot (rate of fire)

    private float nextFireTime = 0f; // Time when we are allowed to fire again

    void Update()
    {
        // Check if enough time has passed since the last shot
        if (Time.time >= nextFireTime)
        {
            if (target != null && bulletPrefab != null)
            {
                ShootAtTarget();
                nextFireTime = Time.time + fireRate;  // Set next available fire time
            }
        }
    }

    // Method to calculate the required velocity and shoot the bullet
    void ShootAtTarget()
    {
        // Calculate direction from the shooter to the target
        Vector3 targetPosition = target.position;
        Vector3 startPosition = transform.position;

        // Calculate the vector from the shooter to the target
        Vector3 direction = targetPosition - startPosition;

        // Adjust direction to ignore horizontal distance (XZ plane)
        float horizontalDistance = new Vector3(direction.x, 0, direction.z).magnitude;
        float verticalDistance = direction.y;

        // Log values for debugging
        Debug.Log($"horizontalDistance: {horizontalDistance}, verticalDistance: {verticalDistance}");

        // Check if horizontalDistance is too small (avoid divide by zero or invalid calculations)
        if (horizontalDistance <= 0f)
        {
            Debug.LogError("Invalid horizontal distance: " + horizontalDistance);
            return; // Prevent further calculations if the distance is zero or invalid
        }

        // Calculate the angle required to reach the target
        float angle = Mathf.Atan2(verticalDistance, horizontalDistance);

        // Log angle for debugging
        Debug.Log($"Calculated angle: {angle}");

        // Ensure the angle is within a reasonable range (between 0 and 90 degrees)
        if (angle <= 0 || angle >= Mathf.PI / 2)
        {
            Debug.LogError("Invalid angle: " + angle);
            return; // Angle should be between 0 and 90 degrees for valid projectile motion
        }

        // Calculate sin(2 * angle)
        float sin2Angle = Mathf.Sin(2 * angle);

        // Log sin(2 * angle) for debugging
        Debug.Log($"sin(2 * angle): {sin2Angle}");

        // Check if sin(2 * angle) is valid
        if (sin2Angle <= 0f)
        {
            Debug.LogError("Invalid sin(2 * angle): " + sin2Angle);
            return; // Prevent invalid speed calculation due to angle
        }

        // Calculate the initial velocity needed to hit the target (based on gravity)
        float speed = Mathf.Sqrt(horizontalDistance * gravity / sin2Angle);

        // Log the speed for debugging
        Debug.Log($"Calculated speed: {speed}");

        // Check if speed is valid
        if (float.IsNaN(speed) || speed <= 0)
        {
            Debug.LogError("Invalid speed calculation: " + speed);
            return; // Prevent invalid velocity assignment
        }

        // Use the shootingSpeed variable to control how fast the bullet travels
        Vector3 launchVelocity = direction.normalized * speed * shootingSpeed;

        // Log launch velocity for debugging
        Debug.Log($"Launch velocity: {launchVelocity}");

        // Ensure launchVelocity is a valid number
        if (float.IsNaN(launchVelocity.x) || float.IsNaN(launchVelocity.y) || float.IsNaN(launchVelocity.z))
        {
            Debug.LogError("Invalid launch velocity: " + launchVelocity);
            return; // Prevent assigning NaN velocity to the Rigidbody
        }

        // Instantiate the bullet prefab at the shooter's position
        GameObject bullet = Instantiate(bulletPrefab, startPosition, Quaternion.identity);

        // Set the bullet's Rigidbody velocity to the calculated launch velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = launchVelocity;
        }
    }
}
