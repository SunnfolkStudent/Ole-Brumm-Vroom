//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a3d2662a-7b54-45b7-ab42-06bfbefc5862"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cda31fec-3c11-4e43-a34b-4f9e01458b11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Float"",
                    ""type"": ""Button"",
                    ""id"": ""fadb9ac1-7692-4582-ad00-f82d47f4c41a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DropBelow"",
                    ""type"": ""Button"",
                    ""id"": ""15a43d02-fa58-4988-8567-093b8b1e6c28"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenPauseScreen"",
                    ""type"": ""Button"",
                    ""id"": ""16a48eec-1672-49f9-ac08-b793af89587f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b62ccd19-ffe5-477d-a0bd-7c5c7d62a9fa"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6aac9d85-e17c-46f8-8679-9a4e8828db87"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""673e769c-c0d4-44ee-89b8-7f6858787491"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""431dc261-f5a0-4554-9530-fad71626a6e9"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""292b8610-650d-4331-8f1c-fe3663cc142e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Float"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c415ab57-2eb6-4252-bfab-e710444b6008"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Float"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b80f47ab-8154-414e-9f8f-58fe6d9e0fb5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Float"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""680fa097-e353-4961-8c0b-a2243e613625"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Float"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8109dc1f-14dd-474d-afd7-b7a63e93deba"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DropBelow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c65dcc4-c060-4312-8789-5dac19bd298b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DropBelow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd8e8233-fc6f-4764-98c1-775f782d8ba9"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DropBelow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7cb305a9-8143-4056-9156-be676bc897ee"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DropBelow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87f24b0c-2a3b-4e0f-a055-c6453c842b57"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""OpenPauseScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41e6a279-7eff-42cd-8e7c-6348db9ff51e"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""OpenPauseScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ResetRun"",
            ""id"": ""e1009cc2-2fbb-414e-aeef-5c9e380d0862"",
            ""actions"": [
                {
                    ""name"": ""AnyKey"",
                    ""type"": ""Button"",
                    ""id"": ""0b6c092a-b8e8-4935-ae0d-d3f7cf3b8f5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f98aeff6-767d-4a84-b31e-4314b222bf4b"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8c8d0e1-7193-462a-88a2-e766932418eb"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PauseScreen"",
            ""id"": ""a13ef50a-e4a4-49e3-a42b-d60f6c397137"",
            ""actions"": [
                {
                    ""name"": ""QuitGame"",
                    ""type"": ""Button"",
                    ""id"": ""08df87e5-13d6-43f6-9668-61a3bf120204"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Continue"",
                    ""type"": ""Button"",
                    ""id"": ""8fa8db7e-ba3e-4c3d-9009-94ce7c3106c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7fd2d1b4-e1ad-4957-b33c-b858797bafc8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""QuitGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c9a3b20-68be-447a-aa8b-af1ada24d828"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""QuitGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5d0717f-685f-40f5-a709-926b1c17b243"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04be6b1d-3400-4fcb-bcfe-0043c231afff"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Float = m_Player.FindAction("Float", throwIfNotFound: true);
        m_Player_DropBelow = m_Player.FindAction("DropBelow", throwIfNotFound: true);
        m_Player_OpenPauseScreen = m_Player.FindAction("OpenPauseScreen", throwIfNotFound: true);
        // ResetRun
        m_ResetRun = asset.FindActionMap("ResetRun", throwIfNotFound: true);
        m_ResetRun_AnyKey = m_ResetRun.FindAction("AnyKey", throwIfNotFound: true);
        // PauseScreen
        m_PauseScreen = asset.FindActionMap("PauseScreen", throwIfNotFound: true);
        m_PauseScreen_QuitGame = m_PauseScreen.FindAction("QuitGame", throwIfNotFound: true);
        m_PauseScreen_Continue = m_PauseScreen.FindAction("Continue", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Float;
    private readonly InputAction m_Player_DropBelow;
    private readonly InputAction m_Player_OpenPauseScreen;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Float => m_Wrapper.m_Player_Float;
        public InputAction @DropBelow => m_Wrapper.m_Player_DropBelow;
        public InputAction @OpenPauseScreen => m_Wrapper.m_Player_OpenPauseScreen;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Float.started += instance.OnFloat;
            @Float.performed += instance.OnFloat;
            @Float.canceled += instance.OnFloat;
            @DropBelow.started += instance.OnDropBelow;
            @DropBelow.performed += instance.OnDropBelow;
            @DropBelow.canceled += instance.OnDropBelow;
            @OpenPauseScreen.started += instance.OnOpenPauseScreen;
            @OpenPauseScreen.performed += instance.OnOpenPauseScreen;
            @OpenPauseScreen.canceled += instance.OnOpenPauseScreen;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Float.started -= instance.OnFloat;
            @Float.performed -= instance.OnFloat;
            @Float.canceled -= instance.OnFloat;
            @DropBelow.started -= instance.OnDropBelow;
            @DropBelow.performed -= instance.OnDropBelow;
            @DropBelow.canceled -= instance.OnDropBelow;
            @OpenPauseScreen.started -= instance.OnOpenPauseScreen;
            @OpenPauseScreen.performed -= instance.OnOpenPauseScreen;
            @OpenPauseScreen.canceled -= instance.OnOpenPauseScreen;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // ResetRun
    private readonly InputActionMap m_ResetRun;
    private List<IResetRunActions> m_ResetRunActionsCallbackInterfaces = new List<IResetRunActions>();
    private readonly InputAction m_ResetRun_AnyKey;
    public struct ResetRunActions
    {
        private @PlayerControls m_Wrapper;
        public ResetRunActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AnyKey => m_Wrapper.m_ResetRun_AnyKey;
        public InputActionMap Get() { return m_Wrapper.m_ResetRun; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ResetRunActions set) { return set.Get(); }
        public void AddCallbacks(IResetRunActions instance)
        {
            if (instance == null || m_Wrapper.m_ResetRunActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ResetRunActionsCallbackInterfaces.Add(instance);
            @AnyKey.started += instance.OnAnyKey;
            @AnyKey.performed += instance.OnAnyKey;
            @AnyKey.canceled += instance.OnAnyKey;
        }

        private void UnregisterCallbacks(IResetRunActions instance)
        {
            @AnyKey.started -= instance.OnAnyKey;
            @AnyKey.performed -= instance.OnAnyKey;
            @AnyKey.canceled -= instance.OnAnyKey;
        }

        public void RemoveCallbacks(IResetRunActions instance)
        {
            if (m_Wrapper.m_ResetRunActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IResetRunActions instance)
        {
            foreach (var item in m_Wrapper.m_ResetRunActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ResetRunActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ResetRunActions @ResetRun => new ResetRunActions(this);

    // PauseScreen
    private readonly InputActionMap m_PauseScreen;
    private List<IPauseScreenActions> m_PauseScreenActionsCallbackInterfaces = new List<IPauseScreenActions>();
    private readonly InputAction m_PauseScreen_QuitGame;
    private readonly InputAction m_PauseScreen_Continue;
    public struct PauseScreenActions
    {
        private @PlayerControls m_Wrapper;
        public PauseScreenActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @QuitGame => m_Wrapper.m_PauseScreen_QuitGame;
        public InputAction @Continue => m_Wrapper.m_PauseScreen_Continue;
        public InputActionMap Get() { return m_Wrapper.m_PauseScreen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseScreenActions set) { return set.Get(); }
        public void AddCallbacks(IPauseScreenActions instance)
        {
            if (instance == null || m_Wrapper.m_PauseScreenActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PauseScreenActionsCallbackInterfaces.Add(instance);
            @QuitGame.started += instance.OnQuitGame;
            @QuitGame.performed += instance.OnQuitGame;
            @QuitGame.canceled += instance.OnQuitGame;
            @Continue.started += instance.OnContinue;
            @Continue.performed += instance.OnContinue;
            @Continue.canceled += instance.OnContinue;
        }

        private void UnregisterCallbacks(IPauseScreenActions instance)
        {
            @QuitGame.started -= instance.OnQuitGame;
            @QuitGame.performed -= instance.OnQuitGame;
            @QuitGame.canceled -= instance.OnQuitGame;
            @Continue.started -= instance.OnContinue;
            @Continue.performed -= instance.OnContinue;
            @Continue.canceled -= instance.OnContinue;
        }

        public void RemoveCallbacks(IPauseScreenActions instance)
        {
            if (m_Wrapper.m_PauseScreenActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPauseScreenActions instance)
        {
            foreach (var item in m_Wrapper.m_PauseScreenActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PauseScreenActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PauseScreenActions @PauseScreen => new PauseScreenActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnFloat(InputAction.CallbackContext context);
        void OnDropBelow(InputAction.CallbackContext context);
        void OnOpenPauseScreen(InputAction.CallbackContext context);
    }
    public interface IResetRunActions
    {
        void OnAnyKey(InputAction.CallbackContext context);
    }
    public interface IPauseScreenActions
    {
        void OnQuitGame(InputAction.CallbackContext context);
        void OnContinue(InputAction.CallbackContext context);
    }
}
