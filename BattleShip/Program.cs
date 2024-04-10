//vi använder oss av array då brädet inte behöver ha en dynamisk storlek, och då blir en array bättre då den är lite snabbare när vi vill komma åt specifika element
// O = shot but miss
// X = shot with hit, or your ship location
// - = not shot at all, or empty space
string[,] board1 = new string[8, 8];
string[,] board2 = new string[8, 8];
int p1Health;
int p2Health;
string[,] ShotBoard1 = new string[8, 8];
string[,] ShotBoard2 = new string[8, 8];
GameLoop();

// PlaceShip(board1);
void Menu()
{
}
void GameLoop()
{
    p1Health = 3;
    p2Health = 3;
writeText("Player 1", "Green");
Thread.Sleep(1000);
(Ship, Ship, Ship) ShipsP1 = PlaceShip(board1);
List<Ship> ShipListP1 = new(){
    ShipsP1.Item1, ShipsP1.Item2, ShipsP1.Item3
};
foreach(Ship Instance in ShipListP1){
    foreach(var cord in Instance.Cords){
        board1[cord.Item1, cord.Item2] = "X";
    }
}
DisplayBoard(board1);
Thread.Sleep(2500);
 Console.Clear();

writeText("Player 2", "Green");
Thread.Sleep(1000);
(Ship, Ship, Ship) ShipsP2 = PlaceShip(board2);
List<Ship> ShipListP2 = new(){
    ShipsP2.Item1, ShipsP2.Item2, ShipsP2.Item3
};
foreach(Ship Instance2 in ShipListP2){
    foreach(var cord2 in Instance2.Cords){
        board2[cord2.Item1, cord2.Item2] = "X";
    }
}
DisplayBoard(board2);
Thread.Sleep(2500);
Console.Clear();

while(true){
Console.Clear();
(string[,], string[,], int) P1Shot = ShootPhase(ShipListP2, board2, ShotBoard1, "player1", p2Health);
board2 = P1Shot.Item1;
ShotBoard1 = P1Shot.Item2;
p2Health = P1Shot.Item3;

    if(p2Health <= 0){
        writeText("Player 1 has won!", "Green");
        writeText("Final Board:     ", "Green");
        DisplayBoard(board1);
        break;
    }  
Console.Clear(); 
(string[,], string[,], int) P2Shot = ShootPhase(ShipListP1, board1, ShotBoard2, "player2", p1Health);
board1 = P2Shot.Item1;
ShotBoard2 = P2Shot.Item2;
p1Health = P2Shot.Item3;

    if(p1Health <= 0){
        writeText("Player 2 has won!", "Green");
        writeText("Final Board:     ", "Green");
        DisplayBoard(board2);
        break;
    } 
}
writeText("Game Finished!", "Red");
Thread.Sleep(15000);
}
static void DisplayBoard(string[,] board){
    for (int i = 0; i < board.GetLength(0); i++){
    for(int j = 0; j < board.GetLength(1); j++){
            if(board[i,j] == null){
                Console.Write("- ");
            }else if(board[i,j] == "X"){
                Console.Write("X ");
            }else{
                Console.Write("O ");
            }
        }
        Console.WriteLine();
    }
}
static Ship ShipGenerator(string[,] board, int shiptype){
(int, int) ShipCords = TryShipInput(1, 8, board, false);
    string Rotation = RotationInput();
    Ship ship = new Ship(shiptype, Rotation, (ShipCords.Item1, ShipCords.Item2));
    // Console.Clear();
return ship;
}
static void writeText(string text, string color){
    if(color == "Red"){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    if(color == "Green"){
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}  

static (Ship, Ship, Ship)PlaceShip(string[,] board)
{
    
    DisplayBoard(board);    
    writeText("Place your Largest ship(3), Write a row(1-8) and a column (1-8) where you want the center of your ship to be", "Green");
    Ship Big = ShipGenerator(board, 3);
    Thread.Sleep(1000);
    Console.Clear();

    DisplayBoard(board);  
    writeText("Place your second largest ship(2), write a row(1-8) and a column (1-8) where the left/bottom part of the ship is going to be", "Green");
    Ship Medium = ShipGenerator(board, 2);
    Thread.Sleep(1000);
    Console.Clear();

    DisplayBoard(board);  
    writeText("Place your smallest ship(1), write a row(1-8) and a column (1-8) where you want the ship to be", "Green");
    Ship Small = ShipGenerator(board, 1);
    Thread.Sleep(1000);
    return (Big, Medium, Small);
}


static string RotationInput(){
    string rotation;
    do{
        Console.WriteLine("What rotation should the ship be (Vertical=(v) or Horizontal=(h))");
        rotation = Console.ReadLine()?? "";
    }while(rotation != "v" && rotation != "h");

    return rotation;
}

static (int, int) TryShipInput(int MinValue, int MaxValue, string[,] board, bool shooting){
int Value;
string Input;
char[] InputDigits;
int value1;
int value2;
while(true){
do{
    writeText($"Write a value  {MinValue} through {MaxValue} for a row and a column (ex 37)", "Green");
    Input = Console.ReadLine()?? "";

}while(!int.TryParse(Input, out Value));

InputDigits = Input.ToCharArray();
value1 = InputDigits[0] - '0' -1;
value2 = InputDigits[1] - '0' -1;
if(value1 >= 0 && value2 >= 0 && value1 <= MaxValue-1 && value2 <= MaxValue-1){

    if(shooting != true){
        if(board[value1, value2] == null){
            break;
        }else{
            writeText("You already have a ship touching this position!!!", "Red");
        }
    }else{
        break;
    }

}else{
    Console.WriteLine("Write a valid number (11-88)");
}
}
(int, int) Values = (value1, value2);
return Values;


}


(string[,], string[,], int) ShootPhase(List<Ship> DefendingShips, string[,] DefendingBoard, string[,] HitBoard, string ShootingPlayer, int defendingHealth)
{
    DisplayBoard(HitBoard);
    writeText(ShootingPlayer + ":    where do you wish to shoot?", "Green");
    (int, int) shot = TryShipInput(1, 8,HitBoard ,true);

    if(DefendingBoard[shot.Item1, shot.Item2] == "X"){
        DefendingBoard[shot.Item1, shot.Item2] = "O";
        Console.WriteLine("Hit!");
        HitBoard[shot.Item1, shot.Item2] = "X";
        foreach(Ship ship in DefendingShips){
            foreach((int, int) Cordinates in ship.Cords){
                if(shot.Item1 == Cordinates.Item1 && shot.Item2 == Cordinates.Item2) {
                    ship.ShipHp -= 1;
                    if(ship.ShipHp <= 0){
                        Console.WriteLine("Sunked!");
                        defendingHealth -= 1;
                    }
                }
            }
        }

    }else{
        Console.WriteLine("Miss");
        HitBoard[shot.Item1, shot.Item2] = "O";
    }
Console.WriteLine($"enemy ship count: {defendingHealth}");
Thread.Sleep(1500);
return (DefendingBoard, HitBoard, defendingHealth);

}
void win()
{

}
