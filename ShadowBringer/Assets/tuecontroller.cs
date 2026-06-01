using UnityEngine;
using UnityEngine.InputSystem;

public class tueController : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform firePoint;

    public float bulletSpeed = 10f;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {

    }
}