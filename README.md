# UdonChips Plus

This project aims to create a workflow enhanced UdonChips implementation for VRChat.
The original concept of UdonChips is by [Lura and Kubiwak](https://lura.booth.pm/items/3060394), which is a "de-facto" standard for VRChat world gimmicks that have the concept of "money", any gimmicks choose to integrate with UdonChips, they can share the currency across them.

This package is a drop-in replacement of `00_UDONCHIPS` part of the starter kit (see above link).

## So whats the difference between this implementation and original?

First of all, this implementation guarantees the UdonChips singleton, that means there will be only one UdonChips exists in your world.

Secondly, all U# scripts that requires UdonChips in their property fields, the references of the UdonChips will automatically binds to them on build.
What does it mean? That means the code like this already works, without any additional setup in inspector or logic to resolve the instance on load:
```csharp
using UnityEngine;
using UdonSharp;
using UCS;

public class MyUdonChipsGimmick : UdonSharpBehaviour {
    [SerializeField]
    [HideInInspector] // HideInInspector is optional, it doesn't matter.
    UdonChips udonChips;

    void OnEnable() {
        // Example: When the current game object active, current user will get 100uc:
        udonChips.money += 100;
    }
}
```

Thirdly, your U# scripts can truly listen to updates of user's wallet, with the event-based system:
```csharp
using UnityEngine;
using UdonSharp;
using UCS;
using JLChnToZ.VRC.Foundation;

public class MyUdonChipsGimmick : UdonSharpBehaviour {
    [SerializeField]
    [HideInInspector]
    [BindUdonSharpEvent] // BindUdonSharpEvent makes this script automatically listen to its callbacks.
    UdonChips udonChips;

    // This will be called when someone adds or takes money from user's udonChips wallet.
    public void _OnMoneyChanged() {
        // Your logic here
    }
}
```

An alternative logic:
```csharp
using UnityEngine;
using UdonSharp;
using UCS;
using JLChnToZ.VRC.Foundation;

public class MyUdonChipsGimmick : UdonSharpBehaviour {
    [SerializeField]
    [HideInInspector]
    UdonChips udonChips;

    void Start() {
        // This registers only when your gimmick first activated.
        // It will have a little runtime performance overhead,
        // but gurantees no callback will be fired before your script initialized.
        udonChips._AddListener(this);
    }

    // This will be called when someone adds or takes money from user's udonChips wallet.
    public void _OnMoneyChanged() {
        // Your logic here
    }
}
```

Under the hood I have integrated [VRCW Foundation](https://github.com/JLChnToZ/vrcw-foundation) to enhance the workflow, including event callback and build-time singleton enforcement/auto bind logic. You can [read the source code](Packages/idv.jlchntoz.ucsplus/Runtime/UdonChips.cs) to see how I integrate.

## Installation

I recommend you install via [VRChat Creator Companion](https://vcc.docs.vrchat.com/) or [its alternatives](https://github.com/vrc-get/vrc-get).
You may add my repository listings from [here](https://xtlcdn.github.io/vpm/).

As written in above, this is a drop-in replacement of `00_UDONCHIPS` part of the starter kit, please make sure `Assets/UdonChips/00_UDONCHIPS` folder and its contents have been properly removed after installation.

## License

[MIT](LICENSE)