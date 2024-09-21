using UdonSharp;
using UnityEngine;
using JLChnToZ.VRC.Foundation;

namespace UCS {
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public partial class UdonChips : UdonSharpEventSender {
        [Tooltip("現在の所持金（初期所持金）")]
        public float money = 1000;

        public string format = "$ {0:F0}";

        // We can't use [FieldChangeCallback(...)] on money field
        // because that will make U# compiler complains
        // when external U# behaviours assigning value to it in C# way.
        // Therfore, we use this low-level way to detect variable change.
        public void _onVarChange_money() => SendEvent("_OnMoneyChanged");
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
