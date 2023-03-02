using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal;

public class SessionManager : MonoBehaviour
{
    #region Member Variables
    private Realtime _realtime;
    #endregion // Member Variables

    #region Unity Inspector Variables
    [SerializeField]
    [Tooltip("Objects to spawn when connected.")]
    private List<GameObject> _spawnOnConnectObjects;
    #endregion // Unity Inspector Variables

    private void Awake()
    {
        // Get the Realtime component on this game object
        _realtime = GetComponent<Realtime>();

        // Notify us when Realtime successfully connects to the room
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        // Do we have objects to spawn?
        if (_spawnOnConnectObjects != null)
        {
            // Define instantiation options for spawned objects
            var options = new Realtime.InstantiateOptions
            {
                ownedByClient = true,    // Make sure the RealtimeView on this prefab is owned by this client.
                preventOwnershipTakeover = true,    // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
                useInstance = realtime // Use the instance of Realtime that fired the didConnectToRoom event.
            };

            // Spawn objects
            foreach (var prefab in _spawnOnConnectObjects)
            {
                Realtime.Instantiate(prefab.name, Vector3.zero, Quaternion.identity, options);
            }
        }
    }
}
