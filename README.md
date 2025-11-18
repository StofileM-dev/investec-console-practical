This project fetches all Star Wars characters from SWAPI (including pagination), loads all related films once, and groups people who appear in the exact same set of films.

Structure:

Investec		-> Entry point (Program.cs)
Domain			-> Entity models (Person, Film, Friend)
Application		-> Buddy grouping logic
Infrastructure	-> SWAPI HTTP calls (people + films)

Approach:

- Fetch all paged people
- Fetch unique films once and add to lookup dictionary
- Group people by matching film sets
- Output buddies