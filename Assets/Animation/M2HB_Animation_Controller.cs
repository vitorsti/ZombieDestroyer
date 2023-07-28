using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2HB_Animation_Controller : MonoBehaviour
{
    private Animator animator;
    public GameObject fxObject;

    public static M2HB_Animation_Controller instance;

    private void Awake()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
        instance = this;
    }

    // Call this method whenever the M2HB fires
    public void ShootAnim()
    {
        // Set the "Fire" bool parameter to true in the animator
        animator.SetBool("Fire", true);
        ActivateParticleSystems();
    }

    // Call this method to reset the "Fire" bool parameter to false in the animator
    public void ResetFireAnim()
    {
        // Set the "Fire" bool parameter to false in the animator
        animator.SetBool("Fire", false);
        Debug.Log("fire:false");
    }


    void ActivateParticleSystems()
    {
        // Check if the targetObject is assigned
        if (fxObject == null)
        {
            Debug.LogWarning("Target object is not assigned!");
            return;
        }

        // Get all ParticleSystems under the targetObject and its children
        ParticleSystem[] particleSystems = fxObject.GetComponentsInChildren<ParticleSystem>();

        // Loop through each ParticleSystem and enable them
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }



}
