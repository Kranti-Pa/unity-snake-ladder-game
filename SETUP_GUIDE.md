# Quick Setup Guide

## 🚀 Getting Started in Unity

### Step 1: Create Basic Scene Setup

1. **Create a new Unity 3D scene**
2. **Add these GameObjects to your scene:**

#### Main Game Objects:
```
Scene Hierarchy:
├── Main Camera (add CameraController script)
├── GameManager (Empty GameObject + GameManager script)
├── DiceController (Empty GameObject + DiceController script)
├── BoardGenerator (Empty GameObject + BoardGenerator script)
├── AudioManager (Empty GameObject + AudioManager script)
├── Canvas (UI Canvas)
│   ├── MainMenuPanel
│   │   ├── PlayButton
│   │   ├── SettingsButton
│   │   └── QuitButton
│   ├── GameUIPanel
│   │   ├── RollDiceButton
│   │   ├── DiceResultText
│   │   ├── CurrentPlayerText
│   │   └── GameStatusText
│   └── WinPanel
│       └── WinnerText
└── Players
    ├── Player1 (Cube + PlayerController script)
    ├── Player2 (Cube + PlayerController script)
    ├── Player3 (Cube + PlayerController script)
    └── Player4 (Cube + PlayerController script)
```

### Step 2: Create Required Prefabs

#### Tile Prefab:
1. Create a **Cube** primitive
2. Scale it to (1, 0.1, 1)
3. Add a **Collider** component
4. Save as prefab: `Assets/Prefabs/TilePrefab.prefab`

#### Snake Prefab:
1. Create a **Cylinder** primitive
2. Scale it to (0.2, 1, 0.2)
3. Set material color to red/green
4. Save as prefab: `Assets/Prefabs/SnakePrefab.prefab`

#### Ladder Prefab:
1. Create a **Cylinder** primitive
2. Scale it to (0.1, 1, 0.1)
3. Set material color to brown/yellow
4. Save as prefab: `Assets/Prefabs/LadderPrefab.prefab`

### Step 3: Configure Scripts in Inspector

#### GameManager Configuration:
- **Number Of Players**: 2
- **Board Size**: 100
- **Roll Dice Button**: Assign from Canvas
- **Dice Result Text**: Assign from Canvas
- **Current Player Text**: Assign from Canvas
- **Game Status Text**: Assign from Canvas
- **Win Panel**: Assign from Canvas
- **Winner Text**: Assign from Canvas
- **Player Pieces**: Assign Player1, Player2, etc.
- **Dice Controller**: Assign DiceController GameObject

#### BoardGenerator Configuration:
- **Tile Prefab**: Assign TilePrefab
- **Snake Prefab**: Assign SnakePrefab
- **Ladder Prefab**: Assign LadderPrefab
- **Board Size**: 100
- **Tile Spacing**: 1

#### DiceController Configuration:
- **Dice Sprites**: Create 6 sprites for dice faces (1-6)
- **Dice Image**: Assign UI Image component
- **Roll Duration**: 1
- **Roll Sound**: Assign audio clip (optional)

### Step 4: UI Setup

#### Canvas Settings:
- **Render Mode**: Screen Space - Overlay
- **Canvas Scaler**: Scale With Screen Size
- **Reference Resolution**: 1920x1080

#### Button Setup:
- Create UI Buttons for Roll Dice, Play, Settings, etc.
- Assign button references in respective script components

#### Text Setup:
- Create UI Text elements for game status, player info, etc.
- Assign text references in GameManager

### Step 5: Player Setup

#### Player GameObjects:
1. Create **Cube** primitives for each player
2. Add **PlayerController** script to each
3. Set different colors for each player:
   - Player 1: Red
   - Player 2: Blue
   - Player 3: Green
   - Player 4: Yellow

### Step 6: Camera Setup

#### Main Camera:
- Add **CameraController** script
- Set **Board Center** to an empty GameObject at board center (5, 0, 5)
- **Focus On Board**: True
- **Board View Height**: 12

## 🎮 Testing Your Game

1. **Press Play** in Unity Editor
2. **Click Roll Dice** to test basic functionality
3. **Check Console** for any error messages
4. **Verify** player movement and UI updates

## 🔧 Common Issues & Solutions

### Scripts Won't Compile:
- Ensure all using statements are present
- Check for missing semicolons
- Verify Unity version compatibility (2022.3 LTS recommended)

### Missing References:
- All public fields in scripts must be assigned in Inspector
- Create UI elements and assign them to script references
- Ensure prefabs are created and assigned

### Players Not Moving:
- Check if BoardGenerator has created board positions
- Verify GameManager has board positions array populated
- Ensure player pieces are assigned in GameManager

### UI Not Working:
- Verify Canvas has EventSystem component
- Check button OnClick events are properly assigned
- Ensure UI elements are active in hierarchy

## 🎯 Quick Test Checklist

- [ ] Scripts compile without errors
- [ ] Board generates with numbered tiles
- [ ] Dice button works and shows random numbers
- [ ] Players move when dice is rolled
- [ ] Snake and ladder mechanics work
- [ ] Win condition triggers properly
- [ ] UI updates correctly

## 📝 Next Steps

Once basic functionality works:
1. Add audio clips for sound effects
2. Create dice face sprites
3. Improve visual materials and textures
4. Add particle effects
5. Test multiplayer functionality
6. Build and deploy your game!

---

**Need Help?** Check the main README.md for detailed documentation and troubleshooting tips.