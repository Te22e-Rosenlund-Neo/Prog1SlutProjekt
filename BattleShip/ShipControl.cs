class Ship{
int shiptype;
//either vertical or Horizontal ->
string rotation;
(int, int) Cord1, Cord2, Cord3;
public List<(int, int)> Cords;

public Ship(int Shiptype, string rotation, (int, int) Cord1){
    this.rotation = rotation;
    this.shiptype = Shiptype;
    this.Cord1 = Cord1;

    Cords.Add(Cord1);
    if(Shiptype == 2){
        if(rotation == "v"){
            Cord2 = (Cord1.Item1+1, Cord1.Item2);
        }else{
            Cord2 = (Cord1.Item1, Cord2.Item2-1);
        }
        Cords.Add(Cord2);
    }
    if(shiptype == 3){
         if(rotation == "v"){
            Cord2 = (Cord1.Item1+1, Cord1.Item2);
            Cord3 = (Cord1.Item1-1, Cord1.Item2);
        }else{
            Cord2 = (Cord1.Item1, Cord2.Item2-1);
            Cord2 = (Cord1.Item1, Cord1.Item2+1);
        }
        Cords.Add(Cord3);
}
}}