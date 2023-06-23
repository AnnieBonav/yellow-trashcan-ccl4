using System.Collections;
using UnityEngine;

public enum SoundMaterialType { Metal, Glass, Solid }
public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private string soundEventName;

    [Header("Complete if it needs to sound on drop")]
    [SerializeField] private bool soundsOnDrop;
    [SerializeField] private SoundMaterialType soundMaterialType;

    private float cooloffPeriod = 2f;
    private bool canSound = false;

    private void Awake()
    {
        StartCoroutine(CoolOff()); // so sounds of stuff that is on the ground do not sound when the game begins
    }

    private void Start()
    {
        AkSoundEngine.SetSwitch("Material", soundMaterialType.ToString(), gameObject);
    }

    private IEnumerator CoolOff()
    {
        yield return new WaitForSeconds(cooloffPeriod);
        canSound = true;
    }
    public void PlaySound()
    {
        AkSoundEngine.PostEvent(soundEventName, gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canSound) return;
        if (!soundsOnDrop) return;
        if (collision.transform.CompareTag("Floor"))
        {
            AkSoundEngine.PostEvent("Play_Drop", gameObject);
        }
    }
}
