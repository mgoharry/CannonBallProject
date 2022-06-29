using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] float rotationSpeed, sensetivityX, sensetivityY;
    [SerializeField] public GameObject CannonHead, CannonBall, Explosion;
    [SerializeField] SceneManagerScript sm;
    [SerializeField] GameObject mainCamera;
    [SerializeField] AudioClip shotSound;
    AudioSource shotAudioSource;

    public float power;
    public Transform Barrel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        shotAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float LookX = Input.GetAxis("Mouse X");
        //LookX = Mathf.Clamp(LookX, -90, 90);
        float LookY = Input.GetAxis("Mouse Y") * sensetivityY * Time.deltaTime;

        Debug.Log(LookY);

        //LookY = Mathf.Clamp(LookY, -140, -70);

        CannonHead.transform.rotation = Quaternion.Euler(CannonHead.transform.rotation.eulerAngles + new Vector3(LookY , 0, 0));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, LookX * sensetivityX * Time.deltaTime, 0));

        if (sm.isGameRunning)
        {
            ShootCannon();
        }
    }

    IEnumerator camShake(float duration, float magnitude)
    {
        Vector3 originalPos = mainCamera.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1f) * magnitude;
            float y = Random.Range(-1, 1f) * magnitude;

            mainCamera.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;  

            yield return null;
        }

        mainCamera.transform.localPosition = originalPos;
    }

    void ShootCannon()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(1))
        {
            GameObject CreatedCannonBall = Instantiate(CannonBall, Barrel.position, Barrel.rotation);

            CreatedCannonBall.GetComponent<Rigidbody>().velocity = Barrel.transform.up * power;

            shotAudioSource.PlayOneShot(shotSound);

            StartCoroutine(camShake(0.15f, 0.4f));

            Destroy(Instantiate(Explosion, Barrel.position, Barrel.rotation), 2);
            Destroy(CreatedCannonBall, 3f);

        }
    }
}
