using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum Frisbee_State { PICKED, THROWN, CALLED }

public class Frisbee : MonoBehaviour
{
    public float speed;
    private float _returningSpeed;
    public float pickForgiveness;
    public float rotationSpeed;

    public float fieldOfViewIncrease;
    public float fielfOfViewTweenTime;
    public Transform playerTransform;

    private Rigidbody _rigidbody;
    private Frisbee_State _frisbeeState;
    private Transform _player;
    private Transform _playerFrisbeePosition;
    private Camera _camera;
    private MouseLook _mouseLook;
    private bool _justPicked = false;
    private bool _sfx = false;

    private SoundController _sounds;
    private AudioSource _bounce;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerFrisbeePosition = _player.Find("FrisbeePositionContainer").GetChild(0).transform;
        _camera = Camera.main;
        _mouseLook = _camera.GetComponent<MouseLook>();
        _frisbeeState = Frisbee_State.THROWN;
        _returningSpeed = speed * 2;
        
        _sounds = GetComponentInChildren<SoundController>();
        _bounce = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_frisbeeState)
        {
            case Frisbee_State.THROWN:
                _sfx = false;
                break;
            case Frisbee_State.CALLED:
                if (Vector3.Distance(this.transform.position, _playerFrisbeePosition.position) <= pickForgiveness)
                {
                    if(!_sfx)
                    {
                        _sfx = true;
                        int rng = UnityEngine.Random.Range(1,5);
                            _sounds.Play("catch" + rng);
                    }

                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.angularVelocity = Vector3.zero;
                    transform.position = Vector3.Lerp(this.transform.position, _playerFrisbeePosition.position,
                        Time.deltaTime * 14);
                    if (Vector3.Distance(this.transform.position, _playerFrisbeePosition.position) <=
                        pickForgiveness / 4)
                    {
                        _frisbeeState = Frisbee_State.PICKED;
                    }

                }
                break;
            case Frisbee_State.PICKED:
                transform.position = _playerFrisbeePosition.position;
                if (!_justPicked)
                {
                    transform.position = _playerFrisbeePosition.position;
                    transform.parent = playerTransform;
                    //transform.position = Vector3.zero;
                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.angularVelocity = Vector3.zero;
                    _justPicked = true;
                }
                else
                {
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, _camera.transform.rotation, Time.deltaTime * rotationSpeed);
                    //transform.rotation = Quaternion.identity;
                }

                break;
        }
    }

    public Frisbee_State FrisbeeState
    {
        get => _frisbeeState;
        set => _frisbeeState = value;
    }

    public void Action()
    {
        switch (_frisbeeState)
        {
            case Frisbee_State.THROWN:
                //call the frisbee
                ReturnToPlayer();
                FrisbeeState = Frisbee_State.CALLED;
                break;
            case Frisbee_State.CALLED:
                //call the frisbee
                ReturnToPlayer();
                break;
            case Frisbee_State.PICKED:
                Throw();
                int rng = UnityEngine.Random.Range(1,5);
                _sounds.Play("throw" + rng);
                _justPicked = false;
                FrisbeeState = Frisbee_State.THROWN;
                break;
        }
    }

    private void ReturnToPlayer()
    {
        _rigidbody.velocity = (_playerFrisbeePosition.position - this.transform.position).normalized * _returningSpeed;
    }

    private void Throw()
    {
        transform.parent = null;
        
        float fov = _camera.fieldOfView;
        _rigidbody.velocity = _camera.transform.forward.normalized * speed;
        DOTween.Sequence()
            .Append(_camera.DOFieldOfView(fov + fieldOfViewIncrease, fielfOfViewTweenTime))
            .Append(_camera.DOFieldOfView(fov, fielfOfViewTweenTime).SetEase(Ease.OutBack));

        _rigidbody.velocity = _camera.transform.forward.normalized * speed;
    }

    private void OnCollisionEnter(Collision other) {
        if(_frisbeeState != Frisbee_State.PICKED)
        {
        _bounce.pitch = UnityEngine.Random.Range(0.9f,1.1f);
        _bounce.Play();
        }
    }
}
