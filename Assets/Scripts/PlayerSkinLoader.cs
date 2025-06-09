using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkinLoader : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController baseController;

    public AnimationClip[] runAnimations;

    void Start()
    {
        var overrideController = new AnimatorOverrideController(baseController);
        overrideController["Run_0"] = runAnimations[PlayerPrefs.GetInt("key", 0)];
        animator.runtimeAnimatorController = overrideController;
    }
}