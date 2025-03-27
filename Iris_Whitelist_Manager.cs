// WhitelistManager.cs - Final Version: Universal Lockdown Edition (With Whitelist Teleport + Persistent Lock + Light Shutdown)
// Purpose: Prevent any unauthorized user from interacting, moving, or seeing the world if not on whitelist
// Designed for VRC SDK3 + UdonSharp

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class WhitelistManager : UdonSharpBehaviour
{
    // === Public Configurable Fields ===

    [Tooltip("Target position to teleport unauthorized users (e.g. a jail or void)")]
    public Transform denyZone; // Location to send unauthorized players

    [Tooltip("Target position to teleport authorized users (e.g. entry point)")]
    public Transform welcomeZone; // Location to send authorized players

    [Tooltip("Full screen black canvas for visual lockout")]
    public GameObject blackScreenCanvas; // Optional visual blocker for locked-out users

    [Tooltip("List of usernames allowed in the world")]
    public string[] whitelist = new string[]
    {
        "yourname",
        "yourfriendsname"
    };

    [Tooltip("Lights to disable for unauthorized users")]
    public Light[] lightsToDisable; // Optional list of lights to turn off for locked users

    private bool isLocked = false; // Tracks whether the local player is currently locked

    // === Initialization ===
    void Start()
    {
        // Get the current local player's display name
        string currentPlayer = Networking.LocalPlayer.displayName;

        // Check if user is whitelisted
        if (!IsAuthorized(currentPlayer))
        {
            LockPlayerOut(); // Lock unauthorized user
        }
        else
        {
            TeleportAuthorizedPlayer(); // Move authorized user to welcome zone
        }
    }

    // === Lock behavior handler (persistent enforcement) ===
    void Update()
    {
        if (isLocked)
        {
            // Force the black screen canvas to stay on
            if (blackScreenCanvas != null && !blackScreenCanvas.activeSelf)
            {
                blackScreenCanvas.SetActive(true);
            }

            // Re-teleport to deny zone if necessary
            if (denyZone != null)
            {
                Networking.LocalPlayer.TeleportTo(denyZone.position, denyZone.rotation);
            }
        }
    }

    // === Authorization Check ===
    private bool IsAuthorized(string playerName)
    {
        foreach (string allowed in whitelist)
        {
            // Case-insensitive comparison against whitelist
            if (allowed.Trim().ToLower() == playerName.Trim().ToLower())
                return true;
        }
        return false;
    }

    // === Lock out unauthorized users ===
    private void LockPlayerOut()
    {
        isLocked = true;

        // Show black screen overlay
        if (blackScreenCanvas != null)
            blackScreenCanvas.SetActive(true);

        // Move to deny zone
        if (denyZone != null)
            Networking.LocalPlayer.TeleportTo(denyZone.position, denyZone.rotation);

        // Prevent movement
        Networking.LocalPlayer.Immobilize(true);

        // Optionally disable world lighting
        if (lightsToDisable != null && lightsToDisable.Length > 0)
        {
            foreach (Light light in lightsToDisable)
            {
                if (light != null)
                    light.enabled = false;
            }
        }

        Debug.Log("[WhitelistManager] Unauthorized user locked out: " + Networking.LocalPlayer.displayName);
    }

    // === Move authorized user to welcome area ===
    private void TeleportAuthorizedPlayer()
    {
        if (welcomeZone != null)
        {
            Networking.LocalPlayer.TeleportTo(welcomeZone.position, welcomeZone.rotation);
            Debug.Log("[WhitelistManager] Authorized user teleported to welcome zone: " + Networking.LocalPlayer.displayName);
        }
    }
}
