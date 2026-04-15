using UnityEngine;

public class RotationTranslationAnimation : MonoBehaviour
{

    private enum RotationAxis {X,Y,Z};

    [SerializeField]
    private RotationAxis rotationAxis;

    [SerializeField]
    [Range(-5f, 5f)]
    private float rotationSpeed;
    [SerializeField]
    [Range(0f, 50f)]
    private float upDownAmplitude;
    
    [SerializeField]
    [Range(0f, 5f)]
    private float upDownFrequency;

    float radIncrement;
    float rad=0;
    private float initialX,initialY,initialZ;

    void Start()
    {
        radIncrement = 2 * Mathf.PI*upDownFrequency;
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();       
         
    }

    private void Rotation()
    {
        switch (rotationAxis)
        {
            case RotationAxis.X:
                transform.Rotate(rotationSpeed * Vector3.right);
                break;
            case RotationAxis.Y:
                transform.Rotate(rotationSpeed * Vector3.up);
                break;
            case RotationAxis.Z:
                transform.Rotate(rotationSpeed * Vector3.forward);
                break;
        }
    }

    private void Movement()
    {
#if UNITY_EDITOR
        radIncrement = 2 * Mathf.PI * upDownFrequency;
#endif
        transform.position = new Vector3(transform.position.x, initialY + upDownAmplitude * Mathf.Sin(rad), transform.position.z);

        rad += radIncrement * Time.fixedDeltaTime;
        if (rad > 2 * Mathf.PI)
        {
            rad = 0;
        }
    }

}
