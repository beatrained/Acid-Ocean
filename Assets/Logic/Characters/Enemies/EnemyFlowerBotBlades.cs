using UnityEngine;

public class EnemyFlowerBotBlades : MonoBehaviour
{
    private Animator _bladesAnimator;
    public bool SpinBlades { get; set; }
    [SerializeField] float _spinningSpeed = 20;


    private void Awake()
    {
        _bladesAnimator = GetComponent<Animator>();
    }

    public void OpenOrCloseBlades(bool isOpened)
    {
        if (isOpened)
        {
            _bladesAnimator.SetBool("Release Blades", true);
        }
        else if (!isOpened)
        {
            _bladesAnimator.SetBool("Release Blades", false);
        }
    }

    private void Update()
    {
        if (SpinBlades)
        {
            transform.Rotate(Vector3.up, 45  * Time.deltaTime * _spinningSpeed);
        }
    }
}