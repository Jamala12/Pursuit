using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Transform firePointAngle;
    private Weapon currentWeapon;
    private LoadCharacter loadCharacter;
    private float attackSpeed;
    private float timeSinceLastAttack = 10f;

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        loadCharacter = GetComponent<LoadCharacter>(); // Assuming LoadCharacter is on the same GameObject
        if (loadCharacter != null)
        {
            attackSpeed = loadCharacter.GetAttackSpeed();
        }
    }

    void Update()
    {
        AimAtMouse();
        timeSinceLastAttack += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && timeSinceLastAttack >= 1f / attackSpeed)
        {
            if (currentWeapon != null)
            {
                currentWeapon.Attack();
                timeSinceLastAttack = 0f;
            }
        }
    }

    void AimAtMouse()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized; // Normalized to get the direction vector
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Aim fire point at mouse without flipping the player
        firePointAngle.eulerAngles = new Vector3(0, 0, angle);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane; // Use the camera's near clip plane to ensure correct conversion
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
}
