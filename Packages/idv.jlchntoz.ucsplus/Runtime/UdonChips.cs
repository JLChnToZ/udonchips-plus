using UdonSharp;
using UnityEngine;
using JLChnToZ.VRC.Foundation;

namespace UCS {
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
    public partial class UdonChips : UdonSharpBehaviour {
        [Tooltip("現在の所持金（初期所持金）")]
        public float money = 1000;

        public string format = "$ {0:F0}";
    }

#if UNITY_EDITOR && !COMPILER_UDONSHARP
    public partial class UdonChips : ISingleton<UdonChips> {
        public void Merge(UdonChips[] instances) {
            transform.SetParent(null, false);
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            gameObject.name = "UdonChips";
        }
    }
#endif
}
