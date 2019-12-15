using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float cameraRotateSpeed = 2;
    [SerializeField] private float cameraMoveSpeed = 50;
    private new Camera camera;
    private Transform cameraTransform;
    private readonly Vector3 xy = new Vector3(1, 1);

    private void Awake()
    {
        camera = Camera.main;
        Debug.Assert(camera != null, nameof(camera) + " != null");
        cameraTransform = camera.transform;

        Cursor.visible = false;
    }

    private void Update()
    {
        float deltaTimeScale = Input.GetAxis("TimeScale") * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale + deltaTimeScale, 0, Mathf.Infinity);
        if (Input.GetButtonDown("TimeScaleReset"))
        {
            Time.timeScale = 1;
        }

        float x = Input.GetAxis("Mouse X");
        float y = -Input.GetAxis("Mouse Y");
        cameraTransform.Rotate(Vector3.up, x * cameraRotateSpeed);
        cameraTransform.Rotate(Vector3.right, y * cameraRotateSpeed);
        cameraTransform.rotation = Quaternion.Euler(Vector3.Scale(xy, cameraTransform.rotation.eulerAngles));

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        cameraTransform.Translate(cameraTransform.forward * (vertical * Time.deltaTime * cameraMoveSpeed), Space.World);
        cameraTransform.Translate(cameraTransform.right * (horizontal * Time.deltaTime * cameraMoveSpeed), Space.World);
    }
}
