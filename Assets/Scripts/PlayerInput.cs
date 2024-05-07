using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the input handling for player actions, particularly aiming and attacking.
public class PlayerInput : MonoBehaviour
{
    public Transform firePointAngle; // Transform to control the direction the weapon fires towards.
    private Weapon currentWeapon; // Currently equipped weapon.
    private LoadCharacter loadCharacter; // Component to load character data.
    [SerializeField] 
    private float attackSpeed;
    private float timeSinceLastAttack = 10f; // Time elapsed since the last attack.
    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>(); // Retrieves the Weapon component from the child GameObject.
        loadCharacter = GetComponent<LoadCharacter>(); // Assuming LoadCharacter is on the same GameObject.
        if (loadCharacter != null)
        {
            attackSpeed = loadCharacter.GetAttackSpeed(); // Set attack speed based on character data.
        }
    }

    void Update()
    {
        AimAtMouse(); // Update aiming direction each frame.
        timeSinceLastAttack += Time.deltaTime; // Increment the time since last attack by the time elapsed since last frame.

        // Check if Fire1 is being held and if enough time has passed since the last attack
        if (Input.GetButton("Fire1") && timeSinceLastAttack >= 1f / attackSpeed)
        {
            if (currentWeapon != null)
            {
                currentWeapon.Attack(); // Trigger attack from the weapon.
                timeSinceLastAttack = 0f; // Reset the time since last attack.
            }
        }
    }

    // Updates the firePointAngle to aim towards the mouse position.
    void AimAtMouse()
    {
        Vector3 mousePosition = GetMouseWorldPosition(); // Get mouse position in world coordinates.
        Vector3 aimDirection = (mousePosition - transform.position).normalized; // Calculate direction to aim.
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; // Convert direction to angle.

        firePointAngle.eulerAngles = new Vector3(0, 0, angle); // Set the rotation of the fire point.
    }

    // Returns the mouse position in world space, accounting for the camera's projection.
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition; // Get the current mouse screen position.
        screenPosition.z = Camera.main.nearClipPlane; // Set Z to the near clip plane of the camera.
        return Camera.main.ScreenToWorldPoint(screenPosition); // Convert to world position.
    }

    public void ApplyAttackSpeedBoost(float multiplier, float duration)
    {
        StartCoroutine(BoostAttackSpeed(multiplier, duration));
    }

    private IEnumerator BoostAttackSpeed(float multiplier, float duration)
    {
        attackSpeed *= multiplier; // Increase the attack speed
        yield return new WaitForSeconds(duration); // Wait for the duration of the boost
        attackSpeed /= multiplier; // Revert the attack speed back to normal
    }

}
