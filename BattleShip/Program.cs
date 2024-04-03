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
(Ship, Ship, Ship) ShipsP1 = PlaceShip(board1);
List<Ship> ShipListP1 = new(){
    ShipsP1.Item1, ShipsP1.Item2, ShipsP1.Item3
};
foreach(Ship Instance in ShipListP1){
    foreach(var cord in Instance.Cords){
        board1[cord.Item1, cord.Item2] = "X";
    }
}
// Console.Clear();

Console.WriteLine("Player 2");
(Ship, Ship, Ship) ShipsP2 = PlaceShip(board1);
List<Ship> ShipListP2 = new(){
    ShipsP2.Item1, ShipsP2.Item2, ShipsP2.Item3
};
foreach(Ship Instance in ShipListP2){
    foreach(var cord in Instance.Cords){
        board1[cord.Item1, cord.Item2] = "X";
    }
}
// Console.Clear();
FillBoard(board1);
FillBoard(board2);
FillBoard(ShotBoard1);
FillBoard(ShotBoard2);

DisplayBoard(board1);
DisplayBoard(board2);

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
static (Ship, Ship, Ship)PlaceShip(string[,] board)
{
    
// Console.Clear();
    DisplayBoard(board);    
    Console.WriteLine("Place your Largest ship(3), Write a row(1-8) and a column (1-8) where you want the center of your ship to be");
    (int, int) BigShip = TryShipInput(89, 10, board, false);
    string BigRotation = RotationInput();
    Ship Big = new Ship(3, BigRotation, (BigShip.Item1, BigShip.Item2));

    Console.WriteLine("Place your second largest ship(2), write a row(1-8) and a column (1-8) where the left/bottom part of the ship is going to be");
    (int, int) MediumShip = TryShipInput(89, 10, board, false);
     string mediumRotation = RotationInput();
    Ship Medium = new Ship(2, mediumRotation, (MediumShip.Item1, MediumShip.Item2));

    Console.WriteLine("Place your smallest ship(1), write a row(1-8) and a column (1-8) where you want the ship to be");
    (int, int) SmalShip = TryShipInput(89, 10, board, false);
     string smallRotation = RotationInput();
    Ship Small = new Ship(1, smallRotation, (SmalShip.Item1, SmalShip.Item2));
    // Console.Clear();
    return (Big, Medium, Small);
}


static string RotationInput(){
    string rotation;
    do{
        Console.WriteLine("What rotation should the ship be (Vertical=(v) or Horizontal=(h))");
        rotation = Console.ReadLine()?? "";
    }while(rotation != "v" || rotation != "h");

    return rotation;
}

static (int, int) TryShipInput(int MaxValue, int MinValue, string[,] board, bool shooting){
int Value;
string Input;
while(true){
do{
    Console.WriteLine($"Write a value between {MinValue} and {MaxValue}");
    Input = Console.ReadLine()?? "";

}while(!int.TryParse(Input, out Value));

if(Convert.ToInt32(Input) > MinValue && Convert.ToInt32(Input) < MaxValue){

    if(shooting != true){
        char[] InputDigits = Input.ToCharArray();
        Console.WriteLine(board[3, 4]);
        Console.WriteLine(board[InputDigits[0], InputDigits[1]]);
        

        if(board[InputDigits[0], InputDigits[1]] == null){
            break;
        }else{
            Console.WriteLine("You already have a ship touching this position!!!");
        }
    }else{
        break;
    }

}else{
    Console.WriteLine("Write a valid number (11-88)");
}
}
string Value1 = Convert.ToString(Input).Split(' ')[0];
string Value2 = Convert.ToString(Input).Split(' ')[1];
(int, int) Values = (Convert.ToInt32(Value1), Convert.ToInt32(Value2));
return Values;


}
static string[,] FillBoard(string[,] board){
    for(int i = 0; i < board.GetLength(0)-1; i++){
        for(int j = 0; j < board.GetLength(1)-1; j++){
            if(board[i,j] == null){
                board[i,j] = "- ";
            }
        }
    }
return board;
}


(string[,], string[,], int) ShootPhase(string[,] DefendingBoard, string[,] HitBoard, string player, int defendingHealth)
{
    for (int i = 0; i < HitBoard.GetLength(0); i++){
        for(int j = 0; j < HitBoard.GetLength(1); j++){
           Console.Write(HitBoard[i,j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine(player + "where do you wish to shoot?");
    (int, int) shot = TryShipInput(8, 0,HitBoard ,true);

    if(DefendingBoard[shot.Item1, shot.Item2] == "X "){
        Console.WriteLine("Hit");
        HitBoard[shot.Item1, shot.Item2] = "X ";
        DefendingBoard[shot.Item1, shot.Item2] = "- ";
        if(DefendingBoard[shot.Item1+1, shot.Item2] == "X "){

        }else if(DefendingBoard[shot.Item1-1, shot.Item2] == "X "){

        }else if(DefendingBoard[shot.Item1, shot.Item2+1] == "X "){

        }else if(DefendingBoard[shot.Item1, shot.Item2-1] == "X "){
            
        }else{
            Console.WriteLine("Sink");
            defendingHealth -= 1;
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
