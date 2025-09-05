# Tetris Mania 🎮

A Unity 2D puzzle game inspired by Block Crush. Players place blocks on an 8x8 grid, clear lines, and chase high scores. Built for mobile (iOS & Android) with rewarded ads, interstitials, and light IAP.

---

## Features
- 8x8 grid block placement
- Score combos for multiple clears
- Game Over & Restart flow with one-shot revive
- Rewarded Ads and interstitial gating
- IAP: No-Ads pack, starter pack
- EditMode tests for core systems

---

## Project Structure
```
Assets/
├── Scripts/
│   ├── AdManager.cs
│   ├── AnalyticsStub.cs
│   ├── BlockShape.cs
│   ├── BoardGrid.cs
│   ├── GameManager.cs
│   ├── IAdManager.cs
│   ├── IAPManager.cs
│   ├── IIAPManager.cs
│   ├── PieceSpawner.cs
│   ├── PlacementValidator.cs
│   ├── SaveSystem.cs
│   ├── ScoreManager.cs
│   ├── UIController.cs
│   └── UnityStubs.cs
└── Tests/
    ├── BoardGridTests.cs
    ├── GameFlowTests.cs
    └── ScoreManagerTests.cs
```

## Development

### Requirements
- Unity 2022.3 LTS
- .NET SDK for running tests via command line

### How to Run
1. Clone repo
   ```bash
   git clone https://github.com/ClaireG90/tetris-mania.git
   ```
2. Open in Unity Hub → Unity 2022.3 LTS
3. Press Play to test

### How to Test
- Via command line: `dotnet test`
- Or open Unity Test Runner (EditMode) and run all tests under `Assets/Tests`

---

### Codex Setup
- Repo includes `AGENTS.md` for guidance
- Assign tasks via ChatGPT Codex or CLI

### License
MIT (update if needed)
