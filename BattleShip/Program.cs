//vi använder oss av array då brädet inte behöver ha en dynamisk storlek, och då blir en array bättre då den är lite snabbare när vi vill komma åt specifika element
using System.Reflection.PortableExecutable;

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
Console.WriteLine("Player 1");
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
Thread.Sleep(10000);
 Console.Clear();

Console.WriteLine("Player 2");
Thread.Sleep(1000);
(Ship, Ship, Ship) ShipsP2 = PlaceShip(board1);
List<Ship> ShipListP2 = new(){
    ShipsP2.Item1, ShipsP2.Item2, ShipsP2.Item3
};
foreach(Ship Instance in ShipListP2){
    foreach(var cord in Instance.Cords){
        Console.WriteLine(cord.Item1 + cord.Item2);
        board2[cord.Item1, cord.Item2] = "X";
    }
}
// Console.Clear();
FillBoard(board1);
FillBoard(board2);
FillBoard(ShotBoard1);
FillBoard(ShotBoard2);

Thread.Sleep(100000);
}
static void DisplayBoard(string[,] board){
    for (int i = 0; i < board.GetLength(0); i++){
    for(int j = 0; j < board.GetLength(1); j++){
            if(board[i,j] == null){
                Console.Write("O ");
            }else{
                Console.Write("X ");
            }
        }
        Console.WriteLine();
    }
}
static Ship ShipGenerator(string[,] board, int shiptype){
(int, int) ShipCords = TryShipInput(89, 10, board, false);
    string Rotation = RotationInput();
    Ship ship = new Ship(shiptype, Rotation, (ShipCords.Item1, ShipCords.Item2));
    // Console.Clear();
return ship;
}

static (Ship, Ship, Ship)PlaceShip(string[,] board)
{
    
Console.Clear();
    DisplayBoard(board);    
    Console.WriteLine("Place your Largest ship(3), Write a row(1-8) and a column (1-8) where you want the center of your ship to be");
    Ship Big = ShipGenerator(board, 3);

    Console.WriteLine("Place your second largest ship(2), write a row(1-8) and a column (1-8) where the left/bottom part of the ship is going to be");
    Ship Medium = ShipGenerator(board, 2);

    Console.WriteLine("Place your smallest ship(1), write a row(1-8) and a column (1-8) where you want the ship to be");
    Ship Small = ShipGenerator(board, 1);
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

static (int, int) TryShipInput(int MaxValue, int MinValue, string[,] board, bool shooting){
int Value;
string Input;
char[] InputDigits;
while(true){
do{
    Console.WriteLine($"Write a value between {MinValue} and {MaxValue}");
    Input = Console.ReadLine()?? "";

}while(!int.TryParse(Input, out Value));

if(Convert.ToInt32(Input) > MinValue && Convert.ToInt32(Input) < MaxValue){

    if(shooting != true){
        InputDigits = Input.ToCharArray();
        if(board[InputDigits[0] - '0'  -1, InputDigits[1]- '0' -1] == null){
            break;
        }else{
            Console.WriteLine("You already have a ship touching this position!!!");
        }
    }else{
        InputDigits = Input.ToCharArray();
        break;
    }

}else{
    Console.WriteLine("Write a valid number (11-88)");
}
}
(int, int) Values = (InputDigits[0] - '0' -1, InputDigits[1] - '0' -1);
return Values;


}
static string[,] FillBoard(string[,] board){
    for(int i = 0; i < board.GetLength(0); i++){
        for(int j = 0; j < board.GetLength(1); j++){
            if(board[i,j] == null){
                board[i,j] = "- ";
            }
        }
    }
return board;
}


(string[,], string[,], int) ShootPhase(List<Ship> Ships, string[,] DefendingBoard, string[,] HitBoard, string player, int defendingHealth)
{
    DisplayBoard(HitBoard);
    Console.WriteLine(player + "where do you wish to shoot?");
    (int, int) shot = TryShipInput(77, 0,HitBoard ,true);

    if(DefendingBoard[shot.Item1, shot.Item2] == "X "){
        DefendingBoard[shot.Item1, shot.Item2] = "O ";
        Console.WriteLine("Hit");
        HitBoard[shot.Item1, shot.Item2] = "X ";
        foreach(Ship ship in Ships){
            foreach((int, int) Cordinates in ship.Cords){
                if(shot.Item1 == Cordinates.Item1 && shot.Item2 == Cordinates.Item2) {
                    ship.ShipHp -= 1;
                    if(ship.ShipHp <= 0){
                        Console.WriteLine("Sunked!");
                    }
                }
            }
        }

    }else{
        Console.WriteLine("Miss");
        HitBoard[shot.Item1, shot.Item2] = "O ";
    }

return (DefendingBoard, HitBoard, defendingHealth);

}
void win()
{

}
