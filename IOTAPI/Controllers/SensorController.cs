// Importerar verktyg från .NET som behövs för att bygga ett Web API
using Microsoft.AspNetCore.Mvc;

// Anger vilken grupp och mapp i projektet som koden tillhör
namespace IOTAPI.Controllers
{
    // Berättar för .NET att denna klass ska fungera som ett Web API[cite: 1]
    [ApiController]

    // Bestämmer webbadressen (URL). [controller] ersätts automatiskt med "Sensor" -> /api/sensor[cite: 1]
    [Route("api/[controller]")]

    // Skapar själva kontrollen som hanterar alla anrop från nätverket[cite: 1]
    public class SensorController : ControllerBase
    {
        // ÖVERVAKNING: En variabel som räknar hur många meddelanden vi tagit emot totalt[cite: 1]
        private static int messageCounter = 0;

        // MINNE: Sparar det senaste mottagna mätvärdet direkt i datorns minne
        private static SensorData latestData = new SensorData
        {
            SensorId = "sensor-01",     // Standard-ID på sensorn
            Timestamp = DateTime.Now,   // Tidsstämpel för när datan skapades[cite: 1]
            Value = 21.5,               // Standardvärde för temperatur[cite: 1]
            Unit = "C"                  // Enhet (Celsius)[cite: 1]
        };

        // API-ENDPOINT (GET): Körs när någon anropar webbadressen för att HÄMTA data[cite: 1]
        [HttpGet]
        public IActionResult GetLatestData()
        {
            // Svarar med statuskod 200 (OK) och skickar tillbaka datan som JSON[cite: 1]
            return Ok(new
            {
                Status = "OK",
                TotalMessagesReceived = messageCounter, // Skickar med hur många meddelanden vi räknat[cite: 1]
                Data = latestData                        // Skickar med det senaste mätvärdet[cite: 1]
            });
        }

        // API-ENDPOINT (POST): Körs när någon SKICKAR in ny sensordata till API:et[cite: 1]
        [HttpPost]
        public IActionResult PostSensorData([FromBody] SensorData incomingData)
        {
            // SÄKERHET / VALIDERING 1: Kollar om datan eller SensorId saknas[cite: 1]
            if (incomingData == null || string.IsNullOrEmpty(incomingData.SensorId))
            {
                // Om fel: Svara med felkod 400 (Bad Request) och ett felmeddelande[cite: 1]
                return BadRequest("Valideringsfel: SensorId saknas.");
            }

            // SÄKERHET / VALIDERING 2: Kollar om temperaturvärdet är orimligt[cite: 1]
            if (incomingData.Value < -50 || incomingData.Value > 100)
            {
                // Om värdet är för extremt: Svara med felkod 400 och felmeddelande[cite: 1]
                return BadRequest("Valideringsfel: Mätvärdet är orimligt.");
            }

            // ÖVERVAKNING: Ökar räknaren med 1 varje gång godkänd data kommer in[cite: 1]
            messageCounter++;

            // Sparar den nya inkommande datan som det senaste mätvärdet
            latestData = incomingData;

            // LOGGNING: Skriver ut en rad i konsolen/terminalen med tidpunkt och värde[cite: 1]
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] LOGG: Mottog mätvärde #{messageCounter} från {incomingData.SensorId}: {incomingData.Value} {incomingData.Unit}");

            // Svarar avsändaren med statuskod 200 (OK) och ett bekräftelsemeddelande[cite: 1]
            return Ok(new
            {
                Message = "Data mottagen framgångsrikt!",
                TotalMessages = messageCounter
            });
        }
    }

    // DATAKONTRAKT: Definierar strukturen för hur vår JSON-data ska se ut[cite: 1]
    public class SensorData
    {
        // Namnet/ID på sensorn (t.ex. "sensor-01")[cite: 1]
        public string SensorId { get; set; } = string.Empty;

        // Tidpunkten då mätningen gjordes[cite: 1]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Själva mätvärdet (t.ex. 22.5)[cite: 1]
        public double Value { get; set; }

        // Enheten för mätvärdet (t.ex. "C" för Celsius)[cite: 1]
        public string Unit { get; set; } = "C";
    }
}   