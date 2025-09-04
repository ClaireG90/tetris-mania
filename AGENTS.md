# AGENTS.md – Tetris Mania

## Project
Unity 2D puzzle game inspired by Block Crush. Players place blocks on an 8x8 grid, clear lines, and chase high scores. Game includes ads (rewarded + interstitial) and light IAP (no-ads pack, starter pack, coin bundles).

## Environment
- Unity 2022.3 LTS
- Scripts in `Assets/Scripts`
- Tests in `Assets/Tests`
- Coding style: PascalCase for classes/public; private fields `_camelCase`
- Null checks before usage, avoid async void

## Rules
- Gameplay loop should be short and addictive (< 60s typical round).
- Rewarded Ads:
  - Revive once per game
  - Double coins after game over
  - Daily chest unlock
- Interstitial Ads:
  - Only after game over
  - Never in first 3 sessions
- IAP:
  - `No-Ads` pack removes interstitials but keeps rewarded
  - Starter pack ($1.99) with coins + skin
  - Coin bundles ($0.99 → $19.99)

## Deliverables
- Write code in small, testable components
- All core systems (line clears, scoring, game over) must have EditMode tests
- Document public methods with XML comments
- Group scripts logically (BoardGrid, PieceSpawner, PlacementValidator, ScoreManager, AdManager, IAPManager, GameManager)

## Future Features (for backlog)
- Daily challenge
- Missions/quests
- Skins/themes
- Leaderboards (weekly reset)
- Seasonal events