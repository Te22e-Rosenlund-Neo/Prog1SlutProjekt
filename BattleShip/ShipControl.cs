class Ship{
int shiptype;
//either vertical or Horizontal ->
string rotation;
public int ShipHp;
(int, int) Cord1, Cord2, Cord3;
public List<(int, int)> Cords = new();

public Ship(int Shiptype, string rotation, (int, int) Cord){
    this.rotation = rotation;
    ShipHp = Shiptype;
    this.shiptype = Shiptype;
    Cord1 = Cord;
    Cords.Add(Cord1);

    if(shiptype == 3){ 
        if(Cord1.Item1 == 0 || Cord1.Item1 == 7){
        rotation = "h";
        Console.WriteLine("Defaulted to horizontal, as ship cannot be outside of the map");
       }else if(Cord.Item2 == 0 || Cord.Item2 == 7){
        rotation = "v";
        Console.WriteLine("Defaulted to vertical, as ship cannot be outside of the map");
       }
    }
    if(shiptype == 2){
        if(Cord1.Item2 == 7){
            rotation = "v";
            Console.WriteLine("Defaulted to vertical, ship cannot be outside the map");
        }else if(Cord1.Item1 == 0){
            rotation = "h";
            Console.WriteLine("Defaulted to horizontal, ship cannot be outside the map");
        }
    }


    if(Shiptype == 2){
        if(rotation.ToLower() == "v"){
            Cord2 = (Cord1.Item1-1, Cord1.Item2);
        }else{
            Cord2 = (Cord1.Item1, Cord1.Item2+1);
        }
        Cords.Add(Cord2);
    }
    if(shiptype == 3){
         if(rotation.ToLower()  == "v"){
            Cord2 = (Cord1.Item1+1, Cord1.Item2);
            Cord3 = (Cord1.Item1-1, Cord1.Item2);
        }else{
            Cord2 = (Cord1.Item1, Cord1.Item2-1);
            Cord3 = (Cord1.Item1, Cord1.Item2+1);
        }
        Cords.Add(Cord2);
        Cords.Add(Cord3);
}
}
}