using CodingNow.Lecture.Oop09;

List<Vehicle> vehicles = [new Vehicle(), new Car(), new Bike(), new Car()];

foreach (var v in vehicles)
{
    v.Move();
}
