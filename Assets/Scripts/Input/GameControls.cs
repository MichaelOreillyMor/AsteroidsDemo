// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Asteroids.Input
{
    public class @GameControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Spaceship"",
            ""id"": ""a5aa67cb-3209-4fd9-837c-19f64495c2f2"",
            ""actions"": [
                {
                    ""name"": ""MainShot"",
                    ""type"": ""PassThrough"",
                    ""id"": ""33cb3cc7-572f-4849-844a-eb5a41136b99"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryShot"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cc59fadf-4cb8-45f0-a2a4-3a3715ede974"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b6886db7-951d-4bdc-bbe1-da3a900518ed"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""12e196ef-eb1b-450d-895d-2428a0c6c831"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c96b6d3c-094f-4cfc-aaa5-931a67c07fc2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e29fbb8b-a182-4c50-9cd6-d9e90ed826b3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f929e3ba-bc7e-48a5-8cc8-96cddb977bb6"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""330365c7-8e3f-455e-a638-4320ba0d898b"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Axis1"",
                    ""id"": ""d98b0f99-0805-4d1b-9464-38e154e3ad69"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a76cba3b-1a53-4b31-a444-59b3f124bc0d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""dbb1fc34-adc5-4f95-ae0a-e0b550d779d3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Axis2"",
                    ""id"": ""78d48aae-e39f-4bc3-b1aa-d51265b8273c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""44947e75-de99-42c4-bf1b-c44d8f47575b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1e219130-ac4f-425d-8317-231a249867c0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3b44a6e2-52b9-4f2d-8dfb-6898a6b0b02e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryShot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Game"",
            ""id"": ""06688c93-59be-4c98-b8a7-b6c6890a7197"",
            ""actions"": [
                {
                    ""name"": ""CloseGame"",
                    ""type"": ""Button"",
                    ""id"": ""cfb277f1-6c43-4c25-b95c-d171f63994c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""39bd6aba-207d-49e3-8d62-6ac130848d67"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Spaceship
            m_Spaceship = asset.FindActionMap("Spaceship", throwIfNotFound: true);
            m_Spaceship_MainShot = m_Spaceship.FindAction("MainShot", throwIfNotFound: true);
            m_Spaceship_SecondaryShot = m_Spaceship.FindAction("SecondaryShot", throwIfNotFound: true);
            m_Spaceship_MoveForward = m_Spaceship.FindAction("MoveForward", throwIfNotFound: true);
            m_Spaceship_Rotate = m_Spaceship.FindAction("Rotate", throwIfNotFound: true);
            // Game
            m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
            m_Game_CloseGame = m_Game.FindAction("CloseGame", throwIfNotFound: true);
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

        // Spaceship
        private readonly InputActionMap m_Spaceship;
        private ISpaceshipActions m_SpaceshipActionsCallbackInterface;
        private readonly InputAction m_Spaceship_MainShot;
        private readonly InputAction m_Spaceship_SecondaryShot;
        private readonly InputAction m_Spaceship_MoveForward;
        private readonly InputAction m_Spaceship_Rotate;
        public struct SpaceshipActions
        {
            private @GameControls m_Wrapper;
            public SpaceshipActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @MainShot => m_Wrapper.m_Spaceship_MainShot;
            public InputAction @SecondaryShot => m_Wrapper.m_Spaceship_SecondaryShot;
            public InputAction @MoveForward => m_Wrapper.m_Spaceship_MoveForward;
            public InputAction @Rotate => m_Wrapper.m_Spaceship_Rotate;
            public InputActionMap Get() { return m_Wrapper.m_Spaceship; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(SpaceshipActions set) { return set.Get(); }
            public void SetCallbacks(ISpaceshipActions instance)
            {
                if (m_Wrapper.m_SpaceshipActionsCallbackInterface != null)
                {
                    @MainShot.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMainShot;
                    @MainShot.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMainShot;
                    @MainShot.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMainShot;
                    @SecondaryShot.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnSecondaryShot;
                    @SecondaryShot.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnSecondaryShot;
                    @SecondaryShot.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnSecondaryShot;
                    @MoveForward.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMoveForward;
                    @MoveForward.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMoveForward;
                    @MoveForward.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnMoveForward;
                    @Rotate.started -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnRotate;
                    @Rotate.performed -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnRotate;
                    @Rotate.canceled -= m_Wrapper.m_SpaceshipActionsCallbackInterface.OnRotate;
                }
                m_Wrapper.m_SpaceshipActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MainShot.started += instance.OnMainShot;
                    @MainShot.performed += instance.OnMainShot;
                    @MainShot.canceled += instance.OnMainShot;
                    @SecondaryShot.started += instance.OnSecondaryShot;
                    @SecondaryShot.performed += instance.OnSecondaryShot;
                    @SecondaryShot.canceled += instance.OnSecondaryShot;
                    @MoveForward.started += instance.OnMoveForward;
                    @MoveForward.performed += instance.OnMoveForward;
                    @MoveForward.canceled += instance.OnMoveForward;
                    @Rotate.started += instance.OnRotate;
                    @Rotate.performed += instance.OnRotate;
                    @Rotate.canceled += instance.OnRotate;
                }
            }
        }
        public SpaceshipActions @Spaceship => new SpaceshipActions(this);

        // Game
        private readonly InputActionMap m_Game;
        private IGameActions m_GameActionsCallbackInterface;
        private readonly InputAction m_Game_CloseGame;
        public struct GameActions
        {
            private @GameControls m_Wrapper;
            public GameActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @CloseGame => m_Wrapper.m_Game_CloseGame;
            public InputActionMap Get() { return m_Wrapper.m_Game; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
            public void SetCallbacks(IGameActions instance)
            {
                if (m_Wrapper.m_GameActionsCallbackInterface != null)
                {
                    @CloseGame.started -= m_Wrapper.m_GameActionsCallbackInterface.OnCloseGame;
                    @CloseGame.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnCloseGame;
                    @CloseGame.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnCloseGame;
                }
                m_Wrapper.m_GameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @CloseGame.started += instance.OnCloseGame;
                    @CloseGame.performed += instance.OnCloseGame;
                    @CloseGame.canceled += instance.OnCloseGame;
                }
            }
        }
        public GameActions @Game => new GameActions(this);
        public interface ISpaceshipActions
        {
            void OnMainShot(InputAction.CallbackContext context);
            void OnSecondaryShot(InputAction.CallbackContext context);
            void OnMoveForward(InputAction.CallbackContext context);
            void OnRotate(InputAction.CallbackContext context);
        }
        public interface IGameActions
        {
            void OnCloseGame(InputAction.CallbackContext context);
        }
    }
}
