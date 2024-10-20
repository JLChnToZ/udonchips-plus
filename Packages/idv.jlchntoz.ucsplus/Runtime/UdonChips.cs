using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
#if VRC_ENABLE_PLAYER_PERSISTENCE
using VRC.SDK3.Persistence;
#endif
using JLChnToZ.VRC.Foundation;

namespace UCS {
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public partial class UdonChips : UdonSharpEventSender {
#if VRC_ENABLE_PLAYER_PERSISTENCE
        const string PERSISTENCE_KEY = "UdonChipsMoney";
#endif

        [Tooltip("現在の所持金（初期所持金）")]
        public float money = 1000;

#if VRC_ENABLE_PLAYER_PERSISTENCE
        [Tooltip("所持金をプレイヤーデータに保存するか")]
        [SerializeField] bool saveToPlayerData = false;
#endif

        public string format = "$ {0:F0}";

        // We can't use [FieldChangeCallback(...)] on money field
        // because that will make U# compiler complains
        // when external U# behaviours assigning value to it in C# way.
        // Therfore, we use this low-level way to detect variable change.
        public void _onVarChange_money() {
            SendEvent("_OnMoneyChanged");
#if VRC_ENABLE_PLAYER_PERSISTENCE
            if (saveToPlayerData) PlayerData.SetFloat(PERSISTENCE_KEY, money);
#endif
        }

#if VRC_ENABLE_PLAYER_PERSISTENCE
        public override void OnPlayerRestored(VRCPlayerApi player) {
            if (saveToPlayerData && player.isLocal && PlayerData.TryGetFloat(player, PERSISTENCE_KEY, out float savedMoney))
                money = savedMoney;
        }
#endif
    }

#if UNITY_EDITOR && !COMPILER_UDONSHARP
    public partial class UdonChips : ISingleton<UdonChips> {
        public void Merge(UdonChips[] instances) {
            MergeTargets(instances);
            gameObject.name = "UdonChips";
        }
    }
#endif
}
