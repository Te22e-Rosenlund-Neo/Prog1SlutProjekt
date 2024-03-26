//vi använder oss av array då brädet inte behöver ha en dynamisk storlek, och då blir en array bättre då den är lite snabbare när vi vill komma åt specifika element
string[,] board1 = new string[8, 8];
string[,] board2 = new string[8, 8];

string[,] ShotBoard1 = new string[8, 8];
string[,] ShotBoard2 = new string[8, 8];


// PlaceShip(board1);
void Menu()
{
}
void GameLoop()
{
Console.WriteLine("Player 1");
board1 = PlaceShip(board1);
Console.Clear();
Console.WriteLine("Player 2");
board2 = PlaceShip(board2);
Console.Clear();


}
static string[,] PlaceShip(string[,] board)
{
Console.Clear();
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
    Console.WriteLine("Place your Largest ship(3), Write a row(1-8) and a column (1-8) where you want the center of your ship to be");
    (int, int) BigShipP1 = TryShipInput(8, 8);
    Console.WriteLine("Place your second largest ship(2), write a row(1-8) and a column (1-8) where the left part of the ship is going to be");
    (int, int) MediumShipP1 = TryShipInput(8,8);
    Console.WriteLine("Place your smallest ship(1), write a row(1-8) and a column (1-8) where you want the ship to be");
    (int, int) SmalShipP1 = TryShipInput(8,8);
    Console.Clear();
    return board;
}


static (int, int) TryShipInput(int MaxValue, int MinValue){
int Value;
string Input;
while(true){
do{
    Console.WriteLine($"Write a value between {MinValue} and {MaxValue}");
    Input = Console.ReadLine()?? "";

}while(!int.TryParse(Input, out Value));

if(Convert.ToInt32(Input) > MinValue && Convert.ToInt32(Input) < MaxValue){
    if(Convert.ToString(Value).Split(' ')[0] == null && Convert.ToString(Value).Split(' ')[1] == null){
        break;
    }else{
        Console.WriteLine("You already have a ship touching these positions!!!");
    }
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


void ShootPhase()
{

}
void win()
{

}
