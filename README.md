# Tetris Mania 🎮

A Unity 2D puzzle game inspired by Block Crush. Players place blocks on an 8x8 grid, clear lines, and chase high scores. Built for mobile (iOS & Android) with rewarded ads, interstitials, and light IAP.

---

## Features
- 8x8 grid block placement
- Score combos for multiple clears
- Game Over & Restart flow
- Rewarded Ads (revive, x2 coins, daily chest)
- Interstitial Ads (after game over, limited)
- IAP: No-Ads pack, starter pack, coin bundles
- Future roadmap: daily challenges, missions, skins, leaderboards

---

## Project Structure
Assets/
├── Scripts/
│ ├── BoardGrid.cs
│ ├── PieceSpawner.cs
│ ├── PlacementValidator.cs
│ ├── ScoreManager.cs
│ ├── GameManager.cs
│ ├── AdManager.cs
│ └── IAPManager.cs
└── Tests/
├── BoardGridTests.cs
├── ScoreManagerTests.cs
└── GameFlowTests.cs


## Development

### Requirements
- Unity 2022.3 LTS
- .NET 4.x Equivalent scripting runtime

### How to Run
Clone repo  
   ```bash
   git clone https://github.com/ClaireG90/tetris-mania.git
2. Open in Unity Hub → Unity 2022.3 LTS
3. Press Play to test

###How to Test
- Use Unity Test Runner (EditMode)
- Tests live under Assets/Tests

###Codex Setup

- Repo includes AGENTS.md for Codex guidance
- Assign tasks via ChatGPT Codex or CLI, e.g.:
    codex exec "create BoardGrid.cs with row/column clear logic and matching tests"
	
###License
MIT (update if needed)