//vi använder oss av array då brädet inte behöver ha en dynamisk storlek, och då blir en array bättre då den är lite snabbare när vi vill komma åt specifika element
string[,] board1 = new string[8, 8];
string[,] board2 = new string[8, 8];

string[,] ShotBoard1 = new string[8, 8];
string[,] ShotBoard2 = new string[8, 8];

PlaceShip(board1);
void Menu()
{

}
void GameLoop()
{

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
return board;

int TryShipInput(int MaxValue, int MinValue){
string Input = Console.ReadLine()?? "";
int Value;

while(!int.TryParse(Input, out Value)){
    Console.WriteLine("Please Write a 2 digit number");
    Input = Console.ReadLine()?? "";
}
Value = Convert.ToInt32(Input);

while(Value < MinValue || Value > MaxValue){
    Console.WriteLine("Please write a number between 9-89");
    Input = Console.ReadLine()?? "";

    while(!int.TryParse(Input, out Value)){
    Console.WriteLine("Please Write a 2 digit number");
    Input = Console.ReadLine()?? "";
}

}
return Value;

}
void Shoot()
{

}
void win()
{

}
