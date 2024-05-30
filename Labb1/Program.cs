using System.Numerics;

// Globals
Random random = new Random();
bool quit = false;
bool randomOn = false;
bool countShortNumbers = true;

while (!quit)
{
    Console.WriteLine("Välj ett alternativ:\n1. Skriv en egen text\n2. Slumpmässig text\n3. Räkna ## tal = " + countShortNumbers + "\n\n0. Avsluta");
    switch (Console.ReadLine())
    {
        case "0":
            quit = true;
            break;
        case "1":
            Console.Clear();
            Console.WriteLine("Skriv en text:");
            Labb1(Console.ReadLine());
            break;
        case "2":
            Console.Clear();
            randomOn = true;
            Labb1(RandomString());
            break;
        case "3":
            Console.Clear();
            if (countShortNumbers)
                countShortNumbers = false;
            else
                countShortNumbers = true;
            break;
        default:
            Console.Clear();
            randomOn = true;    // Bara tryck enter för en ny random text (Ta bort)
            Labb1(RandomString());
            break;
    }
    randomOn = false;
}

//---------------------------------------------------------------------------------------------------------------------------------
// Labb1
//---------------------------------------------------------------------------------------------------------------------------------
void Labb1(string text)
{
    // Om vi får en slumpmässig text, skriv ut den först
    if (randomOn)
    {
        Console.WriteLine(text);
    }

    Console.WriteLine();    // Ny rad

    int numberIndex = -1;       // Start index för siffran
    int numberIndexEnd = -1;    // Slut index för siffran
    int numbersFound = 0;       // Antal nummer vi har hittat totalt
    string numberText = "";     // Nummer omvandlat till text

    BigInteger totalSumBIG = 0; // Datatyp för att hålla, i teorin, ett hur stort tal som helst
    //long totalSum = 0;

    // Loopa igenom alla tecken i texten
    for (int currentChar = 0; currentChar < text.Length; currentChar++)
    {
        // Om det nuvarande tecknet är en siffra
        if (Char.IsDigit(text[currentChar]))
        {
            // Spara vilket index siffran har
            numberIndex = currentChar;

            // Loopa igenom resten av texten från och med nästa tecken
            for (int nextChar = currentChar + 1; nextChar < text.Length; nextChar++)
            {
                // Om ett tecken inte är en siffra avbryter vi andra loopen
                if (!Char.IsDigit(text[nextChar]))
                {
                    break;
                }
                // Annars om samma siffra uppstår igen
                else if (text[nextChar] == text[currentChar])
                {
                    // Spara slutindexet av talet (+1 för att inkludera sista siffran i talet)
                    numberIndexEnd = nextChar + 1;

                    // Om talet bara är 2 siffror långt & vi inte vill räkna med det, avbryt andra loopen
                    if (numberIndexEnd - numberIndex == 2 && !countShortNumbers)
                    {
                        break;
                    }

                    // Om vi kör slumpmässig text vill vi självklart ha slumpmässiga färger också
                    if (randomOn)
                    {
                        TextColor(text, numberIndex, numberIndexEnd, (ConsoleColor)random.Next(0, 16));
                    }
                    // Annars färga texten röd som standard
                    else
                    {
                        TextColor(text, numberIndex, numberIndexEnd, ConsoleColor.Red);
                    }

                    // Spara talet i en egen sträng
                    numberText = text.Substring(numberIndex, numberIndexEnd - numberIndex);
                    // Konvertera strängen & plussa på värdet i den totala summan
                    totalSumBIG += BigInteger.Parse(numberText);
                    // Öka antalet hittade nummer med +1
                    numbersFound++;
                    // Bryt ur andra loopen och återgå till första loopen
                    break;
                }
            }
        }
    }

    Console.WriteLine();    // Ny rad
    Console.WriteLine("Antal = " + numbersFound);   // Antal hittade nummer
    Console.WriteLine("Total = " + totalSumBIG);    // Totala summan
    Console.WriteLine();    // Ny rad
}

//---------------------------------------------------------------------------------------------------------------------------------
// Color
//---------------------------------------------------------------------------------------------------------------------------------
void TextColor(string text, int start, int end, ConsoleColor color)
{
    // Loopa igenom alla tecken i texten
    for (int i = 0; i < text.Length; i++)
    {
        // Om det nuvarande tecknet är inom ramen för 'start' indexet && inte gått förbi 'end' indexet, färga texten.
        if (i >= start && i < end)
        {
            // Om textfärgen är svår att se i konsolen, ändra bakgrundsfärgen
            switch (color)
            {
                case ConsoleColor.Black:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case ConsoleColor.Gray:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case ConsoleColor.Yellow:
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case ConsoleColor.White:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    break;
            }
            // Sätt färgen & skriv ut texten
            Console.ForegroundColor = color;
            Console.Write(text[i]);
            Console.ResetColor();   // Både för- & bakgrundsfärger återställs
        }
        // Annars skriv ut tecknet som vanligt
        else
        {
            Console.Write(text[i]);
        }
    }

    Console.WriteLine();    // Gör en ny rad
}

//---------------------------------------------------------------------------------------------------------------------------------
// Random
//---------------------------------------------------------------------------------------------------------------------------------
string RandomString()
{
    // Skapa en ny char array med 25-80 element i sig
    char[] randomChars = new char[random.Next(25, 80 + 1)];

    // Super duper kreativ lösning för att öka sannolikheten av slumpmässiga siffror
    char[] textChars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                         //'!', '"', '#', '¤', '%', '&', '/', '(', ')', '=', '+', '-', ':', ';', '@', '£', '$', '€', ' ',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    // För varje char i 'randomChars', sätt värdet till ett slumpmässigt valt tecken från 'textChars'
    for (int i = 0; i < randomChars.Length; i++)
    {
        randomChars[i] = textChars[random.Next(0, textChars.Length)];
    }

    // Skapa en ny text sträng av 'randomChars'
    string text = new string(randomChars);

    // Returnera texten
    return text;
}


// Test strängar
//string text1 = "29535123p48723487597645723645";   // Från uppgiftsbeskrivningen
//string text2 = "123456789123456123456789";
//string text3 = "112233123a98988785555cdef#5/667756";
//string text4 = "0 01012580346074£9502430";       // Nollorna räknas med, men första 0 räknas inte: 010 = 10, 03460 = 3460, etc. (Låter vara så länge)
//string text5 = "32875687325872634587hcbf3289579823cd"
//string text6 = "29999352920305++29524u358442824584286"
//string text7 = "01 01012580346074#9502430 78795835"
