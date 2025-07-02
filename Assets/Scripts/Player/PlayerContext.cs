using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerContext
{
    private Transform _playerTransform;
    private float _headOffset;
    private float _footOffset;
    private float _climbOffset;
    private float _jumpOffset;
    private float _moveOffset;
    private float _playerHeight;
    private float _playerCrouchHeight;
    private float _playerProneHeight;
    private Vector3 _climbLocOffset;

    private TwoBoneIKConstraint _leftHandboneIKConstraint;
    private TwoBoneIKConstraint _rightHandboneIKConstraint;
    
    public Transform playerTransform => _playerTransform;
    public float headOffset => _headOffset;
    public float footOffset => _footOffset;
    public float climbOffset => _climbOffset;
    public float jumpOffset => _jumpOffset;
    public float moveOffset => _moveOffset;
    public float playerHeight => _playerHeight;

    public bool IsClimbPossible;
    public Vector3 contactPoint;

    public Vector3 climbLocOffset => _climbLocOffset;
    public float   playerCrouchHeight=> _playerCrouchHeight;
    public float   playerProneHeight=> _playerProneHeight;

    public PlayerContext(Transform playerTransform,float headOffset,float footOffset,float climbOffset,float jumpOffset,float moveOffset,bool isClimbPossible,float playerHeight,float playerCrouchHeight,float playerProneHeight,Vector3 climbLocOffset) {

        _playerTransform = playerTransform;
        _headOffset = headOffset;
        _footOffset = footOffset;
        _climbOffset = climbOffset;
        _jumpOffset = jumpOffset;
        _moveOffset = moveOffset;
        _jumpOffset = jumpOffset;
        _playerHeight = playerHeight;
        IsClimbPossible = isClimbPossible;
        _playerCrouchHeight = playerCrouchHeight;
        _playerProneHeight = playerProneHeight;
        _climbLocOffset = climbLocOffset;

        _rightHandboneIKConstraint.weight = 0f;
        _rightHandboneIKConstraint.data.target = _playerTransform;
    }
}
