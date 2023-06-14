using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        print("Something fell");
        if (collider.CompareTag("Ingredient"))
        {
            print("An ingredient fell");
            Destroy(collider.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Something fell");
        if (collision.gameObject.CompareTag("Ingredient"))
        {
            print("An ingredient fell");
            Destroy(collision.gameObject);
        }
    }
}
