using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the input handling for player actions, particularly aiming and attacking.
public class PlayerInput : MonoBehaviour
{
    public Transform firePointAngle; // Transform to control the direction the weapon fires towards.
    private Weapon currentWeapon; // Currently equipped weapon.
    private LoadCharacter loadCharacter; // Component to load character data.
    private float attackSpeed; // Attack speed derived from character data.
    private float timeSinceLastAttack = 10f; // Time elapsed since the last attack.

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

        if (Input.GetButtonDown("Fire1") && timeSinceLastAttack >= 1f / attackSpeed)
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
}
