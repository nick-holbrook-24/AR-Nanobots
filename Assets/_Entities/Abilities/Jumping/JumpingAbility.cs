using UnityEngine;
using UnityEngine.Assertions;

public class JumpingAbility : MonoBehaviour, IAbility
{
    [SerializeField] private float jumpPowerPerLevel = 0.2f;

    private byte level = 0;
    private Rigidbody jumperRigidbody = null;
    private NanobotData nanobotData = null;

    public void Initialize(byte _level, NanobotData _nanobotData)
    {
        level = _level;

        transform.parent.TryGetComponent(out jumperRigidbody);
        Assert.IsNotNull(jumperRigidbody, "JumpingAbility's jumperRigidbody is not assigned!");

        Assert.IsNotNull(_nanobotData, "JumpingAbility's Initialize's _nanobotData is not assigned!");
        nanobotData = _nanobotData;
    }
    
    public void Action()
    {
        if((nanobotData.energy.Current <= 0) || nanobotData.isJumping)
        {
            return;
        }

        nanobotData.energy.Current--;
        nanobotData.DataChanged = true;

        float jumpForce = (level + 1) * jumpPowerPerLevel;
        jumperRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        nanobotData.isJumping = true;
    }
}