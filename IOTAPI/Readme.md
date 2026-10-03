# IOTAPI - Sensordata Web API

Ett RESTful Web API byggt i ASP.NET Core (.NET 8) för att hantera, validera och övervaka inkommande sensordata från IoT-enheter.

## Funktioner & Endpoints
- **GET `/api/Sensor`**: Hämtar det senaste mottagna mätvärdet samt visar övervakningsdata (totalt antal mottagna meddelanden).
- **POST `/api/Sensor`**: Tar emot ny sensordata i JSON-format.
  - **Validering**: Returnerar `400 Bad Request` om `SensorId` saknas eller om mätvärdet ligger utanför -50 till 100 °C.
  - **Loggning & Övervakning**: Loggar varje godkänt anrop till konsolen och räknar upp meddelanderäknaren.

## Hur man kör projektet
1. Öppna lösningen i Visual Studio 2022.
2. Starta projektet med `F5` eller den gröna **https**-knappen.
3. Testgränssnittet (Swagger UI) öppnas automatiskt i webbläsaren.