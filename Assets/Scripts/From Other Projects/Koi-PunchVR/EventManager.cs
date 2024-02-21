using System;
using UnityEngine;

namespace From_Other_Projects.Koi_PunchVR
{
    public static class EventManager
    {
        private const bool IsDebugging = true;

        #region ---Debugging---

        private static void Log(string message)
        {
            if (IsDebugging) Debug.Log(message);
        }

        #endregion

        #region >>>---FishSpawningActions---

        public static Action SpawnFish; // Spawns 1 fish and launches it to hit the player.
        public static Action FishSpawning = SpawnFishDebug; // Starts fish spawning with frequency gradually increasing.
        public static Action FishSpawningAtMaxRate = SpawnFishDebug; // Starts fish spawning at max rate.
        public static Action StopFishSpawning = StopFishSpawningDebug; // Stops fish spawning.
        
        #endregion

        #region >>>---ActionDebugs---

        private static void SpawnFishDebug()
        {
            Log("---Fish Spawning started---");
        }

        private static void StopFishSpawningDebug()
        {
            Log("---Fish Spawning stopped---");
        }

        private static void StartBossPhase0Debug()
        {
            Log("---BossPhase0 started---");
        }
        #endregion
    }
}
