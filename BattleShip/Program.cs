using ShipClass;
//vi använder oss av array då brädet inte behöver ha en dynamisk storlek, och då blir en array bättre då den är lite snabbare när vi vill komma åt specifika element
// O = shot but miss
// X = shot with hit, or your ship location
// - = not shot at all, or empty space
string[,] board1 = new string[8, 8];
string[,] board2 = new string[8, 8];
int p1Health;
int p2Health;
string[,] shotBoard1 = new string[8, 8];
string[,] shotBoard2 = new string[8, 8];
GameLoop();
//---------------------------------------------------------------------------------------------------------------------------------
void GameLoop()
{
    // 3 = shipcount
    p1Health = 3;
    p2Health = 3;
    WriteText("Player 1", ConsoleColor.Green);
    Thread.Sleep(1000);
    //asks player to place their ships
    (Ship, Ship, Ship) shipsP1 = PlaceShip(board1);
    List<Ship> shipListP1 = new(){
    shipsP1.Item1, shipsP1.Item2, shipsP1.Item3
};
    //sets players ship cordinates to be on their board
    foreach (Ship instance in shipListP1)
    {
        foreach (var cord in instance.Cords)
        {
            board1[cord.Item1, cord.Item2] = "X";
        }
    }
    //shows board
    DisplayBoard(board1);
    Thread.Sleep(2500);
    Console.Clear();

    //asks player to place their ships
    WriteText("Player 2", ConsoleColor.Green);
    Thread.Sleep(1000);
    (Ship, Ship, Ship) shipsP2 = PlaceShip(board2);
    List<Ship> shipListP2 = new(){
    shipsP2.Item1, shipsP2.Item2, shipsP2.Item3
};
    //sets players ship cordinates to be on their board
    foreach (Ship instance2 in shipListP2)
    {
        foreach (var cord2 in instance2.Cords)
        {
            board2[cord2.Item1, cord2.Item2] = "X";
        }
    }
    //shows board
    DisplayBoard(board2);
    Thread.Sleep(2500);
    Console.Clear();

    //gameloop, shooting 
    while (true)
    {
        Console.Clear();
        //player 1 shoots, 
        //method returns 2 boards, one where we display where we shot, 1 updated defending board, and health
        (string[,], string[,], int) p1Shot = ShootPhase(shipListP2, board2, shotBoard1, "player1", p2Health);
        board2 = p1Shot.Item1;
        shotBoard1 = p1Shot.Item2;
        p2Health = p1Shot.Item3;

        //Has player 2 lost all their ships?
        if (p2Health <= 0)
        {
            WriteText("Player 1 has won!", ConsoleColor.Green);
            WriteText("Final Board:     ", ConsoleColor.Green);
            DisplayBoard(board1);
            break;
        }
        Console.Clear();
        //player 2 shoots, 
        //method returns 2 boards, one where we display where we shot, 1 updated defending board, and health
        (string[,], string[,], int) P2Shot = ShootPhase(shipListP1, board1, shotBoard2, "player2", p1Health);
        board1 = P2Shot.Item1;
        shotBoard2 = P2Shot.Item2;
        p1Health = P2Shot.Item3;

        //has player 1 lost their ships?
        if (p1Health <= 0)
        {
            WriteText("Player 2 has won!", ConsoleColor.Green);
            WriteText("Final Board:     ", ConsoleColor.Green);
            DisplayBoard(board2);
            break;
        }
    }
    WriteText("Game Finished!", ConsoleColor.Red);
    Thread.Sleep(15000);
}
//---------------------------------------------------------------------------------------------------------------------------------
static void DisplayBoard(string[,] board)
{
    //function that displays board.
    // - = nothing, X = hit, O = miss
    for (int i = 0; i < board.GetLength(0); i++)
    {
        for (int j = 0; j < board.GetLength(1); j++)
        {
            if (board[i, j] == null)
            {
                Console.Write("- ");
            }
            else if (board[i, j] == "X")
            {
                Console.Write("X ");
            }
            else
            {
                Console.Write("O ");
            }
        }
        Console.WriteLine();
    }
}
//---------------------------------------------------------------------------------------------------------------------------------
static (Ship, string[,]) ShipGenerator(string[,] board, int shipType)
{
    //generates a ship bases on player input
    //first asks player for cords (tryshipinput), sets those to be a ship on board, ask rotation, adds more parts to ship (possibly)
    (int, int) shipCords = TryShipInput(1, 8, board, false);
    board[shipCords.Item1, shipCords.Item2] = "X";
    string rotation = RotationInput();
    Ship Ship = new Ship(shipType, rotation, (shipCords.Item1, shipCords.Item2));
    return (Ship, board);
}
//-----------------------------------------------------------------------------------------------------------------
static void WriteText(string text, ConsoleColor color)
{
    //converts text into colored text
    Console.ForegroundColor = color;
    Console.WriteLine(text);
    Console.ResetColor();
}
//---------------------------------------------------------------------------------------------------------------------------------
static (Ship, Ship, Ship) PlaceShip(string[,] board)
{
    //creates 3 ships for a player
    //shows players board, uses shipgenerator to create a ship based on input,
    //uses temporary board to make sure player does not place ships on another ship
    (Ship, string[,]) returned;
    Ship newShip;
    List<Ship> ships = new();

    for (int i = 3; i > 0; i--)
    {
        DisplayBoard(board);
        WriteText($"Place your ship(size: {i}), Write a row(1-8) and a column (1-8) where you want the center of your ship to be", ConsoleColor.Green);
        returned = ShipGenerator(board, i);
        newShip = returned.Item1;
        ships.Add(newShip);
        board = returned.Item2;
        Thread.Sleep(1000);
        Console.Clear();
    }

    Thread.Sleep(1000);
    return (ships[0], ships[1], ships[2]);
}
//---------------------------------------------------------------------------------------------------------------------------------
static string RotationInput()
{
    string rotation;
    //makes player write in a rotation vertical or horizontal
    do
    {
        Console.WriteLine("What rotation should the ship be (Vertical=(v) or Horizontal=(h))");
        rotation = Console.ReadLine() ?? "";
    } while (rotation != "v" && rotation != "h");

    return rotation;
}
//---------------------------------------------------------------------------------------------------------------------------------
static (int, int) TryShipInput(int minValue, int maxValue, string[,] board, bool shooting)
{
    int value;
    string input;
    char[] inputDigits;
    int value1;
    int value2;
    //here we ask player to type in a value,
    //player input is made sure to be two characters, and it is an integer inside of the board.
    while (true)
    {
        do
        {
            WriteText($"Write a value  {minValue} through {maxValue} for a row and a column (ex 37)", ConsoleColor.Green);
            input = Console.ReadLine() ?? "";
            //checks if it is an int
        } while (!int.TryParse(input, out value));

        inputDigits = input.ToCharArray();
        //checks if it was just two numbers
        if (inputDigits.Length == 2)
        {
            value1 = inputDigits[0] - '0' - 1;
            Console.WriteLine(value1);
            value2 = inputDigits[1] - '0' - 1;
            Console.WriteLine(value2);
            Thread.Sleep(2500);
            //checks if value was inside the board
            if (value1 >= 0 && value2 >= 0 && value1 <= maxValue - 1 && value2 <= maxValue - 1)
            {
                //is player shooting is checked, if it isnt, we need to make sure cordinates arent filled by another ship
                if (shooting != true)
                {
                    if (board[value1, value2] == null)
                    {
                        break;
                    }
                    else
                    {
                        WriteText("You already have a ship touching this position!!!", ConsoleColor.Red);
                    }
                }
                else
                {
                    break;
                }

            }
            else
            {
                Console.WriteLine("Write a valid number (11-88)");
            }
        }
    }
    (int, int) values = (value1, value2);
    return values;


}
//---------------------------------------------------------------------------------------------------------------------------------

