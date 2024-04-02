class Ship{
int shiptype;
//either vertical or Horizontal ->
string rotation;
(int, int) Cord1, Cord2, Cord3;

public Ship(int shiptype, string rotation, (int, int) Cord1){
    this.rotation = rotation;
    this.shiptype = shiptype;
    this.Cord1 = Cord1;
}

public Ship(int Shiptype, string rotation, (int, int) Cord1, (int, int) Cord2){
    this.rotation = rotation;
    this.shiptype = shiptype;
    this.Cord1 = Cord1;
    this.Cord2 = Cord2;
}

public Ship(int Shiptype, string rotation, (int, int) Cord1, (int, int) Cord2, (int, int) Cord3){
    this.rotation = rotation;
    this.shiptype = shiptype;
    this.Cord2 = Cord2;
    this.Cord3 = Cord3;
}
}