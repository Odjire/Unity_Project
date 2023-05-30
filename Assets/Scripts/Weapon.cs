using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera PlayerCamera;
    public Transform shootPoint;
    public GameObject bullet;
    public CharacterController1 characterController; // Référence au script CharacterController1

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            Debug.Log("Gotcha!"); // Affiche des données à chaque fois que l'on fait un clic gauche
        }
    }

    void Shoot()
    {
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 target;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;

            // Vérifiez si le raycast a touché l'un des ennemis.
            if (hit.collider.CompareTag("Enemy2") || hit.collider.CompareTag("Enemy3") || hit.collider.CompareTag("Enemy4"))
            {
                // Si l'ennemi est touché, détruisez-le.
                Destroy(hit.collider.gameObject);
                return; // Sortez de la méthode pour éviter la création de la balle
            }
        }
        else
        {
            target = ray.GetPoint(75);
        }

        Vector3 shootDirection = target - shootPoint.position;
        GameObject realBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        realBullet.transform.forward = shootDirection.normalized;
        realBullet.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * 10, ForceMode.Impulse);

        // Appel de la méthode Shoot() du script CharacterController1
        characterController.Shoot();
    }
}
