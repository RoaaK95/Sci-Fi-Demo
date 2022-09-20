using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzleFlashVFX;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private int _currentAmmu;
    private int _maxAmmu = 50;
    private bool _isReloading = false;
    private UIManager _uiManager;
    public bool _hasCoin = false;
    [SerializeField]
    private GameObject _weapon;
    private bool _hasWeapon = false;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _currentAmmu = _maxAmmu;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && _currentAmmu > 0 && _hasWeapon == true)
        {
            Shoot();
        }
        else
        {
            _muzzleFlashVFX.SetActive(false);
            _weaponAudio.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.R) & _isReloading == false)
        {
            _isReloading = true;
            StartCoroutine(ReloadAmmuntion());
        }
        CheckCoin();
    }

    void CalculateMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        velocity = transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void Shoot()
    {
        _muzzleFlashVFX.SetActive(true);
        if (_weaponAudio.isPlaying == false)
        {
            _weaponAudio.Play();
        }
        _currentAmmu--;
        _uiManager.UpdateAmmo(_currentAmmu);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
            GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitMarker, 2f);
            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    IEnumerator ReloadAmmuntion()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmu = _maxAmmu;
        _uiManager.UpdateAmmo(_currentAmmu);
        _isReloading = false;
    }

    private void CheckCoin()
    {
        if (_hasCoin == true)
        {
            _uiManager.CollectedCoin();
        }
        else
        {
            _uiManager.RemoveCoin();
        }

    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
        _hasWeapon = true;
    }
}
