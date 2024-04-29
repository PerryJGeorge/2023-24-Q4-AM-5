using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip footstepSound; // Sound to play for footsteps
    public float footstepInterval = 0.5f; // Time interval between each footstep sound
    public AudioSource audioSource; // Reference to the AudioSource component

    private float nextFootstepTime; // Time for the next footstep sound
    private bool isMoving; // Flag to track if the player is moving

    private void Start()
    {
        if (audioSource == null)
        {
            // If AudioSource is not assigned, try to get it from the GameObject
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                // If AudioSource is still not found, add it to the GameObject
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void Update()
    {
        // Check if the player is moving
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.01f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.01f)
        {
            // If the player just started moving, reset footstep timing and play footstep sound
            if (!isMoving)
            {
                nextFootstepTime = Time.time + footstepInterval;
                isMoving = true;
                audioSource.PlayOneShot(footstepSound);
            }

            // Check if it's time to play the next footstep sound
            if (Time.time >= nextFootstepTime)
            {
                // Play footstep sound
                audioSource.PlayOneShot(footstepSound);

                // Set next footstep time
                nextFootstepTime = Time.time + footstepInterval;
            }
        }
        else
        {
            // Stop the footstep sound immediately when the player stops moving
            if (isMoving)
            {
                audioSource.Stop();
                isMoving = false;
            }
        }
    }
}

