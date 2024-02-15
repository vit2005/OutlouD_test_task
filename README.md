Technical task:
You have a field with dynamically sized cards facing down.
At the start, the game briefly shows them for a couple of seconds, and then the player clicks on a card, flips it, and then clicks on another card.
The goal is to match all the cards.
A hint button shuffles the cards and shows them again for a couple of seconds.
Additionally, you must implement a timer, win and lose conditions, create a menu, and optionally add sounds and VFX. Briefly describe why you chose this implementation approach (architecture).
Part two: write a storage system for game options. It should allow for various save implementations.
The storage should be able to save in JSON, player prefs, and binary.
It's preferable to use Zenject: Dependency Injection.

Brief description:
The application contains the main AppController (singleton) and other separated controllers (a few of them are MonoBehaviours and one injected by Zenject). 

AppControllerModes controls game modes (preload, menu, gameplay) and transitions between them (I used simple camera rotation, but there can be more complicated things). The application contains one Unity Update() method in AppControllerModes and calls OnUpdate() method in it. That is to be possible to optimize the game when the Update of the calculation overflows, for making stable FPS, and for ordering Update methods. Ordered calling is also a reason to use the controlled call of the custom Init() method of each controller instead of Awake().

The application uses the new Unity input system to be able to handle controllers for easier porting on consoles. Look at "EventSystem" game object in main scene, there are PlayerInput component that holds assets with configured input actions and triggers methods in the InputController component on the AppController gameObject.

Storage system implemented in AppControllerStorage and particularly in IStorage classes. Few saving methods are commented in the ZenjectInstaller class.

AppControllerSound and AppControllerVFX control sound and VFX respectively.

Also, pillars are static and occlusion is baked.

URP config in the root folder.

Assets/plugins used: Cinemachine, Universal RP, TextMeshPro, Zenject, Input System. 
Art from CardHouse: https://assetstore.unity.com/packages/tools/game-toolkits/cardhouse-264045
Sounds from: https://sonniss.com/gameaudiogdc
