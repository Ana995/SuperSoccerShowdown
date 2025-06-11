# ⚽ Super Soccer Showdown

A Web API project that can generate random SuperSoccer teams of 5 player for a given universe(api) and can simulate matches.
---

## 🚀 Features

- 🧠 Clean Architecture (Domain, Application, Infrastructure, WebApi, Tests)
- 🌐 External APIs:
  - [PokeAPI](https://pokeapi.co) – Pokémon stats
  - [SWAPI](https://swapi.dev) – Star Wars characters
- 🔧 Team generation with configurable lineups
- 🤖 Positions: Goalie, Defender, Attacker
- 🏟️ Match simulator with power-based scoring highlights
- 🔁 Resilient API clients with retry & caching
- 🧪 xUnit test coverage (service + controller)

---

## 📦 Project Structure

```bash
SuperSoccerShowdown/
├── Domain/              # Core entities & enums
├── Application/         # Services & interfaces
├── Infrastructure/      # API clients & data access
├── WebApi/              # ASP.NET Core API controllers
├── Tests/               # xUnit tests
└── README.md
```
----
## 🔥 API Endpoints

**GET** /api/teams/generate

Generate a team from "pokemon" or "starwars" universe.

Query Parameters(required):
- universe: pokemon or starwars
- defenders: Number of defenders (1-3)
- attackers: Number of attackers (1-3)

_Constraints: Total defenders + attackers must equal 4 (1 goalie is always included)_

**POST** /api/teams/simulate

Simulates a match between two teams and returns goal highlights.

Sample request:
```
[
  {
    "universe": "pokemon",
    "players": [
      { "name": "Charizard", "heightCm": 170, "weightKg": 90, "position": "Goalie", "soccerPower": 95 },
      { "name": "Blastoise", "heightCm": 160, "weightKg": 100, "position": "Defence", "soccerPower": 85 },
      { "name": "Pikachu", "heightCm": 80, "weightKg": 60, "position": "Defence", "soccerPower": 70 },
      { "name": "Squirtle", "heightCm": 65, "weightKg": 40, "position": "Offence", "soccerPower": 88 },
      { "name": "Jigglypuff", "heightCm": 60, "weightKg": 30, "position": "Offence", "soccerPower": 80 }
    ]
  },
  {
    "universe": "starwars",
    "players": [
      { "name": "Darth Vader", "heightCm": 200, "weightKg": 130, "position": "Goalie", "soccerPower": 90 },
      { "name": "Chewbacca", "heightCm": 230, "weightKg": 140, "position": "Defence", "soccerPower": 85 },
      { "name": "Luke Skywalker", "heightCm": 175, "weightKg": 70, "position": "Defence", "soccerPower": 72 },
      { "name": "Han Solo", "heightCm": 180, "weightKg": 80, "position": "Offence", "soccerPower": 82 },
      { "name": "Leia Organa", "heightCm": 165, "weightKg": 50, "position": "Offence", "soccerPower": 75 }
    ]
  }
]

```
Sample Response:

```
{
  "teamA": { ... },
  "teamB": { ... },
  "highlights": [
    "Jigglypuff scored against Darth Vader",
  ]
}
```
---
## 🛠️ Setup Instructions
Requirements
- .NET 8 SDK
- Visual Studio 2022 / VSCode

✅ Run API locally
```
dotnet build
dotnet run --project SuperSoccerShowdown.WebApi
```
Open Swagger UI:
http://localhost:5000/swagger

✅ Running Tests
```
dotnet test
```
