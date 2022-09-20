using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private AudioSource _audioSource;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_player._hasCoin == true)
                {
                    _player._hasCoin = false;
                    _audioSource.Play();
                    _player.EnableWeapon();
                }
                else
                {
                    Debug.Log("Get out of here!");
                }
            }
        }
    }
}
