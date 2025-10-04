# Unity Snake and Ladder Game

An interactive Snake and Ladder board game built with Unity and C#. Features multiplayer support, smooth animations, sound effects, and a complete UI system.

## 🎮 Features

- **Multiplayer Support**: 2-4 players can play together
- **Interactive Dice**: Animated dice rolling with sound effects
- **Smooth Animations**: Player movement with bounce effects and celebrations
- **Complete UI System**: Main menu, pause menu, settings, and win screen
- **Audio Management**: Background music and sound effects
- **Visual Effects**: Particle effects and player animations
- **Auto-Generated Board**: Procedurally generated board with snakes and ladders

## 🚀 Getting Started

### Prerequisites
- Unity 2021.3 LTS or later
- Basic knowledge of Unity Editor

### Installation
1. Clone this repository:
   ```bash
   git clone https://github.com/Kranti-Pa/unity-snake-ladder-game.git
   ```
2. Open the project in Unity Hub
3. Open the main scene and press Play

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── GameManager.cs          # Main game logic and flow
│   ├── DiceController.cs       # Dice rolling mechanics
│   ├── BoardGenerator.cs       # Board creation and layout
│   ├── PlayerController.cs     # Player movement and animations
│   ├── UIManager.cs           # User interface management
│   └── AudioManager.cs        # Sound and music management
├── Prefabs/                   # Game object prefabs
├── Materials/                 # Visual materials
├── Audio/                     # Sound effects and music
└── Sprites/                   # UI and game sprites
```

## 🎯 Game Rules

1. **Objective**: Be the first player to reach square 100
2. **Movement**: Roll the dice to move your piece
3. **Snakes**: Landing on a snake's head sends you down to its tail
4. **Ladders**: Landing on a ladder's bottom takes you up to its top
5. **Winning**: First player to reach or exceed square 100 wins
6. **Exact Landing**: If you roll past 100, you bounce back

## 🎮 Controls

- **Mouse**: Click the "Roll Dice" button to play
- **Escape**: Pause/Resume game
- **UI Buttons**: Navigate through menus

## 🔧 Customization

### Adding More Snakes and Ladders
Edit the dictionaries in `GameManager.cs`:
```csharp
private Dictionary<int, int> snakes = new Dictionary<int, int>()
{
    {99, 78}, {95, 75}, // Add more snake positions
};

private Dictionary<int, int> ladders = new Dictionary<int, int>()
{
    {1, 38}, {4, 14}, // Add more ladder positions
};
```

### Changing Player Colors
Modify the `PlayerController.cs` script or set colors in the inspector:
```csharp
public Color playerColor = Color.red; // Change default color
```

### Board Customization
Adjust board settings in `BoardGenerator.cs`:
```csharp
public int boardSize = 100;        // Change board size
public float tileSpacing = 1f;     // Adjust tile spacing
public Color lightTileColor;       // Customize tile colors
```

## 🎵 Audio Setup

1. Add audio clips to the AudioManager component
2. Assign clips in the inspector:
   - Background music
   - Dice roll sound
   - Player move sound
   - Snake bite sound
   - Ladder climb sound
   - Win sound

## 🎨 Visual Setup

### Required Prefabs
- **Tile Prefab**: Basic board tile (cube with collider)
- **Snake Prefab**: Visual representation of snakes
- **Ladder Prefab**: Visual representation of ladders
- **Player Pieces**: Different colored pieces for each player

### Materials
- Snake material (red/green)
- Ladder material (brown/yellow)
- Player materials (different colors)

## 🏗️ Building the Game

1. Go to **File > Build Settings**
2. Add your scene to the build
3. Select your target platform
4. Click **Build** or **Build and Run**

## 🐛 Troubleshooting

### Common Issues
- **Missing References**: Ensure all public fields in scripts are assigned in the inspector
- **Audio Not Playing**: Check if AudioSource components are attached and audio clips are assigned
- **UI Not Responding**: Verify Canvas and EventSystem are present in the scene
- **Players Not Moving**: Check if board positions array is properly populated

### Performance Tips
- Use object pooling for particle effects
- Optimize texture sizes for mobile builds
- Consider using Unity's Addressable system for larger projects

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🎯 Future Enhancements

- [ ] Online multiplayer support
- [ ] Different board themes
- [ ] Power-ups and special tiles
- [ ] Tournament mode
- [ ] Mobile touch controls
- [ ] Save/Load game state
- [ ] Statistics and achievements
- [ ] Custom board editor

## 📞 Support

If you encounter any issues or have questions:
1. Check the [Issues](https://github.com/Kranti-Pa/unity-snake-ladder-game/issues) page
2. Create a new issue with detailed description
3. Include Unity version and platform information

---

**Happy Gaming! 🎲🐍🪜**