(string[,], string[,], int) ShootPhase(List<Ship> defendingShips, string[,] defendingBoard, string[,] hitBoard, string shootingPlayer, int defendingHealth)
{
    //displays board and asks where player wishes to shoot
    DisplayBoard(hitBoard);
    WriteText(shootingPlayer + ":    where do you wish to shoot?", ConsoleColor.Green);
    //makes sure the given value is a possible position
    (int, int) shot = TryShipInput(1, 8, hitBoard, true);

    //here we check if a ship is on given position, and then marks the defending board and shot board depending on if a hit or not
    if (defendingBoard[shot.Item1, shot.Item2] == "X")
    {
        defendingBoard[shot.Item1, shot.Item2] = "O";
        Console.WriteLine("Hit!");
        hitBoard[shot.Item1, shot.Item2] = "X";
        foreach (Ship ship in defendingShips)
        {
            //Checks if the ship has any more cordinates (hp) if not, it is sunked and player lost a ship
            foreach ((int, int) Cordinates in ship.Cords)
            {
                if (shot.Item1 == Cordinates.Item1 && shot.Item2 == Cordinates.Item2)
                {
                    ship.ShipHp -= 1;
                    if (ship.ShipHp <= 0)
                    {
                        Console.WriteLine("Sunked!");
                        defendingHealth -= 1;
                    }
                }
            }
        }

    }
    else
    {
        Console.WriteLine("Miss");
        hitBoard[shot.Item1, shot.Item2] = "O";
    }
    Console.WriteLine($"enemy ship count: {defendingHealth}");
    Thread.Sleep(1500);
    return (defendingBoard, hitBoard, defendingHealth);

}
//---------------------------------------------------------------------------------------------------------------------------------
