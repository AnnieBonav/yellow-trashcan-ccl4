using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Ingredient"))
        {
            print("An ingredient fell, trigger. Will Play sound.");
            SoundEmitter ingredientSound = collider.transform.GetComponentInParent<SoundEmitter>();
            ingredientSound.PlaySound();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ingredient"))
        {
            print("An ingredient fell, collider. Should Play sound.");
            // Destroy(collision.gameObject);
        }
    }
}
