//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputSystem/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerBasicMovement"",
            ""id"": ""1e05b642-93e3-4903-99a3-0b49e9b495e4"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7d86923a-bd3d-4e4f-be04-a2665e2f9d13"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""51260790-b8c2-48bc-a4cd-2dcb32fe911f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ChangePose"",
                    ""type"": ""Button"",
                    ""id"": ""574b9661-3d37-4660-8ef0-fe68e8191303"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""680e189d-9c0f-4580-932e-4d952082e348"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""5b531d54-5841-41db-b8b4-f59d7d6ec54c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""f4814de2-3f04-41d0-a01d-9f4c2e9ba935"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91e37b49-afc3-446f-8390-d1f5729e510b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6781e696-3168-4534-a15c-87f306d294a5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a9429822-a520-4ce6-b3ee-0f436de72c74"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0e0b7b99-53db-4b52-ac5f-15770bf63eb8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dbd84651-a258-4fb3-8652-901c1e6df2ed"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""dc59c899-508f-48d0-9b86-c0ca85a2fb0c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""50093a91-1e62-4924-8d62-8c0a02885697"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""ChangePose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""655052d0-f53c-4ad6-9edd-f21255bdd13e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a50e35a6-44d6-4d1c-b536-d63104b89156"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fabd250-b313-4a61-bdf8-c97b547c219d"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bd21760-a9bb-4879-8a10-e1ab042f2217"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcce244d-c6ca-417d-8f4c-c010878008f7"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardPC"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardPC"",
            ""bindingGroup"": ""KeyboardPC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerBasicMovement
        m_PlayerBasicMovement = asset.FindActionMap("PlayerBasicMovement", throwIfNotFound: true);
        m_PlayerBasicMovement_Jump = m_PlayerBasicMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerBasicMovement_Movement = m_PlayerBasicMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerBasicMovement_ChangePose = m_PlayerBasicMovement.FindAction("ChangePose", throwIfNotFound: true);
        m_PlayerBasicMovement_Attack = m_PlayerBasicMovement.FindAction("Attack", throwIfNotFound: true);
        m_PlayerBasicMovement_Block = m_PlayerBasicMovement.FindAction("Block", throwIfNotFound: true);
        m_PlayerBasicMovement_MousePosition = m_PlayerBasicMovement.FindAction("MousePosition", throwIfNotFound: true);
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

    // PlayerBasicMovement
    private readonly InputActionMap m_PlayerBasicMovement;
    private IPlayerBasicMovementActions m_PlayerBasicMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerBasicMovement_Jump;
    private readonly InputAction m_PlayerBasicMovement_Movement;
    private readonly InputAction m_PlayerBasicMovement_ChangePose;
    private readonly InputAction m_PlayerBasicMovement_Attack;
    private readonly InputAction m_PlayerBasicMovement_Block;
    private readonly InputAction m_PlayerBasicMovement_MousePosition;
    public struct PlayerBasicMovementActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerBasicMovementActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_PlayerBasicMovement_Jump;
        public InputAction @Movement => m_Wrapper.m_PlayerBasicMovement_Movement;
        public InputAction @ChangePose => m_Wrapper.m_PlayerBasicMovement_ChangePose;
        public InputAction @Attack => m_Wrapper.m_PlayerBasicMovement_Attack;
        public InputAction @Block => m_Wrapper.m_PlayerBasicMovement_Block;
        public InputAction @MousePosition => m_Wrapper.m_PlayerBasicMovement_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_PlayerBasicMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerBasicMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerBasicMovementActions instance)
        {
            if (m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnJump;
                @Movement.started -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnMovement;
                @ChangePose.started -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnChangePose;
                @ChangePose.performed -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnChangePose;
                @ChangePose.canceled -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnChangePose;
                @Attack.started -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnAttack;
                @Block.started -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnBlock;
                @MousePosition.started -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_PlayerBasicMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @ChangePose.started += instance.OnChangePose;
                @ChangePose.performed += instance.OnChangePose;
                @ChangePose.canceled += instance.OnChangePose;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public PlayerBasicMovementActions @PlayerBasicMovement => new PlayerBasicMovementActions(this);
    private int m_KeyboardPCSchemeIndex = -1;
    public InputControlScheme KeyboardPCScheme
    {
        get
        {
            if (m_KeyboardPCSchemeIndex == -1) m_KeyboardPCSchemeIndex = asset.FindControlSchemeIndex("KeyboardPC");
            return asset.controlSchemes[m_KeyboardPCSchemeIndex];
        }
    }
    public interface IPlayerBasicMovementActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnChangePose(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
