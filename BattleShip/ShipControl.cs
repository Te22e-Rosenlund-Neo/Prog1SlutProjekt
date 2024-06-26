namespace ShipClass
{
    class Ship
    {
        int shipType;
        //either vertical or Horizontal ->
        string rotation;
        public int ShipHp;
        (int, int) cord1, cord2, cord3;
        //list as size in going to be dynamic depending on shiptype
        public List<(int, int)> Cords = new();

        public Ship(int shipType, string rotation, (int, int) cord)
        {
            this.rotation = rotation;
            ShipHp = shipType;
            this.shipType = shipType;
            cord1 = cord;

            //here we check for edge cases, if we are on a edge, we move the ship upwards on step (only if 2 or 3 size)
            if (shipType > 1)
            {
                if (cord1 == (0, 0))
                {
                    cord1 = (1, 0);
                    Console.WriteLine("Cordinate was moved due to corner placement not working");
                }
                else if (cord1 == (0, 7))
                {
                    cord1 = (1, 7);
                    Console.WriteLine("Cordinate was moved due to corner placement not working");
                }
                else if (cord1 == (7, 0))
                {
                    cord1 = (6, 0);
                    Console.WriteLine("Cordinate was moved due to corner placement not working");
                }
                else if (cord1 == (7, 7))
                {
                    cord1 = (6, 7);
                    Console.WriteLine("Cordinate was moved due to corner placement not working");
                }
            }

            Cords.Add(cord1);
            //defaults a certain rotation so that other positions of the ship arent outside the map
            if (this.shipType == 3)
            {
                if (cord1.Item1 == 0 || cord1.Item1 == 7)
                {
                    rotation = "h";
                    Console.WriteLine("Defaulted to horizontal, as ship cannot be outside of the map");
                }
                else if (cord1.Item2 == 0 || cord1.Item2 == 7)
                {
                    rotation = "v";
                    Console.WriteLine("Defaulted to vertical, as ship cannot be outside of the map");
                }
            }
            //defaults a certain rotation so that other positions of the ship arent outside the map
            if (this.shipType == 2)
            {
                if (cord1.Item2 == 7)
                {
                    rotation = "v";
                    Console.WriteLine("Defaulted to vertical, ship cannot be outside the map");
                }
                else if (cord1.Item1 == 0)
                {
                    rotation = "h";
                    Console.WriteLine("Defaulted to horizontal, ship cannot be outside the map");
                }
            }

            //here we add cordinates to the ships cordinate list, as if ship is larger than 1, all spaces must be hit for a sink to happen
            if (shipType == 2)
            {
                if (rotation.ToLower() == "v")
                {
                    cord2 = (cord1.Item1 - 1, cord1.Item2);
                }
                else
                {
                    cord2 = (cord1.Item1, cord1.Item2 + 1);
                }
                Cords.Add(cord2);
            }
            if (this.shipType == 3)
            {
                if (rotation.ToLower() == "v")
                {
                    cord2 = (cord1.Item1 + 1, cord1.Item2);
                    cord3 = (cord1.Item1 - 1, cord1.Item2);
                }
                else
                {
                    cord2 = (cord1.Item1, cord1.Item2 - 1);
                    cord3 = (cord1.Item1, cord1.Item2 + 1);
                }
                Cords.Add(cord2);
                Cords.Add(cord3);
            }
        }
    }
}